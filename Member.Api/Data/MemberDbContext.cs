using Microsoft.EntityFrameworkCore;
using Member.Api.Models;

namespace Member.Api.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
        {
        }
        public DbSet<MemberInfo> MemberInfos { get; set; }
        public DbSet<ThirdPartyBind> ThirdPartyBinds { get; set; }
    }
}
