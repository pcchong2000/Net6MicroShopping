using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.products
{
    public class ProductModel : EntityTenantBase
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public int Sort { get; set; }
    }
}
