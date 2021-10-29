using Microsoft.AspNetCore.Mvc;

namespace Tenant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        
        private readonly ILogger<TenantController> _logger;

        public TenantController(ILogger<TenantController> logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            return "123";
        }
    }
}