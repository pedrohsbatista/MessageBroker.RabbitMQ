namespace Publisher.Model.Interfaces
{
    public interface IMessageBroker
    {
        void Send(string queue, byte[] message);
    }
}
