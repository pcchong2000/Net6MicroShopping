using MediatR;
using Microsoft.AspNetCore.Authentication;
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
        [HttpGet("tenant")]
        public async Task<ProductListResponse> GetTenant([FromQuery] ProductListQuery query)
        {
            return await _mediator.Send(query);

        }
        [HttpGet("Admin")]
        public async Task<string> GetAdmin()
        {
            return "Admin";

        }
        [HttpPost("tenant")]
        public async Task<ProductAddResponse> Post(ProductEditCommand query)
        {

            var handlers = HttpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            var Schemes = HttpContext.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
            var list =await Schemes.GetRequestHandlerSchemesAsync();
            foreach (var scheme in list)
            {
                var handler = await handlers.GetHandlerAsync(HttpContext, scheme.Name) as IAuthenticationRequestHandler;
                if (handler != null && await handler.HandleRequestAsync())
                {
                    //return;
                }
            }
            var bbb = HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
            
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var xxx = await bbb.AuthenticateAsync(HttpContext, defaultAuthenticate.Name);

                var result = await HttpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result?.Principal != null)
                {
                    HttpContext.User = result.Principal;
                }
            }

            return await _mediator.Send(query);

        }
        [HttpPut("tenant")]
        public async Task<ProductAddResponse> Put(ProductEditCommand query)
        {

            return await _mediator.Send(query);

        }
    }
}