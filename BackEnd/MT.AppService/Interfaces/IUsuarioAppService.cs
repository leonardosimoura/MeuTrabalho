using MT.AppService.ViewModels.Usuario;
using MT.Infra.Extensions.PagedList;
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

        Task<IPagedList<UsuarioViewModel>> SelecionarAsync(int page , int pagesize = 25);

        Task SalvarContatoUsuarioAsync(ContatoUsuarioViewModel contatoUsuario);
    }
}
