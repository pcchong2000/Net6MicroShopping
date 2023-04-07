using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Framework.Web;

namespace Shopping.Api.IdentityMember.TenantControllers
{
    [ApiController]
    [Route($"api/member/{JwtBearerIdentity.TenantScheme}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerIdentity.TenantScheme)]
    public class TenantApiController : ControllerBase
    {

    }

}