using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.products
{
    public class Product : EntityTenantBase
    {
        public string ProductCategoryId { get; set; }
        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
