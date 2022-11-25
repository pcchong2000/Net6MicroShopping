using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Configs
{
    public static class Appsettings
    {
        public static string TenantId = "3a00a01f-8a3b-9d59-a59c-281e8bb589bf";
        public static string IPAddress = "192.168.1.100";
        //public static string BaseAddress =    DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.238:5251" : "http://192.168.1.238:5251";


        #region 登录接口
        public static string IdentityAddress = $"http://{IPAddress}:5101";

        public static string ClientPasswordId = "membermauipassword";
        public static string ClientId = "membermaui";
        public static string ClientSecret = "secret";
        public static string ClientCallback = IdentityAddress+"/membermauicallback";

        
        public static string IdentityAuthorizeEndpoint = IdentityAddress + "/connect/authorize";
        public static string IdentityTokenEndpoint = IdentityAddress + "/connect/token";
        public static string IdentityAuthUrl = IdentityAddress + "/account/auth?scheme=";
        public static string TestUrl = IdentityAddress + "/auth/test";
        #endregion


        public static string ApiBaseAddress = $"http://{IPAddress}:5200";


        #region product Api
        public static string ProductApiBase = ApiBaseAddress + "/api/product";

        
        public static string ProductCategoryList = ProductApiBase + "/ProductCategory";
        public static string ProductList = ProductApiBase + "/Product";
        public static string ProductDetail = ProductApiBase + "/Product/detail";
        public static string ProductHome = ProductApiBase + "/Product/Home";
        #endregion


        #region order Api
        public static string OrderApiBase = ApiBaseAddress + "/api/order";

        public static string OrderSubmmit = OrderApiBase + "/order";
        public static string OrderList = OrderApiBase + "/order";
        public static string OrderDetail = OrderApiBase + "/order/detail";
        #endregion


        #region account Api
        public static string AccountApiBase = ApiBaseAddress + "/api/member";

        public static string AccountMyInfo = AccountApiBase + "/member/myinfo";
        public static string AccountUpdateAvatar = AccountApiBase + "/member/updateAvatar";
        #endregion
    }
}
