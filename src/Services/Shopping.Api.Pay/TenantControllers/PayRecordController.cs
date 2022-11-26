using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Pay.TenantControllers
{
    public class PayRecordController : TenantApiController
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