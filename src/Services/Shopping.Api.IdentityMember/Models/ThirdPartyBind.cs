using Shopping.Framework.DomainBase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Framework.AccountDomain.Entities.Members
{
    [Table("ThirdPartyBind")]
    public class ThirdPartyBind : EntityBase
    {
        [MaxLength(36)]
        public string? MemberId { get; set; }
        [MaxLength(50)]
        public string? NickName { get; set; }
        [MaxLength(200)]
        public string? OpenId { get; set; }
        [MaxLength(200)]
        public string? UnionId { get; set; }
        [MaxLength(200)]
        public string? Scheme { get; set; }
        public ThirdPartyType SourceType { get; set; }

    }
    public enum ThirdPartyType
    {
        WeChart = 1,
        QQ = 2,
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 3,
        WeiBo = 4,
        GitHub = 5

    }
}
