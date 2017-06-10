using MT.Infra.Extensions.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using MT.Infra.Extensions;

namespace System.Linq
{
    public static class IQueryableExtensions
    {

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, int paginaAtual, int qtdePorPagina, Func<T, object> orderBy , bool orderAscending = true)
        {
            return new PagedList<T>((orderAscending)?query.OrderBy(orderBy): query.OrderByDescending(orderBy), paginaAtual, qtdePorPagina);
        }


        public static IPagedList<TReturn> ToViewModelPagedList<TEntity, TReturn>(this IQueryable<TEntity> query, int paginaAtual, int qtdePorPagina, Func<TEntity, TReturn> parserFunc, Func<TEntity, object> orderBy, bool orderAscending = true)
        {
            return query.ToPagedList<TEntity>(paginaAtual, qtdePorPagina, orderBy, orderAscending)
                    .ToViewModelPagedList<TEntity, TReturn>(parserFunc);
        }
    }
}
