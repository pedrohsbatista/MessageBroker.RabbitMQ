using Microsoft.AspNetCore.Mvc;
using Publisher.Model.Entities;
using Publisher.Model.Services;

namespace Publisher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Post(Payment payment)
        {
            try
            {
                _paymentService.SendPayment(payment);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
