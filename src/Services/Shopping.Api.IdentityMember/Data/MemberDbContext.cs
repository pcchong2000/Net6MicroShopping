using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityMember.Models;

namespace Shopping.Api.IdentityMember.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
        {
        }
        public DbSet<MemberInfo> MemberInfos { get; set; }
        public DbSet<ThirdPartyBind> ThirdPartyBinds { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
