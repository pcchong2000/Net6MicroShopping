using Dapr;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Product.Applications.Commands;
using Shopping.Api.Product.Applications.Queries;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.MemberControllers
{
    public class ProductController : ApiController
    {
        private ISender _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ISender mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("list-in")]
        [AllowAnonymous]
        public async Task<ProductListInQueryResponse> GetIn(ProductListInQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("home")]
        [AllowAnonymous]
        public async Task<ResponsePageBase<ProductListItemResponse>> GetHome([FromQuery] ProductListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponsePageBase<ProductListItemResponse>> Get([FromQuery] ProductListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("detail")]
        [AllowAnonymous]
        public async Task<ProductDetailQueryResponse> GetDetail([FromQuery] ProductDetailQuery query)
        {
            return await _mediator.Send(query);

        }

        /// <summary>
        /// 更新库存
        /// dapr 订阅必须是post
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Topic("pubsub", "newOrder")]
        [HttpPost("kucun-in")]
        [AllowAnonymous]
        public async Task newOrder(ProductInUpdateReserveCommand query)
        {
            await _mediator.Send(query);
        }
    }
}