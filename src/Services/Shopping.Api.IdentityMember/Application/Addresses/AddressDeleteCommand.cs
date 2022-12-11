using MediatR;

namespace Shopping.Api.IdentityMember.Application.Addresses
{
    public class AddressDeleteCommand : IRequest<bool>
    {
        public string MemberId { get; set; }
        public string Id { get; set; }
    }
}
