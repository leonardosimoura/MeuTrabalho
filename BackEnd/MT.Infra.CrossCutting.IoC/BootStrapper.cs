using AutoMapper;
using MT.AppService;
using MT.AppService.Interfaces;
using MT.Domain.Interfaces.Repositories;
using MT.Domain.Interfaces.Services;
using MT.Domain.Services;
using MT.Domain.Shared.Logs;
using MT.Domain.Shared.Notifications;
using MT.Domain.Shared.UoW;
using MT.Domain.Validations;
using MT.Infra.CrossCutting.Log;
using MT.Infra.DataEF.Context;
using MT.Infra.DataEF.Repositories;
using MT.Infra.DataEF.UoW;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace MT.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {

        protected static Container Container { get; set; }

        public static TEntity ObterInstancia<TEntity>() where TEntity : class
        {
            return Container.GetInstance<TEntity>();
        }

        public static void IniciarMvcContainer()
        {
            var container = new Container();
            MvcContainer.IniciarContainer(ref container);
            RegistrarServicos(container);
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            Container = container;
        }

        public static Container IniciarTestsContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            RegistrarServicos(container);
            container.Verify();
            return container;
        }

        public static void IniciarWebApiContainer(HttpConfiguration config)
        {
            var container = new Container();
            WebApiContainer.IniciarContainer(ref container);
            RegistrarServicos(container);
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            Container = container;
        }

        public static void IniciarOwinWebApiContainer(IAppBuilder app, HttpConfiguration config)
        {

            var container = new Container();
            WebApiContainer.IniciarContainer(ref container);
            RegistrarServicos(container);
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            Container = container;

            app.Use(async (context, next) => {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });
        }

        private static void RegistrarServicos(Container container)
        {
            var config = AutoMapperConfigs.RegisterMappings();

            //container.RegisterSingleton<MapperConfiguration>(config);
            //container.RegisterSingleton<IMapper>(() => config.CreateMapper(container.GetInstance));
            container.RegisterSingleton<IMapper>(() => new Mapper(config));

            container.RegisterSingleton<IDomainNotificationHandler, DomainNotificationHandler>();
            // App Service
            container.Register<IUsuarioAppService, UsuarioAppService>();
            container.Register<IAutenticacaoApiService, AutenticacaoApiService>();


            // Domain
            container.Register<IUsuarioService, UsuarioService>();

            //Validation
            container.Register<IUsuarioValidation, UsuarioValidation>();
            container.Register<IContatoUsuarioValidation, ContatoUsuarioValidation>();            

            // Infra - Data
            //container.Register(typeof(IRepositorySQL<>), typeof(RepositorySQL<>));
            container.Register<MTContext>();
            container.Register<IUsuarioRepository, UsuarioRepository>();
            container.Register<IContatoUsuarioRepository, ContatoUsuarioRepository>();
            
            container.Register<IUnitOfWork, UnitOfWork>();

            //Logger
            container.RegisterSingleton<ILogger, Logger>();            
        }
    }
}
