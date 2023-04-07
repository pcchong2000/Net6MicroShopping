using Shopping.Framework.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.EFCore
{
    public static class IQueryableExpand
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IQueryable<T> PageList<T>(this IQueryable<T> source, RequestPageBase page)
        {
            return source.Skip(page.PageSize * (page.PageIndex - 1)).Take(page.PageSize);
        }
    }
}
