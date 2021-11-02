using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Models;

namespace Shopping.Api.Product.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Models.StoreProductCategory> StoreProductCategory { get; set; }
        public DbSet<Models.Product> Product { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
        public DbSet<Models.ProductModelCategory> ProductModelCategory { get; set; }


    }
}
