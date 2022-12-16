using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.DomainBase.Base;

namespace Shopping.Api.Product.MemberApplications.Queries
{
    public class ProductDetailQuery : IRequest<ProductDetailQueryResponse>
    {
        public string ProductId { get; set; }
    }
    public class ProductDetailQueryResponse
    {
        public string Id { get; set; }
        public string ProductCategoryId { get; set; }
        public string StoreProductCategoryId { get; set; }
        public string Code { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
    }
    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, ProductDetailQueryResponse>
    {
        private readonly ProductDbContext _context;
        public ProductDetailQueryHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDetailQueryResponse> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
        {
            var query = from a in _context.Product 
                        where a.Id == request.ProductId
                        select new ProductDetailQueryResponse {
                            Id = a.Id,
                            Code = a.Code,
                            Description = a.Description,
                            ImageUrl = a.ImageUrl,
                            StoreId = a.StoreId,
                            StoreName = a.StoreName,
                            Name = a.Name,
                            Price = a.Price,
                            ProductCategoryId = a.ProductCategoryId,
                            Sort = a.Sort,
                            Status = a.Status,
                            StoreProductCategoryId = a.StoreProductCategoryId,
                        };

            var resp = await query.FirstOrDefaultAsync();
            return resp!;
        }
    }
}
