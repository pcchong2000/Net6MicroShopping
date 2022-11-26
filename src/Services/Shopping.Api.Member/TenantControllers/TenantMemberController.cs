using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shopping.Api.Member.TenantControllers
{
    public class TenantMemberController : TenantApiController
    {
        private readonly ILogger<TenantMemberController> _logger;

        public TenantMemberController(ILogger<TenantMemberController> logger)
        {
            _logger = logger;
        }
        [HttpGet("test")]
        public string Get()
        {
            return "123";
        }
    }
}