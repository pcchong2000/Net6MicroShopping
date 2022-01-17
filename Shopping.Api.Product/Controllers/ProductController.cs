using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Commands;
using Shopping.Api.Product.Applications.Querys;
using Shopping.Framework.Web;

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
        [HttpGet("detail")]
        public async Task<ProductDetailQueryResponse> GetDetail([FromQuery] ProductDetailQuery query)
        {
            return await _mediator.Send(query);

        }
        [HttpGet(JwtBearerIdentity.TenantScheme +"/detail")]
        public async Task<ProductDetailQueryResponse> GetTenantDetail([FromQuery] ProductDetailQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet(JwtBearerIdentity.TenantScheme)]
        public async Task<ProductListTenantResponse> GetTenant([FromQuery] ProductListTenantQuery query)
        {
            return await _mediator.Send(query);

        }
        [HttpGet("Admin")]
        public async Task<string> GetAdmin()
        {
            return "Admin";

        }
        [HttpPost(JwtBearerIdentity.TenantScheme)]
        
        public async Task<ProductAddResponse> Post(ProductEditCommand query)
        {
            return await _mediator.Send(query);
        }
        [HttpPut(JwtBearerIdentity.TenantScheme)]
        public async Task<ProductAddResponse> Put(ProductEditCommand query)
        {
            var handlers = HttpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            var Schemes = HttpContext.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
            var list = await Schemes.GetRequestHandlerSchemesAsync();
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
    }
}