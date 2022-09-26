using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.Product.Controllers
{
    [ApiController]
    [Route("api/product/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        
    }

}