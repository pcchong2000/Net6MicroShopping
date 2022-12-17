using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Api.IdentityMember.MemberApplications.Addresses
{
    public class AddressUpdateCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [MaxLength(50)]
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [MaxLength(50)]
        public string Latitude { get; set; }
        [MaxLength(50)]
        public string Province { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        [MaxLength(50)]
        public string County { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [MaxLength(50)]
        public string Street { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        [MaxLength(100)]
        public string AddressDetail { get; set; }
        /// <summary>
        /// 是否默认地址
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
