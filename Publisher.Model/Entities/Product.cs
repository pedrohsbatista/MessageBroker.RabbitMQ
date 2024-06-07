namespace Publisher.Model.Entities
{
    public class Product
    {
        public Product()
        {
        }

        public Product(string description)
        {
            Description = description;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Description { get; set; }
    }
}
