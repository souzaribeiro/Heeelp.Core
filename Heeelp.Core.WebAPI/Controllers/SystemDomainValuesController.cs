using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using System;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SystemDomainValuesController : BaseController
    {
        private readonly ICommandBus bus;
        private readonly ICurrencyDao _CurrencyDao;
        private readonly ILanguageDao _LanguageDao;
        private readonly ICountryDao _CountryDao;
        private readonly INeighbourhoodDao _NeighbourhoodDao;
        private readonly ICityDao _CityDao;
        private readonly IStateDao _StateDao;
        private readonly IPersonDao _PersonDao;
        private readonly IPersonOriginTypeDao _PersonOriginTypeDao;
        private readonly IPersonProfileDao _PersonProfileDao;
        private readonly IPersonStatusDao _PersonStatusDao;
        private readonly IPersonTypeDao _PersonTypeDao;
        private readonly IPersonAddressDao _PersonAddressDao;
        private IPersonExpertiseDao _PersonExpertiseDao;
        private readonly IFileTempDao _FileTemp;
        private readonly ISecuritySourceDao _SecurityDao;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;
        private long? campaignID;
        private long? inviteId;


        private long fileId;


        public SystemDomainValuesController(ICommandBus bus, ICurrencyDao _CurrencyDao, ILanguageDao _LanguageDao, ICountryDao _countryDao,
                                            INeighbourhoodDao _neighbourhoodDao, IStateDao _stateDao, ICityDao _cityDao,
                                            IPersonDao _PersonDao, IPersonOriginTypeDao _personOriginTypeDao, IPersonProfileDao _personProfileDao,
                                            IPersonStatusDao _personStatusDao, IPersonTypeDao _personTypeDao, IPersonAddressDao _personAddressDao,
                                            IPersonExpertiseDao _personExpertiseDao, IFileTempDao fileTemp, ISecuritySourceDao _SecurityDao) : base()
        {
            this.bus = bus;
            this._CurrencyDao = _CurrencyDao;
            this._LanguageDao = _LanguageDao;
            this._CountryDao = _countryDao;
            this._NeighbourhoodDao = _neighbourhoodDao;
            this._StateDao = _stateDao;
            this._CityDao = _cityDao;
            this._PersonDao = _PersonDao;
            this._PersonOriginTypeDao = _personOriginTypeDao;
            this._PersonProfileDao = _personProfileDao;
            this._PersonStatusDao = _personStatusDao;
            this._PersonTypeDao = _personTypeDao;
            this._PersonAddressDao = _personAddressDao;
            this._listFileTemp = new List<int>();
            this._FileTemp = fileTemp;
            this._PersonExpertiseDao = _personExpertiseDao;
            this._SecurityDao = _SecurityDao;
        }


        #region Currency
        /// <summary>
        /// Lista moedas
        /// </summary>
        /// <remarks>Get a list Currency</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(CurrencyDTO))]
        [Route("api/SystemDomainValues/ListCurrencys")]
        public HttpResponseMessage ListCurrencys()
        {

            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<CurrencyListDTO> CurrencyList = _CurrencyDao.ListCurrencys();

                return Request.CreateResponse(HttpStatusCode.OK, CurrencyList);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar listagem de Moedas", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        /// <summary>
        /// Lista moedas
        /// </summary>
        /// <param name="id">CurrencyId</param>
        /// <remarks>Get a list Currency</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(CurrencyDTO))]
        [Route("api/SystemDomainValues/GetCurrency/{currendyId}")]
        public HttpResponseMessage GetCurrency(byte currendyId)
        {

            try
            {
                Claims claims = new Claims().Values();

                CurrencyDTO currency = _CurrencyDao.Get(currendyId);

                return Request.CreateResponse(HttpStatusCode.OK, currency);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Moeda", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }
        #endregion

        #region Language
        [HttpGet]
        [Route("api/SystemDomainValues/ListLanguages")]
        public HttpResponseMessage ListLanguages()
        {

            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<LanguageListDTO> languagesList = _LanguageDao.ListLanguages();

                return Request.CreateResponse(HttpStatusCode.OK, languagesList);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar listagem de Idiomas", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        #endregion

        #region Location   
        [HttpGet]
        [Route("api/SystemDomainValues/ListCountrys")]
        public HttpResponseMessage ListCountrys()
        {

            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<CountryListDTO> CountryList = _CountryDao.ListCountrys();

                return Request.CreateResponse(HttpStatusCode.OK, CountryList);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar listagem de Paises", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }
        [Route("api/SystemDomainValues/GetCountry/{countryId}")]
        public HttpResponseMessage GetCountry(int countryId)
        {
            try
            {
                Claims claims = new Claims().Values();

                CountryDTO country = _CountryDao.Get(countryId);

                return Request.CreateResponse(HttpStatusCode.OK, country);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar País", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        [Route("api/SystemDomainValues/GetNeighbourhood/{neighbourhoodId}")]
        public HttpResponseMessage GetNeighbourhood(int neighbourhoodId)
        {
            try
            {
                Claims claims = new Claims().Values();

                NeighbourhoodDTO neighbourhood = _NeighbourhoodDao.Get(neighbourhoodId);

                return Request.CreateResponse(HttpStatusCode.OK, neighbourhood);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Bairro", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }
        [Route("api/SystemDomainValues/GetState/{StateId}")]
        public HttpResponseMessage GetState(int StateId)
        {
            try
            {
                Claims claims = new Claims().Values();

                StateDTO state = _StateDao.Get(StateId);

                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Estado", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        [Route("api/SystemDomainValues/GetCity/{cityId}")]
        public HttpResponseMessage GetCity(int cityId)
        {
            try
            {
                Claims claims = new Claims().Values();

                CityDTO city = _CityDao.Get(cityId);

                return Request.CreateResponse(HttpStatusCode.OK, city);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Cidade", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [Route("api/SystemDomainValues/GetStates")]
        public HttpResponseMessage GetStates()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.EnumState>();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/SystemDomainValues/GetCities")]
        public HttpResponseMessage GetCities()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.EnumCity>();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Person&User

        [HttpGet]
        [Route("api/SystemDomainValues/ListPersonStatus")]
        public HttpResponseMessage ListPersonStatus()
        {
            IEnumerable<PersonStatusListDTO> PersonStatusList = _PersonStatusDao.ListPersonStatus();

            return Request.CreateResponse(HttpStatusCode.OK, PersonStatusList);
        }

        [HttpGet]
        [Route("api/SystemDomainValues/ListPersonProfiles")]
        public HttpResponseMessage ListPersonProfiles()
        {
            IEnumerable<PersonProfileListDTO> PersonProfileList = _PersonProfileDao.ListPersonProfiles();

            return Request.CreateResponse(HttpStatusCode.OK, PersonProfileList);
        }

        [HttpGet]
        [Route("api/SystemDomainValues/ListPersonTypes")]
        public HttpResponseMessage ListPersonTypes()
        {
            IEnumerable<PersonTypeListDTO> PersonTypeList = _PersonTypeDao.ListPersonTypes();

            return Request.CreateResponse(HttpStatusCode.OK, PersonTypeList);
        }

        [HttpGet]
        [Route("api/SystemDomainValues/ListPersonOriginTypes")]
        public HttpResponseMessage ListPersonOriginTypes()
        {
            IEnumerable<PersonOriginTypeListDTO> PersonOriginTypeList = _PersonOriginTypeDao.ListPersonOriginTypes();

            return Request.CreateResponse(HttpStatusCode.OK, PersonOriginTypeList);
        }


        #endregion

        #region SecuritySources
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListSecuritySources()
        {
            IEnumerable<SecuritySourceListDTO> SecurityList = _SecurityDao.ListSecuritySource();

            return Request.CreateResponse(HttpStatusCode.OK, SecurityList);
        }

        #endregion

        #region Version          
        [HttpGet]
        public HttpResponseMessage GetLastestVersion()
        {
            var version = CustomConfiguration.HeeelpClientVersion.ToString();
            return Request.CreateResponse(HttpStatusCode.OK, version);
        }
        #endregion
    }
}
