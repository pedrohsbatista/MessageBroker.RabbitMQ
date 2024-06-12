using Microsoft.AspNetCore.Mvc;
using Publisher.Model.Entities;
using Publisher.Model.Services;

namespace Publisher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            try
            {
                _orderService.SendOrder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
