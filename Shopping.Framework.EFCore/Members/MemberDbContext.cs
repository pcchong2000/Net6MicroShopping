using Microsoft.EntityFrameworkCore;
using Shopping.Framework.Domain.Entities.Members;

namespace Shopping.Framework.EFCore.Members
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
