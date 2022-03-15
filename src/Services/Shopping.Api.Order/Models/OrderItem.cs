using Shopping.Framework.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Order.Models
{
    public class OrderItem : EntityTenantBase
    {
        [MaxLength(36)]
        public string OrderId { get; set; }
        [MaxLength(36)]
        public string OrderNo { get; set; }
        [MaxLength(36)]
        public string ProductId { get; set; }
        [MaxLength(50)]
        public string? ProductName { get; set; }
        [MaxLength(200)]
        public string? ProductImageUrl { get; set; }
        [MaxLength(36)]
        public string MemberId { get; set; }
        public int Status { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Unit { get; set; }
    }
}
