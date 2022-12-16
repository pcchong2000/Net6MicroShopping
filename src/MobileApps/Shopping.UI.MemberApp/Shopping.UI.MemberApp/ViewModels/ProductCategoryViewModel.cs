using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.ProductServices;
using Shopping.UI.MemberApp.Services.ProductServices.ProductModels;
using System.Collections.ObjectModel;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class ProductCategoryViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        public ProductCategoryViewModel(IProductService productService)
        {
            _productService = productService;
            dataList = new ObservableCollection<ProductCategoryResponseModel>();
            //InitData();
        }
        [ObservableProperty]
        public ObservableCollection<ProductCategoryResponseModel> dataList;
        public async Task InitData()
        {
            await GetDataAsync();
        }
        async Task GetDataAsync()
        {
            var resp = await _productService.GetProductCategoryAsync();
            if (resp != null)
            {
                var parentList= resp.Where(a=>a.ParentId==null).ToList();
                
                for (int i = 0; i < parentList.Count; i++)
                {
                    var item = parentList[i];
                    if (i == 0)
                    {
                        item.CheckColor = Color.Parse("#fff");
                    }
                    else 
                    {
                        item.CheckColor = Color.Parse("#eee");
                    }
                    item.Childrens = resp.Where(a => a.ParentId == item.Id).ToList();

                    dataList.Add(item);
                }

            }
        }
        [RelayCommand]
        async Task ItemParnetClick(ProductCategoryResponseModel item)
        {
            foreach (var child in dataList)
            {
                child.CheckColor = Color.Parse("#eee");

            }
            item.CheckColor = Color.Parse("#fff");

            this.OnPropertyChanged("DataList");
            
        }
        [RelayCommand]
        async Task ItemClick(ProductCategoryResponseModel item)
        {

        }
    }
}
