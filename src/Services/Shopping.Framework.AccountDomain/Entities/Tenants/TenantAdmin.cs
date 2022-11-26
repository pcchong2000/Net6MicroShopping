using Shopping.Framework.AccountDomain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Framework.AccountDomain.Entities.Tenants
{
    public class TenantAdmin : UserBase
    {
        [MaxLength(50)]
        public string? NickName { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        public string TenantId { get; set; }

    }
}
