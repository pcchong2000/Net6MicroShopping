using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Framework.Domain.Base;

namespace Shopping.Api.Product.MemberApplications.ProductCategorys
{
    public class ProductCategoryListQuery : IRequest<List<ProductCategoryListQueryItemResponse>>
    {
    }

    public class ProductCategoryListQueryItemResponse
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
    }
    public class ProductCategoryListQueryHandler : IRequestHandler<ProductCategoryListQuery, List<ProductCategoryListQueryItemResponse>>
    {
        private readonly ProductDbContext _context;
        public ProductCategoryListQueryHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductCategoryListQueryItemResponse>> Handle(ProductCategoryListQuery request, CancellationToken cancellationToken)
        {
            List<ProductCategoryListQueryItemResponse> resp = new List<ProductCategoryListQueryItemResponse>();

            return await _context.ProductCategory.Select(a => new ProductCategoryListQueryItemResponse()
            {
                Id = a.Id,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                ParentId = a.ParentId,
                Sort = a.Sort,

            }).ToListAsync();

        }
    }
}
