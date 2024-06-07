
using Consumer.Model.Config;
using Consumer.Model.Entities;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Consumer.Model.Services
{
    public class ProductService
    {
        private static int timeSleep = 0;

        private readonly AppSettings _appSettings;

        public ProductService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void ProcessProduct(byte[] message)
        {
            var product = JsonSerializer.Deserialize<Product>(message);
            var log = JsonSerializer.Serialize(product);
            Console.WriteLine(log);

            //Usado para simular processo demorado
            timeSleep++;                      
            Thread.Sleep((timeSleep * 1000 / _appSettings.TimeDelay));            
        }
    }
}
