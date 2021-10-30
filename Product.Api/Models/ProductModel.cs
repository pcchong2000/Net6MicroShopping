using MicroShoping.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api.Models
{
    public class ProductModel : EntityTenantBase
    {
        [MaxLength(36)]
        public string ProductModelCategoryId { get; set; }
        
        [MaxLength(36)]
        public string ProductId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Number { get; set; }
        public int Sort { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
