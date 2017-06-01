using AutoMapper;
using MT.AppService.Interfaces;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Interfaces.Services;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.UoW;
using MT.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService
{
    public class AutenticacaoApiService : AppServiceBase, IAutenticacaoApiService
    {
        private readonly IUsuarioService _usuarioService;

        public AutenticacaoApiService(IMapper mapper, IUnitOfWork uow, IDomainNotificationHandler notificacao, IUsuarioService usuarioService) : base(mapper, uow, notificacao)
        {
            _usuarioService = usuarioService;
        }

        public async Task<UsuarioLogadoViewModel> LoginAsync(string email, string senha)
        {
            var usuario = await _usuarioService.SelecionarPorEmaileSenhaAsync(new Email(email), new Senha(senha)); ;
            if (usuario == null)
            {
                return null;
            }
            var usuarioLogado = new UsuarioLogadoViewModel()
            {
                Nome = usuario.Nome,
                Email = usuario.Email.Endereco,
                UsuarioId = usuario.UsuarioId
            };
            return usuarioLogado;
        }
    }
}
