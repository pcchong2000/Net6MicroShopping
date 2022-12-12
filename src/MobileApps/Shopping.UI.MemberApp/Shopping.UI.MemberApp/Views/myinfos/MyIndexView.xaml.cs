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
        if (IAccountService.CurrentAccount == null)
        {
            Shell.Current.GoToAsync(nameof(LoginView));
        }
        else
        {
            _vm.GetMyInfoCommand.Execute(null);
        }
    }
    protected override async void OnAppearing()
    {
        if (IAccountService.CurrentAccount == null)
        {
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
        else
        {
            _vm.GetMyInfoCommand.Execute(null);
        }

    }
}