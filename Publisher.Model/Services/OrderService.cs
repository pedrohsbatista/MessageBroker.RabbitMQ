using Publisher.Model.Entities;
using Publisher.Model.Interfaces;
using System.Text.Json;

namespace Publisher.Model.Services
{
    public class OrderService
    {
        private readonly IMessageBroker _messageBroker;
        public OrderService(IMessageBroker messageBroker)
        {            
            _messageBroker = messageBroker;
        }

        public void SendOrder(Order order)
        {
            var message = JsonSerializer.SerializeToUtf8Bytes(order);
            _messageBroker.SendExchange("order", "fanout", message);
        }
    }
}
