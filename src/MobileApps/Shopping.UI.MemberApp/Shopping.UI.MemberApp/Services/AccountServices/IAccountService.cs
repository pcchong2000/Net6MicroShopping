using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.AccountServices
{
    public interface IAccountService
    {
        Task<LoginResponseModel> LoginAsync(IEnumerable<KeyValuePair<string, string>> request);
        Task SaveToken(LoginResponseModel loginResponse);
        Task<AccountInfoResponseModel> MyInfoAsync();
        Task<List<FileResponse>> UpdateFileAsync(Stream file,string name);
        Task<bool> AccountUpdateAsync(AccountUpdateModel update);
        Task TestAsync();
        Task ClearAsync();
        public static AccountInfo CurrentAccount { get; set; } = new AccountInfo();
    }
}
