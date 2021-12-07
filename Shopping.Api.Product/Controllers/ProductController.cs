using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Commands;
using Shopping.Api.Product.Applications.Querys;

namespace Shopping.Api.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ISender _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ISender mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ProductListResponse> Get([FromQuery]ProductListQuery query)
        {
            return await _mediator.Send(query);

        }
        [HttpGet("Admin")]
        public async Task<string> GetAdmin()
        {
            return "Admin";

        }
        [HttpPost]
        public async Task<ProductAddResponse> Post(ProductAddCommand query)
        {
            return await _mediator.Send(query);

        }
    }
}