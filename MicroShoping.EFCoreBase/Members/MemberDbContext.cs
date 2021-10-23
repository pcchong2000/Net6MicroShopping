using Microsoft.EntityFrameworkCore;
using MicroShoping.Domain.Entities.Members;

namespace MicroShoping.EFCore.Members
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
