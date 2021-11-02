using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Tenant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantAdminController : ControllerBase
    {

        private ISender _mediator;
        private readonly ILogger<TenantAdminController> _logger;

        public TenantAdminController(ILogger<TenantAdminController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        public string Get()
        {
            return "123";
        }
    }
}