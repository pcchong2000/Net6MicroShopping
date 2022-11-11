using Shopping.UI.MemberApp.Services.AccountServices;

namespace Shopping.UI.MemberApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
    }
	public void GotoHome()
	{
        Shell.Current.CurrentItem = homeTabItem;
    }
}
