using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Command.User;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Drawing;
using Heeelp.Core.Command.ExternalModules;
using System.Linq;
using Newtonsoft.Json;
using Heeelp.Core.Domain.ReadModel;

namespace Heeelp.Core.WebAPI.Controllers
{


    public class UserSessionController : BaseController
    {
        private readonly ICommandBus bus;
        private long fileId;
        private List<int> _listFileTemp;
        private List<FileTempDTO> listFileTemp;
        private List<FIleServer> fs;
        private IStorage _storage;
        private string _containerName;
        private readonly IFileTempDao _FileTemp;
        private IPersonDao _person;
        private readonly IPersonSkinDao _PersonSkinDao;
        private IUserDao _userDao;
        private IContractDao _contractDao;
        private IExpertiseDao _expertiseDao;

        public UserSessionController(ICommandBus bus, IFileTempDao fileTemp, IPersonDao person, IUserDao userDao, IContractDao contractDao, IExpertiseDao expertiseDao,
            IPersonSkinDao personSkinDao) : base()
        {
            this.bus = bus;
            this._FileTemp = fileTemp;
            this._listFileTemp = new List<int>();
            this._person = person;
            _storage = new StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
            this._userDao = userDao;
            this._contractDao = contractDao;
            this._expertiseDao = expertiseDao;
            this._PersonSkinDao = personSkinDao;
        }

        public UserSessionController()
        {
        }



        [HttpGet]
        [HttpPost]
        [Authorize]
        [Route("api/UserSession/CheckIsAuthenticated")]
        public HttpResponseMessage CheckIsAuthenticated()
        {
            Claims claims = new Claims().Values();
            //var Description = _PersonSkinDao.GetPersonBenefitClub(claims.personId);

            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [HttpGet]
        [HttpPost]
        [Route("api/UserSession/AddUserSession")]
        public HttpResponseMessage AddUserSession(AddUserSessionDto userSession)
        {
            Guid IntegrationCode = Guid.NewGuid();
            Claims claims = new Claims().Values();
            if (ModelState.IsValid)
            {
                try
                {
                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
                    userSession.IntegrationCode = IntegrationCode;     
                    userSession.UserId = claims.userSystemId;
                    var resultTask = _clientPromotion.PostAsJsonAsync("api/UserSession/AddUserSession", userSession).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        var error = "AddUserSession Core Handler: Erro ao enviar web.api Contab:  status: " + resultTask.StatusCode;
                        LogManager.Error(error);
                        throw new Exception(error);
                    }                                                                   
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, IntegrationCode);
        }



    }

}
