using Heeelp.Core.Command.Location;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;
using Heeelp.Core.Logging;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LocationController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly ICountryDao _CountryDao;
        private readonly INeighbourhoodDao _NeighbourhoodDao;
        private readonly ICityDao _CityDao;
        private readonly IStateDao _StateDao;
        private long fileId;

        public LocationController(ICommandBus bus, ICountryDao _countryDao, INeighbourhoodDao _neighbourhoodDao, IStateDao _stateDao, ICityDao _cityDao) : base()
        {

            this.bus = bus;
            this._CountryDao = _countryDao;
            this._NeighbourhoodDao = _neighbourhoodDao;
            this._StateDao = _stateDao;
            this._CityDao = _cityDao;
        }

        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListCountrys()
        {
            IEnumerable<CountryListDTO> CountryList = _CountryDao.ListCountrys();

            return Request.CreateResponse(HttpStatusCode.OK, CountryList);
        }



        public HttpResponseMessage GetCountry(int id)
        {
            CountryDTO country = _CountryDao.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, country);
        }

        public HttpResponseMessage GetNeighbourhood(int id)
        {
            NeighbourhoodDTO neighbourhood = _NeighbourhoodDao.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, neighbourhood);
        }

        public HttpResponseMessage GetState(int id)
        {
            StateDTO state = _StateDao.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, state);
        }

        public HttpResponseMessage GetCity(int id)
        {
            CityDTO city = _CityDao.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, city);
        }
    }
}
