using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        Task CommitAsync();
        void Rollback();
    }
}
