using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Order.MemberControllers
{
    [ApiController]
    [Route("api/order/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {

    }

}