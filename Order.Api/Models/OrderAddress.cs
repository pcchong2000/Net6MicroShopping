using MicroShoping.DomainBase;

namespace Ordering.Api.Models
{
    public class OrderAddress: EntityBase
    {
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 区/县
        /// </summary>
        public string CountyCode { get; set; }
        /// <summary>
        /// 镇
        /// </summary>
        public string TownCode { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
    }
}
