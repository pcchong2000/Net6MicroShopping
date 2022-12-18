using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class MyIndexView : ContentPage
{
    private MyIndexViewModel _vm;
    public MyIndexView( MyIndexViewModel vm)
	{
        InitializeComponent();
        BindingContext = _vm = vm;
    }
    protected override async void OnAppearing()
    {
        App.InitAccessToken();

        if (IAccountService.CurrentAccount.IsExpired)
        {
            await Shell.Current.GoToAsync(nameof(LoginView) + "?action=login");
        }
        else
        {
            _vm.GetMyInfoCommand.Execute(null);
        }

    }
}