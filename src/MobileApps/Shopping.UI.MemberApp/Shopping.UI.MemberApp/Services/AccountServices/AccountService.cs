using Shopping.UI.MemberApp.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly HttpClientService _httpClient;
        private AccountInfo currentAccount;
        public AccountInfo CurrentAccount { get => currentAccount; set => currentAccount=value; }

        public AccountService(HttpClientService  httpClientService)
        {
            _httpClient = httpClientService;
            
        }

        public async Task<LoginResponseModel> LoginAsync(IEnumerable<KeyValuePair<string, string>> request)
        {
            var resp= await _httpClient.PostFormUrlEncodedAsync<LoginResponseModel>(Appsettings.IdentityTokenEndpoint, request);
            

            return resp;
        }
        public async Task SaveToken(LoginResponseModel resp)
        {
            var info = new AccountInfo()
            {
                AccessToken = resp.access_token,
                RefreshToken=resp.refresh_token,
                ExpiredTime = DateTime.Now.AddSeconds(resp.expires_in),
            };
            IAccountService.CurrentAccount = info;
            await SecureStorage.Default.SetAsync("AccessToken", IAccountService.CurrentAccount.AccessToken);
            await SecureStorage.Default.SetAsync("RefreshToken", IAccountService.CurrentAccount.RefreshToken);
            await SecureStorage.Default.SetAsync("ExpiredTime", IAccountService.CurrentAccount.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        public async Task TestAsync()
        {
            
        }

        public Task ClearAsync()
        {
            SecureStorage.Default.RemoveAll();
            IAccountService.CurrentAccount = null;
            return Task.CompletedTask;
        }

        public async Task<AccountInfoResponseModel> MyInfoAsync()
        {
            return await _httpClient.GetAsync<AccountInfoResponseModel>(Appsettings.AccountMyInfo);
        }

        public async Task<List<FileResponse>> UpdateFileAsync(Stream file,string name)
        {
            return await _httpClient.PostFileAsync<List<FileResponse>>(Appsettings.OssUpLoad, file, name);
            
        }

        public async Task<bool> AccountUpdateAsync(AccountUpdateModel update)
        {
            return await _httpClient.PostAsync<AccountUpdateModel, bool>(Appsettings.AccountUpdate, update);
        }
    }
}
