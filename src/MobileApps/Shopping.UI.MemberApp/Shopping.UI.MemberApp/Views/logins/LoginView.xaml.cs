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

public partial class LoginView : ContentPage
{
    public static LoginView Current;
    private readonly HttpClientService _httpClient;
    private readonly IAccountService _accountService;
    private string _codeVerifier;
    public LoginView(LoginViewModel vm, HttpClientService httpClientService, IAccountService accountService)
	{
		InitializeComponent();
        Current = this;
        BindingContext = vm;
        _httpClient= httpClientService;
        _accountService = accountService;

        Init();
    }
    public async void Init()
    {
        webView.Navigating += WebViewNavigating;
        //await App.InitAccessToken();
        // 登录
        if (IAccountService.CurrentAccount == null)
        {
            await LoginWebview();
        }

        // 刷新token和 cookie
        if (IAccountService.CurrentAccount != null)
        {
            await RefreshToken();

            await RefreshCookieWebview();
        }
    }
    private async Task LoginWebview()
    {
        var dic = new Dictionary<string, string>();
        dic.Add("client_id", Appsettings.ClientId);
        dic.Add("client_secret", Appsettings.ClientSecret);
        dic.Add("response_type", "code");
        dic.Add("scope", "openid profile orderapi memberapi productapi offline_access");
        dic.Add("redirect_uri", Appsettings.ClientCallback);
        dic.Add("nonce", Guid.NewGuid().ToString("N"));
        dic.Add("state", Guid.NewGuid().ToString("N"));
        dic.Add("code_challenge", CreateCodeChallenge());
        dic.Add("code_challenge_method", "S256");

        string IdentityAuthorizeEndpoint = CreateAuthorizeEndpoint(dic);

        webView.Source = IdentityAuthorizeEndpoint;
        
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
    private async Task RefreshToken()
    {
        var token = await _httpClient.PostFormUrlEncodedAsync<LoginResponseModel>(Appsettings.IdentityTokenEndpoint,new Dictionary<string, string>() {
            {"grant_type","refresh_token" },
            {"client_id",Appsettings.ClientId },
            {"client_secret",Appsettings.ClientSecret },
            {"refresh_token",IAccountService.CurrentAccount.RefreshToken },
        });
        await _accountService.SaveToken(token);
        
    }
    private async Task RefreshCookieWebview()
    {
        string RefreshCookie = Appsettings.RefreshCookie + "?access_token=" + IAccountService.CurrentAccount.AccessToken;
        //webView.Source = RefreshCookie;
        
    }
    private async void WebViewNavigating(object? sender, WebNavigatingEventArgs e)
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
