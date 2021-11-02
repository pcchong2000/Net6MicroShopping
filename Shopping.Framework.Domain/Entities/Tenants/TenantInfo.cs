using Shopping.Framework.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Framework.Domain.Entities.Tenants
{
    public class TenantInfo : EntityBase
    {
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string TenantCode { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public TenantStatus Status { get; set; }
    }
    public enum TenantStatus
    {
        /// <summary>
        /// 申请中
        /// </summary>
        Apply = 0,
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 2,
        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = 3,
    }
}
