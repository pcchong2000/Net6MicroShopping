using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityMember.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.Application.Members.Queries
{
    public class MyInfoQuery:IRequest<MyInfoQueryResponse>
    {
        public string Id { get; set; }
    }
    public class MyInfoQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public DateTime CreateTime { get; set; }
    }
    public class MyInfoQueryHandler : IRequestHandler<MyInfoQuery, MyInfoQueryResponse>
    {
        private readonly MemberDbContext _context;
        public MyInfoQueryHandler(MemberDbContext context)
        {
            _context = context;
        }
        public async Task<MyInfoQueryResponse> Handle(MyInfoQuery request, CancellationToken cancellationToken)
        {
            return await _context.MemberInfos.Where(a => a.Id == request.Id).Select(a => new MyInfoQueryResponse()
            {
                Id = a.Id,
                Name = a.Name,
                UserName = a.UserName,
                NickName = a.NickName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                AvatarUrl = a.AvatarUrl,
            }).FirstOrDefaultAsync();
        }
    }
}
