using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.AccountServices
{
    public class AccountListItemResponseModel
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
    public class AccountListRequestModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class AccountNextRequestModel
    {
        public string CurrentId { get; set; }
        /// <summary>
        /// 1 上一个  2 下一个
        /// </summary>
        public int Action { get; set; }
    }
    public class AccountInfo
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredTime { get; set; }
    }

    public class LoginResponseModel
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
    public class AccountInfoResponseModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime? BirthdayTime { get; set; }
        public string? Phone { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public DateTime CreateTime { get; set; }
    }
}
