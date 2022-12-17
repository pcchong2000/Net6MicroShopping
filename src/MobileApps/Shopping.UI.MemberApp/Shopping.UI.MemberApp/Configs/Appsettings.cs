using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Configs
{
    public static class Appsettings
    {
        public const string TenantId = "3a00a01f-8a3b-9d59-a59c-281e8bb589bf";
        public const string IPAddress = "192.168.1.100";
        //public const string BaseAddress =    DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.238:5251" : "http://192.168.1.238:5251";


        #region 登录接口
        public const string IdentityAddress = $"http://{IPAddress}:5101";

        public const string ClientPasswordId = "membermauipassword";
        public const string ClientId = "membermaui";
        public const string ClientSecret = "secret";
        public const string ClientCallback = IdentityAddress+"/membermauicallback";

        
        public const string IdentityAuthorizeEndpoint = IdentityAddress + "/connect/authorize";
        public const string IdentityTokenEndpoint = IdentityAddress + "/connect/token";
        public const string IdentityAuthUrl = IdentityAddress + "/account/auth?scheme=";

        

        #endregion


        public const string ApiBaseAddress = $"http://{IPAddress}:5200";


        #region product Api
        public const string ProductApiBase = ApiBaseAddress + "/api/product";

        
        public const string ProductCategoryList = ProductApiBase + "/ProductCategory";
        public const string ProductList = ProductApiBase + "/Product";
        public const string ProductDetail = ProductApiBase + "/Product/detail";
        public const string ProductHome = ProductApiBase + "/Product/Home";
        #endregion


        #region order Api
        public const string OrderApiBase = ApiBaseAddress + "/api/order";

        public const string OrderSubmmit = OrderApiBase + "/order";
        public const string OrderList = OrderApiBase + "/order";
        public const string OrderDetail = OrderApiBase + "/order/detail";
        #endregion


        #region account Api
        public const string AccountApiBase = ApiBaseAddress + "/api/member";
        public const string AccountMyInfo = AccountApiBase + "/member/myinfo";
        public const string AccountUpdate = AccountApiBase + "/member/update";

        public const string IdentityRefreshCookie = AccountApiBase + "/auth/refresh_cookie";
        public const string IdentityLogout = AccountApiBase + "/auth/logout";
        #endregion

        #region oss Api
        public const string OssApiBase = ApiBaseAddress + "/api/oss";

        public const string OssUpLoad = OssApiBase + "/file";
        #endregion
    }
}
