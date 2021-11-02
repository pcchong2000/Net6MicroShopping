
using Shopping.Framework.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Product.Models
{
    public class Product : EntityTenantBase
    {

        [MaxLength(36)]
        public string ProductCategoryId { get; set; }
        [MaxLength(36)]
        public string StoreProductCategoryId { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Sort { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }

    }
    public enum ProductStatus
    {
        WaitUp,
        UpShelf,
        DownShelf,
    }
}
