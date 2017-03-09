using Heeelp.Core.Command.ExternalModules;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/classified")]
    public class ClassifiedController : BaseController
    {
        private readonly ICommandBus bus;
        private readonly IPersonDao _PersonDao;
        private object _clientPromotion;

        public ClassifiedController(ICommandBus bus, IPersonDao _PersonDao) : base()
        {
            this.bus = bus;
            this._PersonDao = _PersonDao;
        }

        #region Query Classified

        /// <summary>
        /// Busca Promocao
        /// </summary>
        /// <param name="id">id</param>
        /// <remarks>Busca Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("getPromotion/{id}")]
        public HttpResponseMessage GetPromotion(int id)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                var resultTask = _client.GetAsync("api/classified/getPromotion?promotionId=" + id).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotion Handler: Erro ao enviar web.api classified:  status: " + resultTask.StatusCode);
                }

                var classified = resultTask.Content.ReadAsAsync<PromotionClassifiedDTO>().Result;
                return Request.CreateResponse(HttpStatusCode.OK, classified);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
                //throw ex;
            }
        }


        /// <summary>
        /// Busca Promocao
        /// </summary>
        /// <param name="id">id</param>
        /// <remarks>Busca Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(Guid))]
        [HttpGet]
        [Authorize]
        [Route("getFullPromotion/{id}")]
        public HttpResponseMessage GetFullPromotion(int id)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                var resultTask = _client.GetAsync("api/classified/GetFullPromotion?promotionId=" + id).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotion Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }

                var promotion = resultTask.Content.ReadAsAsync<PromotionClassifiedDTO>();
                return Request.CreateResponse(HttpStatusCode.OK, promotion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Busca Promocao para Aprovacao
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="orderBy">orderBy</param>
        /// <remarks>Busca Promocao para Aprovacao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("ListPromotionClassifiedPerPageWaitingApproval/{page}/{orderBy}")]
        public HttpResponseMessage ListPromotionClassifiedPerPageWaitingApproval(int page, int orderBy)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                var resultTask = _client.GetAsync("api/classified/ListPromotionClassifiedPerPageWaitingApproval?page=" + page + "&orderBy=" + orderBy).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotion Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }

                var promotion = resultTask.Content.ReadAsAsync<IEnumerable<PromotionClassifiedListDTO>>();
                return Request.CreateResponse(HttpStatusCode.OK, promotion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca Promocoes
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="orderBy">orderBy</param>
        /// <remarks>Busca Promocoes</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("ListPromotionClassifiedPerPage/{page}/{orderBy}/{personId}")]
        public HttpResponseMessage ListPromotionClassifiedPerPage(int page, int orderBy, int id)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                var resultTask = _client.GetAsync("api/classified/ListPromotionClassifiedPerPage?page=" + page + "&orderBy=" + orderBy + "&personId=" + id).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotion Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }

                var promotion = resultTask.Content.ReadAsAsync<IEnumerable<PromotionClassifiedListDTO>>();
                return Request.CreateResponse(HttpStatusCode.OK, promotion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca Promocoes
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="orderBy">orderBy</param>
        /// <remarks>Busca Promocoes</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("ListPromotionClassifiedPerPage/{page}/{orderBy}")]
        public HttpResponseMessage ListPromotionClassifiedPerPage(int page, int orderBy)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                var resultTask = _client.GetAsync("api/classified/ListPromotionClassifiedPerPage?page=" + page + "&orderBy=" + orderBy).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotion Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }

                var promotion = resultTask.Content.ReadAsAsync<IEnumerable<PromotionClassifiedListDTO>>();
                return Request.CreateResponse(HttpStatusCode.OK, promotion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
