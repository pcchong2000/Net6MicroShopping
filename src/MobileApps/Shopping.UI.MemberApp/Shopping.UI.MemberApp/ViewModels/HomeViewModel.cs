﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.ProductServices;
using Shopping.UI.MemberApp.Services.ProductServices.ProductModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        
        private readonly IProductService _productService;
        public HomeViewModel(IProductService productService)
        {
            _productService = productService;
            dataList = new ObservableCollection<ProductHomeItemResponseModel>();
            InitData();
        }
        async void InitData()
        {
            var productList = await GetDataAsync();
            AddDatalist(productList);
        }
        async Task<List<ProductHomeItemResponseModel>> GetDataAsync()
        {
            var pageData = await _productService.GetProductHomeAsync(new ProductHomeRequestModel() { PageIndex = this.pageIndex, PageSize = pageSize });
            return pageData.List;
        }

        [ObservableProperty]
        public ObservableCollection<ProductHomeItemResponseModel> dataList;

        [ObservableProperty]
        public int pageIndex = 1;
        [ObservableProperty]
        public int pageSize= DeviceInfo.Platform == DevicePlatform.WinUI? 12:8;
        [ObservableProperty]
        public bool isRefreshing=false;
        /// <summary>
        /// 下一页
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task NextPageData()
        {
            this.PageIndex++;

            var productList = await GetDataAsync();
            AddDatalist(productList);
        }
        void AddDatalist(List<ProductHomeItemResponseModel> data)
        {
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    DataList.Add(item);
                } 
            }
        }
        [RelayCommand]
        async Task ItemClick(ProductHomeItemResponseModel blog)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailView)}?Id={blog.Id}");
        }
        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task Refresh()
        {
            //isRefreshing = true;
            //this.PageIndex=Random.Shared.Next(1,10);//随机一个分页数
            this.PageIndex = 1;
            this.DataList.Clear();

            var productList = await GetDataAsync();
            AddDatalist(productList);

            IsRefreshing = false;
        }
    }
}
