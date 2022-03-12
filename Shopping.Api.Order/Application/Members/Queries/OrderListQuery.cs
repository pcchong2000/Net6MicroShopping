using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Order.Data;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.Application.Members.Queries
{
    public class MembersOrderListQuery : RequestPageBase, IRequest<MembersOrderListQueryResponse>
    {
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
    }
    public class MembersOrderListQueryResponse : ResponsePageBase<MembersOrderListQueryItemResponse>
    {

    }
    public class MembersOrderListQueryItemResponse
    {
        public string? Id { get; set; }
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderAddressId { get; set; }
        public int Status { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class MembersOrderListQueryHandler : IRequestHandler<MembersOrderListQuery, MembersOrderListQueryResponse>
    {
        private readonly OrderDbContext _context;
        public MembersOrderListQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<MembersOrderListQueryResponse> Handle(MembersOrderListQuery request, CancellationToken cancellationToken)
        {
            MembersOrderListQueryResponse resp = new MembersOrderListQueryResponse();

            var query = from o in _context.Order
                        where !o.IsDeleted
                        select new MembersOrderListQueryItemResponse()
                        {
                            Id = o.Id,
                            OrderAmount = o.OrderAmount,
                            CreateTime = o.CreatTime,
                            OrderAddressId = o.OrderAddressId,
                            MemberId = o.MemberId,
                            MemberName = o.MemberName,
                            OrderNo = o.OrderNo,
                            Status = o.Status,
                            StoreId = o.StoreId,
                            TenantId = o.TenantId,
                            StoreName = o.StoreName,

                        };

            resp.List = await query.OrderByDescending(a => a.CreateTime).PageList(request).ToListAsync();

            return resp;
        }
    }
}
