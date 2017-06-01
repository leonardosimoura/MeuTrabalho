using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Models;

namespace MT.AppService.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                .ConstructUsing(c => new Usuario(Guid.NewGuid(), c.Nome, c.Email, c.Senha))
                .ForMember(d => d.Email, m => m.Ignore())
                .ForMember(d => d.Senha, m => m.Ignore());
        }
    }
}
