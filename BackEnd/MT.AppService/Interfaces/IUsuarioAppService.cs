using MT.AppService.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService.Interfaces
{
    public interface IUsuarioAppService
    {
        Task RegistrarUsuarioAsync(RegistrarUsuarioViewModel model);

        Task<IEnumerable<UsuarioViewModel>> SelecionarAsync(int page , int pagesize = 25);

        Task SalvarContatoUsuarioAsync(ContatoUsuarioViewModel contatoUsuario);
    }
}
