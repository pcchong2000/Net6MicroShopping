using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shopping.Framework.Web
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null)
            {
                UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                TenantId = user.FindFirstValue("tenantId");
                Name = user.FindFirstValue(ClaimTypes.Name);
            }
        }

        public string? UserId { get; set; }
        public string? TenantId { get; set; }
        public string? Name { get; set; }
    }
}