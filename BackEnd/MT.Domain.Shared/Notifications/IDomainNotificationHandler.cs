using MT.Domain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.Notifications
{
    public interface IDomainNotificationHandler : IDisposable
    {
        bool HasNotifications { get; }
        IEnumerable<DomainNotification> GetNotifications();
        void Adicionar(string key, string value);
       
    }
}
