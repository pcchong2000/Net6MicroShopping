
using Microsoft.EntityFrameworkCore;
using Shopping.Framework.AccountDomain.Entities.Tenants;

namespace Shopping.Framework.AccountEFCore.Tenants
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
