using MT.AppService.Interfaces;
using MT.AppService.ViewModels.Usuario;
using MT.Domain.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MT.Service.API.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IAutenticacaoApiService _authservice;
        public UsuarioController(IDomainNotificationHandler notificacao,
                                IUsuarioAppService usuarioAppService,
                                IAutenticacaoApiService authservice) : base(notificacao)
        {
            _usuarioAppService = usuarioAppService;
            _authservice = authservice;
        }

        [Route("{page:int}/{pagesize:int}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int page,int pagesize = 25)
        {
            return Response(await _usuarioAppService.SelecionarAsync(page, pagesize));
        }

        [Route("registrar")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Registrar(RegistrarUsuarioViewModel model)
        {
            if (ModelState.IsValid)        
                await _usuarioAppService.RegistrarUsuarioAsync(model);
                      
            return Response();
        }

        [Route("salvarcontatousuario")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> SalvarContatoUsuario(ContatoUsuarioViewModel model)
        {
            if (ModelState.IsValid)
                await _usuarioAppService.SalvarContatoUsuarioAsync(model);

            return Response();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            var usuario = await _authservice.LoginAsync(model.Email, model.Senha);
            return Response(new { usuario = usuario });
        }
    }
}
