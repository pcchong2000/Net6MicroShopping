using MediatR;

namespace Shopping.Api.IdentityMember.MemberApplications.Addresses
{
    public class AddressDeleteCommand : IRequest<bool>
    {
        public string MemberId { get; set; }
        public string Id { get; set; }
    }
}
