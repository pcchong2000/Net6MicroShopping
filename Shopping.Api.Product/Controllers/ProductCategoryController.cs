using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {

        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(ILogger<ProductCategoryController> logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            return "123";
        }
    }
}