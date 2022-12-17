using MediatR;
using Shopping.Api.IdentityMember.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using Dapr.Client;
using System.Xml.Linq;

namespace Shopping.Api.IdentityMember.MemberApplications.Members
{
    public class MemberUpdateCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? BirthdayTime { get; set; }
    }
    public class MemberUpdateCommandHandler : IRequestHandler<MemberUpdateCommand, bool>
    {
        private readonly MemberDbContext _context;
        private readonly DaprClient _daprClient;
        public MemberUpdateCommandHandler(MemberDbContext context, DaprClient daprClient)
        {
            _context = context;
            _daprClient = daprClient;
        }
        public async Task<bool> Handle(MemberUpdateCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.MemberInfos.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (member != null)
            {

                if (!string.IsNullOrWhiteSpace(request.AvatarUrl))
                {
                    member.AvatarUrl = request.AvatarUrl;
                }
                if (!string.IsNullOrWhiteSpace(request.NickName))
                {
                    member.NickName = request.NickName;
                }
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    member.Name = request.Name;
                }
                if (request.BirthdayTime != null)
                {
                    member.BirthdayTime = request.BirthdayTime;
                }

                await _context.SaveChangesAsync();

                //mq
                await _daprClient.PublishEventAsync("pubsub", typeof(MemberUpdateCommand).Name, new { member.Id, member.AvatarUrl, member.NickName, member.Name, member.BirthdayTime });

                return true;
            }
            return false;
        }
    }
}
