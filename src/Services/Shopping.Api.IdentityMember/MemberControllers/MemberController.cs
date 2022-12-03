using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shopping.Api.IdentityMember.MemberControllers
{
    public class MemberController : ApiController
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
        [HttpGet("myinfo")]
        public string MyInfo()
        {
            return "123";
        }
        [HttpPost("updateAvatar")]
        public string UpdateAvatar()
        {
            return "123";
        }
    }
}