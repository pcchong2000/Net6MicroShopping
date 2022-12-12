using Shopping.UI.MemberApp.Services.AccountServices;

namespace Shopping.UI.MemberApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        var shell = MauiProgram.Services.GetService<AppShell>();
        MainPage = shell;
        //InitAccessToken().GetAwaiter().GetResult();
    }
    public static async Task InitAccessToken()
    {
        string accessToken = await SecureStorage.Default.GetAsync("AccessToken");
        string refreshToken = await SecureStorage.Default.GetAsync("RefreshToken");
        string expiredTimeStr = await SecureStorage.Default.GetAsync("ExpiredTime");
        if (expiredTimeStr != null && DateTime.TryParse(expiredTimeStr, out DateTime expiredTime))
        {
            if (expiredTime > DateTime.Now)
            {
                IAccountService.CurrentAccount = new AccountInfo()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiredTime = expiredTime
                };
            }
        }

    }
}
