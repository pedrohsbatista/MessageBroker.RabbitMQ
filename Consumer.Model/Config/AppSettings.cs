using Consumer.Model.Enums;

namespace Consumer.Model.Config
{
    public class AppSettings
    {
        public int TimeDelay { get; set; }

        public List<PaymentType> PaymentQueues { get; set; } = new();
    }
}
