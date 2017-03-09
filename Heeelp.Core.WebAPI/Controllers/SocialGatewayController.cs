using Heeelp.Core.Common;
using Heeelp.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Heeelp.Core.WebAPI.Controllers
{
    public class SocialGatewayController : BaseController
    {


        [HttpGet]
        public HttpResponseMessage GetPersonThreads()
        {
            {
                try
                {
                    //todo: o valor da usersession deveria estar presente nas claims!! Ao fazer a consulta, devemos registrar que o usuario fez a consulta, com data e hora, para fins de rastreamento
                    Claims claims = new Claims().Values();


                    var _clientSocial = new HttpClient();
                    _clientSocial.BaseAddress = new Uri(CustomConfiguration.WebApiSocial);
                    var resultTask = _clientSocial.PostAsJsonAsync("api/Messaging/GetPersonThreads", claims.personIntegrationCode).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("Failure getting person message threads.  Status: " + resultTask.StatusCode);
                    }
                    else
                    {
                        //todo: o retorno do metodo pode ser assim? Ou o correto seria extrair do result um objeto colecao de MessageThreadLIst?
                        return Request.CreateResponse(HttpStatusCode.OK, resultTask);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }

        }


    }
}
