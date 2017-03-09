using System;
using System.Net.Http;
using System.Web.Mvc;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Command.ExternalModules;
using Heeelp.Core.Infrastructure.Messaging;
using System.Net;
using Heeelp.Core.Logging;

namespace Heeelp.Core.WebAPI.Controllers
{
    public class AccountGatewayController : BaseController
    {
        private readonly ICommandBus bus;

        public AccountGatewayController(ICommandBus bus) : base()
        {
            this.bus = bus;
        }

        
        /// <summary>
        /// Obter Extrato C/C
        /// </summary>
        /// <param name="userid">userid</param>
        /// <remarks>Recuperar Senha</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("api/account/GetCredits/")]
        public HttpResponseMessage GetExtractPersonAccount()
        {
            Claims claims = new Claims().Values();
            ExtractOutputDTO ret = null;
            try
            {
                HttpClient _client;
                _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiAccount);
                var response = _client.GetAsync("api/Person/GetExtractPersonAccount/?id=" + claims.personId).Result;
                if (response.IsSuccessStatusCode)
                {
                    ret = response.Content.ReadAsAsync<ExtractOutputDTO>().Result;
                    try
                    {
                        var _clientPromotion = new HttpClient();
                        _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                        var resultTask = _clientPromotion.GetAsync("api/Coupon/GetMyValueSavedCoupon?id=" + claims.personId).Result;
                        if (resultTask.IsSuccessStatusCode)
                        {
                            ret.MyValueSaved = resultTask.Content.ReadAsAsync<decimal>().Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error("Erro ao recuperar Valor economizado com cupons", ex);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, ret);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Account not find");
                }

            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao recuperar senha", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            

        }
        



    }
}