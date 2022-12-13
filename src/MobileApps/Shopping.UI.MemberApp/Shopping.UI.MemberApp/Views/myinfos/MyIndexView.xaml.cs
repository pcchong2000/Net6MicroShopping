using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class MyIndexView : ContentPage
{
    private MyIndexViewModel _vm;
    public MyIndexView( MyIndexViewModel vm)
	{
		
        BindingContext = _vm = vm;
        App.InitAccessToken();
        if (IAccountService.CurrentAccount == null|| IAccountService.CurrentAccount.IsExpired)
        {
            Shell.Current.GoToAsync(nameof(LoginView));
        }
        else
        {
            _vm.GetMyInfoCommand.Execute(null);
        }
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        App.InitAccessToken();

        if (IAccountService.CurrentAccount == null || IAccountService.CurrentAccount.IsExpired)
        {
            string a = IAccountService.CurrentAccount.AccessToken;
            string r = IAccountService.CurrentAccount.RefreshToken;
            
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
        else
        {
            _vm.GetMyInfoCommand.Execute(null);
        }

    }
}