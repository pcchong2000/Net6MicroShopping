using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly DaprClient _daprClient;
        public OrderController(ILogger<OrderController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet]

        public async Task<string> Get()
        {
            //return await _daprClient.InvokeMethodAsync<IEnumerable<Product>>(HttpMethod.Get, "productapi", "Product");
            return "111111111111111";
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