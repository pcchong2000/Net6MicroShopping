using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.IdentityTenant.TenantControllers
{
    [ApiController]
    [Route("api/tenant/[controller]")]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class ApiController : ControllerBase
    {

    }

}