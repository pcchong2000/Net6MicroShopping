using Shopping.Framework.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Api.Product.Models
{
    /// <summary>
    /// 商品在商户店铺所在的分类
    /// </summary>
    public class StoreProductCategory : EntityTenantBase
    {
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Sort { get; set; }
    }
}
