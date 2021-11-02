using Microsoft.EntityFrameworkCore;
using Shopping.Api.Pay.Models;

namespace Shopping.Api.Pay.Data
{
    public class PayDbContext : DbContext
    {
        public PayDbContext(DbContextOptions<PayDbContext> options) : base(options)
        {
        }
        public DbSet<PayRecord> PayRecord { get; set; }

    }
}
