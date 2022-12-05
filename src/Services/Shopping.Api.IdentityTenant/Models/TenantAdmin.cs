using Shopping.Framework.DomainBase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.IdentityTenant.Models
{
    public class TenantAdmin : UserBase
    {
        [MaxLength(50)]
        public string NickName { get; set; }
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        public string TenantId { get; set; }
    }
}
