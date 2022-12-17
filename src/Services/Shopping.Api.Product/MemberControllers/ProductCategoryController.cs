using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.MemberApplications.ProductCategorys;
using Shopping.Framework.DomainBase.Base;

namespace Shopping.Api.Product.MemberControllers
{
    /// <summary>
    /// 总产品品类
    /// </summary>
    public class ProductCategoryController : ApiController
    {
        private ISender _mediator;
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(ILogger<ProductCategoryController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<ProductCategoryListQueryItemResponse>> Get([FromQuery] ProductCategoryListQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}