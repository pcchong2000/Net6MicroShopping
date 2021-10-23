using MicroShoping.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Api.Models
{
    public class ShoppingCart: EntityTenantBase
    {
        [MaxLength(36)]
        public string MemberId { get; set; }
        [MaxLength(36)]
        public string ProductId { get; set; }
        [MaxLength(50)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        public string ProductImageUrl { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Unit { get; set; }
    }
}
