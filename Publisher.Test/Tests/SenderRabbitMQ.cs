using Microsoft.Extensions.DependencyInjection;
using Publisher.Model.Entities;
using Publisher.Model.Services;
using System.Text.Json;

namespace Publisher.Test.Tests
{
    [TestFixture]
    public class SenderRabbitMQ
    {
        [Test]
        public void SendMultipleProductTest()
        {
            const short qtdProducts = 100;

            var products = new List<Product>();

            for (var i = 1; i < qtdProducts; i++)
                products.Add(new Product($"Produto {i}"));

            var productService = TestSetup.ServiceProvider.GetService<ProductService>();            

            foreach(var product in products)
            {
                productService.SendProduct(product);
                Console.WriteLine(JsonSerializer.Serialize(product));
            }            
        }
    }
}
