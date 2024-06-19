using Publisher.Model.Entities;
using Publisher.Model.Interfaces;
using System.Text.Json;

namespace Publisher.Model.Services
{
    public class PaymentService
    {
        private readonly IMessageBroker _messageBroker;

        public PaymentService(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public void SendPayment(Payment payment)
        {
            var message = JsonSerializer.SerializeToUtf8Bytes(payment);
            _messageBroker.SendExchange("payment", "direct", message, $"payment_{payment.PaymentType}".ToLower());
        }
    }
}
