using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService.ViewModels.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail necessário.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Informe um e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha necessária.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
