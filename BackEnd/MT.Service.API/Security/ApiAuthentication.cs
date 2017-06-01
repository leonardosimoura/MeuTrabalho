using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using MT.AppService.Interfaces;

namespace MT.Service.API.Security
{
    public class ApiAuthentication : OAuthAuthorizationServerProvider
    {
        private IAutenticacaoApiService _service;

        //public ApiAuthentication(IAutenticacaoApiService service)
        //{
        //    _service = service;
        //}


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            _service = Infra.CrossCutting.IoC.BootStrapper.ObterInstancia<IAutenticacaoApiService>();

            //Para aceitar Requisições de todos os ips
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                //var user = new User() { Email = context.UserName, Name = context.UserName, Password = context.Password };
                var usuario = await _service.LoginAsync(context.UserName, context.Password);

                if (usuario == null)
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");
                    return;
                }

                Infra.CrossCutting.Seguranca.Autenticacao.UsuarioAutenticacao.SetarUsuarioLogado(usuario.UsuarioId, usuario.Nome, usuario.Email, context.Options.AuthenticationType);

                context.Validated(Thread.CurrentPrincipal.Identity as ClaimsIdentity);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", "Usuário ou senha inválidos");
            }
        }
    }
}