using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Product.MemberControllers
{
    [ApiController]
    [Route("api/product/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {

    }

}