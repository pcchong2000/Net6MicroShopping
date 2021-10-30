using MicroShoping.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Product.Api.Models
{
    /// <summary>
    /// 平台的商品分类
    /// </summary>
    public class ProductCategory : EntityTenantBase
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
