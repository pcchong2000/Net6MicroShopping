using MicroShoping.DomainBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Api.Models
{
    public class Order : EntityTenantBase
    {
        [MaxLength(36)]
        public string MemberId { get; set; }
        [MaxLength(50)]
        public string MemberName { get; set; }
        [MaxLength(36)]
        public string OrderNo { get; set; }
        [MaxLength(36)]
        public string OrderAddressId { get; set; }
        public int Status { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderAmount { get; set; }
    }
}
