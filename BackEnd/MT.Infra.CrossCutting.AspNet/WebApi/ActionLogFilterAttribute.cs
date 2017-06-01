using MT.Domain.Shared.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MT.Infra.CrossCutting.AspNet.WebApi
{
    public class ActionLogFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {

        private readonly ILogger _logger;

        public ActionLogFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Request.Properties.Add("TimeStamp", DateTime.Now);
            actionContext.Request.Properties.Add("RequestId", Guid.NewGuid());

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            EnviarRequest(actionExecutedContext);

            base.OnActionExecuted(actionExecutedContext);
        }

        private void EnviarRequest(HttpActionExecutedContext actionExecutedContext)
        {
            object dataHoraRequest = null;
            actionExecutedContext.Request.Properties.TryGetValue("TimeStamp", out dataHoraRequest);
            object RequestId = null;
            actionExecutedContext.Request.Properties.TryGetValue("RequestId", out RequestId);

            var principal = actionExecutedContext.ActionContext.RequestContext.Principal as ClaimsPrincipal;
            var identity = actionExecutedContext.ActionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            object userData = null;
            try
            {
                userData = JsonConvert.DeserializeObject(identity.Claims
                                                    .FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value,
                                                    new JsonSerializerSettings
                                                    {
                                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                                        NullValueHandling = NullValueHandling.Ignore
                                                    });
            }
            catch (Exception) { }

            string Request_Content = "";
            try
            {
                Request_Content = actionExecutedContext.Request.Content.ReadAsStringAsync().Result;
            }
            catch (Exception) { }

            var data = new
            {
                Version = "v1.0",
                Application = "AppExemplo",
                DataHora = ((DateTime)dataHoraRequest),
                Duracao = DateTime.Now - ((DateTime)dataHoraRequest),
                Source = "ActionFilterAttribute",
                Url = actionExecutedContext.Request.RequestUri,
                ControllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                HttpMethod = actionExecutedContext.Request.Method.Method,
                Host = actionExecutedContext.Request.Headers.Host,
                Usuario = identity.Name,
                Email = identity.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Email)?.Value,
                UserData = userData,
                Successo = actionExecutedContext.Exception == null,
                Request_Headers = JsonConvert.SerializeObject(actionExecutedContext.Request.Headers),
                ResponseCode = actionExecutedContext.Response?.StatusCode,
                Request_Content = Request_Content
            };

            _logger.AddLog(data);
        }
    }
}