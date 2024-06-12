namespace Publisher.Model.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        public Guid ProductId { get; set; }
    }
}
