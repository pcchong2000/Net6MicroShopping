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
            data=new List<ProductCategoryResponseModel> ();
            GetDataAsync();
        }
        [ObservableProperty]
        public ObservableCollection<ProductCategoryResponseModel> dataList;
        private List<ProductCategoryResponseModel> data;
        async void GetDataAsync()
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
                    data.Add(item);
                    DataList.Add(item);
                }

            }
        }
        [RelayCommand]
        void ItemParnetClick(ProductCategoryResponseModel item)
        {
            DataList.Clear();
            foreach (var child in data)
            {
                child.CheckColor = Color.Parse("#eee");
                if (child.Id==item.Id)
                {
                    child.CheckColor = Color.Parse("#fff");
                }
                DataList.Add(child);
            }
            
        }
        [RelayCommand]
        void ItemClick(ProductCategoryResponseModel item)
        {

        }
    }
}
