using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Pay.TenantControllers
{
    public class TenantPayRecordController : TenantApiController
    {

        private readonly ILogger<TenantPayRecordController> _logger;

        public TenantPayRecordController(ILogger<TenantPayRecordController> logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            return "";
        }
    }
}