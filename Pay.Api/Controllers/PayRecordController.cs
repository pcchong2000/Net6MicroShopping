using Microsoft.AspNetCore.Mvc;

namespace Pay.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayRecordController : ControllerBase
    {

        private readonly ILogger<PayRecordController> _logger;

        public PayRecordController(ILogger<PayRecordController> logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            return "";
        }
    }
}