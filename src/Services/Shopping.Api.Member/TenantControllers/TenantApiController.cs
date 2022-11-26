using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Framework.Web;

namespace Shopping.Api.Member.TenantControllers
{
    [ApiController]
    [Route($"api/member/{JwtBearerIdentity.TenantScheme}/[controller]")]
    [Authorize]
    public class TenantApiController : ControllerBase
    {

    }

}