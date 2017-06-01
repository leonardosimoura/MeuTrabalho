using MT.Domain.Models;
using MT.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        void Registrar(Usuario model);

        Task<Usuario> SelecionarPorEmailAsync(Email email);

        Task<Usuario> SelecionarPorEmaileSenhaAsync(Email email, Senha senha);

        Task<IQueryable<Usuario>> SelecionarAsync();
    }
}
