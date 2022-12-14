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
    public static void InitAccessToken()
    {
        string accessToken = SecureStorage.Default.GetAsync("AccessToken").GetAwaiter().GetResult();
        string refreshToken = SecureStorage.Default.GetAsync("RefreshToken").GetAwaiter().GetResult();
        string expiredTimeStr = SecureStorage.Default.GetAsync("ExpiredTime").GetAwaiter().GetResult();

        if (IAccountService.CurrentAccount==null)
        {
            IAccountService.CurrentAccount = new AccountInfo();
        }
        
        IAccountService.CurrentAccount.RefreshToken = refreshToken;
        IAccountService.CurrentAccount.AccessToken = accessToken;
        
        if (expiredTimeStr != null && DateTime.TryParse(expiredTimeStr, out DateTime expiredTime))
        {
            IAccountService.CurrentAccount.ExpiredTime = expiredTime;
        }

    }
}
