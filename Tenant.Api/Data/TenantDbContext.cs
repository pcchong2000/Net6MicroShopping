using Microsoft.EntityFrameworkCore;
using Tenant.Api.Models;

namespace Tenant.Api.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {
        }
        public DbSet<Tenant.Api.Models.Tenant> Tenant { get; set; }
        public DbSet<Tenant.Api.Models.TenantStore> TenantStore { get; set; }
        public DbSet<Tenant.Api.Models.TenantAdmin> TenantAdmin { get; set; }
        
    }
}
