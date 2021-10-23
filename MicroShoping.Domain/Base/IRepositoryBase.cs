using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroShoping.Domain
{
    public interface IRepositoryBase<T> where T : class, IEntityBase
    {
        public DbSet<T> _dbSet { get; set; }
        Task AddAsync(T t);
        Task AddAsync(IEnumerable<T> listT);
        void Update(T t);
        void Update(IEnumerable<T> listT);
        void Delete(T t);
        void Delete(IEnumerable<T> t);
        void RealDelete(T t);
        void RealDelete(IEnumerable<T> t);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        Task<T> FindAsync(Expression<Func<T, bool>> where);
        Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> where);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> where);
        Task<List<TResult>> ListAsync<TResult>(Expression<Func<T, bool>> where);
        Task<List<TResult>> ListOrderAsync<Tkey, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, bool Asc);
        Task<ResponsePageBase<T>> PageListAsync<TKey>(RequestPageBase request, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, bool Asc);
        Task<ResponsePageBase<TResult>> PageListAsync<TKey, TResult>(RequestPageBase request, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, bool Asc);
        Task<int> SaveChangeAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
