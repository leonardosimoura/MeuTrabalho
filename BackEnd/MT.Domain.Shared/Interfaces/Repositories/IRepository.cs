using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Selecionar();
        void Adicionar(TEntity model);
        void Atualizar(TEntity model);
        Task ExcluirAsync(Expression<Func<TEntity, bool>> predicado);
    }
}
