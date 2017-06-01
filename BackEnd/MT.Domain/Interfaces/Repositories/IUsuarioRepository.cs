using MT.Domain.Models;
using MT.Domain.Shared.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> SelecionarPorEmailAsync(string email);

        Task<Usuario> SelecionarPorEmaileSenhaAsync(string email, string senha);
    }
}
