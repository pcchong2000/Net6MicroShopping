﻿using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityMember.Data;
using Shopping.Api.IdentityMember.Models;
using Shopping.Identity.AccountApplication.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.IdentityServerConfig
{
    public class ProfileService : IProfileService
    {
        private readonly IAccountManage<MemberInfo, MemberDbContext> _accountManage;
        public ProfileService(IAccountManage<MemberInfo, MemberDbContext> accountManage)
        {
            _accountManage = accountManage;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string subjectId = context.Subject.GetSubjectId();
            var account = await _accountManage.GetAccountById(subjectId);
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("identityType", "IdentityMember"));
            claims.Add(new Claim("name", account.Name ?? ""));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string subjectId = context.Subject.GetSubjectId();
            var account = await _accountManage.GetAccountById(subjectId);

            if (account != null && !account.IsDeleted)
            {
                context.IsActive = true;
            }
        }
    }
}
