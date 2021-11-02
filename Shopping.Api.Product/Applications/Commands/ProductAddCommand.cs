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
        public string Code { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public string? CreatorId { get; set; }
        public string? CreatorName { get; set; }
        public ProductStatus Status { get; set; }
        public List<ProductAddModelCategory> ModelCategoryList { get; set; }
    }
    public class ProductAddModelCategory
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public List<ProductAddModel> ModelList { get; set; }
    }
    public class ProductAddModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
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
            var productModels = new List<ProductModel>();
            var categorys = new List<ProductModelCategory>();
            request.ModelCategoryList.ForEach(a =>
            {
                var category = new ProductModelCategory()
                {
                    CreatorId = product.CreatorId,
                    CreatorName = request.CreatorName,
                    ProductId = product.Id,
                    StoreId = request.StoreId,
                    TenantId = request.TenantId,
                    Name = a.Name,
                    Sort = a.Sort,
                    Description = a.Description,
                };
                categorys.Add(category);
                productModels.AddRange(a.ModelList.Select(b => new ProductModel()
                {
                    CreatorId = category.CreatorId,
                    CreatorName = request.CreatorName,
                    ProductId = product.Id,
                    StoreId = request.StoreId,
                    TenantId = request.TenantId,
                    Name = b.Name,
                    Sort = b.Sort,
                    ProductModelCategoryId = category.Id,
                    Description = b.Description,

                }));


            });
            await _context.Product.AddAsync(product);
            await _context.ProductModelCategory.AddRangeAsync(categorys);
            await _context.ProductModel.AddRangeAsync(productModels);
            await _context.SaveChangesAsync();
            resp.Id = product.Id;
            return resp;


        }
    }
}
