using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Api.IdentityTenant.Applications.Commands;
using Shopping.Api.IdentityTenant.Applications.Querys;
using Shopping.Framework.Web;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityTenant.Controllers
{
    public class StoreController : ApiController
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
        public async Task<StoreListResponse> GetList([FromQuery] StoreListQuery query)
        {
            var iss=  HttpContext.GetIdentityServerIssuerUri();
            var user = HttpContext.User;
            query.TenantId = _currentUser.TenantId;
            return await _mediator.Send(query);
        }
        [HttpGet("{id}")]
        public string GetDetail(string id)
        {
            return "123";
        }
        [HttpGet("detail-in/{id}")]
        [AllowAnonymous]
        public async Task<StoreDetailInQueryResponse> GetDetailIn(string id)
        {

            return await _mediator.Send(new StoreDetailInQuery()
            {
                Id = id
            });

        }
    }
}