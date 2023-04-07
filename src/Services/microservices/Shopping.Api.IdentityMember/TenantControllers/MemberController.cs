using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shopping.Api.IdentityMember.TenantControllers
{
    public class MemberController : TenantApiController
    {
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
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