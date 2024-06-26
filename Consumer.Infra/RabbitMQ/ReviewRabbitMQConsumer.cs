using Consumer.Model.Config;
using Consumer.Model.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Infra.RabbitMQ
{
    public class ReviewRabbitMQConsumer : BackgroundService, IDisposable
    {
        private readonly ReviewService _reviewService;
        private readonly AppSettings _appSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ReviewRabbitMQConsumer(ReviewService reviewService, IOptions<AppSettings> appSettings)
        {            
            _reviewService = reviewService;
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
            _channel.ExchangeDeclare(exchange: "review",
                                     type: "topic");

            var queue = _channel.QueueDeclare().QueueName;

            foreach (var routingKey in _appSettings.ReviewRoutingKeys)
            {
                _channel.QueueBind(queue: queue,
                                  exchange: "review",
                                  routingKey: routingKey);
            }

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var message = ea.Body.ToArray();
                _reviewService.ProcessReview(message);
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
