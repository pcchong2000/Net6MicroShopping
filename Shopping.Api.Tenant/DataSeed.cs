

using Microsoft.EntityFrameworkCore;
using Shopping.Framework.Domain.Entities.Tenants;
using Shopping.Framework.EFCore.Tenants;
using Shopping.Framework.Web;
using Shopping.Framework.Web.AccountServices;

namespace Shopping.Api.Tenant
{
    public class DataSeed : IDataSeed
    {
        private readonly IAccountManage<TenantAdmin, TenantDbContext> _accountManage;
        public TenantDbContext _context;
        public DataSeed(TenantDbContext context, IAccountManage<TenantAdmin, TenantDbContext> accountManage)
        {
            _context = context;
            _accountManage = accountManage;
        }

        public async Task Init()
        {
            var tenant = new TenantInfo()
            {
                Id= "3a00a01f-8a3b-9d59-a59c-281e8bb589bf",
                Name = "初始商户",
                TenantCode = "TenantCode",
                Description = "",
                Status = TenantStatus.Agree,
            };
            if (!await _context.TenantInfo.AnyAsync(a => a.TenantCode == tenant.TenantCode))
            {
                await _context.TenantInfo.AddAsync(tenant);

                var tenantAdmin = new TenantAdmin()
                {
                    TenantId = tenant.Id,
                    Name = "初始商户管理员",
                    UserName = "admin1",

                };
                var store = new TenantStore()
                {
                    TenantId = tenant.Id,
                    StoreCode = "StoreCode",
                    Name = "初始商户门店",
                    Description = "",
                    Status = TenantStoreStatus.Agree,
                    CreatorId = tenantAdmin.Id,
                    CreatorName = tenantAdmin.Name
                };
                await _context.TenantStore.AddAsync(store);

                await _accountManage.Create(tenantAdmin, "123456");

                await _context.SaveChangesAsync();
            }

        }
    }
}
