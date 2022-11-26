using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.Applications.Commands
{
    public class ProductEditCommand : IRequest<ProductAddResponse>
    {
        public string? Id { get; set; }
        public string StoreId { get; set; }
        public string? StoreName { get; set; }
        public string ProductCategoryId { get; set; }
        public string StoreProductCategoryId { get; set; }
        public string Code { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public List<ProductAddModelCategory> StoreProductModelCategoryList { get; set; }
        public List<ProductAddModel> StoreProductModelList { get; set; }
    }
    public class ProductAddModelCategory
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Items { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }
    }
    public class ProductAddModel
    {
        public string? Id { get; set; }
        public string? Value { get; set; }
        public int Number { get; set; }
        public int Sort { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
    public class ProductAddResponse
    {
        public string? Id { get; set; }
    }
    public class ProductAddCommandHandler : IRequestHandler<ProductEditCommand, ProductAddResponse>
    {
        private readonly ProductDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public ProductAddCommandHandler(ProductDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ProductAddResponse> Handle(ProductEditCommand request, CancellationToken cancellationToken)
        {
            ProductAddResponse resp = new ProductAddResponse();
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                if (await _context.Product.AnyAsync(a => a.TenantId == _currentUser.TenantId! && a.Code == request.Code))
                {

                    return resp;
                }
                var product = new Models.Product()
                {
                    Code = request.Code,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    Name = request.Name,
                    Price = request.Price,
                    ProductCategoryId = request.ProductCategoryId!,
                    Status = request.Status,
                    Sort = request.Sort,
                    StoreId = request.StoreId!,
                    StoreProductCategoryId = request.StoreProductCategoryId!,
                    TenantId = _currentUser.TenantId!,
                    CreatorId = _currentUser.Id!,
                    CreatorName = _currentUser.Name,
                    StoreName = request.StoreName,
                };
                var productModels = new List<StoreProductModel>();
                var categorys = new List<StoreProductModelCategory>();
                request.StoreProductModelCategoryList.ForEach(a =>
                {
                    var category = new StoreProductModelCategory()
                    {
                        CreatorId = product.CreatorId,
                        ProductId = product.Id,
                        StoreId = request.StoreId,
                        TenantId = _currentUser.TenantId!,
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
                    CreatorId = _currentUser.Id!,
                    CreatorName = _currentUser.Name,
                    ProductId = product.Id,
                    StoreId = request.StoreId,
                    TenantId = _currentUser.TenantId!,
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
            }
            else
            {
                var product = await _context.Product.Where(a=>a.Id==request.Id && a.StoreId==request.StoreId && a.TenantId==_currentUser.TenantId).FirstOrDefaultAsync();
                if (product!=null)
                {
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.ImageUrl = request.ImageUrl;
                    product.Price= request.Price;
                    product.Sort= request.Sort;
                    product.ProductCategoryId= request.ProductCategoryId;
                    product.StoreProductCategoryId= request.StoreProductCategoryId;

                    //清空原有
                    var _categorys = await _context.StoreProductModelCategory.Where(a => a.ProductId == product.Id).ToListAsync();
                    var _productModels = await _context.StoreProductModel.Where(a => a.ProductId == product.Id).ToListAsync();
                    _context.StoreProductModelCategory.RemoveRange(_categorys);
                    _context.StoreProductModel.RemoveRange(_productModels);

                    //添加新的
                    var productModels = new List<StoreProductModel>();
                    var categorys = new List<StoreProductModelCategory>();
                    request.StoreProductModelCategoryList.ForEach(a =>
                    {
                        var category = new StoreProductModelCategory()
                        {
                            CreatorId = product.CreatorId,
                            ProductId = product.Id,
                            StoreId = request.StoreId,
                            TenantId = _currentUser.TenantId!,
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
                        CreatorId = _currentUser.Id!,
                        CreatorName = _currentUser.Name,
                        ProductId = product.Id,
                        StoreId = request.StoreId,
                        TenantId = _currentUser.TenantId!,
                        Sort = b.Sort,
                        Description = b.Description,
                        Number = b.Number,
                        Price = b.Price,
                        Value = b.Value,

                    }));

                    await _context.StoreProductModelCategory.AddRangeAsync(categorys);
                    await _context.StoreProductModel.AddRangeAsync(productModels);

                    await _context.SaveChangesAsync();
                }
            }
            
            return resp;


        }
    }
}
