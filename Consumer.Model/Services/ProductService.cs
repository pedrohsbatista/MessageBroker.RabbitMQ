
using Consumer.Model.Entities;
using System.Text.Json;

namespace Consumer.Model.Services
{
    public class ProductService
    {
        public void ProcessProduct(byte[] message)
        {
            var product = JsonSerializer.Deserialize<Product>(message);
            var log = JsonSerializer.Serialize(product);
            Console.WriteLine(log);
        }
    }
}
