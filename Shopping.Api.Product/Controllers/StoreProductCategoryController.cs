using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreProductCategoryController : ControllerBase
    {

        private readonly ILogger<StoreProductCategoryController> _logger;

        public StoreProductCategoryController(ILogger<StoreProductCategoryController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string Get()
        {
            return "123";
        }
    }
}