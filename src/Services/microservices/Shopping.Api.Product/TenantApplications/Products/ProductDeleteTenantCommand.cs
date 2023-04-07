using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.TenantApplications.Products
{
    public class ProductDeleteTenantCommand : IRequest<ProductDeleteTenantResponse>
    {
        public string ProductId { get; set; }
    }
    public class ProductDeleteTenantResponse
    {
        public bool IsOk { get; set; }
    }
    public class ProductDeleteTenantCommandHandler : IRequestHandler<ProductDeleteTenantCommand, ProductDeleteTenantResponse>
    {
        private readonly ProductDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<ProductDeleteTenantCommandHandler> _logger;
        public ProductDeleteTenantCommandHandler(ProductDbContext context, ICurrentUserService currentUser, ILogger<ProductDeleteTenantCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }
        public async Task<ProductDeleteTenantResponse> Handle(ProductDeleteTenantCommand request, CancellationToken cancellationToken)
        {
            ProductDeleteTenantResponse resp = new ProductDeleteTenantResponse();

            var product = await _context.Product.Where(a => a.Id == request.ProductId).FirstOrDefaultAsync();
            if (product != null)
            {
                product.IsDeleted = true;
            }
            resp.IsOk = true;
            await _context.SaveChangesAsync();

            return resp;
        }
    }
}
