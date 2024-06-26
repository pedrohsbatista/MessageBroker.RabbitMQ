using Publisher.Model.Enums;

namespace Publisher.Model.Entities
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }

        public ReviewCategory ReviewCategory { get; set; }

        public Rating Rating { get; set; }

        public string Description { get; set; }
    }
}
