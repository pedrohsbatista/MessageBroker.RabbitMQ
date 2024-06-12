using Consumer.Model.Services;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Infra.RabbitMQ
{
    public class ProductRabbitMQConsumer : BackgroundService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private ProductService _productService;


        public ProductRabbitMQConsumer(ProductService productService)
        {
            _productService = productService;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.QueueDeclare(queue: "products",
                                durable: true, // se false a fila será perdida caso o servidor do rabbitmq seja reiniciado
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            //prefetchCount indica para o rabbitmq quantas mensagens devem ser entregues ao consumir por vez até que tenha processado e confirmado.
            //Ajuda a evitar que um consumir fique sobrecarregado e outro livre

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var body = ea.Body.ToArray();
                // teste para mensagens não processadas completamente.
                // throw new Exception("Read message incomplete");
                _productService.ProcessProduct(body);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                  multiple: false);
            };

            _channel.BasicConsume(queue: "products",
                                  autoAck: false, // set true a mensagem é confirmada automáticamente, mesmo que o processamento da mensagem fique incompleto
                                  consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
