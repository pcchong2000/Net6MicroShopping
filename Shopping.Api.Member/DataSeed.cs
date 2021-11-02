

using Microsoft.EntityFrameworkCore;
using Shopping.Framework.Domain.Entities.Members;
using Shopping.Framework.EFCore.Members;
using Shopping.Framework.Web;
using Shopping.Framework.Web.AccountServices;

namespace Shopping.Api.Member
{
    public class DataSeed : IDataSeed
    {
        private readonly IAccountManage<MemberInfo, MemberDbContext> _accountManage;
        public MemberDbContext _context;
        public DataSeed(IAccountManage<MemberInfo, MemberDbContext> accountManage)
        {
            _accountManage = accountManage;
        }

        public async Task Init()
        {
            var memeber = new MemberInfo()
            {
                UserName = "member1",
                Name = "默认会员1",
                Email = "member@qq.com",

            };
            if (!await _context.MemberInfos.AnyAsync(a => a.UserName == memeber.UserName))
            {
                await _accountManage.Create(memeber, "123456");
            }
                
        }
    }
}
