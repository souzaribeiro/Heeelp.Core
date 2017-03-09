using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CurrencyController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly ICurrencyDao _CurrencyDao;
        private long fileId;


        public CurrencyController(ICommandBus bus, ICurrencyDao _CurrencyDao) : base()
        {

            this.bus = bus;
            this._CurrencyDao = _CurrencyDao;
        }


        /// <summary>
        /// Lista moedas
        /// </summary>
        /// <remarks>Get a list Currency</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [HttpGet]
        [ResponseType(typeof(CurrencyDTO))]
        public HttpResponseMessage ListCurrencys()
        {
            IEnumerable<CurrencyListDTO> CurrencyList = _CurrencyDao.ListCurrencys();

            return Request.CreateResponse(HttpStatusCode.OK, CurrencyList);
        }





        /// <summary>
        /// Lista moedas
        /// </summary>
        /// <param name="id">CurrencyId</param>
        /// <remarks>Get a list Currency</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(CurrencyDTO))]
        public HttpResponseMessage GetCurrency(byte id)
        {
            CurrencyDTO currency = _CurrencyDao.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, currency);
        }






    }
}
