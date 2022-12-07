using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Order.Data;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.Application.Tenants.Queries
{
    public class OrderListQuery : RequestPageBase, IRequest<OrderListQueryResponse>
    {
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
    }
    public class OrderListQueryResponse : ResponsePageBase<OrderListQueryItemResponse>
    {

    }
    public class OrderListQueryItemResponse
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
    public class OrderListQueryHandler : IRequestHandler<OrderListQuery, OrderListQueryResponse>
    {
        private readonly OrderDbContext _context;
        public OrderListQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<OrderListQueryResponse> Handle(OrderListQuery request, CancellationToken cancellationToken)
        {
            OrderListQueryResponse resp = new OrderListQueryResponse();

            var query = from o in _context.Order
                        where !o.IsDeleted
                        select new OrderListQueryItemResponse()
                        {
                            Id = o.Id,
                            OrderAmount = o.OrderAmount,
                            CreateTime = o.CreateTime,
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
