using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Tenant.Controllers
{
    [ApiController]
    [Route("api/tenant/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        
    }

}