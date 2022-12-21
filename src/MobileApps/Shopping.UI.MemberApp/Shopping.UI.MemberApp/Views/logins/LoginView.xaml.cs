using IdentityModel;
using IdentityModel.Client;
using Shopping.UI.MemberApp.Commons;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Shopping.UI.MemberApp;

[QueryProperty(nameof(Action), "action")]
[QueryProperty(nameof(QRCodeLink), "qrcode")]
public partial class LoginView : ContentPage
{
    public string Action { get; set; }
    public string QRCodeLink { get; set; }
    private readonly HttpClientService _httpClient;
    private readonly IAccountService _accountService;
    private string _codeVerifier;
    private string _launcherUrl;

    public LoginView(LoginViewModel vm, HttpClientService httpClientService, IAccountService accountService)
	{
		InitializeComponent();
        
        //BindingContext = vm;
        _httpClient= httpClientService;
        _accountService = accountService;

    }
    protected override async void OnAppearing()
    {
        App.InitAccessToken();
        if (Action == "logout")
        {
            LogoutWebview();
        }
        else if (Action == "qrcodelogin")
        {
            QRCodeLoginWebview();
        }
        else
        {
            // 登录
            if (!IAccountService.CurrentAccount.IsLogin)
            {
                LoginWebview();
            }
            else  // 刷新token和 cookie
            {
                bool isCookie = true;
                if (IAccountService.CurrentAccount.IsExpired)
                {
                    isCookie = await RefreshToken();
                }
                if (isCookie)
                {
                    RefreshCookieWebview();
                    
                }
                else
                {
                    LoginWebview();
                }
            }
        }
    }

