using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.TenantControllers
{
    [ApiController]
    [Route($"api/product/{JwtBearerIdentity.TenantScheme}/[controller]")]
    [Authorize]
    public class TenantApiController : ControllerBase
    {

    }

}