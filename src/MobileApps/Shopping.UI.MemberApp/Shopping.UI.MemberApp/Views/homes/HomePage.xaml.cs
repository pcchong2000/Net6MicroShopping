using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class HomePage : ContentPage
{
    public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();

        BindingContext= vm;
	}
    //async void OnCollectionViewRemainingItemsThresholdReached(object sender, EventArgs e)
    //{
    //    await viewModel.NextPageData();
    //}
}