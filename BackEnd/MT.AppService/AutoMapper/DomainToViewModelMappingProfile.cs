using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Models;
using AutoMapper;
namespace MT.AppService.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Endereco))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha.CodigoAcesso));

        }
    }
}
