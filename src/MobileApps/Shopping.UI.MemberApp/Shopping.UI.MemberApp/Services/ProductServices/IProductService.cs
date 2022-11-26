using Shopping.UI.MemberApp.Services.ProductServices.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.ProductServices
{
    public interface IProductService
    {
        Task<ResponsePageBase<ProductListItemResponseModel>> GetProductListAsync(ProductListRequestModel request);
        Task<ProductDetailResponseModel> GetProductDetailAsync(ProductDetailRequestModel request);
        Task<List<ProductCategoryResponseModel>> GetProductCategoryAsync();
        Task<ResponsePageBase<ProductHomeItemResponseModel>> GetProductHomeAsync(ProductHomeRequestModel request);
    }
}