    #region 触发事件
    private void LoginWebview()
    {
        GetCode(Appsettings.ClientId, Appsettings.ClientSecret, "openid profile orderapi memberapi productapi offline_access", null);
    }
    public void GetCode(string clientId, string clientSecret, string scope, string returnUrl)
    {
        var dic = new Dictionary<string, string>();
        dic.Add("client_id", clientId);
        dic.Add("client_secret", clientSecret);
        dic.Add("response_type", "code");
        dic.Add("scope", scope);
        dic.Add("redirect_uri", Appsettings.ClientCallback);
        dic.Add("nonce", Guid.NewGuid().ToString("N"));
        dic.Add("state", Guid.NewGuid().ToString("N"));
        dic.Add("code_challenge", CreateCodeChallenge());
        dic.Add("code_challenge_method", "S256");

        var queryString = string.Join("&", dic.Select(kvp => string.Format("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value))).ToArray());
        string IdentityAuthorizeEndpoint = string.Format("{0}?{1}", Appsettings.IdentityAuthorizeEndpoint, queryString);

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            WebViewNavigating(IdentityAuthorizeEndpoint, WebViewLoginNavigating);
        }
        else
        {
            this._launcherUrl = returnUrl;
            WebViewNavigating(IdentityAuthorizeEndpoint, WebViewGetCodeNavigating);
        }
    }
    private void RefreshCookieWebview()
    {
        string source = Appsettings.IdentityRefreshCookie + "?access_token=" + IAccountService.CurrentAccount.AccessToken;

        WebViewNavigating(source, WebViewRefreshCookieNavigating);
    }
    private void QRCodeLoginWebview()
    {
        //var unescapedUrl = System.Net.WebUtility.UrlEncode(QRCodeLink);
        var qrcode = UrlHelper.UrlGetParam(QRCodeLink, "qrcode");
        string source = Appsettings.IdentityQRCodeLoginConfirm + "?qrcode=" + qrcode;

        WebViewNavigating(source, WebViewQRCodeloginNavigating);
    }
    public void LogoutWebview()
    {
        //webView.Cookie.Clear();

        string source = Appsettings.IdentityLogout;
        WebViewNavigating(source, WebViewLogoutNavigating);
    }
    #endregion

    #region WebViewNavigating
    private async void WebViewLoginNavigating(object sender, WebNavigatingEventArgs e)
    {
        var unescapedUrl = System.Net.WebUtility.UrlDecode(e.Url);
        if (unescapedUrl.StartsWith(Appsettings.ClientCallback))
        {
            webView.HeightRequest = 0;
            var authResponse = new AuthorizeResponse(unescapedUrl);
            var resp = await GetTokenAsync(authResponse.Code);

            await _accountService.SaveToken(resp);

            await Shell.Current.GoToAsync("..");
        }
    }
    private async void WebViewRefreshCookieNavigating(object sender, WebNavigatingEventArgs e)
    {
        var unescapedUrl = System.Net.WebUtility.UrlDecode(e.Url);
        if (unescapedUrl.StartsWith(Appsettings.ClientCallback))
        {
            webView.HeightRequest = 0;

            await Shell.Current.GoToAsync(nameof(MyIndexView));
        }
    }
    private async void WebViewQRCodeloginNavigating(object sender, WebNavigatingEventArgs e)
    {
        var unescapedUrl = System.Net.WebUtility.UrlDecode(e.Url);
        if (unescapedUrl.StartsWith(Appsettings.IdentityQRCodeLoginConfirmCallBack))
        {
            webView.HeightRequest = 0;

            await Shell.Current.GoToAsync(nameof(MyIndexView));
        }
    }
    private async void WebViewGetCodeNavigating(object sender, WebNavigatingEventArgs e)
    {
        var unescapedUrl = System.Net.WebUtility.UrlDecode(e.Url);
        if (unescapedUrl.StartsWith(Appsettings.ClientCallback))
        {
            webView.HeightRequest = 0;
            var authResponse = new AuthorizeResponse(unescapedUrl);
            //第三方应用打开回调
            bool supportsUri = await Launcher.Default.CanOpenAsync(_launcherUrl);
            if (supportsUri)
            {
                await Launcher.Default.OpenAsync($"{_launcherUrl}?code={authResponse.Code}&codeVerifier={_codeVerifier}");
            }
            await Shell.Current.GoToAsync("..");
        }
    }
    private void WebViewLogoutNavigating(object sender, WebNavigatingEventArgs e)
    {
        var unescapedUrl = System.Net.WebUtility.UrlDecode(e.Url);
        if (unescapedUrl.Equals(Appsettings.IdentityLogoutCallBack))
        {
            Action = "";
            var shell = (AppShell)Shell.Current;
            shell.GotoHome();
            
            //await Shell.Current.GoToAsync(nameof(HomeView));
        }
    }
    public void WebViewNavigating(string source, EventHandler<WebNavigatingEventArgs> weBViewEvent)
    {
        webView.Navigating -= WebViewRefreshCookieNavigating;
        webView.Navigating -= WebViewLoginNavigating;
        webView.Navigating -= WebViewGetCodeNavigating;
        webView.Navigating -= WebViewLogoutNavigating;
        webView.Navigating -= WebViewQRCodeloginNavigating;

        webView.Navigating += weBViewEvent;
        webView.Source = source;
    }
    #endregion

    #region api http 
    private async Task<bool> RefreshToken()
    {
        var token = await _httpClient.PostFormUrlEncodedAsync<LoginResponseModel>(Appsettings.IdentityTokenEndpoint, new Dictionary<string, string>() {
            {"grant_type","refresh_token" },
            {"client_id",Appsettings.ClientId },
            {"client_secret",Appsettings.ClientSecret },
            {"refresh_token",IAccountService.CurrentAccount.RefreshToken },
        });
        if (token == null)
        {
            return false;
        }
        else
        {
            await _accountService.SaveToken(token);
            return true;
        }
    }
    private async Task<LoginResponseModel> GetTokenAsync(string code)
    {
        Dictionary<string, string> data = new Dictionary<string, string>() {
                {"grant_type","authorization_code" },
                {"client_id",Appsettings.ClientId },
                {"client_secret",Appsettings.ClientSecret },
                {"code",code },
                {"redirect_uri",Appsettings.ClientCallback },
                {"code_verifier",_codeVerifier },
            };
        var token = await _httpClient.PostFormUrlEncodedAsync<LoginResponseModel>(Appsettings.IdentityTokenEndpoint, data);
        return token;
    }
    #endregion

    private string CreateCodeChallenge()
    {
        var _codeVerifierBytes = RandomNumberGenerator.GetBytes(64);
        _codeVerifier = CommonHelper.ByteArrayToString(_codeVerifierBytes);

        using (var sha = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(_codeVerifier);
            var hash = sha.ComputeHash(bytes);
            var resp = Base64Url.Encode(hash);
            return resp;
        }
    }
}
