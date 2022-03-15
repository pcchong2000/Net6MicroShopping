using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Tenant.Applications.Commands;
using Shopping.Api.Tenant.Applications.Querys;
using Shopping.Framework.Web;

namespace Shopping.Api.Tenant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private ICurrentUserService _currentUser;
        private ISender _mediator;
        private readonly ILogger<StoreController> _logger;

        public StoreController(ILogger<StoreController> logger, ISender mediator, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<CreateStoreResponse> CreateStore(CreateStoreCommand store)
        {
            store.TenantId = _currentUser.TenantId;
            return await _mediator.Send(store);
        }
        [HttpGet]
        public async Task<StoreListResponse> GetList([FromQuery]StoreListQuery query)
        {
            var user =  HttpContext.User;
            query.TenantId= _currentUser.TenantId;
            return await _mediator.Send(query);
        }
        [HttpGet("{id}")]
        public string GetDetail(string id)
        {
            return "123";
        }
    }
}