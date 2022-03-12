using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Order.Data;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.Application.Tenants.Queries
{
    public class OrderDetailQuery : IRequest<OrderDetailQueryResponse>
    {
        public OrderDetailQuery(string orderNo)
        {
            OrderNo=orderNo;
        }
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
        public string OrderNo { get; set; }
    }
    public class OrderDetailQueryResponse
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
    public class OrderDetailQueryHandler : IRequestHandler<OrderDetailQuery, OrderDetailQueryResponse>
    {
        private readonly OrderDbContext _context;
        public OrderDetailQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetailQueryResponse> Handle(OrderDetailQuery request, CancellationToken cancellationToken)
        {
            OrderDetailQueryResponse resp = new OrderDetailQueryResponse();

            var query = from o in _context.Order
                        where !o.IsDeleted && o.OrderNo==request.OrderNo 
                        && o.StoreId==request.StoreId 
                        && o.TenantId==request.TenantId
                        select new OrderDetailQueryResponse()
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
            var order = await query.FirstOrDefaultAsync();
            if (order!=null)
            {
                resp = order;
            }
            return resp;
        }
    }
}
