using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class ProductCategoryView : ContentPage
{
    private ProductCategoryViewModel _vm;
    public ProductCategoryView(ProductCategoryViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }
}