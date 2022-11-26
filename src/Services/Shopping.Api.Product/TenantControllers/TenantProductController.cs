using Dapr;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Commands;
using Shopping.Api.Product.Applications.Queries;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.TenantControllers
{
    public class TenantProductController : TenantApiController
    {
        private ISender _mediator;
        private readonly ILogger<TenantProductController> _logger;

        public TenantProductController(ILogger<TenantProductController> logger, ISender mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("detail")]
        public async Task<ProductDetailQueryResponse> GetTenantDetail([FromQuery] ProductDetailQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        public async Task<ResponsePageBase<ProductListTenantItemResponse>> GetTenant([FromQuery] ProductListTenantQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpDelete]
        public async Task<ProductDeleteTenantResponse> DeleteTenant([FromQuery] ProductDeleteTenantCommand query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]

        public async Task<ProductAddResponse> Post(ProductEditCommand query)
        {
            return await _mediator.Send(query);
        }
        [HttpPut]
        public async Task<ProductAddResponse> Put(ProductEditCommand query)
        {
            return await _mediator.Send(query);
        }
    }
}