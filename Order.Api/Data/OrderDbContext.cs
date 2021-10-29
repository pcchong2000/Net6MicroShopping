using Microsoft.EntityFrameworkCore;
using Order.Api.Models;

namespace Order.Api.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Order.Api.Models.Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderAddress> OrderAddress { get; set; }
        public DbSet<MemberAddress> MemberAddress { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

    }
}
