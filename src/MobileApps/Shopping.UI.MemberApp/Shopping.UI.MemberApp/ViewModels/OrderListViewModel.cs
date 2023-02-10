using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.OrderServices;
using Shopping.UI.MemberApp.Services.ProductServices;
using Shopping.UI.MemberApp.Services.ProductServices.ProductModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class OrderListViewModel : ObservableObject, IQueryAttributable
    {
        private IOrderService _orderService;
        public OrderListViewModel(IOrderService orderService)
        {
            _orderService= orderService;
            dataList = new ObservableCollection<OrderListItemResponseModel>();
            
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("status"))
            {
                var value = query["status"].ToString();

                await InitData(int.Parse(value));
            }
        }
        [ObservableProperty]
        public ObservableCollection<OrderListItemResponseModel> dataList;
        [ObservableProperty]
        public int pageIndex = 1;
        [ObservableProperty]
        public int pageSize = DeviceInfo.Platform == DevicePlatform.WinUI ? 12 : 8;
        [ObservableProperty]
        public bool isRefreshing = false;
        async Task InitData(int orderStatus)
        {
            await GetDataAsync();
        }
        async Task GetDataAsync()
        {
            for (int i = (PageSize*(PageIndex-1)); i < (PageSize * PageIndex); i++)
            {
                DataList.Add(new OrderListItemResponseModel() { 
                    Title ="订单"+ i.ToString(),
                    Id = i.ToString(),

                });
            }
        }
        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task Refresh()
        {
            //this.pageIndex=Random.Shared.Next(1,10);//随机一个分页数
            this.PageIndex = 1;
            DataList.Clear();
            await GetDataAsync();

            IsRefreshing = false;
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task NextPageData()
        {
            this.PageIndex++;
            await GetDataAsync();
        }
        [RelayCommand]
        async Task ItemClick(OrderListItemResponseModel blog)
        {
            //await Shell.Current.GoToAsync($"{nameof(OrderDetailView)}?Id={blog.Id}");
        }
    }
}
