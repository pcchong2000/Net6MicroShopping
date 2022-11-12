using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Configs
{
    public static class Appsettings
    {
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


        #region API访问接口
        public static string BaseAddress = $"http://{IPAddress}:5200";
        public static string MyInfoUrl = BaseAddress + "/account/myinfo";
        public static string UpdateAvatar = BaseAddress + "/account/updateAvatar";
        public static string BlogListUrl = BaseAddress + "/blog";
        public static string BlogInfoUrl = BaseAddress + "/blog/";

        #endregion
    }
}
