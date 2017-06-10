using AutoMapper;
using MT.AppService.Interfaces;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Interfaces.Services;
using MT.Domain.Models;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.UoW;
using MT.Infra.Extensions.PagedList;
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

        public async Task SalvarContatoUsuarioAsync(ContatoUsuarioViewModel contatoUsuario)
        {
            UoW.BeginTransaction();
            var _contatosuario = Mapper.Map<ContatoUsuarioViewModel, ContatoUsuario>(contatoUsuario);
            _usuarioService.SalvarContatoUsuario(_contatosuario);
            await UoW.CommitAsync();
        }

        public async Task<IPagedList<UsuarioViewModel>> SelecionarAsync(int page , int pagesize = 25)
        {
            var query = await _usuarioService.SelecionarAsync();
            return query.OrderBy(a => a.Email.Endereco).ToViewModelPagedList(page, pagesize, s => new UsuarioViewModel()
            {
                Email = s.Email.Endereco,
                Nome = s.Nome,
                Senha = s.Senha.CodigoAcesso,
                UsuarioId = s.UsuarioId
            });
        }
    }
}
