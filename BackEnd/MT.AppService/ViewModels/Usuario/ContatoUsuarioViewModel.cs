using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService.ViewModels.Usuario
{
    public class ContatoUsuarioViewModel
    {
        public Guid UsuarioId { get;  set; }
        public string Nome { get;  set; }
        public string Empresa { get;  set; }
        public string DDDCelular { get; set; }
        public string TelefoneCelular { get;  set; }
        public string DDDFixo { get; set; }
        public string TelefoneFixo { get;  set; }
    }
}
