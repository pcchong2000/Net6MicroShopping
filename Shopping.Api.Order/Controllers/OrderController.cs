using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Order.Application.Members.Queries;
using Shopping.Api.Order.Application.Tenants.Queries;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly DaprClient _daprClient;
        private ISender _mediator;
        public OrderController(ILogger<OrderController> logger, DaprClient daprClient, ISender mediator)
        {
            _logger = logger;
            _daprClient = daprClient;
            _mediator=mediator;
        }

        [HttpGet(JwtBearerIdentity.TenantScheme)]
        public async Task<OrderListQueryResponse> TenantGet([FromQuery] OrderListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet(JwtBearerIdentity.TenantScheme+"/detail")]
        public async Task<OrderDetailQueryResponse> TenantGetDetail([FromQuery] OrderDetailQuery query)
        {
            return await _mediator.Send(query);
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

            var httpClient = DaprClient.CreateInvokeHttpClient();
            var resp = await httpClient.GetAsync("http://memberapi/member/test");
            string respContent = await resp.Content.ReadAsStringAsync();
            return await _daprClient.InvokeMethodAsync<int>(HttpMethod.Get, "memberapi", "member/test");

        }
        [HttpPost]
        public async Task<IActionResult> Post(AddOrderDto addOrder)
        {
            await _daprClient.PublishEventAsync("pubsub", "newOrder",
                new List<JianKuCunDto>() {
                    new JianKuCunDto
                    {
                        ProductId = addOrder.ProductId,
                        Model = addOrder.Model,
                        Number = addOrder.Number

                    },
            });
            return Ok();
        }
    }
    public class AddOrderDto
    {
        public string MemberId { get; set; }
        public string ProductId { get; set; }
        public string Model { get; set; }
        public int Number { get; set; }
    }
    public class JianKuCunDto
    {
        public string ProductId { get; set; }
        public string Model { get; set; }
        public int Number { get; set; }
    }
}