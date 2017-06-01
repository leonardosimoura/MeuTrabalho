using AutoMapper;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService
{
    public abstract class AppServiceBase
    {
        protected IMapper Mapper { get; private set; }
        protected IUnitOfWork UoW { get; private set; }
        protected IDomainNotificationHandler Notificacao { get; private set; }

        public AppServiceBase(IMapper mapper, IUnitOfWork uow, IDomainNotificationHandler notificacao)
        {
            Mapper = mapper;
            UoW = uow;
            Notificacao = notificacao;
        }
    }
}
