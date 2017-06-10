using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.Infra.Extensions.PagedList
{
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Construtor vazio para Deserialização
        /// </summary>
        public PagedList()
        {

        }

        internal PagedList(IOrderedQueryable<T> query, int paginaAtual, int qtdeItensPorPagina)
        {
            QtdeItens = query.Count();
            Itens = query.Skip((paginaAtual - 1) * qtdeItensPorPagina)
                         .Take(qtdeItensPorPagina).ToList();
            QtdeItensPorPagina = qtdeItensPorPagina;
            SetarQtdePaginas(qtdeItensPorPagina, QtdeItens);
        }

        internal PagedList(IEnumerable<T> itens, int qtdeItensPorPagina, int qtdeItens)
        {
            Itens = itens;
            QtdeItensPorPagina = qtdeItensPorPagina;
            QtdeItens = qtdeItens;
            SetarQtdePaginas(qtdeItensPorPagina, qtdeItens);
        }

        #region Props
        private int qtdeItens;
        public int QtdeItens
        {
            get { return qtdeItens; }
            set { qtdeItens = value; }
        }

        private int qtdeItensPorPagina;
        public int QtdeItensPorPagina
        {
            get { return qtdeItensPorPagina; }
            set { qtdeItensPorPagina = value; }
        }

        private int qtdePaginas;
        public int QtdePaginas
        {
            get { return qtdePaginas; }
            set { qtdePaginas = value; }
        }

        private IEnumerable<T> _itens;
        public IEnumerable<T> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }


        #endregion

        private void SetarQtdePaginas(int qtdePorPagina, int qtdeRegistros)
        {
            QtdePaginas = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(qtdeRegistros) / Convert.ToDecimal(qtdePorPagina))));
        }
    }
}
