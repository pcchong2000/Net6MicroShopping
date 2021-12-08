using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Models;

namespace Shopping.Api.Product.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 产品
        /// </summary>
        public DbSet<Models.Product> Product { get; set; }
        /// <summary>
        /// 产品全平台分类
        /// </summary>
        public DbSet<Models.ProductCategory> ProductCategory { get; set; }
        /// <summary>
        /// 产品门店分类
        /// </summary>
        public DbSet<Models.StoreProductCategory> StoreProductCategory { get; set; }
        /// <summary>
        /// 产品门店型号
        /// </summary>
        public DbSet<Models.StoreProductModel> StoreProductModel { get; set; }
        /// <summary>
        /// 产品门店型号分类
        /// </summary>
        public DbSet<Models.StoreProductModelCategory> StoreProductModelCategory { get; set; }


    }
}
