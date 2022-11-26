using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Queries;
using Shopping.Framework.DomainBase.Base;

namespace Shopping.Api.Product.TenantControllers
{
    /// <summary>
    /// 总产品品类
    /// </summary>
    public class TenantProductCategoryController : TenantApiController
    {
        private ISender _mediator;
        private readonly ILogger<TenantProductCategoryController> _logger;

        public TenantProductCategoryController(ILogger<TenantProductCategoryController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponsePageBase<ProductCategoryPageQueryItemResponse>> Get([FromQuery] ProductCategoryPageQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}