using MT.Domain.Interfaces.Repositories;
using MT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Infra.DataEF.Context;

namespace MT.Infra.DataEF.Repositories
{
    public class ContatoUsuarioRepository : Repository<ContatoUsuario>, IContatoUsuarioRepository
    {
        public ContatoUsuarioRepository(MTContext context) : base(context)
        {
        }
    }
}
