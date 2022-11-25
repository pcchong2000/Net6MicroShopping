using Shopping.UI.MemberApp.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly HttpClientService _httpClient;
        public OrderService(HttpClientService httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<List<OrderListItemResponseModel>> GetOrderListAsync(OrderListRequestModel request)
        {
            var resp = await this._httpClient.GetAsync<OrderListRequestModel,List<OrderListItemResponseModel>>(Appsettings.OrderList, request);
            return resp;
        }
        public async Task<OrderListItemResponseModel> GetOrderDetailAsync(string id)
        {
            return await this._httpClient.GetAsync<string,OrderListItemResponseModel>(Appsettings.OrderDetail,id);
            
        }
    }
}
