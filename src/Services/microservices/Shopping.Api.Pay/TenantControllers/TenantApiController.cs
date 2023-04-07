using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Pay.MemberControllers;
using Shopping.Framework.Web;

namespace Shopping.Api.Pay.TenantControllers
{
    [ApiController]
    [Route($"api/member/{JwtBearerIdentity.TenantScheme}/[controller]")]
    [Authorize]
    public class TenantApiController : ControllerBase
    {

    }

}