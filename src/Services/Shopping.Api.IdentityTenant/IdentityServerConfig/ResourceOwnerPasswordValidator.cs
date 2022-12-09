using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;
using IdentityServer4.Services;
using IdentityServer4.Events;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.AccountApplication.AccountServices;
using Shopping.Api.IdentityTenant.Data;
using Shopping.Api.IdentityTenant.Models;

namespace Shopping.Api.IdentityTenant.IdentityServerConfig
{

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IEventService _events;
        private readonly IAccountManage<TenantAdmin, TenantDbContext> _accountManage;
        private readonly ILogger<ResourceOwnerPasswordValidator> _logger;


        public ResourceOwnerPasswordValidator(
            IAccountManage<TenantAdmin, TenantDbContext> accountManage,
            IEventService events,
            ILogger<ResourceOwnerPasswordValidator> logger)
        {
            _accountManage = accountManage;
            _events = events;
            _logger = logger;
        }

        public virtual async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var clientId = context.Request?.Client?.ClientId;
            var user = await _accountManage.GetAccountByUserName(context.UserName);
            if (user != null)
            {
                var result = _accountManage.CheckPassword(user, context.Password);
                if (result)
                {
                    var sub = user.Id;

                    _logger.LogInformation("Credentials validated for username: {username}", context.UserName);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(context.UserName, sub, context.UserName, false, clientId));

                    context.Result = new GrantValidationResult(
                     subject: sub,
                     authenticationMethod: AuthenticationMethods.Password,
                     customResponse: new Dictionary<string, object>() {
                         { "code",ResponseBaseCode.Success}
                     }
                    );
                    return;
                }
            }
            else
            {
                _logger.LogInformation("No user found matching username: {username}", context.UserName);
                await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid username", false, clientId));
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, customResponse: new Dictionary<string, object>() {
                         { "code",ResponseBaseCode.Fail},
                         { "message","登录错误检查用户名密码"},
                     });
        }
    }
}
