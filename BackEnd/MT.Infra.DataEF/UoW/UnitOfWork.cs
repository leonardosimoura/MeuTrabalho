using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.UoW;
using MT.Infra.DataEF.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.DataEF.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MTContext _context;
        private DbContextTransaction _transaction;
        private IDomainNotificationHandler _notification;
        public UnitOfWork(MTContext context, IDomainNotificationHandler notification)
        {
            _context = context;
            _notification = notification;
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            if (!_notification.HasNotifications)
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    _transaction.Commit();
                }
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }
    }
}
