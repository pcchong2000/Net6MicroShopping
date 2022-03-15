using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.Applications.Commands
{
    public class ProductDeleteTenantCommand : IRequest<ResponseBase>
    {
        public string ProductId { get; set; }
    }
    public class ProductDeleteTenantCommandHandler : IRequestHandler<ProductDeleteTenantCommand, ResponseBase>
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
        public async Task<ResponseBase> Handle(ProductDeleteTenantCommand request, CancellationToken cancellationToken)
        {
            ResponseBase resp = new ResponseBase()
            {
                Code = ResponseBaseCode.Success
            };
            try
            {
                var product = await _context.Product.Where(a => a.Id == request.ProductId).FirstOrDefaultAsync();
                if (product != null)
                {
                    product.IsDeleted = true;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_currentUser.Name + ex.ToString());
                resp.Code = ResponseBaseCode.Fail;
            }
            
            return resp;
        }
    }
}
