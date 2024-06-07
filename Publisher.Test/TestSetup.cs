using Microsoft.Extensions.DependencyInjection;
using Publisher.Infra.RabbitMQ;
using Publisher.Model.Interfaces;
using Publisher.Model.Services;

namespace Publisher.Test
{
    [SetUpFixture]
    public class TestSetup
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddScoped<ProductService>();
            services.AddScoped<IMessageBroker, MessageBrokerRabbitMQ>();
            ServiceProvider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            ServiceProvider.Dispose();
        }
    }
}
