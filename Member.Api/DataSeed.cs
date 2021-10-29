using MicroShoping.Application;
using MicroShoping.Domain.Entities.Members;
using MicroShoping.EFCore.Members;

namespace Member.Api
{
    public class DataSeed
    {
        private readonly IAccountManage<MemberInfo, MemberDbContext> _accountManage;
        public MemberDbContext _context;
        public DataSeed(IAccountManage<MemberInfo, MemberDbContext> accountManage)
        {
            _accountManage = accountManage;
        }

        public async Task Init()
        {
            var memeber = new MemberInfo() { 
                UserName="member1",
                Name="默认会员1",
                Email="member@qq.com",
                
            
            };
            await _accountManage.Create(memeber, "123456");
        }
    }
}
