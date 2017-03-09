using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Heeelp.Core.WebAPI.Controllers
{
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LanguageController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly ILanguageDao _LanguageDao;
       

        public LanguageController(ICommandBus bus, ILanguageDao _languageDao) : base()
        {
            this.bus = bus;
            this._LanguageDao = _languageDao;
        }
 

        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListLanguages()
        {
            IEnumerable<LanguageListDTO> languagesList = _LanguageDao.ListLanguages();

            return Request.CreateResponse(HttpStatusCode.OK, languagesList);
        }


        [HttpGet]
        [Authorize(Roles ="Managed")]
        public HttpResponseMessage Managed()
        {
            

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize(Roles = "Public")]
        public HttpResponseMessage Public()
        {


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public HttpResponseMessage Teste()
        {


            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}
