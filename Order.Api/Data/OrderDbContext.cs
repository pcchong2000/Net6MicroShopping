using Microsoft.EntityFrameworkCore;
using Ordering.Api.Models;

namespace Ordering.Api.Data
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderAddress> OrderAddress { get; set; }
        public DbSet<MemberAddress> MemberAddress { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

    }
}
