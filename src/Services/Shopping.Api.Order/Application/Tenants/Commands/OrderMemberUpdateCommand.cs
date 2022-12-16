using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Order.Data;

namespace Shopping.Api.Order.Application.Tenants.Commands
{
    public class OrderMemberUpdateCommand : IRequest
    {
        public string Id { get; set; }=null!;
        public string? NickName { get; set; }
        public string? Name { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? BirthdayTime { get; set; }
    }
    public class OrderMemberUpdateCommandHandler : AsyncRequestHandler<OrderMemberUpdateCommand>
    {
        private OrderDbContext _context;
        public OrderMemberUpdateCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(OrderMemberUpdateCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return;
            }
            var list = await _context.Order.Where(a => a.MemberId == request.Id).ToListAsync();
            foreach (var item in list)
            {
                item.MemberName = request.Name;
            }
            await _context.SaveChangesAsync();
        }
    }
}
