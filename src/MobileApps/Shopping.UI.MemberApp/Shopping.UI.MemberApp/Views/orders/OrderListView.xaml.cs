using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class OrderListView : ContentPage
{
	public OrderListView(OrderListViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}