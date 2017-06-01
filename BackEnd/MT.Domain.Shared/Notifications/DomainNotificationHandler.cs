using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Domain.Shared.Models;

namespace MT.Domain.Shared.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }
        public void Adicionar(DomainNotification notification)
        {
            _notifications.Add(notification);
        }

        public void Adicionar(string key, string value)
        {
            _notifications.Add(new DomainNotification(key, value));
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

        public IEnumerable<DomainNotification> GetNotifications()
        {
            return _notifications;
        }
        
        //public void AdicionarValidacoesEntidade<TEntity>(ModelBase<TEntity> entidade)
        //{
        //    _notifications.AddRange(entidade.Notificacoes);
        //}

        public bool HasNotifications
        {
            get
            {
                return _notifications.Any();
            }
        }
    }
}
