using MicroShoping.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api.Models
{
    public class ProductModelCategory : EntityTenantBase
    {
        [MaxLength(36)]
        public string ProductId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int Sort { get; set; }
    }
}
