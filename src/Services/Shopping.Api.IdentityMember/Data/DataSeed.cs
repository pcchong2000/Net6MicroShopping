using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityMember.Models;
using Shopping.Framework.AccountApplication.AccountServices;
using Shopping.Framework.Web;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.Data
{
    public class DataSeed : IDataSeed
    {
        private readonly IAccountManage<MemberInfo, MemberDbContext> _accountManage;
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

            if (!await _accountManage.DbContext.MemberInfos.AnyAsync(a => a.UserName == memeber.UserName))
            {
                await _accountManage.Create(memeber, "123456");
            }

        }
    }
}
