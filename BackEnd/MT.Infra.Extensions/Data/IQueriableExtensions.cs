using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class IQueryableExtensions
    {

        public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int page, int pagesize)
        {
            return source.Skip((page - 1) * pagesize)
                         .Take(pagesize);
        }
    }

    public static class IOrderedQueryableExtensions
    {

        public static IQueryable<TSource> Pagination<TSource>(this IOrderedQueryable<TSource> source, int page, int pagesize)
        {
            return source.Skip((page - 1) * pagesize)
                         .Take(pagesize);
        }
    }
}
