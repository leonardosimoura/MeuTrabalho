using AutoMapper;
using MT.AppService.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.CrossCutting.IoC
{
    public class AutoMapperConfigs
    {

        public static MapperConfiguration RegisterMappings()
        {
            //TODO Remover referencia do appService
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
                //cfg.AddProfile(new POCOToDomainMappingProfile());
            });
        }
    }
}
