using MicroShoping.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroShoping.Domain.Entities.Tenants
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
