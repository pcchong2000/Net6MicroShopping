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

        IAccountService.CurrentAccount = new AccountInfo();
        IAccountService.CurrentAccount.RefreshToken = refreshToken;
        IAccountService.CurrentAccount.AccessToken = accessToken;
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            IAccountService.CurrentAccount.IsLogin = true;
        }
        else
        {
            IAccountService.CurrentAccount.IsLogin = false;
        }
        if (expiredTimeStr != null && DateTime.TryParse(expiredTimeStr, out DateTime expiredTime))
        {
            if (expiredTime > DateTime.Now)
            {
                if (IAccountService.CurrentAccount == null)
                {
                    IAccountService.CurrentAccount = new AccountInfo();
                }
                
                IAccountService.CurrentAccount.IsExpired = false;
            }
            else
            {
                IAccountService.CurrentAccount.IsExpired = true;
            }
        }
        else
        {
            IAccountService.CurrentAccount.IsExpired = true;
        }
    }
}
