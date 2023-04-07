using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Api.IdentityMember.MemberApplications.Addresses;
using Shopping.Framework.Web;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.MemberControllers
{
    public class AddressController : ApiController
    {
        private readonly ILogger<AddressController> _logger;
        private readonly ICurrentUserService _currentUserService;
        private ISender _mediator;
        public AddressController(ILogger<AddressController> logger, ICurrentUserService currentUserService, ISender mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<AddressListQueryResponse> Get()
        {
            return await _mediator.Send(new AddressListQuery() { MemberId= _currentUserService.Id });
        }
        [HttpPost]
        public async Task<bool> Post(AddressCreateCommand request)
        {
            request.MemberId = _currentUserService.Id;
            return await _mediator.Send(request);
        }
        [HttpPut]
        public async Task<bool> Put(AddressUpdateCommand request)
        {
            request.MemberId = _currentUserService.Id;
            return await _mediator.Send(request);
        }
        [HttpDelete]
        public async Task<bool> Delete(AddressDeleteCommand request)
        {
            request.MemberId = _currentUserService.Id;
            return await _mediator.Send(request);
        }
    }
}