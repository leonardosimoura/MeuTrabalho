using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService.ViewModels.Usuario
{
    public class UsuarioViewModel
    {
        
        public Guid UsuarioId { get; set; }

        public string Nome { get;  set; }

        public string Email { get;  set; }

        public string Senha { get;  set; }
    }
}
