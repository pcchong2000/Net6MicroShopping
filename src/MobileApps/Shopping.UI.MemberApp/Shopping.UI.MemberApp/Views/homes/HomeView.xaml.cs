using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class HomeView : ContentPage
{
    public HomeView(HomeViewModel vm)
	{
		InitializeComponent();

        BindingContext= vm;
	}
    //async void OnCollectionViewRemainingItemsThresholdReached(object sender, EventArgs e)
    //{
    //    await viewModel.NextPageData();
    //}
}