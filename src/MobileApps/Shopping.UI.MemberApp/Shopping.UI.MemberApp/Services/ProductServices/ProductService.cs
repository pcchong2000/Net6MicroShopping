using Shopping.UI.MemberApp.Configs;
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


        public async Task<ProductDetailResponseModel> GetProductDetailAsync(ProductDetailRequestModel request)
        {
            ProductDetailResponseModel resp = await _httpClient.GetAsync<ProductDetailRequestModel, ProductDetailResponseModel>(Appsettings.ProductDetail, request);

            return resp;
        }

        public async Task<List<ProductCategoryResponseModel>> GetProductCategoryAsync()
        {
            List<ProductCategoryResponseModel> resp = await _httpClient.GetAsync<List<ProductCategoryResponseModel>>(Appsettings.ProductCategoryList);

            return resp;
        }

        public async Task<ResponsePageBase<ProductHomeResponseModel>> GetProductHomeAsync(ProductHomeRequestModel request)
        {
            ResponsePageBase<ProductHomeResponseModel> resp = await _httpClient.GetAsync<ProductHomeRequestModel, ResponsePageBase<ProductHomeResponseModel>>(Appsettings.ProductHome, request);

            return resp;
        }
    }
}
