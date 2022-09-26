using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Member.Controllers
{
    [ApiController]
    [Route("api/member/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        
    }

}