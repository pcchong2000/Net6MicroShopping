using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.TenantApplications.Queries;
using Shopping.Framework.DomainBase.Base;

namespace Shopping.Api.Product.MemberControllers
{
    public class StoreProductCategoryController : ApiController
    {

        private ISender _mediator;
        private readonly ILogger<StoreProductCategoryController> _logger;

        public StoreProductCategoryController(ILogger<StoreProductCategoryController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> GetList([FromQuery] StoreProductCategoryTenantPageQuery reqeust)
        {
            return await _mediator.Send(reqeust);
        }
    }
}