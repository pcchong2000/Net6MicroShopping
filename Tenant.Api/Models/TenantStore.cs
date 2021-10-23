using MicroShoping.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tenant.Api.Models
{
    public class TenantStore : EntityBase
    {
        public string TenantId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

    }
}
