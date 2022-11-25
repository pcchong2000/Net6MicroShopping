using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.OrderServices
{
    public class OrderListItemResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImageUrl { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 1 图片 2 视频
        /// </summary>
        public int Type { get; set; }
        public string ImageUrls { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreateTime { get; set; }
        public int Follow { get; set; }
        public string AccountName { get; set; }
        public string AccountAvatarUrl { get; set; }

    }
    public class OrderListRequestModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
