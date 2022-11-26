using Shopping.Framework.DomainBase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Api.Product.Models
{
    /// <summary>
    /// 型号分类
    /// 例如：尺寸(size)：28 29 30 31,颜色(color)：红 黄
    /// 其中 尺寸是 Name,size  是 Code，  28,29,30,31是Items
    /// 其中 颜色是 Name,color 是 Code，  红,黄  是Items
    /// </summary>
    public class StoreProductModelCategory : EntityTenantBase
    {
        [MaxLength(36)]
        public string? ProductId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Code { get; set; }
        [MaxLength(500)]
        public string? Items { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Sort { get; set; }
    }
}
