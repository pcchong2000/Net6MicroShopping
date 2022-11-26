using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Pay.MemberControllers
{
    public class PayRecordController : ApiController
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