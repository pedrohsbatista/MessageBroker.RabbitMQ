using Consumer.Model.Entities;
using System.Text.Json;

namespace Consumer.Model.Services
{
    public class PaymentService
    {
        public void ProcessPayment(byte[] message)
        {
            var payment = JsonSerializer.Deserialize<Payment>(message);
            var log = JsonSerializer.Serialize(payment);
            Console.WriteLine(log);
        }
    }
}
