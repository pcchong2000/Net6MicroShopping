using MediatR;
using Shopping.Api.IdentityMember.Application.Members.Queries;
using Shopping.Api.IdentityMember.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Api.IdentityMember.Application.Members.Commands
{
    public class UpdateAvatarCommand: IRequest<bool>
    {
        public string Id { get; set; }
        public string AvatarUrl { get; set; }
    }
    public class UpdateAvatarCommandHandler : IRequestHandler<UpdateAvatarCommand, bool>
    {
        private readonly MemberDbContext _context;
        public UpdateAvatarCommandHandler(MemberDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.MemberInfos.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (member!=null)
            {
                member.AvatarUrl = request.AvatarUrl;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
