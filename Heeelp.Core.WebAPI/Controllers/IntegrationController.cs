using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Integration;

namespace Heeelp.Core.WebAPI.Controllers
{
    public class IntegrationController : BaseController
    {

        [HttpGet]
        [Authorize(Roles = "IntegrationHeeelp")]
        public HttpResponseMessage CheckPermission(CheckPermissionInputDTO checkPermission)
        {
            {
                try
                {                    
                    Claims claims = new Claims().Values();
                    
                    //todo: necessario desenvolver a consulta se esse usuario tem permissoes nessa person. 
                    //Considerar o cenario em que um facilitador fara acoes em nome de uma empresa e aguardara que a seguir um gestor aprove ou aceite a acao
                    //ex.: Um facilitador cria uma promocao para um restaurante e o gestor do restaurante depois tem de aprovar a promocao antes da mesma ser publicada
                    //o metodo responde apenas verdadeiro ou falso
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                    
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
