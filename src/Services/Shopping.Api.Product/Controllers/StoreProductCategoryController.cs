using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Commands;
using Shopping.Api.Product.Applications.Queries;
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
        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> GetTenantList([FromQuery] StoreProductCategoryTenantPageQuery reqeust)
        {

            return await _mediator.Send(reqeust);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> GetList([FromQuery] StoreProductCategoryTenantPageQuery reqeust)
        {

            return await _mediator.Send(reqeust);
        }
    }
}