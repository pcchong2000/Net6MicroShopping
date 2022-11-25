﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        public string avatarUrl;
        [ObservableProperty]
        public bool isRunning = false;
        [RelayCommand]
        async void GetMyInfo()
        {
            var resp = await _accountService.MyInfoAsync();
            if (resp != null)
            {
                avatarUrl = resp.AvatarUrl;
                name = resp.Name;
                userName=resp.UserName;
                id=resp.Id;
                this.OnPropertyChanged("AvatarUrl");
                this.OnPropertyChanged("Name");
                this.OnPropertyChanged("UserName");
                this.OnPropertyChanged("Id");
            }
        }
        [RelayCommand]
        async Task Clear()
        {
            await _accountService.ClearAsync();
            var shell = (AppShell)Shell.Current;
            shell.GotoHome();
            //await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        [RelayCommand]
        async Task Test()
        {
            await _accountService.TestAsync();
            
        }
        [RelayCommand]
        async Task CheckFile()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    this.SetProperty(ref isRunning, true, "IsRunning");

                    using Stream sourceStream = await photo.OpenReadAsync();
                    var resp = await _accountService.UpdateAvatarAsync(sourceStream, photo.FileName);

                    await Task.Delay(1000);
                    this.SetProperty(ref avatarUrl, resp.AvatarUrl, "AvatarUrl");
                    this.SetProperty(ref isRunning, false, "IsRunning");

                }
            }
        }
    }
}
