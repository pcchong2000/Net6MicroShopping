using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class ProductDetailView : ContentPage
{
	public ProductDetailView()
	{
		InitializeComponent();
	}
    async void OnSwiped(object sender, SwipedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}