using MT.Domain.Interfaces.Repositories;
using MT.Domain.Interfaces.Services;
using MT.Domain.Models;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.ValueObjects;
using MT.Domain.Validations;
using MT.Infra.CrossCutting.Seguranca.Senha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioValidation _usuarioValidation;
        private readonly IContatoUsuarioRepository _contatoUsuarioRepository;
        private readonly IContatoUsuarioValidation _contatoUsuarioValidation;
        public UsuarioService(IUsuarioRepository Repositorio, 
                            IDomainNotificationHandler Notificacao,
                            IUsuarioValidation usuarioValidation,
                            IContatoUsuarioRepository contatoUsuarioRepository,
                            IContatoUsuarioValidation contatoUsuarioValidation)
            : base(Notificacao)
        {
            _usuarioRepository = Repositorio;
            _usuarioValidation = usuarioValidation;
            _contatoUsuarioRepository = contatoUsuarioRepository;
            _contatoUsuarioValidation = contatoUsuarioValidation;
        }

        public void Registrar(Usuario model)
        {
            _usuarioValidation.ValidarRegistroUsuario(model);
            if (!Notificacao.HasNotifications)
            {
                model.Registrar();
                _usuarioRepository.Adicionar(model);
            }
        }        

        public async Task<Usuario> SelecionarPorEmailAsync(Email email)
        {
            if (!Email.EhValido(email))
            {
                Notificacao.Adicionar("Email", "Informe um email valido.");
                return null;
            }

            return await _usuarioRepository.SelecionarPorEmailAsync(email.Endereco);
        }

        public async Task<Usuario> SelecionarPorEmaileSenhaAsync(Email email, Senha senha)
        {
            if (!Email.EhValido(email))
            {
                Notificacao.Adicionar("Email", "Informe um email válido.");
                return null;
            }

            if (!Senha.EhValido(senha))
            {
                Notificacao.Adicionar("Senha", "Informe uma senha válida.");
                return null;
            }

            return await _usuarioRepository.SelecionarPorEmaileSenhaAsync(email.Endereco, HashSenhas.HashMD5(senha.CodigoAcesso));
        }

        public async Task<IQueryable<Usuario>> SelecionarAsync()
        {
            return _usuarioRepository.Selecionar();
        }

        public void SalvarContatoUsuario(ContatoUsuario contatoUsuario)
        {
            _contatoUsuarioValidation.ValidarSalvar(contatoUsuario);

            if (!Notificacao.HasNotifications)
            {
                if (!_contatoUsuarioRepository.Selecionar().Any(a => a.UsuarioId == contatoUsuario.UsuarioId))
                {
                    _contatoUsuarioRepository.Adicionar(contatoUsuario);
                }
                else
                {
                    _contatoUsuarioRepository.Atualizar(contatoUsuario);
                }
            }            
        }
    }
}
