using IdentityModel;
using IdentityModel.Client;
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
public partial class LoginView : ContentPage
{
    public string Action { get; set; }
    
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
    private async Task Init()
    {
        
        App.InitAccessToken();
        var token = IAccountService.CurrentAccount.AccessToken;
        var isLogin = IAccountService.CurrentAccount.IsLogin;
        if (Action == "logout")
        {
            Logout();
        }
        else 
        {
            // 登录
            if (!isLogin)
            {
                LoginWebview();
            }
            else  // 刷新token和 cookie
            {
                bool isCookie = false;
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

    private void LoginWebview()
    {
        GetCode(Appsettings.ClientId, Appsettings.ClientSecret, "openid profile orderapi memberapi productapi offline_access",null);
    }
    private string CreateAuthorizeEndpoint(IDictionary<string, string> values)
    {
        var queryString = string.Join("&", values.Select(kvp => string.Format("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value))).ToArray());
        return string.Format("{0}?{1}", Appsettings.IdentityAuthorizeEndpoint, queryString);
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
    private string CreateCodeChallenge()
    {
        var _codeVerifierBytes = RandomNumberGenerator.GetBytes(64);
        _codeVerifier = Common.ByteArrayToString(_codeVerifierBytes);

        using (var sha = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(_codeVerifier);
            var hash = sha.ComputeHash(bytes);
            var resp = Base64Url.Encode(hash);
            return resp;
        }
    }
    private async Task<bool> RefreshToken()
    {
        var token = await _httpClient.PostFormUrlEncodedAsync<LoginResponseModel>(Appsettings.IdentityTokenEndpoint,new Dictionary<string, string>() {
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
    private void RefreshCookieWebview()
    {
        webView.Navigating -= WebViewRefreshCookieNavigating;
        webView.Navigating -= WebViewLoginNavigating;
        webView.Navigating -= WebViewGetCodeNavigating;
        webView.Navigating -= WebViewLogoutNavigating;
        webView.Navigating += WebViewRefreshCookieNavigating;

        string RefreshCookie = Appsettings.IdentityRefreshCookie + "?access_token=" + IAccountService.CurrentAccount.AccessToken;
        webView.Source = RefreshCookie;
    }
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
        if (unescapedUrl.StartsWith(Appsettings.ClientCallback))
        {
            Action = "";
            var shell = (AppShell)Shell.Current;
            shell.GotoHome();
            
            //await Shell.Current.GoToAsync(nameof(HomeView));
        }
    }
    public void GetCode(string clientId,string clientSecret,string scope,string returnUrl)
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

        string IdentityAuthorizeEndpoint = CreateAuthorizeEndpoint(dic);
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            webView.Navigating -= WebViewRefreshCookieNavigating;
            webView.Navigating -= WebViewLoginNavigating;
            webView.Navigating -= WebViewGetCodeNavigating;
            webView.Navigating -= WebViewLogoutNavigating;
            webView.Navigating += WebViewLoginNavigating;
        }
        else
        {
            this._launcherUrl = returnUrl;
            webView.Navigating -= WebViewRefreshCookieNavigating;
            webView.Navigating -= WebViewLoginNavigating;
            webView.Navigating -= WebViewGetCodeNavigating;
            webView.Navigating -= WebViewLogoutNavigating;
            webView.Navigating += WebViewGetCodeNavigating;
        }
        
        webView.Source = IdentityAuthorizeEndpoint;
        //webView.Reload();
    }
    public void Logout()
    {
        //webView.Cookie.Clear();

        webView.Navigating -= WebViewRefreshCookieNavigating;
        webView.Navigating -= WebViewLoginNavigating;
        webView.Navigating -= WebViewGetCodeNavigating;
        webView.Navigating -= WebViewLogoutNavigating;
        webView.Navigating += WebViewLogoutNavigating;

        string RefreshCookie = Appsettings.IdentityLogout + "?returnuri=" + Appsettings.ClientCallback;
        webView.Source = RefreshCookie;
    }
    protected override async void OnAppearing()
    {
        await Init();
    }
}
internal static class Common
{
    public static string ByteArrayToString(byte[] array)
    {
        var hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}
