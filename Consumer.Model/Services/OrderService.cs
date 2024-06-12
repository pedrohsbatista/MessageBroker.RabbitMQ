using Consumer.Model.Entities;
using System.Text.Json;

namespace Consumer.Model.Services
{
    public class OrderService
    {
        public void ProcessOrder(byte[] message)
        {
            var order = JsonSerializer.Deserialize<Order>(message);
            var log = JsonSerializer.Serialize(order);
            Console.WriteLine(log);
        }
    }
}
