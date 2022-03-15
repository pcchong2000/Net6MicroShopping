using Shopping.Framework.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Product.Models
{
    /// <summary>
    /// 平台的商品分类
    /// </summary>
    public class ProductCategory : EntityBase
    {
        [MaxLength(36)]
        public string? ParentId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Code { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Sort { get; set; }
        [NotMapped]
        public List<ProductCategory> Categories { get; set; }
    }
}
