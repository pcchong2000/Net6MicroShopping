using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class MyIndexViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        public MyIndexViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string nickName;
        
        [ObservableProperty]
        public string avatarUrl;
        [ObservableProperty]
        public bool isRunning = false;
        [RelayCommand]
        async void GetMyInfo()
        {
            var resp = await _accountService.MyInfoAsync();
            if (resp != null)
            {
                AvatarUrl = resp.AvatarUrl;
                Name = resp.Name;
                UserName=resp.UserName;
                NickName= resp.NickName;
                Id =resp.Id;
            }
        }
        [RelayCommand]
        async Task Clear()
        {
            await _accountService.ClearAsync();
            //var _loginView = LoginView.Current == null ? MauiProgram.Services.GetService<LoginView>() : LoginView.Current;
            //_loginView.Logout();
            //var shell = (AppShell)Shell.Current;
            //shell.GotoHome();
            await Shell.Current.GoToAsync(nameof(LoginView)+ "?action=logout");
        }
        [RelayCommand]
        async Task Test()
        {
            await _accountService.TestAsync();
            
        }
        [RelayCommand]
        async Task GotoUpdateInfo()
        {
            await Shell.Current.GoToAsync($"{nameof(UpdateInfoView)}");
        }

        [RelayCommand]
        async Task GotoOrderList(string orderStatus)
        {
           int _orderStatus=int.Parse(orderStatus);
           await Shell.Current.GoToAsync($"{nameof(OrderListView)}?status={_orderStatus}");
        }
        [RelayCommand]
        public async void QRCodeScanner()
        {
            await Shell.Current.GoToAsync($"{nameof(CameraBarcodeReaderView)}");
        }

    }
}
