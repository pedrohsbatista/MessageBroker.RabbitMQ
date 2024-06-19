namespace Publisher.Model.Interfaces
{
    public interface IMessageBroker
    {
        void SendQueue(string queue, byte[] message);

        void SendExchange(string exchange, string type, byte[] message, string routingKey = "");
    }
}
