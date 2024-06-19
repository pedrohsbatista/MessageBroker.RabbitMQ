using Consumer.Model.Config;
using Consumer.Model.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Infra.RabbitMQ
{
    public class PaymentRabbitMQConsumer : BackgroundService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly PaymentService _paymentService;
        private readonly AppSettings _appSettings;

        public PaymentRabbitMQConsumer(PaymentService paymentService, IOptions<AppSettings> appSettings)
        {
            _paymentService = paymentService;
            _appSettings = appSettings.Value;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();          
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.ExchangeDeclare(exchange: "payment", 
                                     type: "direct");

            var queue = _channel.QueueDeclare().QueueName;

            foreach (var paymentType in _appSettings.PaymentQueues)
            {
                _channel.QueueBind(queue: queue,
                                   exchange: "payment",
                                   routingKey: $"payment_{paymentType}".ToLower());
            }

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var body = ea.Body.ToArray();
                _paymentService.ProcessPayment(body);
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
