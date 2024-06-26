using Consumer.Model.Entities;
using System.Text.Json;

namespace Consumer.Model.Services
{
    public class ReviewService
    {
        public void ProcessReview(byte[] message)
        {
            var review = JsonSerializer.Deserialize<Review>(message);
            var log = JsonSerializer.Serialize(review);
            Console.WriteLine(log);
        }
    }
}
