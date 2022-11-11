using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}