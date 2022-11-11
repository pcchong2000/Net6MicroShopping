using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class MyIndexPage : ContentPage
{
    private MyIndexPageViewModel _vm;
    public MyIndexPage( MyIndexPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
        
    }
    protected override async void OnAppearing()
    {
        if (IAccountService.CurrentAccount == null)
        {
           await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else 
        {
            _vm.GetMyInfoCommand.Execute(null);
        }
        
    }
}