using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.Domain.Base;

namespace Shopping.Api.Product.Applications.Commands
{
    public class ProductAddCommand : IRequest<ProductAddResponse>
    {
        public string TenantId { get; set; }
        public string StoreId { get; set; }
        public string ProductCategoryId { get; set; }
        public string StoreProductCategoryId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public string? CreatorId { get; set; }
        public string? CreatorName { get; set; }
        public ProductStatus Status { get; set; }
        public List<ProductAddModelCategory> StoreProductModelCategoryList { get; set; }
        public List<ProductAddModel> StoreProductModelList { get; set; }
    }
    public class ProductAddModelCategory
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Items { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
    }
    public class ProductAddModel
    {
        public string? Value { get; set; }
        public int Number { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
    public class ProductAddResponse : RequestBase
    {
        public string? Id { get; set; }
    }
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, ProductAddResponse>
    {
        private readonly ProductDbContext _context;
        public ProductAddCommandHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ProductAddResponse> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            ProductAddResponse resp = new ProductAddResponse();
            if (await _context.Product.AnyAsync(a => a.TenantId == request.TenantId && a.Code == request.Code))
            {

                return resp;
            }
            var product = new Models.Product()
            {
                Code = request.Code,
                CreatorId = request.CreatorId,
                CreatorName = request.CreatorName,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Name = request.Name,
                Price = request.Price,
                ProductCategoryId = request.ProductCategoryId,
                Status = request.Status,
                Sort = request.Sort,
                StoreId = request.StoreId,
                StoreProductCategoryId = request.StoreProductCategoryId,
                TenantId = request.TenantId,

            };
            var productModels = new List<StoreProductModel>();
            var categorys = new List<StoreProductModelCategory>();
            request.StoreProductModelCategoryList.ForEach(a =>
            {
                var category = new StoreProductModelCategory()
                {
                    CreatorId = product.CreatorId,
                    CreatorName = request.CreatorName,
                    ProductId = product.Id,
                    StoreId = request.StoreId,
                    TenantId = request.TenantId,
                    Name = a.Name,
                    Sort = a.Sort,
                    Description = a.Description,
                    Code = a.Code,
                    Items = a.Items,
                };
                categorys.Add(category);
                


            });

            productModels.AddRange(request.StoreProductModelList.Select(b => new StoreProductModel()
            {
                CreatorId = product.CreatorId,
                CreatorName = request.CreatorName,
                ProductId = product.Id,
                StoreId = request.StoreId,
                TenantId = request.TenantId,
                Sort = b.Sort,
                Description = b.Description,
                Number = b.Number,
                Price = b.Price,
                Value = b.Value,

            }));

            await _context.Product.AddAsync(product);
            await _context.StoreProductModelCategory.AddRangeAsync(categorys);
            await _context.StoreProductModel.AddRangeAsync(productModels);
            await _context.SaveChangesAsync();
            resp.Id = product.Id;
            return resp;


        }
    }
}
