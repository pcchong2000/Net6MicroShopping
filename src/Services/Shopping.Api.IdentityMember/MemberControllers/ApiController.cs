using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Api.IdentityMember.MemberControllers
{
    [ApiController]
    [Route("api/member/[controller]")]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class ApiController : ControllerBase
    {

    }

}