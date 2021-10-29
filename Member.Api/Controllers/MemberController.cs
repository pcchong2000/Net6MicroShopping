using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Member.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
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