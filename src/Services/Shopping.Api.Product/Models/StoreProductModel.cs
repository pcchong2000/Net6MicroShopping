using Shopping.Framework.DomainBase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Product.Models
{
    /// <summary>
    /// 产品型号 
    /// 例如：尺寸(size)：28 29 30 31,颜色(color)：红 黄
    /// 其中 尺寸28 颜色红 为一个型号  Value 保存 "size:28,color:红"
    /// 
    /// </summary>
    public class StoreProductModel : EntityTenantBase
    {

        [MaxLength(36)]
        public string ProductId { get; set; }
        [MaxLength(200)]
        public string? Value { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Number { get; set; }
        public int Sort { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
// 添加产品时对型号设计应为 可以添加列也可以添加行
// 颜色  尺寸  单价   库存
// 红    26    95.00  100 
// 红    27    96.00  100
// 红    28    99.00  100
// 黄    26    95.00  100 
// 黄    27    96.00  100
// 黄    28    99.00  100