using Shopping.Framework.DomainBase.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.IdentityMember.Models
{
    [Table("MemberInfo")]
    public class MemberInfo : UserBase
    {
        [MaxLength(50)]
        public string NickName { get; set; }
        [MaxLength(500)]
        public string AvatarUrl { get; set; }
        public DateTime? BirthdayTime { get; set; }
    }
}
