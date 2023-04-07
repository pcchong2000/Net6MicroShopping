using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Order.Data;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.MemberApplications.Orders
{
    public class MembersOrderDetailQuery : IRequest<MembersOrderDetailQueryResponse>
    {
        public MembersOrderDetailQuery(string orderNo)
        {
            OrderNo = orderNo;
        }
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
        public string OrderNo { get; set; }
    }
    public class MembersOrderDetailQueryResponse
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
    public class MembersOrderDetailQueryHandler : IRequestHandler<MembersOrderDetailQuery, MembersOrderDetailQueryResponse>
    {
        private readonly OrderDbContext _context;
        public MembersOrderDetailQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<MembersOrderDetailQueryResponse> Handle(MembersOrderDetailQuery request, CancellationToken cancellationToken)
        {
            MembersOrderDetailQueryResponse resp = new MembersOrderDetailQueryResponse();

            var query = from o in _context.Order
                        where !o.IsDeleted && o.OrderNo == request.OrderNo
                        && o.StoreId == request.StoreId
                        && o.TenantId == request.TenantId
                        select new MembersOrderDetailQueryResponse()
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
            var order = await query.FirstOrDefaultAsync();
            if (order != null)
            {
                resp = order;
            }
            return resp;
        }
    }
}
