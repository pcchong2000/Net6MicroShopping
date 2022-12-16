using Dapr;
using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Order.Application.Members.Commands;
using Shopping.Api.Order.Application.Members.Queries;
using Shopping.Api.Order.Application.Tenants.Commands;
using Shopping.Api.Order.Application.Tenants.Queries;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.TenantControllers
{
    public class OrderController : TenantApiController
    {
        private readonly ILogger<OrderController> _logger;

        private ISender _mediator;
        public OrderController(ILogger<OrderController> logger, ISender mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }

        [HttpGet(JwtBearerIdentity.TenantScheme)]
        public async Task<OrderListQueryResponse> TenantGet([FromQuery] OrderListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet(JwtBearerIdentity.TenantScheme + "/detail")]
        public async Task<OrderDetailQueryResponse> TenantGetDetail([FromQuery] OrderDetailQuery query)
        {
            return await _mediator.Send(query);
        }
        /// <summary>
        /// 更新库存
        /// dapr 订阅必须是post
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Topic("pubsub", "MemberUpdateCommand")]
        [HttpPost("MemberUpdateCommand-in")]
        [AllowAnonymous]
        public async Task MemberUpdateCommand(OrderMemberUpdateCommand query)
        {
            await _mediator.Send(query);
        }
    }

}