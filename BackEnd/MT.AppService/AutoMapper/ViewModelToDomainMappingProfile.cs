using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Models;
using MT.Domain.Shared.ValueObjects;

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

            CreateMap<ContatoUsuarioViewModel, ContatoUsuario>()
                .ConstructUsing(c => new ContatoUsuario(c.UsuarioId,c.Nome,c.Empresa,new Telefone(c.DDDCelular,c.TelefoneCelular),new Telefone(c.DDDFixo,c.TelefoneFixo)))
                .ForMember(d => d.TelefoneCelular, m => m.Ignore())
                .ForMember(d => d.TelefoneFixo, m => m.Ignore());
        }
    }
}
