using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.Domain.Base;

namespace Shopping.Api.Product.Applications.Querys
{
    public class ProductListQuery : RequestPageBase, IRequest<ProductListResponse>
    {
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
    }
    public class ProductListResponse : ResponsePageBase<ProductListItemResponse>
    {

    }
    public class ProductListItemResponse
    {
        public string? Id { get; set; }
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? ProductCategoryId { get; set; }
        public string? StoreProductCategoryId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public string? CreatorId { get; set; }
        public string? CreatorName { get; set; }
        public DateTime CreateTime { get; set; }
        public ProductStatus Status { get; set; }
    }
    public class ProductListQueryHandler : IRequestHandler<ProductListQuery, ProductListResponse>
    {
        private readonly ProductDbContext _context;
        public ProductListQueryHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ProductListResponse> Handle(ProductListQuery request, CancellationToken cancellationToken)
        {
            ProductListResponse resp = new ProductListResponse();
            resp.List = await _context.Product.Select(a => new ProductListItemResponse()
            {
                Id = a.Id,
                CreateTime = a.CreatTime,
                CreatorId = a.CreatorId,
                CreatorName = a.CreatorName,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                TenantId = a.TenantId,
                Code = a.Code,
                Price = a.Price,
                ProductCategoryId = a.ProductCategoryId,
                Sort = a.Sort,
                Status = a.Status,
                StoreId = a.StoreId,
                StoreProductCategoryId = a.StoreProductCategoryId,
                StoreName = a.StoreName,
            }).ToListAsync();

            return resp;
        }
    }
}
