using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.OSS.Controllers
{
    [ApiController]
    [Route("api/oss/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        
    }

}