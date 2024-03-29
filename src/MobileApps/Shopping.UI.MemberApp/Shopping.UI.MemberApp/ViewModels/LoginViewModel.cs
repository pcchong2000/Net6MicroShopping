﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Shopping.UI.MemberApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [ObservableProperty]
        public string userName= "member1";
        [ObservableProperty]
        public string password= "123456";
        [RelayCommand]
        async Task Login()
        {
            //var request = new LoginRequestModel()
            //{
            //    userName = userName,
            //    password = password,
            //    grant_type= "password",
            //    client_id= Appsettings.ClientId
            //};
            Dictionary<string, string> data = new Dictionary<string, string>() {
                {"userName",UserName },
                {"password",Password },
                {"grant_type","password" },
                {"client_id",Appsettings.ClientPasswordId },
            };
            var resp = await _accountService.LoginAsync(data);
            if (resp!=null)
            {
                await _accountService.SaveToken(resp);
                
                //var shell = MauiProgram.Services.GetService<AppShell>();
                //Application.Current.MainPage = shell;
                
                //Application.Current.MainPage = new AppShell();
                //((AppShell)Shell.Current).GotoHome();

                await Shell.Current.GoToAsync("..");
            }
        }
        [RelayCommand]
        async Task LocalhostAccount()
        {
            //IdentityServerConstants.DefaultCookieAuthenticationScheme
            await OnAuthenticate("oidc");
        }
        [RelayCommand]
        void BackHome()
        {
            var shell = (AppShell)Shell.Current;
            shell.GotoHome();
        }
        async Task OnAuthenticate(string scheme)
        {
            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple", StringComparison.Ordinal) && DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Version.Major >= 13)
                {
                    // Make sure to enable Apple Sign In in both the
                    // entitlements and the provisioning profile.
                    var options = new AppleSignInAuthenticator.Options
                    {
                        IncludeEmailScope = true,
                        IncludeFullNameScope = true,
                    };
                    r = await AppleSignInAuthenticator.AuthenticateAsync(options);
                }
                else
                {
                    var authUrl = new Uri(Appsettings.IdentityAuthUrl + scheme);
                    var callbackUrl = new Uri("membermaui://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                //AuthToken = string.Empty;
                //if (r.Properties.TryGetValue("name", out var name) && !string.IsNullOrEmpty(name))
                //    AuthToken += $"Name: {name}{Environment.NewLine}";
                //if (r.Properties.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
                //    AuthToken += $"Email: {email}{Environment.NewLine}";
                //AuthToken += r?.AccessToken ?? r?.IdToken;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Login canceled.");

                //AuthToken = string.Empty;
                //await DisplayAlertAsync("Login canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message}");

                //AuthToken = string.Empty;
                //await DisplayAlertAsync($"Failed: {ex.Message}");
            }
        }
    }
}
