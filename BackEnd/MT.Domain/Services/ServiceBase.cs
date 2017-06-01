using MT.Domain.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Services
{
    public abstract class ServiceBase<TEntity> : IDisposable where TEntity : class
    {
        protected readonly IDomainNotificationHandler Notificacao;

        public ServiceBase(IDomainNotificationHandler notificacao)
        {
            this.Notificacao = notificacao;
        }

        public void Dispose()
        {
            Notificacao.Dispose();
        }
    }
}
