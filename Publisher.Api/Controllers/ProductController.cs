using Microsoft.AspNetCore.Mvc;
using Publisher.Model.Entities;
using Publisher.Model.Services;

namespace Publisher.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            try
            {
                _productService.SendProduct(product);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
