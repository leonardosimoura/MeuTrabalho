using AutoMapper;
using MT.AppService.Interfaces;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Interfaces.Services;
using MT.Domain.Models;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService
{
    public class UsuarioAppService : AppServiceBase, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IMapper mapper,
                            IUnitOfWork uow,
                            IDomainNotificationHandler notificacao,
                            IUsuarioService usuarioService) : base(mapper, uow, notificacao)
        {
            _usuarioService = usuarioService;
        }

        public async Task RegistrarUsuarioAsync(RegistrarUsuarioViewModel model)
        {
            UoW.BeginTransaction();
            var usuario = new Usuario(Guid.NewGuid(), model.Nome, model.Email, model.Senha);
            _usuarioService.Registrar(usuario);
            await UoW.CommitAsync();
        }

        public async Task<IEnumerable<UsuarioViewModel>> SelecionarAsync(int page , int pagesize = 25)
        {
            var query = await _usuarioService.SelecionarAsync();
            return Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(query.OrderBy(a => a.Email.Endereco).Pagination(page,pagesize));
        }
    }
}
