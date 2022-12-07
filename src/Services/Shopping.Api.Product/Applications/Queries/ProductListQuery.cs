﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.Applications.Queries
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
        public string? ProductCategoryName { get; set; }
        public string? StoreProductCategoryName { get; set; }
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

            var query = from p in _context.Product
                        join pc in _context.ProductCategory on p.ProductCategoryId equals pc.Id into pc1
                        from pct in pc1.DefaultIfEmpty()
                        join spc in _context.StoreProductCategory on p.StoreProductCategoryId equals spc.Id into spc1
                        from spct in spc1.DefaultIfEmpty()
                        select new ProductListItemResponse()
                        {
                            Id = p.Id,
                            CreateTime = p.CreateTime,
                            CreatorId = p.CreatorId,
                            CreatorName = p.CreatorName,
                            Description = p.Description,
                            ImageUrl = p.ImageUrl,
                            Name = p.Name,
                            TenantId = p.TenantId,
                            Code = p.Code,
                            Price = p.Price,
                            ProductCategoryId = p.ProductCategoryId,
                            Sort = p.Sort,
                            Status = p.Status,
                            StoreId = p.StoreId,
                            StoreProductCategoryId = p.StoreProductCategoryId,
                            StoreName = p.StoreName,
                            ProductCategoryName = pct.Name,
                            StoreProductCategoryName = spct.Name,
                        };

            resp.List = await query.OrderByDescending(a => a.Sort).PageList(request).ToListAsync();

            return resp;
        }
    }
}
