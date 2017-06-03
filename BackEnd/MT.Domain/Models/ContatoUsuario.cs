using MT.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Models
{
    public class ContatoUsuario
    {
        public ContatoUsuario(Guid usuarioId, string nome, string empresa, Telefone telefoneCelular, Telefone telefoneFixo)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Empresa = empresa;
            TelefoneCelular = telefoneCelular;
            TelefoneFixo = telefoneFixo;
        }

        protected ContatoUsuario()
        {

        }

        public Guid UsuarioId { get; protected set; }
        public string Nome { get; protected set; }
        public string Empresa { get; protected set; }
        public Telefone TelefoneCelular { get;protected set; }
        public Telefone TelefoneFixo { get; protected set; }
    }
}
