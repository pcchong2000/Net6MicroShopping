﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductListItemResponseModel>> GetProductListAsync(ProductListRequestModel request);
        Task<ProductListItemResponseModel> GetProductAsync(string id);
        Task<ProductListItemResponseModel> GetProductNextAsync(ProductNextRequestModel request);
    }
}
