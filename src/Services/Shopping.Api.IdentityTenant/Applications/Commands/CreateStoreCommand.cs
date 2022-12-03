using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityTenant.Data;
using Shopping.Api.IdentityTenant.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityTenant.Applications.Commands
{
    public class CreateStoreCommand : IRequest<CreateStoreResponse>
    {
        public string CreatorId { get; set; }
        public string TenantId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string StoreCode { get; set; }

    }
    public class CreateStoreResponse
    {
        public string Id { get; set; }
    }
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, CreateStoreResponse>
    {
        private readonly TenantDbContext _context;
        public CreateStoreCommandHandler(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<CreateStoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            CreateStoreResponse resp = new CreateStoreResponse();
            if (await _context.TenantStore.AnyAsync(a => a.TenantId == request.TenantId && a.StoreCode == request.StoreCode))
            {
                return resp;
            }

            var store = new TenantStore()
            {
                Name = request.StoreName,
                StoreCode = request.StoreCode,
                Description = request.StoreDescription,
                Status = TenantStoreStatus.Apply,
                CreatorId = request.CreatorId
            };
            await _context.TenantStore.AddAsync(store);
            await _context.SaveChangesAsync();
            resp.Id = store.Id;
            return resp;
        }
    }
}
