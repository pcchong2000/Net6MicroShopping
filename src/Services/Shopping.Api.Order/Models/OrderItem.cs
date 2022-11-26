using Shopping.Framework.DomainBase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Order.Models
{
    public class OrderItem : EntityBase
    {
        public OrderItem(string orderId,string orderNo,string productId, string productModelId, string? productModelValue, decimal price,int number)
        { 
            this.OrderId = orderId;
            this.OrderNo = orderNo;
            this.ProductId = productId;
            this.Price = price;
            this.Number = number;
            this.Amount = price* number;
            this.ProductModelId = productModelId;
            this.ProductModelValue = productModelValue;
        }
        [MaxLength(36)]
        public string OrderId { get; set; }
        [MaxLength(36)]
        public string OrderNo { get; set; }
        [MaxLength(36)]
        public string ProductId { get; set; }
        [MaxLength(50)]
        public string? ProductName { get; set; }
        [MaxLength(200)]
        public string? ProductImageUrl { get; set; }
        [MaxLength(36)]
        public string ProductModelId { get; set; }
        [MaxLength(50)]
        public string? ProductModelValue { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
    }
}
