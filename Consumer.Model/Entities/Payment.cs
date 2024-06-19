using Consumer.Model.Enums;

namespace Consumer.Model.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
