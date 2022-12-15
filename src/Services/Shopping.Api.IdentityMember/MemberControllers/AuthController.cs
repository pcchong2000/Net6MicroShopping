using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Framework.Web;
using System.Diagnostics.Metrics;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Stores;
using System.Linq;
using Shopping.Api.IdentityMember.IdentityServerControllers.Account;
using IdentityServer4.Validation;
using IdentityServer4.Hosting.LocalApiAuthentication;
using Microsoft.Extensions.Options;
using IdentityModel;
using System.Security.Claims;

namespace Shopping.Api.IdentityMember.MemberControllers
{
    public class AuthController : ApiController
    {
        private readonly ILogger<MemberController> _logger;
        private readonly ITokenValidator _tokenValidator;
        private readonly LocalApiAuthenticationOptions _options;
        public AuthController(ILogger<MemberController> logger,  ITokenValidator tokenValidator, IOptionsMonitor<LocalApiAuthenticationOptions> options)
        {
            _logger = logger;
            _tokenValidator= tokenValidator;
            _options = options.CurrentValue;
        }
        /// <summary>
        /// AddAuthentication().AddLocalApi 无法添加token 为 query 参数的请求
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpGet("refresh_cookie")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshCookie(string access_token)
        {
            var validate = await _tokenValidator.ValidateAccessTokenAsync(access_token, _options.ExpectedScope);
            if (validate.IsError)
            {
                return BadRequest();
            }
            
            string clientId = "membermaui";
            if (validate.Client.ClientId == clientId)
            {
                var id = validate.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier || a.Type == JwtClaimTypes.Subject).Value;
                var name = validate.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name || a.Type == JwtClaimTypes.Name).Value;
                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration),
                };
                // issue authentication cookie with subject ID and username
                var isuser = new IdentityServerUser(id)
                {
                    DisplayName = name
                };

                await HttpContext.SignInAsync(isuser, props);
                string redirectUri = validate.Client.RedirectUris.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(redirectUri))
                {
                    return Redirect(redirectUri);
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnuri)
        {
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            return Redirect(returnuri);
            
        }

    }
}