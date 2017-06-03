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

            CreateMap<ContatoUsuario, ContatoUsuarioViewModel>()
                .ForMember(dest => dest.DDDCelular, opt => opt.MapFrom(src => src.TelefoneCelular.CodigoDDD))
                .ForMember(dest => dest.TelefoneCelular, opt => opt.MapFrom(src => src.TelefoneCelular.Numero))
                .ForMember(dest => dest.DDDFixo, opt => opt.MapFrom(src => src.TelefoneFixo.CodigoDDD))
                .ForMember(dest => dest.TelefoneFixo, opt => opt.MapFrom(src => src.TelefoneFixo.Numero));

        }
    }
}
