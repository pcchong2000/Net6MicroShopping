using Microsoft.EntityFrameworkCore;
using Pay.Api.Models;

namespace Pay.Api.Data
{
    public class PayDbContext : DbContext
    {
        public PayDbContext(DbContextOptions<PayDbContext> options) : base(options)
        {
        }
        public DbSet<PayRecord> PayRecord { get; set; }

    }
}
