using MediatR;
using Shopping.Api.IdentityMember.Application.Members;
using Shopping.Api.IdentityMember.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;

namespace Shopping.Api.IdentityMember.Application.Members
{
    public class MemberUpdateCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? BirthdayTime { get; set; }
    }
    public class MemberUpdateCommandHandler : IRequestHandler<MemberUpdateCommand, bool>
    {
        private readonly MemberDbContext _context;
        public MemberUpdateCommandHandler(MemberDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(MemberUpdateCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.MemberInfos.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (member != null)
            {
                
                if (string.IsNullOrWhiteSpace(request.AvatarUrl))
                {
                    member.AvatarUrl = request.AvatarUrl;
                }
                if (string.IsNullOrWhiteSpace(request.NickName))
                {
                    member.NickName = request.NickName;
                }
                if (request.BirthdayTime!=null)
                {
                    member.BirthdayTime = request.BirthdayTime;
                }
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
