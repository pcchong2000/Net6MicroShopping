using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Member.MemberControllers
{
    [ApiController]
    [Route("api/member/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {

    }

}