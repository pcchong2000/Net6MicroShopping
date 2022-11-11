﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        
        private readonly IBlogService _blogService;
        public HomePageViewModel(IBlogService blogService)
        {
            _blogService = blogService;
            //_blogService = new BlogService(null);
            blogList = new ObservableCollection<BlogListItemResponseModel>();
            InitData();
        }
        async void InitData()
        {
            var BlogList = await GetDataAsync();
            if (BlogList != null)
            {
                foreach (var item in BlogList)
                {
                    ItemHandler(item);
                    blogList.Add(item);
                }
            }

        }
        async Task<List<BlogListItemResponseModel>> GetDataAsync()
        {
            return await _blogService.GetBlogListAsync(new BlogListRequestModel() { PageIndex = this.pageIndex, PageSize = pageSize });  
        }

        [ObservableProperty]
        public ObservableCollection<BlogListItemResponseModel> blogList;
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
            this.pageIndex++;

            var nextData =await GetDataAsync();
            if (nextData != null)
            {
                foreach (var item in nextData)
                {
                    ItemHandler(item);
                    blogList.Add(item);
                }
            }
        }
        [RelayCommand]
        async Task ItemClick(BlogListItemResponseModel blog)
        {
            await Shell.Current.GoToAsync($"{nameof(BlogDetailPage)}?Id={blog.Id}");
        }
        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task Refresh()
        {
            //isRefreshing = true;
            this.pageIndex=Random.Shared.Next(1,10);//随机一个分页数
            
            var nextData = await GetDataAsync();
            if (nextData!=null)
            {
                //清空数据
                blogList.Clear();
                foreach (var item in nextData)
                {
                    ItemHandler(item);
                    blogList.Add(item);
                }
            }
            
            isRefreshing = false;

            this.OnPropertyChanged("IsRefreshing");
        }
        private void ItemHandler(BlogListItemResponseModel item)
        {
            item.CoverImageUrl = Appsettings.BaseAddress + item.CoverImageUrl;
            item.AccountAvatarUrl = Appsettings.BaseAddress + item.AccountAvatarUrl;
        }
    }
}
