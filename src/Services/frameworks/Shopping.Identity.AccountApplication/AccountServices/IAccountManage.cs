using Microsoft.EntityFrameworkCore;
using Shopping.Framework.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Identity.AccountApplication.AccountServices
{
    public interface IAccountManage<TAccount, TDbContext>
        where TAccount : IUserBase
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; set; }
        Task<bool> Create(TAccount account, string password);
        Task<TAccount> GetAccountById(string id);
        Task<TAccount> GetAccountByPhone(string phone);
        Task<TAccount> GetAccountByEmail(string email);
        Task<TAccount> GetAccountByUserName(string userName);
        Task<bool> AnyByUserName(string userName);
        Task<bool> AnyById(string id);
        bool CheckPassword(TAccount account, string password);
    }
}
