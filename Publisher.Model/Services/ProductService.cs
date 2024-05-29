using Publisher.Model.Entities;
using Publisher.Model.Interfaces;
using System.Text.Json;

namespace Publisher.Model.Services
{
    public class ProductService
    {
        private readonly IMessageBroker _messageBroker;

        public ProductService(IMessageBroker messageBroker)
        {            
            _messageBroker = messageBroker;
        }

        public void SendProduct(Product product)
        {
            var message = JsonSerializer.SerializeToUtf8Bytes(product);
            _messageBroker.Send("products", message);
        }
    }
}
