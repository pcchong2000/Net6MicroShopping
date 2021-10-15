using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroShoping.DomainBase
{
    public class EFRepositoryBase<T> : IRepositoryBase<T> where T : class, IEntityBase
    {
        private readonly DbContext _context;
        public DbSet<T> _dbSet { get; set; }
        private readonly IMapper _mapper;
        public EFRepositoryBase(DbContext context, IMapper mapper)
        {
            this._context = context;
            this._dbSet = this._context.Set<T>();
            this._mapper = mapper;

        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task AddAsync(T t)
        {
            await _dbSet.AddAsync(t);
        }

        public async Task AddAsync(IEnumerable<T> list)
        {
            await _dbSet.AddRangeAsync(list);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AnyAsync(where);
        }

        

        public void Delete(T t)
        {
            t.IsDeleted = true;
        }

        public void Delete(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                item.IsDeleted = true;
            }
        }
        public void RealDelete(T t)
        {
            _dbSet.Remove(t);

        }

        public void RealDelete(IEnumerable<T> t)
        {
            _dbSet.RemoveRange(t);
        }
        public void Update(T t)
        {
            _dbSet.Update(t);
        }

        public void Update(IEnumerable<T> list)
        {
            _dbSet.UpdateRange(list);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.FirstOrDefaultAsync(where);
        }

        public async Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> where)
        {
           var t = await _dbSet.FirstOrDefaultAsync(where);
            return _mapper.Map<TResult>(t);
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<T, bool>> where)
        {
            var list = await _dbSet.Where(where).ToListAsync();
            return _mapper.Map<List<TResult>>(list);
        }
        public async Task<List<T>> ListOrderAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, bool Asc)
        {
            var query = _dbSet.Where(where);
            if (Asc)
            {
                query = query.OrderBy(order);
            }
            else
            {
                query = query.OrderByDescending(order);
            }

            return await query.ToListAsync();
        }
        public async Task<List<TResult>> ListOrderAsync<Tkey, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, bool Asc)
        {
            var query = _dbSet.Where(where);
            if (Asc)
            {
                query = query.OrderBy(order);
            }
            else
            {
                query = query.OrderByDescending(order);
            }

            return _mapper.Map<List<T>, List<TResult>>(await query.ToListAsync());
        }

        public async Task<ResponsePageBase<T>> PageListAsync<TKey>(RequestPageBase request, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, bool Asc)
        {
            var Iquery = _dbSet.Where(where);
            var orderQuery = Iquery.OrderByDescending(a => a.Id);
            if (Asc)
            {
                orderQuery = Iquery.OrderBy(order);
            }
            else
            {
                orderQuery = Iquery.OrderByDescending(order);
            }

            ResponsePageBase<T> resp = new ResponsePageBase<T>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            resp.TotalCount = Iquery.Count();
            resp.PageTotal = (int)Math.Ceiling((double)resp.TotalCount / (double)resp.PageSize);

            resp.List = await orderQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return resp;
        }

        public async Task<ResponsePageBase<TResult>> PageListAsync<TKey, TResult>(RequestPageBase request, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, bool Asc)
        {
            var Iquery = _dbSet.Where(where);
            var orderQuery = Iquery.OrderByDescending(a => a.Id);
            if (Asc)
            {
                orderQuery = Iquery.OrderBy(order);
            }
            else
            {
                orderQuery = Iquery.OrderByDescending(order);
            }

            ResponsePageBase<TResult> resp = new ResponsePageBase<TResult>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            resp.TotalCount = Iquery.Count();
            resp.PageTotal = (int)Math.Ceiling((double)resp.TotalCount / (double)resp.PageSize);

            var list = await orderQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            resp.List = _mapper.Map<List<T>, List<TResult>>(list);

            return resp;
        }


        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

       
    }
}
