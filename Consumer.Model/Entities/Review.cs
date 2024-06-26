using Consumer.Model.Enums;

namespace Consumer.Model.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public ReviewCategory ReviewCategory { get; set; }

        public Rating Rating { get; set; }

        public string Description { get; set; }
    }
}
