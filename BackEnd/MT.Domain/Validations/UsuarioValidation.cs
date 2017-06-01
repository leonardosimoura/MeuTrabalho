using MT.Domain.Interfaces.Repositories;
using MT.Domain.Models;
using MT.Domain.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MT.Domain.Shared.ValueObjects;

namespace MT.Domain.Validations
{
    public interface IUsuarioValidation
    {
        bool ValidarRegistroUsuario(Usuario usuario);
    }

    public class UsuarioValidation :  AbstractValidator<Usuario>,IUsuarioValidation
    {
        private readonly IUsuarioRepository _repository;
        private readonly IDomainNotificationHandler _notificacao;
        public UsuarioValidation(IDomainNotificationHandler notificacao, IUsuarioRepository repository)
        {
            this._repository = repository;
            this._notificacao = notificacao;
        }        

        public bool ValidarRegistroUsuario(Usuario usuario)
        {
            ValidarNome();
            ValidarEmail();
            ValidarEmailExistente();
            ValidarSenha();
            var results =  this.Validate(usuario);
            foreach (var item in results.Errors)            
                _notificacao.Adicionar(item.PropertyName, item.ErrorMessage);

            return results.IsValid;
        }

        private void ValidarId()
        {
            RuleFor(p => p.UsuarioId).NotEmpty().NotNull().WithMessage("Código não pode ser vazio.");
        }

        private void ValidarSenha()
        {
            RuleFor(p => p.Senha).NotEmpty().NotNull().Must(a => Senha.EhValido(a)).WithMessage("Insira uma senha válida.");
        }

        private void ValidarEmail()
        {
            RuleFor(p => p.Email).NotEmpty().NotNull().Must(a => Email.EhValido(a)).WithMessage("Insira um e-mail válido.");
        }
        private void ValidarEmailExistente()
        {
            RuleFor(r => r.Email).MustAsync(async (email, token) => await _repository.SelecionarPorEmailAsync(email.Endereco) == null).WithMessage("Já existe um usuário com esse e-mail cadastrado no sistema.");
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome).NotEmpty().NotNull()
                .WithMessage("Insira um nome.");

            RuleFor(p => p.Nome).MinimumLength(3).MaximumLength(250).WithMessage("IInsira um nome com no mínimo 3 caracteres e no máximo 250 caracters");
           
        }
    }
    
}
