using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityTenant.Data;
using Shopping.Api.IdentityTenant.Models;
using Shopping.Framework.Domain.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityTenant.TenantApplications.TenantStores
{
    public class StoreListQuery : RequestPageBase, IRequest<StoreListResponse>
    {
        public string TenantId { get; set; }
    }
    public class StoreListResponse : ResponsePageBase<StoreListItemResponse>
    {

    }
    public class StoreListItemResponse
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string StoreCode { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public TenantStoreStatus Status { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class StoreListQueryHandler : IRequestHandler<StoreListQuery, StoreListResponse>
    {
        private readonly TenantDbContext _context;
        public StoreListQueryHandler(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<StoreListResponse> Handle(StoreListQuery request, CancellationToken cancellationToken)
        {
            StoreListResponse resp = new StoreListResponse();
            resp.List = await _context.TenantStore.Where(a => a.TenantId == request.TenantId).Select(a => new StoreListItemResponse()
            {
                Id = a.Id,
                CreateTime = a.CreateTime,
                CreatorId = a.CreatorId,
                CreatorName = a.CreatorName,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                Status = a.Status,
                StoreCode = a.StoreCode,
                TenantId = a.TenantId,
            }).ToListAsync();

            return resp;
        }
    }
}
