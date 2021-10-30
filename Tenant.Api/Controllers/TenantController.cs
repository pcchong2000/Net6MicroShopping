using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tenant.Api.Applications.Commands;

namespace Tenant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        private ISender _mediator;
        private readonly ILogger<TenantController> _logger;
        
        public TenantController(ILogger<TenantController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost("Register")]
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