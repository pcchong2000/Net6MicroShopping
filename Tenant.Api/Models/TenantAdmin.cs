using MicroShoping.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tenant.Api.Models
{
    public class TenantAdmin : EntityTenantBase
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string NickName { get; set; }
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        [MaxLength(200)]
        public string PasswordSecert { get; set; }
        [MaxLength(200)]
        public string PasswordHash { get; set; }

    }
}
