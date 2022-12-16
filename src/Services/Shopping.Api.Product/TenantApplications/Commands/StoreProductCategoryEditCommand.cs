using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.TenantApplications.Commands
{
    public class StoreProductCategoryEditCommand : IRequest<string>
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public string StoreId { get; set; }
    }
    public class StoreProductCategoryEditCommandHandler : IRequestHandler<StoreProductCategoryEditCommand, string>
    {
        private readonly ProductDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public StoreProductCategoryEditCommandHandler(ProductDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<string> Handle(StoreProductCategoryEditCommand request, CancellationToken cancellationToken)
        {

            if (request.Id == null)
            {
                StoreProductCategory storeProductCategory = new StoreProductCategory()
                {
                    Name = request.Name,
                    Code = request.Code,
                    ImageUrl = request.ImageUrl,
                    Description = request.Description,
                    Sort = request.Sort,
                    StoreId = request.StoreId,
                    CreatorId = _currentUser.Id!,
                    TenantId = _currentUser.TenantId!,
                };

                await _context.StoreProductCategory.AddAsync(storeProductCategory);
                await _context.SaveChangesAsync();

                return storeProductCategory.Id;
            }
            else
            {
                var storeProductCategory = await _context.StoreProductCategory.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (storeProductCategory != null)
                {
                    storeProductCategory.Name = request.Name;
                    storeProductCategory.Code = request.Code;
                    storeProductCategory.Description = request.Description;
                    storeProductCategory.ImageUrl = request.ImageUrl;
                    storeProductCategory.Sort = request.Sort;


                    await _context.SaveChangesAsync();

                }
            }
            return request.Id;
        }
    }
}
