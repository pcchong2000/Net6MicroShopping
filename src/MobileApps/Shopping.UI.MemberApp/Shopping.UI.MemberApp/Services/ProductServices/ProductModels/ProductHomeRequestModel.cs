using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.ProductServices.ProductModels
{
    public class ProductHomeRequestModel : RequestPageBase
    {

    }
    public class ProductHomeItemResponseModel
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
