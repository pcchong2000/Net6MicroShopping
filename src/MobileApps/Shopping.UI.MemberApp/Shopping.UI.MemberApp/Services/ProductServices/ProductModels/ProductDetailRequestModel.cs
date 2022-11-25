using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.ProductServices.ProductModels
{
    public class ProductDetailRequestModel: RequestBaseModel
    {
        public string Id { get; set; }
    }
    public class ProductDetailResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImageUrl { get; set; }
        public string Body { get; set; }
        /// <summary>
        /// 1 图片 2 视频
        /// </summary>
        public int Type { get; set; }
        public List<string> ImageUrls { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
    

}
