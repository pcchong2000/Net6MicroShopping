using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityMember.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.MemberApplications.Members
{
    public class MemberMyInfoQuery : IRequest<MemberMyInfoQueryResponse>
    {
        public string Id { get; set; }
    }
    public class MemberMyInfoQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime? BirthdayTime { get; set; }
    }
    public class MemberMyInfoQueryHandler : IRequestHandler<MemberMyInfoQuery, MemberMyInfoQueryResponse>
    {
        private readonly MemberDbContext _context;
        public MemberMyInfoQueryHandler(MemberDbContext context)
        {
            _context = context;
        }
        public async Task<MemberMyInfoQueryResponse> Handle(MemberMyInfoQuery request, CancellationToken cancellationToken)
        {
            return await _context.MemberInfos.Where(a => a.Id == request.Id).Select(a => new MemberMyInfoQueryResponse()
            {
                Id = a.Id,
                Name = a.Name,
                UserName = a.UserName,
                NickName = a.NickName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                AvatarUrl = a.AvatarUrl,
                BirthdayTime = a.BirthdayTime
            }).FirstOrDefaultAsync();
        }
    }
}
