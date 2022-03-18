using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.Applications.Commands
{

    public class ProductInUpdateReserveCommand : IRequest<ResponseBase>
    {
        public ProductInUpdateReserveCommand(List<JianKuCunItemDto> productModels)
        {
            ProductModels = productModels;
        }
        public List<JianKuCunItemDto> ProductModels { get; set; }
    }
    public class JianKuCunItemDto
    {
        public string ProductId { get; set; }
        public string ProductModelId { get; set; }
        public int Number { get; set; }
    }
    public class ProductInUpdateReserveCommandHandler : IRequestHandler<ProductInUpdateReserveCommand, ResponseBase>
    {
        private readonly ProductDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<ProductDeleteTenantCommandHandler> _logger;
        public ProductInUpdateReserveCommandHandler(ProductDbContext context, ICurrentUserService currentUser, ILogger<ProductDeleteTenantCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }
        public async Task<ResponseBase> Handle(ProductInUpdateReserveCommand request, CancellationToken cancellationToken)
        {
            ResponseBase resp = new ResponseBase()
            {
                Code = ResponseBaseCode.Success
            };
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                var modelIds = request.ProductModels.Select(a => a.ProductModelId).ToList();
                var productModels = await _context.StoreProductModel.Where(a => modelIds.Contains(a.Id)).ToListAsync();
                foreach (var item in request.ProductModels)
                {
                    var productModel = productModels.FirstOrDefault(a=>a.ProductId==item.ProductId && a.Id==item.ProductModelId);
                    if (productModel != null)
                    {
                        if (productModel.Number > item.Number)
                        {
                            productModel.Number = productModel.Number - item.Number;
                        }
                        else
                        {
                            throw new Exception("此型号商品库存不足");
                        }
                    }
                    else
                    {
                        throw new Exception("商品型号不存在");
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(_currentUser.Name + ex.ToString());
                resp.Code = ResponseBaseCode.Fail;
            }
            
            return resp;
        }
    }
}
