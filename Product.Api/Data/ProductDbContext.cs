using Microsoft.EntityFrameworkCore;
using Product.Api.Models;

namespace Product.Api.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<Product.Api.Models.ProductCategory> ProductCategory { get; set; }
        public DbSet<Product.Api.Models.TenantProductCategory> TenantProductCategory { get; set; }
        public DbSet<Product.Api.Models.Product> Product { get; set; }
        public DbSet<Product.Api.Models.ProductModel> ProductModel { get; set; }
        public DbSet<Product.Api.Models.ProductModelCategory> ProductModelCategory { get; set; }


    }
}
