using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tenant.Api.Applications.Commands;

namespace Tenant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantStoreController : ControllerBase
    {

        private ISender _mediator;
        private readonly ILogger<TenantStoreController> _logger;

        public TenantStoreController(ILogger<TenantStoreController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<CreateStoreResponse> CreateStore(CreateStoreCommand store)
        {
            return await _mediator.Send(store);
        }
        [HttpGet]
        public string GetList()
        {
            return "123";
        }
        [HttpGet("{id}")]
        public string GetDetail(string id)
        {
            return "123";
        }
    }
}