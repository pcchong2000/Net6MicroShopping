using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class UpdateInfoViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        public UpdateInfoViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            isRunning=false;
            GetMyInfo();
        }
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string nickName;
        [ObservableProperty]
        public string avatarUrl;
        [ObservableProperty]
        public DateTime? birthdayTime;
        [ObservableProperty]
        public bool isRunning = false;
        async void GetMyInfo()
        {
            var resp = await _accountService.MyInfoAsync();
            if (resp != null)
            {
                AvatarUrl = resp.AvatarUrl;
                Name = resp.Name;
                UserName=resp.UserName;
                NickName = resp.NickName;
                BirthdayTime = resp.BirthdayTime;
            }
        }

        [RelayCommand]
        async Task CheckFile()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    this.IsRunning = true;
                    using Stream sourceStream = await photo.OpenReadAsync();
                    var respFiles = await _accountService.UpdateFileAsync(sourceStream, photo.FileName);
                    if (respFiles.Count>0)
                    {
                        this.IsRunning = false;
                        this.AvatarUrl= Appsettings.ApiBaseAddress + respFiles[0].PathUrl;
                    }
                }
            }
        }

        [RelayCommand]
        async Task Save()
        {
            this.IsRunning = true;
            var resp = await _accountService.AccountUpdateAsync(new AccountUpdateModel
            {
                AvatarUrl = avatarUrl,
                Name= name,
                NickName= nickName,
                BirthdayTime= birthdayTime
            });
            if (resp)
            {
                this.IsRunning = false;
                await Shell.Current.GoToAsync(nameof(MyIndexView));
                
            }
        }

    }
}
