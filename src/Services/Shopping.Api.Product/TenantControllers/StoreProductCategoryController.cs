using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.TenantApplications.Commands;
using Shopping.Api.Product.TenantApplications.Queries;
using Shopping.Framework.DomainBase.Base;

namespace Shopping.Api.Product.TenantControllers
{
    public class StoreProductCategoryController : TenantApiController
    {
        private ISender _mediator;
        private readonly ILogger<StoreProductCategoryController> _logger;

        public StoreProductCategoryController(ILogger<StoreProductCategoryController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<string> Post(StoreProductCategoryEditCommand reqeust)
        {
            return await _mediator.Send(reqeust);
        }
        [HttpPut]
        public async Task<string> Put(StoreProductCategoryEditCommand reqeust)
        {
            return await _mediator.Send(reqeust);
        }
        [HttpGet]
        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> GetTenantList([FromQuery] StoreProductCategoryTenantPageQuery reqeust)
        {
            return await _mediator.Send(reqeust);
        }

    }
}