using MT.Domain.Shared.Models;
using MT.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Models
{
    public class Usuario : ModelBase<Usuario>
    {
        public Usuario(Guid id, string nome, string email, string senha)
        {
            UsuarioId = id;
            Nome = nome;
            SetarEmail(email);
            SetarSenha(senha);
        }
        protected Usuario()
        {

        }
        public void SetarSenha(string senha)
        {
            Senha = new Senha(senha);
        }
        public void SetarEmail(string email)
        {
            Email = new Email(email);
        }
        public void SetarNome(string nome)
        {
            Nome = nome;
        }
        public void Registrar()
        {
            DataCadastro = DateTime.Now;
            Senha.CriptografarSenha();
        }
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Senha Senha { get; private set; }
        public DateTime DataCadastro { get; private set; }

    }
}
