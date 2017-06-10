using MT.Infra.Extensions.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.Infra.Extensions
{
    public static class IPagedListExtensions
    {
        public static IPagedList<TReturn> ToViewModelPagedList<TEntity, TReturn>(this IPagedList<TEntity> pagedList, Func<TEntity,TReturn> parserFunc)
        {
            var itens = new List<TReturn>();

            foreach (var item in pagedList.Itens)            
                itens.Add(parserFunc(item));            

            return new PagedList<TReturn>(itens,pagedList.QtdeItensPorPagina, pagedList.QtdeItens);
        }
    }
}
