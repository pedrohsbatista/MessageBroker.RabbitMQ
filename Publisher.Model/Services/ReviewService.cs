using Publisher.Model.Entities;
using Publisher.Model.Interfaces;
using System.Text.Json;

namespace Publisher.Model.Services
{
    public class ReviewService
    {
        private readonly IMessageBroker _messageBroker;

        public ReviewService(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public void SendReview(Review review)
        {
            var message = JsonSerializer.SerializeToUtf8Bytes(review);
            //exchange do tipo topic só podem ser delimitadas por .
            _messageBroker.SendExchange("review", "topic", message, $"review.{review.ReviewCategory}.{review.Rating}".ToLower());
        }
    }
}
