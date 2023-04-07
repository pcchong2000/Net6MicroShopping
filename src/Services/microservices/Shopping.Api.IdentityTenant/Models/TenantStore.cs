using Shopping.Framework.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.IdentityTenant.Models
{
    public class TenantStore : EntityBase
    {
        [MaxLength(36)]
        public string TenantId { get; set; }
        public string StoreCode { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public TenantStoreStatus Status { get; set; }
        [MaxLength(36)]
        public string CreatorId { get; set; }
        [MaxLength(36)]
        public string CreatorName { get; set; }
    }
    public enum TenantStoreStatus
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
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 4,
    }
}
