using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class ProductListView : ContentPage
{
	public ProductListView()
	{
		InitializeComponent();
	}
    async void OnSwiped(object sender, SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Left:
                // Handle the swipe
                break;
            case SwipeDirection.Right:
                var shell = (AppShell)Shell.Current;
                shell.GotoHome();
                //await Shell.Current.GoToAsync(nameof(HomePage));//无底部切换效果
                //await Shell.Current.GoToAsync("..");//无法返回到home
                // Handle the swipe
                break;
            case SwipeDirection.Up:
                // Handle the swipe
                break;
            case SwipeDirection.Down:
                // Handle the swipe
                break;
        }
    }
}