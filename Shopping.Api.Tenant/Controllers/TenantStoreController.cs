using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Tenant.Applications.Commands;
using Shopping.Api.Tenant.Applications.Querys;

namespace Shopping.Api.Tenant.Controllers
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
        public async Task<StoreListResponse> GetList(StoreListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("{id}")]
        public string GetDetail(string id)
        {
            return "123";
        }
    }
}