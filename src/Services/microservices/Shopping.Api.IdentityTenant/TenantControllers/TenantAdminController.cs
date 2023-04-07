using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shopping.Api.IdentityTenant.TenantControllers
{
    public class TenantAdminController : ApiController
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