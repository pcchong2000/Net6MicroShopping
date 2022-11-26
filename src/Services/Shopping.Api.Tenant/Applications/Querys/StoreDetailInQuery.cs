using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Framework.AccountDomain.Entities.Tenants;
using Shopping.Framework.AccountEFCore.Tenants;

namespace Shopping.Api.Tenant.Applications.Querys
{
    public class StoreDetailInQuery : IRequest<StoreDetailInQueryResponse>
    {
        public string? Id { get; set; }
    }

    public class StoreDetailInQueryResponse
    {
        public string? Id { get; set; }
        public string? TenantId { get; set; }
        public string? StoreCode { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public TenantStoreStatus Status { get; set; }
    }
    public class StoreDetailInQueryHandler : IRequestHandler<StoreDetailInQuery, StoreDetailInQueryResponse>
    {
        private readonly TenantDbContext _context;
        public StoreDetailInQueryHandler(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<StoreDetailInQueryResponse> Handle(StoreDetailInQuery request, CancellationToken cancellationToken)
        {
            var resp = await _context.TenantStore.Where(a => a.Id== request.Id).Select(a => new StoreDetailInQueryResponse()
            {
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                Status = a.Status,
                StoreCode = a.StoreCode,
                TenantId = a.TenantId,
            }).FirstOrDefaultAsync();

            return resp??=new StoreDetailInQueryResponse();
        }
    }
}
