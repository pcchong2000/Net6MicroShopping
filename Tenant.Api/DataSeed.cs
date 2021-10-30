﻿
using MicroShoping.Application;
using MicroShoping.Domain.Entities.Tenants;
using MicroShoping.EFCore.Tenants;

namespace Tenant.Api
{
    public class DataSeed
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
            var tenant = new MicroShoping.Domain.Entities.Tenants.TenantInfo() { 
                Name="初始商户",
                TenantCode= "TenantCode",
                Description= "",
                Status=TenantStatus.Agree,
            };
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
                CreatorId = tenantAdmin.Id
            };
            await _context.TenantStore.AddAsync(store);


            await _accountManage.Create(tenantAdmin, "123456");

            

            


            await _context.SaveChangesAsync();
        }
    }
}
