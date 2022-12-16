using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.ProductServices.ProductModels
{
    public class ProductCategoryResponseModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public Color CheckColor { get; set; }
        public List<ProductCategoryResponseModel> Childrens { get; set; }
    }
}
