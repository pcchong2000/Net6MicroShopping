using Shopping.UI.MemberApp.Services.ProductServices.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.ProductServices
{
    public class ProductService : IProductService
    {

        private readonly HttpClientService _httpClient;
        public ProductService(HttpClientService httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<List<ProductListItemResponseModel>> GetProductListAsync(ProductListRequestModel request)
        {
            List<ProductListItemResponseModel> resp = await _httpClient.GetAsync<List<ProductListItemResponseModel>>($"/api/blog");
            
            return resp;
        }


        public async Task<ProductListItemResponseModel> GetProductDetailAsync(string id)
        {
            ProductListItemResponseModel resp = await _httpClient.GetAsync<ProductListItemResponseModel>($"/api/blog");

            return resp;
        }

        public async Task<List<ProductCategoryResponseModel>> GetProductCategoryAsync()
        {
            List<ProductCategoryResponseModel> resp = await _httpClient.GetAsync<List<ProductCategoryResponseModel>>($"/api/blog");

            return resp;
        }

        public async Task<List<ProductHomeResponseModel>> GetProductHomeAsync(ProductHomeRequestModel request)
        {
            List<ProductHomeResponseModel> resp = await _httpClient.GetAsync<List<ProductHomeResponseModel>>($"/api/blog");

            return resp;
        }
    }
}
