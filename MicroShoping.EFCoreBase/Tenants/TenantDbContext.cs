using MicroShoping.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;

namespace MicroShoping.EFCore.Tenants
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
