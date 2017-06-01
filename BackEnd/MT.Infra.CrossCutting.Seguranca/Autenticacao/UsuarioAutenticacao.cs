using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Infra.CrossCutting.Seguranca.Autenticacao
{
    public class UsuarioAutenticacao
    {
        public static void SetarUsuarioLogado(Guid usuarioId, string nomeUsuario, string email, string authType, params string[] papeis)
        {
            var identity = new ClaimsIdentity(authType);
            identity.AddClaim(new Claim(ClaimTypes.Name, nomeUsuario));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, nomeUsuario));
            identity.AddClaim(new Claim(ClaimTypes.Sid, usuarioId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Email, email));
            for (int i = 0; i < papeis.Count(); i++)
                identity.AddClaim(new Claim(ClaimTypes.Role, papeis.ElementAt(i)));


            var principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
        }

        public static Guid ObterIdUsuarioLogado()
        {
            var princial = Thread.CurrentPrincipal as GenericPrincipal;

            return princial.ObterId();
        }

        public static string ObterEmailUsuarioLogado()
        {
            var princial = Thread.CurrentPrincipal as GenericPrincipal;

            return princial.ObterEmail();
        }
    }

    #region Extensions
    public static class GenericPrincipalExtension
    {
        /// <summary>
        /// Retorna o Id do usuario logado
        /// </summary>
        /// <param name="principal"></param>
        /// <returns><see cref="Guid"/> Id do Usuario Logado</returns>
        public static Guid ObterId(this GenericPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Sid);
            Guid id = Guid.Empty;
            if (claim != null)
            {
                id = Guid.Parse(claim.Value);
            }
            return id;
        }

        /// <summary>
        /// Retorna o email do usuario logado
        /// </summary>
        /// <param name="principal"></param>
        /// <returns><see cref="string"/>Email do usuario logado</returns>
        public static string ObterEmail(this GenericPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Email);

            if (claim == null)
                return "";

            return claim.Value;
        }
    }
    public static class GenericIdentityExtension
    {
        /// <summary>
        /// Retorna o Id do usuario logado
        /// </summary>
        /// <param name="principal"></param>
        /// <returns><see cref="Guid"/> Id do Usuario Logado</returns>
        public static Guid ObterId(this GenericIdentity identity)
        {
            var claim = identity.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Sid);
            Guid id = Guid.Empty;
            if (claim != null)
            {
                id = Guid.Parse(claim.Value);
            }
            return id;
        }
        /// <summary>
        /// Retorna o email do usuario logado
        /// </summary>
        /// <param name="principal"></param>
        /// <returns><see cref="string"/>Email do usuario logado</returns>
        public static string ObterEmail(this GenericIdentity identity)
        {
            var claim = identity.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Email);

            if (claim == null)
                return "";

            return claim.Value;
        }
    }
    #endregion
}
