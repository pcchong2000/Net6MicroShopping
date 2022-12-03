using Microsoft.EntityFrameworkCore;
using Shopping.Framework.AccountDomain.Entities.Members;

namespace Shopping.Framework.AccountEFCore.Members
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
