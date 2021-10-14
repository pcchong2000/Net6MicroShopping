using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.orders
{
    public class OrderItem : EntityTenantBase
    {
        public string MemberId { get; set; }
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal RealAmount { get; set; }
        public int Status { get; set; }
    }
}
