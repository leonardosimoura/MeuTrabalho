using MT.Domain.Interfaces.Repositories;
using MT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Infra.DataEF.Context;
using System.Data.Entity;

namespace MT.Infra.DataEF.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MTContext context) : base(context)
        {
        }

        public Task<Usuario> SelecionarPorEmailAsync(string email)
        {
            return DbSet.FirstOrDefaultAsync(a => a.Email.Endereco == email);
        }

        public Task<Usuario> SelecionarPorEmaileSenhaAsync(string email, string senha)
        {
            return DbSet.FirstOrDefaultAsync(a => a.Email.Endereco == email &&
                                             a.Senha.CodigoAcesso == senha);
        }
    }
}
