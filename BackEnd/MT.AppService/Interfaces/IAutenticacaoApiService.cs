using MT.AppService.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.AppService.Interfaces
{
    public interface IAutenticacaoApiService
    {
        Task<UsuarioLogadoViewModel> LoginAsync(string email, string senha);
    }
}
