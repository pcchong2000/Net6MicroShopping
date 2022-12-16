using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Api.IdentityMember.Application.Members;
using Shopping.Framework.Web;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.MemberControllers
{
    public class MemberController : ApiController
    {
        private readonly ILogger<MemberController> _logger;
        private readonly ICurrentUserService _currentUserService;
        private ISender _mediator;
        public MemberController(ILogger<MemberController> logger, ICurrentUserService currentUserService, ISender mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }
        [HttpGet("myinfo")]
        public async Task<MemberMyInfoQueryResponse> MyInfo()
        {
            return await _mediator.Send(new MemberMyInfoQuery() { Id = _currentUserService.Id });
        }
        [HttpPost("update")]
        public async Task<bool> Update(MemberUpdateCommand request)
        {
            request.Id = _currentUserService.Id;
            return await _mediator.Send(request);
        }
    }
}