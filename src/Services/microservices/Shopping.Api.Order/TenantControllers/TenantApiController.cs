using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.TenantControllers
{
    [ApiController]
    [Route($"api/order/{JwtBearerIdentity.TenantScheme}/[controller]")]
    [Authorize]
    public class TenantApiController : ControllerBase
    {

    }

}