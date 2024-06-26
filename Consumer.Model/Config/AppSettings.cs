using Consumer.Model.Enums;

namespace Consumer.Model.Config
{
    public class AppSettings
    {
        public int TimeDelay { get; set; }

        public List<string> PaymentRoutingKeys { get; set; } = new();

        public List<string>  ReviewRoutingKeys {  get; set; } = new();
    }
}
