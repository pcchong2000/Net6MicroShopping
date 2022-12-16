using Dapr;
using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Order.Application.Members.Commands;
using Shopping.Api.Order.Application.Members.Queries;
using Shopping.Api.Order.Application.Tenants.Queries;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.MemberControllers
{
    public class OrderController : ApiController
    {
        private readonly ILogger<OrderController> _logger;

        private ISender _mediator;
        public OrderController(ILogger<OrderController> logger, ISender mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }
        [HttpGet]
        public async Task<MembersOrderListQueryResponse> MemberGet([FromQuery] MembersOrderListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("detail")]
        public async Task<MembersOrderDetailQueryResponse> MemberGetDetail([FromQuery] MembersOrderDetailQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("test")]
        public async Task<int> Get2()
        {
            //_daprClient.get

            //var httpClient = DaprClient.CreateInvokeHttpClient();
            //var resp = await httpClient.GetAsync("http://memberapi/member/test");
            //string respContent = await resp.Content.ReadAsStringAsync();
            //return await _daprClient.InvokeMethodAsync<int>(HttpMethod.Get, "memberapi", "member/test");
            return 1;
        }
        [HttpPost]
        public async Task<OrderCreateCommandResponse> Post(OrderCreateCommand addOrder)
        {

            return await _mediator.Send(addOrder);
        }


    }

}