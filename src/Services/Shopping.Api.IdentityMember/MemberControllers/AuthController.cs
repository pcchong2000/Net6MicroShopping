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

namespace Shopping.Api.IdentityMember.MemberControllers
{
    public class AuthController : ApiController
    {
        private readonly ILogger<MemberController> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IClientStore _clientStore;
        public AuthController(ILogger<MemberController> logger, ICurrentUserService currentUserService, IClientStore clientStore)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _clientStore = clientStore;
        }
        [HttpGet("refreshcookie")]
        public async Task<IActionResult> RefreshCookie()
        {
            string clientId = "membermaui";
            if (_currentUserService.ClientId == clientId)
            {
                var client = await _clientStore.FindClientByIdAsync(clientId);
                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
                // issue authentication cookie with subject ID and username
                var isuser = new IdentityServerUser(_currentUserService.Id)
                {
                    DisplayName = _currentUserService.Name
                };

                await HttpContext.SignInAsync(isuser, props);
                string redirectUri = client.RedirectUris.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(redirectUri))
                {
                    return Redirect(redirectUri);
                }
            }
            return BadRequest();
        }

    }
}