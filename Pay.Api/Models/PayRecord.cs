using MicroShoping.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pay.Api.Models
{
    public class PayRecord : EntityTenantBase
    {
        [MaxLength(36)]
        public string PayId { get; set; }
        [MaxLength(36)]
        public string MemberId { get; set; }
        [MaxLength(50)]
        public string? MemberName { get; set; }
        [MaxLength(36)]
        public string OrderNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderAmount { get; set; }
        public PayStatus Status { get; set; }
    }
    public enum PayStatus
    {
        PayWait,
        PayComplete,
        PayBad
    }
    public enum PayServiceProvider
    { 
        WeChart,
        Alipay,
    }
}
