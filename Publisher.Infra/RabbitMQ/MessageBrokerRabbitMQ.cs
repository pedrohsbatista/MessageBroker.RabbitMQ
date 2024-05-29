using Publisher.Model.Interfaces;
using RabbitMQ.Client;

namespace Publisher.Infra.RabbitMQ
{
    public class MessageBrokerRabbitMQ : IMessageBroker, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBrokerRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
        }

        public void Send(string queue, byte[] message)
        {
            _channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicPublish(exchange: string.Empty,
                                  routingKey: queue,
                                  basicProperties: null,
                                  body: message);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();            
        }
    }
}
