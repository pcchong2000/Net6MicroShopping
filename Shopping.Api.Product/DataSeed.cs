using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Models;
using Shopping.Framework.Web;

namespace Shopping.Api.Product
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
            List<ProductCategory> productCategories = new List<ProductCategory>() {
                new ProductCategory()
                {
                    Name = "数码",
                    Sort = 1,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Name = "耳机",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Name = "相机",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
                new ProductCategory()
                {
                    Name = "手机",
                    Sort = 2,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Name = "苹果",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Name = "小米",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
                new ProductCategory()
                {
                    Name = "男装",
                    Sort = 3,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Name = "衬衫",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Name = "T恤",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
                new ProductCategory()
                {
                    Name = "女装",
                    Sort = 4,
                    Description = "",
                    Categories = new List<ProductCategory>(){
                        new ProductCategory()
                        {
                            Name = "连衣裙",
                            Sort = 1,
                            Description = "",
                        },
                        new ProductCategory()
                        {
                            Name = "卫衣",
                            Sort = 2,
                            Description = "",
                        },
                    }
                },
            };

            var list = new List<ProductCategory>();
            foreach (var item in productCategories)
            {
                if (!await _context.ProductCategory.AnyAsync(a=>a.Name==item.Name))
                {
                    list.Add(item);
                    foreach (var item1 in item.Categories)
                    {
                        item1.ParentId = item.Id;
                    }
                }
            }

            await _context.ProductCategory.AddRangeAsync(list);

            await _context.SaveChangesAsync();
        }
    }
}
