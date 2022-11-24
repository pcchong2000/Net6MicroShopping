using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.ProductServices;
using Shopping.UI.MemberApp.Services.ProductServices.ProductModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class ProductCategoryViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        public ProductCategoryViewModel(IProductService productService)
        {
            _productService = productService;
            dataList = new ObservableCollection<ProductCategoryResponseModel>();
            InitData();
        }
        [ObservableProperty]
        public ObservableCollection<ProductCategoryResponseModel> dataList;
        async void InitData()
        {
            await GetDataAsync();
        }
        async Task GetDataAsync()
        {
            var resp = await _productService.GetProductCategoryAsync();
            if (resp != null)
            {
                foreach (var item in resp)
                {
                    dataList.Add(item);
                }
            }
        }
    }
}
