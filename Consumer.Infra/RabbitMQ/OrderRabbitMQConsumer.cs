using Consumer.Model.Services;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Infra.RabbitMQ
{
    public class OrderRabbitMQConsumer : BackgroundService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly OrderService _orderService;

        public OrderRabbitMQConsumer(OrderService orderService)
        {
            _orderService = orderService;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = _channel.QueueDeclare().QueueName; // cria fila com nome automático, não durável, excluída automaticamente

            _channel.QueueBind(
                queue: queue,
                exchange: "order",
                routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var body = ea.Body.ToArray();
                _orderService.ProcessOrder(body);
            };

            _channel.BasicConsume(queue: queue,
                                   autoAck: true,
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
