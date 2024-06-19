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

        public void SendQueue(string queue, byte[] message)
        {
            _channel.QueueDeclare(queue: queue,
                                 durable: true, // se false a fila será perdida caso o servidor do rabbitmq seja reiniciado
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Persistent = true; // garante (não em sua tototalidade) que as mensagens na fila fiquem armazenadas em disco e não sejam perdidas caso o servidor do rabbitmq seja reiniciado

            _channel.BasicPublish(exchange: string.Empty,
                                  routingKey: queue,
                                  basicProperties: basicProperties,
                                  body: message);
        }

        public void SendExchange(string exchange, string type, byte[] message, string routingKey)
        {
            _channel.ExchangeDeclare(exchange, type);

            _channel.BasicPublish(exchange: exchange,
                                  routingKey: routingKey,
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
