﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.Domain.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.Applications.Queries
{
    public class ProductListInQuery : IRequest<List<ProductListInQueryItemResponse>>
    {
        public ProductListInQuery(List<string> productIds)
        {
            ProductIds = productIds;
        }
        public List<string> ProductIds { get; set; }
        public string StoreId { get; set; }
    }

    public class ProductListInQueryItemResponse
    {
        public string? Id { get; set; }
        public string? TenantId { get; set; }
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }

        public List<StoreProductModelDto> ProductModels { get; set; }
    }
    public class StoreProductModelDto
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? Value { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
    }
    public class ProductListInQueryHandler : IRequestHandler<ProductListInQuery, List<ProductListInQueryItemResponse>>
    {
        private readonly ProductDbContext _context;
        public ProductListInQueryHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductListInQueryItemResponse>> Handle(ProductListInQuery request, CancellationToken cancellationToken)
        {
            List<ProductListInQueryItemResponse> resp = new List<ProductListInQueryItemResponse>();

            var query = from p in _context.Product
                        join pc in _context.ProductCategory on p.ProductCategoryId equals pc.Id into pc1
                        from pct in pc1.DefaultIfEmpty()
                        where request.ProductIds.Contains(p.Id) && p.StoreId == request.StoreId
                        select new ProductListInQueryItemResponse()
                        {
                            Id = p.Id,
                            ImageUrl = p.ImageUrl,
                            Name = p.Name,
                            TenantId = p.TenantId,
                            Code = p.Code,
                            Price = p.Price,
                            Status = p.Status,
                            StoreId = p.StoreId,
                            StoreName = p.StoreName,
                            ProductModels = (from pm in _context.StoreProductModel
                                            where pm.ProductId == p.Id
                                            select new StoreProductModelDto
                                            {
                                                Id = pm.Id,
                                                Number = pm.Number,
                                                Price = pm.Price,
                                                ProductId = pm.ProductId,
                                                Value = pm.Value,
                                            }).ToList()
                        };

            resp = await query.ToListAsync();

            return resp;
        }
    }
}
