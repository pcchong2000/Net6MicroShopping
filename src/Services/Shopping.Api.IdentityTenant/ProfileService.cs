using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Shopping.Framework.AccountApplication.AccountServices;
using Shopping.Framework.AccountDomain.Entities.Tenants;
using Shopping.Framework.AccountEFCore.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityTenant
{
    public class ProfileService : IProfileService
    {
        private readonly IAccountManage<TenantAdmin, TenantDbContext> _accountManage;
        public ProfileService(IAccountManage<TenantAdmin, TenantDbContext> accountManage)
        {
            _accountManage= accountManage;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string subjectId = context.Subject.GetSubjectId();
            var account = await _accountManage.GetAccountById(subjectId);
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("identityType", "IdentityTenant"));
            claims.Add(new Claim("tenantId", account.TenantId));
            claims.Add(new Claim("name", account.Name??""));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string subjectId = context.Subject.GetSubjectId();
            var account = await _accountManage.GetAccountById(subjectId);

            if (account!=null && !account.IsDeleted)
            {
                context.IsActive = true;
            }
        }
    }
}
