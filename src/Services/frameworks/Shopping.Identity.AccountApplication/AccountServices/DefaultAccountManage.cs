using Microsoft.EntityFrameworkCore;
using Shopping.Framework.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Identity.AccountApplication.AccountServices
{
    public class DefaultAccountManage<TAccount, TDbContext> : IAccountManage<TAccount, TDbContext>

        where TAccount : class, IUserBase
        where TDbContext : DbContext
    {
        public TDbContext DbContext { get; set; }
        private DbSet<TAccount> _account;
        private IPasswordHandler _passwordHandler;
        public DefaultAccountManage(IPasswordHandler passwordHandler, TDbContext dbContext)
        {
            _passwordHandler = passwordHandler;
            DbContext = dbContext;
            _account = DbContext.Set<TAccount>();
        }
        public async Task<bool> Create(TAccount account, string password)
        {
            account.PasswordSecert = _passwordHandler.GetPasswordSecert();
            account.PasswordHash = _passwordHandler.GetPasswordHash(account.PasswordSecert, password);

            await _account.AddAsync(account);
            return await DbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> AnyByUserName(string userName)
        {
            return await _account.AnyAsync(a => a.UserName == userName);
        }
        public async Task<bool> AnyById(string id)
        {
            return await _account.AnyAsync(a => a.Id == id);
        }
        public async Task<TAccount> GetAccountById(string id)
        {
            return await _account.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<TAccount> GetAccountByUserName(string userName)
        {
            return await _account.FirstOrDefaultAsync(a => a.UserName == userName);
        }
        public async Task<TAccount> GetAccountByPhone(string phone)
        {
            return await _account.FirstOrDefaultAsync(a => a.PhoneNumber == phone);
        }
        public async Task<TAccount> GetAccountByEmail(string email)
        {
            return await _account.FirstOrDefaultAsync(a => a.Email == email);
        }

        public bool CheckPassword(TAccount account, string password)
        {
            return _passwordHandler.VerifyPassword(account.PasswordHash, account.PasswordSecert, password);
        }
    }
}
