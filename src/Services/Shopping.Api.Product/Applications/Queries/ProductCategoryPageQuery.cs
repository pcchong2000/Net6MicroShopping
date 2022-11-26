using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Framework.DomainBase.Base;

namespace Shopping.Api.Product.Applications.Queries
{
    public class ProductCategoryPageQuery : RequestPageBase, IRequest<ResponsePageBase<ProductCategoryPageQueryItemResponse>>
    {
    }

    public class ProductCategoryPageQueryItemResponse
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
    }
    public class ProductCategoryPageQueryHandler : IRequestHandler<ProductCategoryPageQuery, ResponsePageBase<ProductCategoryPageQueryItemResponse>>
    {
        private readonly ProductDbContext _context;
        public ProductCategoryPageQueryHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<ResponsePageBase<ProductCategoryPageQueryItemResponse>> Handle(ProductCategoryPageQuery request, CancellationToken cancellationToken)
        {
            ResponsePageBase<ProductCategoryPageQueryItemResponse> resp = new ResponsePageBase<ProductCategoryPageQueryItemResponse>(request);

            resp.List = await _context.ProductCategory.Select(a => new ProductCategoryPageQueryItemResponse()
            {
                Id = a.Id,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                ParentId = a.ParentId,
                Sort = a.Sort,

            }).ToListAsync();

            return resp;
        }
    }
}
