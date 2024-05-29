namespace Publisher.Model.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Description { get; set; }
    }
}
