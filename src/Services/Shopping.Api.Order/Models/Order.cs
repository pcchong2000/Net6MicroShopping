using Microsoft.EntityFrameworkCore;
using Shopping.Framework.DomainBase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Order.Models
{
    [Index(nameof(OrderNo),IsUnique =true)]
    public class Order : EntityTenantBase
    {
        private static Random random = new Random();
        public Order(){}
        public Order(string storeId, string storeName, string memberId, string memberName,string tenantId)
        {
            this.StoreId = storeId;
            this.StoreName = storeName;
            this.MemberId = memberId;
            this.MemberName = memberName;
            this.TenantId = tenantId;
            this.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssms")+ random.Next();
            this.OrderItems = new List<OrderItem>();
            this.CreatorId = memberId;
        }
        [MaxLength(100)]
        public string StoreName { get; set; }
        [MaxLength(36)]
        public string MemberId { get; set; }
        [MaxLength(50)]
        public string? MemberName { get; set; }
        [MaxLength(36)]
        public string OrderNo { get; set; }
        [MaxLength(36)]
        public string? OrderAddressId { get; set; }
        public int Status { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderAmount { get; set; }
        [NotMapped]
        public List<OrderItem>  OrderItems { get; set; }
        [NotMapped]
        public OrderAddress OrderAddress { get; set; }
        public void AddItem(string productId, string productName, string? productImageUrl, string productModelId, string? productModelValue, decimal price, int number)
        {
            this.OrderItems.Add(new OrderItem(this.Id, this.OrderNo, productId, productModelId, productModelValue, price, number)
            {
                ProductImageUrl = productImageUrl,
                ProductName = productName,
            });

        }
        public OrderAddress AddOredrAddress(MemberAddress memberAddress)
        {
            this.OrderAddress = new OrderAddress(this, memberAddress);
            this.OrderAddressId= this.OrderAddress.Id;

            return this.OrderAddress;
        }
       
    }
}
