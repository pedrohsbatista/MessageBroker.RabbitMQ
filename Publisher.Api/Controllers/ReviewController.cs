using Microsoft.AspNetCore.Mvc;
using Publisher.Model.Entities;
using Publisher.Model.Services;

namespace Publisher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {                
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult Post(Review review)
        {
            try
            {
                _reviewService.SendReview(review);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
