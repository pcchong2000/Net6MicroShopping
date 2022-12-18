using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;

namespace Shopping.UI.MemberApp;

public partial class UpdateInfoView : ContentPage
{
    private UpdateInfoViewModel _vm;
    public UpdateInfoView(UpdateInfoViewModel vm)
	{
        InitializeComponent();
        BindingContext = _vm = vm;
    }
}