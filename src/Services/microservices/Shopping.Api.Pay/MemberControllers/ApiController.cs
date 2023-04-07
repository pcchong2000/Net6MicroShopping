using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Pay.MemberControllers
{
    [ApiController]
    [Route("api/member/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {

    }

}