namespace Consumer.Model.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid ProductId { get; set; }
    }
}
