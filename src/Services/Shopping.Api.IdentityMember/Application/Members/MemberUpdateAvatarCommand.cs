using MediatR;
using Shopping.Api.IdentityMember.Application.Members;
using Shopping.Api.IdentityMember.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Api.IdentityMember.Application.Members
{
    public class MemberUpdateAvatarCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string AvatarUrl { get; set; }
    }
    public class MemberUpdateAvatarCommandHandler : IRequestHandler<MemberUpdateAvatarCommand, bool>
    {
        private readonly MemberDbContext _context;
        public MemberUpdateAvatarCommandHandler(MemberDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(MemberUpdateAvatarCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.MemberInfos.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (member != null)
            {
                member.AvatarUrl = request.AvatarUrl;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
