
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityTenant.Models;

namespace Shopping.Api.IdentityTenant.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {
        }
        public DbSet<TenantInfo> TenantInfo { get; set; }
        public DbSet<TenantStore> TenantStore { get; set; }
        public DbSet<TenantAdmin> TenantAdmin { get; set; }

    }
}
