﻿using MT.Domain.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MT.Service.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected IDomainNotificationHandler Notificacao { get; set; }
        public BaseController(IDomainNotificationHandler notificacao)
        {
            Notificacao = notificacao;
        }


        [NonAction]
        /// <summary>
        /// Metodo utilizado para realizar o retorno da api;
        /// </summary>
        /// <param name="result">o objeto de retorno</param>
        /// <returns>retorna um <see cref="IHttpActionResult"/> já configurado</returns>
        public IHttpActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            if (!ModelState.IsValid)
            {
                var notificacoes = from state in ModelState
                            from error in state.Value.Errors
                            select new
                            {
                                DomainNotificationId = Guid.NewGuid(),
                                Version = 1,
                                Key = state.Key,
                                Value = error.ErrorMessage
                            };
                response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    success = false,
                    errors = notificacoes.Select(s => s.Value)
                }), Encoding.UTF8, "application/json");
            }
            else
            {
                response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    success = false,
                    errors = Notificacao.GetNotifications().Select(n => n.Value)
                }), Encoding.UTF8, "application/json");
            }
            return base.ResponseMessage(response);
        }

        [NonAction]
        protected bool OperacaoValida()
        {
            return (!Notificacao.HasNotifications && ModelState.IsValid);
        }
    }
}
