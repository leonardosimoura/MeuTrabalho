using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService.ViewModels.Usuario
{
    public class RegistrarUsuarioViewModel
    {
        [Required(ErrorMessage = "{0} é necessario.")]
        public string Nome { get;  set; }
        [Required(ErrorMessage = "{0} é necessario.")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get;  set; }
        [Required(ErrorMessage = "{0} é necessario.")]
        [DataType(DataType.Password)]
        public string Senha { get;  set; }

        [Required(ErrorMessage = "{0} é necessario.")]
        [DataType(DataType.Password)]
        [Compare("Senha",ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmaSenha { get;  set; }
    }
}
