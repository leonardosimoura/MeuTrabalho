using FluentValidation;
using MT.Domain.Interfaces.Repositories;
using MT.Domain.Models;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Validations
{
    public interface IContatoUsuarioValidation
    {
        bool ValidarSalvar(ContatoUsuario contatoUsuario);
    }
    
    public class ContatoUsuarioValidation : AbstractValidator<ContatoUsuario>, IContatoUsuarioValidation
    {      


        private readonly IDomainNotificationHandler _notificacao;
        public ContatoUsuarioValidation(IDomainNotificationHandler notificacao)
        {

            this._notificacao = notificacao;
        }

        public bool ValidarSalvar(ContatoUsuario contatoUsuario)
        {
            ValidarUsuarioId();
            ValidarNome();
            ValidarEmpresa();
            ValidarTelefoneCelular();
            ValidarTelefoneFixo();
            ValidarUsuarioId();

            var results = Validate(contatoUsuario);
            foreach (var item in results.Errors)
                _notificacao.Adicionar(item.PropertyName, item.ErrorMessage);

            return results.IsValid;
        }

        private void ValidarUsuarioId()
        {
            RuleFor(a => a.UsuarioId).Empty().WithMessage("Informe o código do usuário");
        }

        private void ValidarEmpresa()
        {
            RuleFor(a => a.Empresa).Empty()                
                .WithMessage("Informe a empresa");
            RuleFor(a => a.Empresa).MinimumLength(3)
                .MaximumLength(250).WithMessage("Empresa deve ter entre 3 e 250 caracteres");
        }

        private void ValidarNome()
        {
            RuleFor(a => a.Nome).Empty()
                .WithMessage("Informe o nome");
            RuleFor(a => a.Empresa).MinimumLength(3)
                .MaximumLength(250).WithMessage("Nome deve ter entre 3 e 250 caracteres");
        }

        private void ValidarTelefoneCelular()
        {
            When(a => a.TelefoneCelular != null, () =>
            {
                RuleFor(a => a.TelefoneCelular.Numero).Empty()
               .WithMessage("Informe o número do celular");
                RuleFor(a => a.TelefoneCelular.Numero)
                    .MaximumLength(20).WithMessage("Número deve ter no máximo 20 caracteres");
                RuleFor(a => a.TelefoneCelular).Must(t => Telefone.EhValido(t)).WithMessage("Informe um telefone válido.");
            });           
        }

        private void ValidarTelefoneFixo()
        {
            When(a => a.TelefoneFixo != null, () =>
            {
                RuleFor(a => a.TelefoneFixo.Numero).Empty()
               .WithMessage("Informe o número do celular");
                RuleFor(a => a.TelefoneFixo.Numero)
                    .MaximumLength(20).WithMessage("Número deve ter no máximo 20 caracteres");
                RuleFor(a => a.TelefoneFixo).Must(t => Telefone.EhValido(t)).WithMessage("Informe um telefone válido.");
            });
        }

        //private void ValidarContatos()
        //{
        //    When(a => a.Whatssap != null, () =>
        //    {
        //        RuleFor(a => a.Whatssap).Empty().Must(c => Contato.EhValido(c)).WithMessage("Informe um Whatssap válido.");
        //    });
        //    When(a => a.Twitter != null, () =>
        //    {
        //        RuleFor(a => a.Twitter).Empty().Must(c => Contato.EhValido(c)).WithMessage("Informe um Twitter válido.");
        //    });
        //    When(a => a.LinkedIn != null, () =>
        //    {
        //        RuleFor(a => a.LinkedIn).Empty().Must(c => Contato.EhValido(c)).WithMessage("Informe um LinkedIn válido.");
        //    });
        //    When(a => a.Facebook != null, () =>
        //    {
        //        RuleFor(a => a.Facebook).Empty().Must(c => Contato.EhValido(c)).WithMessage("Informe um Facebook válido.");
        //    });
        //}
    }
}
