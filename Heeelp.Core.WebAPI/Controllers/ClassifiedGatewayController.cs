using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Heeelp.Core.WebAPI.Controllers
{
    public class ClassifiedGatewayController : BaseController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> ListPromotion()
        {

            try
            {
                Claims claims = new Claims().Values();
                HttpClient _client;
                _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                var response = await _client.GetAsync("api/Classified/ListPromotion/");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var ret = await response.Content.ReadAsAsync<PromotionClassifiedDTO>();
                    return Request.CreateResponse(HttpStatusCode.OK, ret);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Classified list not found");
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
