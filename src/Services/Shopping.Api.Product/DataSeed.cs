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

            var list = new List<ProductCategory>();
            foreach (var item in productCategories)
            {
                if (!await _context.ProductCategory.AnyAsync(a=>a.Name==item.Name))
                {
                    list.Add(item);
                    foreach (var item1 in item.Categories)
                    {
                        item1.Code = item.Code+item1.Code;
                        item1.ParentId = item.Id;
                        list.Add(item1);
                    }
                }
            }

            await _context.ProductCategory.AddRangeAsync(list);

            await _context.SaveChangesAsync();
        }
    }
}
