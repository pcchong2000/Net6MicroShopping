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
        public async Task<List<BlogListItemResponseModel>> GetBlogListAsync(BlogListRequestModel request)
        {
            var resp = await this._httpClient.GetAsync<BlogListRequestModel,List<BlogListItemResponseModel>>(Appsettings.BlogListUrl, request);
            return resp;
        }
        public async Task<BlogListItemResponseModel> GetBlogAsync(string id)
        {
            return await this._httpClient.GetAsync<BlogListItemResponseModel>(Appsettings.BlogInfoUrl+ id);
            
        }
    }
}
