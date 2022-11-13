using IdentityModel;
using IdentityModel.Client;
using PCLCrypto;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;
using System.Linq;
using System.Net;
using System.Text;
using static PCLCrypto.WinRTCrypto;

namespace Shopping.UI.MemberApp;

public partial class LoginPage : ContentPage
{
    private readonly HttpClientService _httpClient;
    private readonly IAccountService _accountService;
    private string _codeVerifier;
    public LoginPage(LoginPageViewModel vm, HttpClientService httpClientService, IAccountService accountService)
	{
		InitializeComponent();
		BindingContext = vm;
        _httpClient= httpClientService;
        _accountService = accountService;

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
        webView.Navigating += async (e,b) => {
            var unescapedUrl = System.Net.WebUtility.UrlDecode(b.Url);
            if (unescapedUrl.StartsWith(Appsettings.ClientCallback))
            {
                webView.HeightRequest = 0;
                var authResponse = new AuthorizeResponse(unescapedUrl);
                var resp = await GetTokenAsync(authResponse.Code);

                await _accountService.SaveToken(resp);

                await Shell.Current.GoToAsync("..");
            }
        };

    }
    public string CreateAuthorizeEndpoint(IDictionary<string, string> values)
    {
        var queryString = string.Join("&", values.Select(kvp => string.Format("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value))).ToArray());
        return string.Format("{0}?{1}", Appsettings.IdentityAuthorizeEndpoint, queryString);
    }
    public async Task<LoginResponseModel> GetTokenAsync(string code)
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
        _codeVerifier = RandomNumberGenerator.CreateUniqueId();
        var sha256 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);
        var challengeBuffer = sha256.HashData(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(_codeVerifier)));
        byte[] challengeBytes;
        CryptographicBuffer.CopyToByteArray(challengeBuffer, out challengeBytes);
        return Base64Url.Encode(challengeBytes);
    }
}
internal static class RandomNumberGenerator
{
    public static string CreateUniqueId(int length = 64)
    {
        var bytes = PCLCrypto.WinRTCrypto.CryptographicBuffer.GenerateRandom(length);
        return ByteArrayToString(bytes);
    }

    private static string ByteArrayToString(byte[] array)
    {
        var hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}
