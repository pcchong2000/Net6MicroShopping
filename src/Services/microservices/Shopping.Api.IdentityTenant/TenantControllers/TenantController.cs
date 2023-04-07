using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Shopping.Api.IdentityTenant.TenantApplications.TenantAdmins;

namespace Shopping.Api.IdentityTenant.TenantControllers
{
    public class TenantController : ApiController
    {
        private ISender _mediator;
        private readonly ILogger<TenantController> _logger;

        public TenantController(ILogger<TenantController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<RegisterTenantResponse> Register(RegisterTenantCommand register)
        {
            return await _mediator.Send(register);
        }
        [HttpGet]
        public string Get()
        {
            return "123";
        }
    }
}