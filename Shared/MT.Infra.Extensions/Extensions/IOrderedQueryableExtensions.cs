using MT.Infra.Extensions;
using MT.Infra.Extensions.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class IIOrderedQueryableExtensions
    {

        public static IPagedList<T> ToPagedList<T>(this IOrderedQueryable<T> query, int paginaAtual, int qtdePorPagina)
        {
            return new PagedList<T>(query, paginaAtual, qtdePorPagina);
        }

        public static IPagedList<TReturn> ToViewModelPagedList<TEntity, TReturn>(this IOrderedQueryable<TEntity> query, int paginaAtual, int qtdePorPagina, Func<TEntity, TReturn> parserFunc)
        {
            return query.ToPagedList<TEntity>(paginaAtual, qtdePorPagina)
                    .ToViewModelPagedList<TEntity, TReturn>(parserFunc);
        }
    }
}
