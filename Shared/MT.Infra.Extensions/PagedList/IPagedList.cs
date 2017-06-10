using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Infra.Extensions.PagedList
{
    public interface IPagedList<T>
    {
        IEnumerable<T> Itens { get; }
        int QtdeItens { get; }
        int QtdeItensPorPagina { get; }
        int QtdePaginas { get; }
    }
}
