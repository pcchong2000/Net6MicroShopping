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
                Id = user.Claims.FirstOrDefault(a=>a.Type==ClaimTypes.NameIdentifier)?.Value;
                Name = user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)?.Value;
                TenantId = user.Claims.FirstOrDefault(a => a.Type == "tenantId")?.Value;
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string? TenantId { get; set; }
        
    }
}