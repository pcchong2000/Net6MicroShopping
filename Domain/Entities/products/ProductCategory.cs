using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.products
{
    public class ProductCategory : EntityTenantBase
    {
        public string Name { get; set; }
        public int Sort { get; set; }
    }
}
