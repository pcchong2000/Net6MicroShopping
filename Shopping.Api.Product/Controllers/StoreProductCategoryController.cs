using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Commands;
using Shopping.Api.Product.Applications.Querys;
using Shopping.Framework.Domain.Base;

namespace Shopping.Api.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreProductCategoryController : ControllerBase
    {

        private ISender _mediator;
        private readonly ILogger<StoreProductCategoryController> _logger;

        public StoreProductCategoryController(ILogger<StoreProductCategoryController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost("tenant")]
        public async Task<ResponseBase> Post(StoreProductCategoryEditCommand reqeust)
        {

            return await _mediator.Send(reqeust);
        }
        [HttpPut("tenant")]
        public async Task<ResponseBase> Put(StoreProductCategoryEditCommand reqeust)
        {

            return await _mediator.Send(reqeust);
        }
        [HttpGet("tenant")]
        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> GetTenantList(StoreProductCategoryTenantPageQuery reqeust)
        {

            return await _mediator.Send(reqeust);
        }
        [HttpGet]
        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> GetList(StoreProductCategoryTenantPageQuery reqeust)
        {

            return await _mediator.Send(reqeust);
        }
    }
}