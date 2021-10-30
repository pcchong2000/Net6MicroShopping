using MicroShoping.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Order.Api.Models
{
    public class MemberAddress : EntityBase
    {
        [MaxLength(36)]
        public string MemberId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        [MaxLength(50)]
        public string? CountryCode { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [MaxLength(50)]
        public string? ProvinceCode { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [MaxLength(50)]
        public string? CityCode { get; set; }
        /// <summary>
        /// 区/县
        /// </summary>
        [MaxLength(50)]
        public string? CountyCode { get; set; }
        /// <summary>
        /// 镇
        /// </summary>
        [MaxLength(50)]
        public string? TownCode { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [MaxLength(200)]
        public string? Address { get; set; }
    }
}
