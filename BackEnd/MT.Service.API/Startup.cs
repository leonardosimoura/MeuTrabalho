using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using MT.AppService.Interfaces;
using System.Configuration;
using MT.Service.API.Security;
using MT.Infra.CrossCutting.AspNet.WebApi;
using MT.Domain.Shared.Logs;

[assembly: OwinStartup(typeof(MT.Service.API.Startup))]

namespace MT.Service.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureWebApi(config);
            Infra.Data.Migrations.DatabaseConfig.MigrateDatabase(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Infra.CrossCutting.IoC.BootStrapper.IniciarOwinWebApiContainer(app, config);
            //var service = Infra.CrossCutting.IoC.BootStrapper.ObterInstancia<IAutenticacaoApiService>();
            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            config.Filters.Add(new ActionLogFilterAttribute(Infra.CrossCutting.IoC.BootStrapper.ObterInstancia<ILogger>()));
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            WebApiConfig.Register(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/usuario/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new ApiAuthentication()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
