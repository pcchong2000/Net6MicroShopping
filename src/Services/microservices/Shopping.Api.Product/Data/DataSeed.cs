using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Models;
using Shopping.Framework.EFCore;

namespace Shopping.Api.Product.Data
{
    public class DataSeed : IDataSeed
    {
        public ProductDbContext _context;
        public DataSeed(ProductDbContext context)
        {
            _context = context;
        }

        public async Task Init()
        {
            if (await _context.ProductCategory.AnyAsync())
            {
                return;
            }
            List<ProductCategory> list = new List<ProductCategory>() {
                new ProductCategory()
                {
                    Code = "shuma",
                    Name = "数码",
                    Sort = 1,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Code = "erji",
                            Name = "耳机",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Code = "xiangji",
                            Name = "相机",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
                new ProductCategory()
                {
                    Code = "shouji",
                    Name = "手机",
                    Sort = 2,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Code = "apple",
                            Name = "苹果",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Code = "xiaomi",
                            Name = "小米",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
                new ProductCategory()
                {
                    Code = "nanzhuang",
                    Name = "男装",
                    Sort = 3,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Code = "chenshan",
                            Name = "衬衫",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Code = "txu",
                            Name = "T恤",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
                new ProductCategory()
                {
                    Code = "nvzhuang",
                    Name = "女装",
                    Sort = 4,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Code = "lianyiqun",
                            Name = "连衣裙",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Code = "weiyi",
                            Name = "卫衣",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
            };

            var productCategories = new List<ProductCategory>();
            foreach (var item in list)
            {
                productCategories.Add(item);
                foreach (var item1 in item.Categories)
                {
                    item1.Code = item.Code + item1.Code;
                    item1.ParentId = item.Id;
                    productCategories.Add(item1);
                }
            }

            await _context.ProductCategory.AddRangeAsync(productCategories);

            List<StoreProductCategory> storeCategories = new List<StoreProductCategory>() {
                new StoreProductCategory(){
                Name="手机",
                Code="phone",
                Sort=1,
                Description="手机",
                TenantId="3a00a01f-8a3b-9d59-a59c-281e8bb589bf",
                StoreId="4a00a01f-8a3b-9d59-a59c-281e8bb589gf",
                CreatorId="",
                },
            };
            List<Shopping.Api.Product.Models.Product> products = new List<Shopping.Api.Product.Models.Product>() {
                new Shopping.Api.Product.Models.Product(){
                    Name="phone 10",
                    Code="phone10",
                    Sort=1,
                    Description="phone 10",
                    TenantId="3a00a01f-8a3b-9d59-a59c-281e8bb589bf",
                    StoreId="4a00a01f-8a3b-9d59-a59c-281e8bb589gf",
                    ProductCategoryId=productCategories[4].Id,
                    StoreProductCategoryId=storeCategories[0].Id,
                    Status= ProductStatus.UpShelf,
                    Price=(decimal)199.00,
                    StoreName="初始商户门店",
                    CreatorId="",
                },
                new Shopping.Api.Product.Models.Product(){
                    Name="phone 11",
                    Code="phone11",
                    Sort=1,
                    Description="phone 11",
                    TenantId="3a00a01f-8a3b-9d59-a59c-281e8bb589bf",
                    StoreId="4a00a01f-8a3b-9d59-a59c-281e8bb589gf",
                    ProductCategoryId=productCategories[4].Id,
                    StoreProductCategoryId=storeCategories[0].Id,
                    Status= ProductStatus.UpShelf,
                    Price=(decimal)199.00,
                    StoreName="初始商户门店",
                    CreatorId="",
                },
                new Shopping.Api.Product.Models.Product(){
                    Name="phone 12",
                    Code="phone12",
                    Sort=1,
                    Description="phone 12",
                    TenantId="3a00a01f-8a3b-9d59-a59c-281e8bb589bf",
                    StoreId="4a00a01f-8a3b-9d59-a59c-281e8bb589gf",
                    ProductCategoryId=productCategories[4].Id,
                    StoreProductCategoryId=storeCategories[0].Id,
                    Status= ProductStatus.UpShelf,
                    Price=(decimal)199.00,
                    StoreName="初始商户门店",
                    CreatorId="",
                },
                new Shopping.Api.Product.Models.Product(){
                    Name="phone 13",
                    Code="phone13",
                    Sort=1,
                    Description="phone 13",
                    TenantId="3a00a01f-8a3b-9d59-a59c-281e8bb589bf",
                    StoreId="4a00a01f-8a3b-9d59-a59c-281e8bb589gf",
                    ProductCategoryId=productCategories[4].Id,
                    StoreProductCategoryId=storeCategories[0].Id,
                    Status= ProductStatus.UpShelf,
                    Price=(decimal)199.00,
                    StoreName="初始商户门店",
                    CreatorId="",
                },
            };
            await _context.StoreProductCategory.AddRangeAsync(storeCategories);
            await _context.Product.AddRangeAsync(products);

            await _context.SaveChangesAsync();
        }
    }
}
