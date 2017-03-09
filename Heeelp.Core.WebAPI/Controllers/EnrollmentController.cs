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


    public class EnrollmentController : BaseController
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

        public EnrollmentController(ICommandBus bus, IFileTempDao fileTemp, IPersonDao person, IUserDao userDao, IContractDao contractDao, IExpertiseDao expertiseDao,
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

        public EnrollmentController()
        {
        }



        [HttpGet]
        [HttpPost]
        [Route("api/enrollment/GetPersonBenefitClubDescription")]
        public HttpResponseMessage GetPersonBenefitClubDescription()
        {

            Claims claims = new Claims().Values();
            var Description = _PersonSkinDao.GetPersonBenefitClub(claims.personId);

            return Request.CreateResponse(HttpStatusCode.OK, Description);
        }

        [HttpPost]
        [Route("api/enrollment/GetPersonBenefitClubDescriptionByDomain/")]
        public HttpResponseMessage GetPersonBenefitClubDescriptionByDomain(SkinDomainDTO skinDomain)
        {

            Claims claims = new Claims().Values();
            var Description = _PersonSkinDao.GetPersonBenefitClubByDomain(skinDomain.Url);

            return Request.CreateResponse(HttpStatusCode.OK, Description);
        }


        [HttpGet]
        [HttpPost]
        [Route("api/enrollment/GetPersonCompanySkin")]
        public HttpResponseMessage GetPersonCompanySkin(SkinDomainDTO skinDomain)
        {
            PersonSkinDTO personSkin = _PersonSkinDao.GetPersonSkinBySubDomain(skinDomain.Url);

            return Request.CreateResponse(HttpStatusCode.OK, personSkin);
        }

        #region Add       

        /// <summary>
        /// Adiciona Nova Empresa Parceira
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Add nova Empresa Parceira</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Route("api/enrollment/AddPartnerCompany")]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        public HttpResponseMessage AddPartnerCompany(NewCompanyDTO company)
        {

            Claims claims = new Claims().Values();
            Guid IntegrationCode = Guid.NewGuid();

            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddPersonCompanyPartnerCommand();

                    command.IntegrationCode = IntegrationCode;
                    command.CompanyName = company.CompanyName;
                    command.CompanyFantasyName = company.FantasyName;
                    command.CompanyPhoneNumber = company.CompanyPhoneNumber;
                    command.ManagerName = company.ManagerName;
                    command.ManagerPhoneNumber = company.ManagerSmartPhoneNumber;
                    command.UserSystemId = claims.userSystemId;
                    command.ManagerEmail = company.ManagerEmail;
                    command.CreatedBy = claims.userSystemId;
                    command.EnrollmentIP = HttpContext.Current.Request.UserHostAddress;
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.PainelAdministrativoHeeelp;
                    command.SkinId = company.SkinId;
                    command.CustomClubName = company.CustomClubName;
                    command.CustomHeeelpPersonDomain = company.CustomHeeelpPersonDomain;
                    command.PersonIntegrationFatherId = claims.personIntegrationCode;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK, IntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Empresa Coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Adiciona Nova empresa de Coworking
        /// </summary>
        /// <param name="coworking">coworking</param>
        /// <remarks>Add nova empresa de coworking</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Authorize(Roles = "AdminHeeelp")]
        [Route("api/enrollment/AddCoworking")]
        public HttpResponseMessage AddCoworking(NewCompanyDTO coworking)
        {
            Claims claims = new Claims().Values();
            Guid IntegrationCode = Guid.NewGuid();

            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddPersonCoworkingCommand();

                    command.IntegrationCode = IntegrationCode;
                    command.CompanyName = coworking.CompanyName;
                    command.CompanyPhoneNumber = coworking.CompanyPhoneNumber;
                    command.ManagerName = coworking.ManagerName;
                    command.ManagerPhoneNumber = coworking.ManagerSmartPhoneNumber;
                    command.UserSystemId = claims.userSystemId;
                    command.ManagerEmail = coworking.ManagerEmail;
                    command.CreatedBy = claims.userSystemId;
                    command.EnrollmentIP = HttpContext.Current.Request.UserHostAddress;
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.PainelAdministrativoHeeelp;
                    command.SkinId = 3;
                    command.CustomClubName = "Gowork Wins"; // coworking.CustomClubName;
                    command.CustomHeeelpPersonDomain = "https://goworkwins.heeelp.com";// coworking.CustomHeeelpPersonDomain;

                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK, IntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Empresa Coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Adiciona Novo PRestador de Servicos
        /// </summary>
        /// <param name="serviceProvider">serviceProvider</param>
        /// <remarks>Add novo Prestador de Servicos</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Authorize(Roles = "AdminHeeelp")]
        [Route("api/enrollment/AddServiceProvider")]
        public HttpResponseMessage AddServiceProvider(NewCompanyDTO serviceProvider)
        {

            Claims claims = new Claims().Values();
            Guid integrationCode = Guid.NewGuid();

            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddPersonServiceProviderCommand();

                    command.IntegrationCode = integrationCode;
                    command.CompanyName = serviceProvider.CompanyName;
                    command.CompanyPhoneNumber = serviceProvider.CompanyPhoneNumber;
                    command.CompanyFantasyName = serviceProvider.FantasyName;
                    command.ManagerName = serviceProvider.ManagerName;
                    command.ManagerPhoneNumber = serviceProvider.ManagerSmartPhoneNumber;
                    command.UserSystemId = claims.userSystemId;
                    command.ManagerEmail = serviceProvider.ManagerEmail;
                    command.CreatedBy = claims.userSystemId;
                    command.EnrollmentIP = HttpContext.Current.Request.UserHostAddress;
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.PainelAdministrativoHeeelp;
                    command.SkinId = (byte)GeneralEnumerators.EnumSkinType.Default;
                    command.CustomClubName = serviceProvider.CustomClubName;
                    command.CustomHeeelpPersonDomain = serviceProvider.CustomHeeelpPersonDomain;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK, integrationCode.ToString());
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Empresa Coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Adiciona Novo Colaborador a uma Empresa
        /// </summary>
        /// <param name="employee">employee</param>
        /// <remarks> Adiciona Novo Colaborador a uma Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewEmployeeDTO))]
        [Authorize(Roles = "GestorCoWorking,GestorRH,GestorPrestadorServico,AdminHeeelp")]
        [Route("api/enrollment/AddEmployee")]
        public HttpResponseMessage AddEmployee(NewEmployeeDTO employee)
        {

            try
            {
                Claims claims = new Claims().Values();
                Guid integrationCode = Guid.NewGuid();

                if (ModelState.IsValid)
                {
                    var command = new AddPersonEmployeeCommand();
                    command.IntegrationCode = integrationCode;
                    command.Name = employee.Name;
                    command.PhoneNumber = employee.SmartPhoneNumber;
                    command.CreatedBy = claims.userSystemId;
                    command.Email = employee.Email;
                    command.SecundaryEmail = employee.SecundaryEmail;
                    command.EnrollmentIP = HttpContext.Current.Request.UserHostAddress;
                    command.PersonIntegrationFatherId = claims.personIntegrationCode;
                    command.UserProfileId = (GeneralEnumerators.EnumUserProfile)employee.UserProfileId;
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.PainelAdministrativoGestor;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK, integrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        /// <summary>
        /// Adiciona Foto Perfil
        /// /// </summary>
        /// <param name="photoLogo">photoLogo</param>
        /// <remarks> Adiciona Foto Perfil</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(PhotoLogoDTO))]
        [Authorize]
        [Route("api/enrollment/AddOwnPhotoProfile/{x}/{y}/{width}/{height}/{scale}")]
        public async Task<HttpResponseMessage> AddOwnPhotoProfile(int x, int y, int width, int height, float scale)
        {
            var provider = new MultipartMemoryStreamProvider();
            NameValueCollection parameters = HttpContext.Current.Request.Params;
            listFileTemp = new List<FileTempDTO>();
            string filePath = string.Empty;
            if (Request.Content.IsMimeMultipartContent())
            {

                try
                {


                    await Request.Content.ReadAsMultipartAsync(provider);
                    fs = new List<FIleServer>();
                    int count = 0;
                    foreach (var file in HttpContext.Current.Request.Files)
                    {
                        var f = provider.Contents[count];
                        string fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                        string mimeType = new MimeType().GetMimeType(fileName);
                        var stream = (HttpContext.Current.Request.Files[count]).InputStream;

                        byte[] bytesInStream = ImageUtility.CropImage(stream, x, y, width, height, scale);


                        FileTempDTO ft = new FileTempDTO();
                        ft.FileIntegrationCode = Guid.NewGuid();
                        filePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                        ft.FilePath = filePath;
                        ft.OriginalName = fileName;
                        try
                        {
                            using (Image img = Image.FromStream(stream: stream,
                           useEmbeddedColorManagement: false,
                           validateImageData: false))
                            {
                                ft.Width = img.PhysicalDimension.Width.ToString();
                                ft.Height = img.PhysicalDimension.Height.ToString();
                            }
                            count++;
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Erro ao recuperar dimensoes da imagen", ex);
                        }


                        try
                        {
                            //add list de arquivos
                            listFileTemp.Add(ft);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }


                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("User Profile image Error:{0}", ex));
                }



                try
                {
                    if (_listFileTemp != null)
                    {
                        Claims claims = new Claims().Values();
                        var command = new UploadPhotoUserCommand();
                        command.IntegrationCode = claims.userIntegrationCode;
                        command.ListFileTemp = listFileTemp;
                        command.CreatedBy = claims.userSystemId;
                        this.bus.Send(command);
                    }
                }
                catch (System.Exception e)
                {
                    LogManager.Error("Erro ao Add Photo colaborador", e);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, filePath);

        }


        /// <summary>
        /// Adiciona Foto Colaborador
        /// /// </summary>
        /// <param name="photoLogo">photoLogo</param>
        /// <remarks> Adiciona Foto Colaborador</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(PhotoLogoDTO))]
        [Authorize]
        [Route("api/enrollment/AddEmployeePhotoProfile/{integrationId}/{x}/{y}/{width}/{height}")]
        public async Task<HttpResponseMessage> AddEmployeePhotoProfile(Guid integrationId, int x, int y, int width, int height)
        {

            var provider = new MultipartMemoryStreamProvider();
            NameValueCollection parameters = HttpContext.Current.Request.Params;
            listFileTemp = new List<FileTempDTO>();
            if (Request.Content.IsMimeMultipartContent())
            {

                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                    fs = new List<FIleServer>();
                    int count = 0;
                    foreach (var file in HttpContext.Current.Request.Files)
                    {
                        var f = provider.Contents[count];
                        string fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                        string mimeType = new MimeType().GetMimeType(fileName);
                        var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                        byte[] bytesInStream = new byte[stream.Length];
                        stream.Read(bytesInStream, 0, bytesInStream.Length);
                        bytesInStream = ImageUtility.CropImage(bytesInStream, x, y, width, height);

                        FileTempDTO ft = new FileTempDTO();
                        ft.FileIntegrationCode = Guid.NewGuid();
                        ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                        ft.OriginalName = fileName;
                        try
                        {
                            using (Image img = Image.FromStream(stream: stream,
                           useEmbeddedColorManagement: false,
                           validateImageData: false))
                            {
                                ft.Width = img.PhysicalDimension.Width.ToString();
                                ft.Height = img.PhysicalDimension.Height.ToString();
                            }
                            count++;
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Erro ao recuperar dimensoes da imagen", ex);
                        }


                        try
                        {
                            //add list de arquivos
                            listFileTemp.Add(ft);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }


                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("User Profile image Error:{0}", ex));
                }



                try
                {
                    if (_listFileTemp != null)
                    {
                        Claims claims = new Claims().Values();
                        var command = new UploadPhotoUserCommand();
                        command.IntegrationCode = integrationId;
                        command.ListFileTemp = listFileTemp;
                        command.CreatedBy = claims.userSystemId;
                        this.bus.Send(command);

                    }


                }
                catch (System.Exception e)
                {
                    LogManager.Error("Erro ao Add Photo colaborador", e);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }


        /// <summary>
        /// Adiciona Img Company
        /// /// </summary>
        /// <param name="companyId">companyId</param>
        /// <remarks> Adiciona Img Company</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(Guid))]
        [Authorize]
        [Route("api/enrollment/AddCompanyFile/{companyId}")]
        public async Task<HttpResponseMessage> AddCompanyFile(Guid companyId)
        {

            var provider = new MultipartMemoryStreamProvider();
            //NameValueCollection parameters = HttpContext.Current.Request.Params;
            listFileTemp = new List<FileTempDTO>();
            if (Request.Content.IsMimeMultipartContent())
            {

                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                    fs = new List<FIleServer>();
                    int count = 0;
                    foreach (var file in HttpContext.Current.Request.Files)
                    {
                        var f = provider.Contents[count];
                        string fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                        string mimeType = new MimeType().GetMimeType(fileName);
                        var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                        byte[] bytesInStream = new byte[stream.Length];
                        stream.Read(bytesInStream, 0, bytesInStream.Length);

                        FileTempDTO ft = new FileTempDTO();
                        ft.FileIntegrationCode = Guid.NewGuid();
                        ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                        ft.OriginalName = fileName;
                        try
                        {
                            using (Image img = Image.FromStream(stream: stream,
                           useEmbeddedColorManagement: false,
                           validateImageData: false))
                            {
                                ft.Width = img.PhysicalDimension.Width.ToString();
                                ft.Height = img.PhysicalDimension.Height.ToString();
                            }
                            count++;
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Erro ao recuperar dimensoes da imagen", ex);
                        }


                        try
                        {
                            //add list de arquivos
                            listFileTemp.Add(ft);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }


                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("Person image Error:{0}", ex));
                }



                try
                {
                    if (_listFileTemp != null)
                    {
                        Claims claims = new Claims().Values();
                        var command = new UploadFilePersonCommand();
                        command.IntegrationCode = companyId;
                        command.ListFileTemp = listFileTemp;
                        command.CreatedBy = claims.userSystemId;
                        this.bus.Send(command);

                    }


                }
                catch (System.Exception e)
                {
                    LogManager.Error("Erro ao Add Photo colaborador", e);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        /// <summary>
        /// Adiciona Img Company
        /// /// </summary>
        /// <param name="companyId">companyId</param>
        /// <remarks> Adiciona Img Company</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(Guid))]
        [Authorize]
        [Route("api/enrollment/AddCompanyLogo/{companyId}")]
        public async Task<HttpResponseMessage> AddCompanyLogo(Guid companyId)
        {

            var provider = new MultipartMemoryStreamProvider();
            //NameValueCollection parameters = HttpContext.Current.Request.Params;
            listFileTemp = new List<FileTempDTO>();
            if (Request.Content.IsMimeMultipartContent())
            {

                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                    fs = new List<FIleServer>();
                    int count = 0;
                    foreach (var file in HttpContext.Current.Request.Files)
                    {
                        var f = provider.Contents[count];
                        string fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                        string mimeType = new MimeType().GetMimeType(fileName);
                        var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                        byte[] bytesInStream = new byte[stream.Length];
                        stream.Read(bytesInStream, 0, bytesInStream.Length);

                        FileTempDTO ft = new FileTempDTO();
                        ft.FileIntegrationCode = Guid.NewGuid();
                        ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                        ft.OriginalName = fileName;
                        try
                        {
                            using (Image img = Image.FromStream(stream: stream,
                           useEmbeddedColorManagement: false,
                           validateImageData: false))
                            {
                                ft.Width = img.PhysicalDimension.Width.ToString();
                                ft.Height = img.PhysicalDimension.Height.ToString();
                            }
                            count++;
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Erro ao recuperar dimensoes da imagen", ex);
                        }


                        try
                        {
                            //add list de arquivos
                            listFileTemp.Add(ft);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }


                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("Person image Error:{0}", ex));
                }



                try
                {
                    if (_listFileTemp != null)
                    {
                        Claims claims = new Claims().Values();
                        var command = new UploadLogoPersonCommand();
                        command.IntegrationCode = companyId;
                        command.ListFileTemp = listFileTemp;
                        command.CreatedBy = claims.userSystemId;
                        this.bus.Send(command);

                    }


                }
                catch (System.Exception e)
                {
                    LogManager.Error("Erro ao Add Photo colaborador", e);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }


        /// <summary>
        /// Adiciona Informações da Company
        /// /// </summary>
        /// <param name="companyInformation">companyInformation</param>
        /// <remarks>Adiciona Informações da Company</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(CompanyInformationDTO))]
        [Authorize]
        [Route("api/enrollment/AddCompanyInformation")]
        public HttpResponseMessage AddCompanyInformation(CompanyInformationDTO companyInformation)
        {

            try
            {

                Claims claims = new Claims().Values();
                if (ModelState.IsValid && companyInformation.IntegrationCode != Guid.Empty)
                {
                    var address = new AddCompanyInformationCommand();

                    address.StreetName = companyInformation.StreetName;
                    address.Number = companyInformation.Number;
                    address.Complement = companyInformation.Complement;
                    address.Neighbourhood = companyInformation.Neighbourhood;
                    address.PostCode = companyInformation.PostCode;
                    address.ContactEmail = companyInformation.ContactEmail;
                    address.IntegrationCode = companyInformation.IntegrationCode;
                    address.CreatedBy = claims.userSystemId;
                    address.DocumentNumber = companyInformation.DocumentNumber;
                    address.StateId = companyInformation.StateId;
                    address.CityId= companyInformation.CityId;

                    this.bus.Send(address);
                }



                return Request.CreateResponse(HttpStatusCode.OK, new { OK = "OK" });
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("Add Image Address Error:{0}", ex));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        ///// <summary>
        ///// Get Membros Empresa Associada
        ///// </summary>
        ///// <param name="personId">personId</param>
        ///// <param name="userId">userId</param>
        ///// <remarks>Get Membros Empresa Associada</remarks>
        ///// <returns></returns>
        ///// <response code="200"></response>
        //[HttpGet]
        //[ResponseType(typeof(int))]
        //[Authorize]
        //public async Task<HttpResponseMessage> GetAssociateComapnyUsers(int personId, int userId)
        //{
        //    //try
        //    //{
        //    //    Claims claims = new Claims().Values();
        //    //    IEnumerable<UserListAssociateDTO> user = _person.ListCompanyCoworkers(claims.personFatherId, claims.userSystemId);

        //    //    IEnumerable<EmployeeDTO> user = _person.ListEmployes(personId, userId);

        //    //    return Request.CreateResponse(HttpStatusCode.OK, user);
        //    //}
        //    //catch (System.Exception e)
        //    //{
        //    //    LogManager.Error("Erro ao Add UserAdd", e);
        //    //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    //}
        //}


        /// <summary>
        /// Adiciona Expertise Empresa
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Add Expertise Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(CompanyExpertiseDTO))]
        [Route("api/enrollment/AddCompanyExpertise")]
        [Authorize]
        public HttpResponseMessage AddCompanyExpertise(CompanyExpertiseDTO company)
        {

            Claims claims = new Claims().Values();

            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddPersonCompanyExpertiseCommand();
                    command.IntegrationCode = company.CompanyId;
                    command.ExpertiseId = company.ExpertiseId;
                    command.CreatedBy = claims.userSystemId;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Empresa Coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        #endregion


        [HttpPost]
        [HttpGet]
        [Route("api/enrollment/Logout")]
        [Authorize]
        public HttpResponseMessage Logout()
        {
            base.LogoutUser();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #region Select
        /// <summary>
        /// Get Colaboradores da Empresa
        /// </summary>
        /// <remarks>Get Colaboradores da Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>   
        [HttpPost]
        [Authorize(Roles = "GestorCoWorking,GestorRH,GestorPrestadorServico,AdminHeeelp")]
        [Route("api/enrollment/ListEmployes")]
        public HttpResponseMessage ListEmployes()
        {
            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<EmployeeDTO> user = _person.ListEmployes(claims.Values().personId, claims.Values().userSystemId);

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Get Colaboradores da Empresa
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <remarks>Get Colaboradores da Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(Guid))]
        [Authorize(Roles = "GestorCoWorking,GestorRH,GestorPrestadorServico,AdminHeeelp")]
        [Route("api/enrollment/GetEmployee/{employeeId}")]
        public HttpResponseMessage GetEmployee(Guid employeeId)
        {

            //executa validacao sobre tipos basicos
            if (employeeId == Guid.Empty)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "employeeId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error, "EmployeeId");
            }

            try
            {
                Claims claims = new Claims().Values();

                EmployeeDTO employee = _person.GetEmployeeByUserIntegrationCode(claims.Values().personId, employeeId);

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        [HttpGet]
        [ResponseType(typeof(Guid))]
        [Authorize]
        [Route("api/enrollment/CheckCurrentPassword/{password}")]
        public HttpResponseMessage CheckCurrentPassword(string password)
        {
            try
            {
                Claims claims = new Claims().Values();

                bool isPasswordOk = _userDao.CheckCurrentPassword(claims.userIntegrationCode, password);

                return Request.CreateResponse(HttpStatusCode.OK, isPasswordOk);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Guid))]
        [Authorize]
        [Route("api/enrollment/GetOwnUserInformation/")]
        public HttpResponseMessage GetOwnUserInformation()
        {
            try
            {
                Claims claims = new Claims().Values();

                EmployeeDTO employee = _person.GetEmployeeByUserIntegrationCode(claims.userIntegrationCode);

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Consulta informacoes do clube de beneficios do usuario
        /// </summary>
        /// <remarks>Get Empresa </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(Guid))]
        [Authorize]
        [Route("api/enrollment/GetPersonBenefitClubInfo/")]
        public HttpResponseMessage GetPersonBenefitClubInfo()
        {
            try
            {
                Claims claims = new Claims().Values();

                PersonBenefitClubDTO employee = this._userDao.GetUserPersonBenefitClub(claims.userIntegrationCode);

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        //todo:Quem usa esse metodo? Nao podemos ter na camada de API chamadas baseadas em parametros Int, senao o usuario vai ficar mudando de 1 a 1000 pra bisbilhotar. Se o metodo nao eh usado, vamos remove-lo
        /// <summary>
        /// Get Empresa 
        /// </summary>
        /// <param name="CompanyId">CompanyId</param>
        /// <remarks>Get Empresa </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(Guid))]
        [Authorize(Roles = "GestorCoWorking,GestorRH,GestorPrestadorServico,AdminHeeelp")]
        [Route("api/enrollment/GetCompany/{IntegrationCode}")]
        public HttpResponseMessage GetCompany(Guid IntegrationCode)
        {

            //executa validacao sobre tipos basicos
            if (IntegrationCode == Guid.Empty)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "CompanyId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para a empresa não é válido."
                };

                this.ThrowFormattedApiResponse(error, "CompanyId");
            }

            try
            {
                Claims claims = new Claims().Values();

                CompanyDetailsDTO company = _person.GetCompany(IntegrationCode);

                return Request.CreateResponse(HttpStatusCode.OK, company);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar empresa", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Lista Empresas
        /// </summary>
        /// <remarks>Lista Empresas</remarks>
        /// <returns></returns>
        /// <response code="200"></response>       
        [HttpPost]
        [HttpGet]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/enrollment/ListCompanies")]
        public HttpResponseMessage ListCompanies()
        {
            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<CompanyDTO> companies = _person.ListCompanies(claims.Values().personId);

                return Request.CreateResponse(HttpStatusCode.OK, companies);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao listar empresas", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Lista Empresas
        /// </summary>
        /// <remarks>Lista contratos assinados pelo usuario navegando no sistema</remarks>
        /// <returns></returns>
        /// <response code="200"></response>       
        [HttpPost]
        [HttpGet]
        [Authorize]
        [Route("api/enrollment/ListCurrentUserContracts")]
        public HttpResponseMessage ListCurrentUserContracts()
        {
            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<Contract> contracts = _contractDao.GetUserContracts(claims.Values().userSystemId);

                return Request.CreateResponse(HttpStatusCode.OK, contracts);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao listar empresas", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        /// <summary>
        /// Lista Empresas
        /// </summary>
        /// <remarks>Lista contratos assinados pelo usuario navegando no sistema</remarks>
        /// <returns></returns>
        /// <response code="200"></response> 
        [HttpGet]
        [Route("api/enrollment/ListUserContracts")]
        public HttpResponseMessage ListUserContracts(Guid UserIntegrationCode)
        {
            try
            {
                IEnumerable<ContractListDTO> contracts = _contractDao.ListUserContracts(UserIntegrationCode);

                var listFiles = contracts.Select(x => x.FileId).ToList();
                if (listFiles != null && listFiles.Count > 0)
                {
                    var _client = new HttpClient();
                    _client.BaseAddress = new Uri(CustomConfiguration.WebApiFileServer);
                    HttpResponseMessage response = _client.PostAsJsonAsync("/api/FileServer/GetFileList", listFiles).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic res = response.Content.ReadAsAsync<dynamic>().Result;
                        foreach (var item in res)
                        {
                            if (item.FileId != null)
                            {
                                var fileId = long.Parse(item.FileId.ToString());
                                var ctr = contracts.Where(x => x.FileId == fileId).FirstOrDefault();
                                if (ctr != null)
                                    ctr.Url = item.URL.ToString();
                            }
                        }
                    }
                    else
                    {
                        LogManager.Error("Erro GetExpertise" + response.Content.ToString());
                    }
                }



                return Request.CreateResponse(HttpStatusCode.OK, contracts);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao listar empresas", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Lista Expertises
        /// </summary>
        /// <remarks>Lista Expertises</remarks>
        /// <returns></returns>
        /// <response code="200"></response>       
        [HttpGet]
        [Authorize]
        [Route("api/enrollment/ListExpertises/{expertiseFatherId}")]
        public HttpResponseMessage ListExpertises(int? expertiseFatherId)
        {
            IEnumerable<ExpertiseListDTO> expertiseList = _expertiseDao.List(expertiseFatherId);

            return Request.CreateResponse(HttpStatusCode.OK, expertiseList);
        }



        /// <summary>
        /// Lista Expertises
        /// </summary>
        /// <remarks>Lista Expertises</remarks>
        /// <returns></returns>
        /// <response code="200"></response>       
        //[HttpGet]
        //[Authorize]
        //[Route("api/enrollment/ListExpertises/{expertiseFatherId}")]
        //public HttpResponseMessage ListExpertises(int? expertiseFatherId)
        //{
        //    IEnumerable<ExpertiseListDTO> expertiseList = _expertiseDao.List(expertiseFatherId);

        //    return Request.CreateResponse(HttpStatusCode.OK, expertiseList);
        //}

        #endregion

        #region Edit


        /// <summary>
        /// Editar colaborador
        /// </summary>
        /// <param name="employee">employee</param>
        /// <remarks>Editar colaborador</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(EmployeeDTO))]
        [Authorize]
        [Route("api/enrollment/EditEmployee")]
        public HttpResponseMessage EditEmployee(EmployeeDTO employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Claims claims = new Claims().Values();

                    var commandUpdateUser = new UpdateUserCommand
                    {
                        UserIntegrationCode = employee.EmployeeId,
                        Name = employee.Name,
                        SmartPhoneNumber = employee.SmartPhoneNumber,
                        SecundaryEmail = employee.SecundaryEmail,
                        UpdatedBy = claims.Values().userSystemId
                    };
                    if (!claims.Roles.Contains(GeneralEnumerators.EnumProfileClaims.Colaborador))
                    {
                        commandUpdateUser.UserProfileId = employee.UserProfileId;
                        commandUpdateUser.UserStatusId = employee.UserStatusId;
                    }
                    bus.Send(commandUpdateUser);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Editar empresa Coworker
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Editar empresa coworker</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(CompanyDTO))]
        [Authorize(Roles = "GestorCoWorking,GestorRH,GestorPrestadorServico,AdminHeeelp")]
        [Route("api/enrollment/EditCompany")]
        public HttpResponseMessage EditCompany(CompanyDTO company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Claims claims = new Claims().Values();

                    var command = new UpdatePersonCompanyCommand()
                    {
                        IntegrationCode = company.CompanyIntegrationCode,
                        FantasyName = company.FantasyName,
                        FriendlyNameURL = company.FriendlyNameURL,
                        Name = company.Name,
                        PersonalWebSite = company.PersonalWebSite,
                        PersonStatusId = company.PersonStatusId,
                        PhoneNumber = company.PhoneNumber,
                        UpdatedBy = claims.Values().userSystemId
                    };

                    bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deleta usuario associado
        /// </summary>
        /// <param name="userid">userid</param>
        /// <remarks>Deleta uma lista de ususarios associados</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(int))]
        [Authorize(Roles = "GestorCoWorking,GestorRH,GestorPrestadorServico,AdminHeeelp")]
        [Route("api/enrollment/DeleteEmployees")]
        public HttpResponseMessage DeleteEmployees(UserListDeleteDTO UserListDelete)
        {

            //executa validacao sobre tipos basicos
            if (UserListDelete != null && UserListDelete.EmployeeListId == null && UserListDelete.EmployeeListId.Count > 0)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "employeeId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error, "EmployeeId");
            }

            try
            {
                Claims claims = new Claims().Values();

                var command = new InactiveEmployeeCommand();
                command.EmployeeListId = UserListDelete.EmployeeListId;
                command.DeletedBy = claims.Values().userSystemId;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Deleta Empresa
        /// </summary>
        /// <param name="company">Guid PersonIntegrationID</param>
        /// <remarks>Deleta Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(Guid))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/enrollment/DeleteCompany")]
        public HttpResponseMessage DeleteCompany(DeleteCompanyDTO deleteCompanyDTO)
        {

            //executa validacao sobre tipos basicos
            if (deleteCompanyDTO == null)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "companyId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para a empresa não é válido."
                };

                this.ThrowFormattedApiResponse(error, "EmployeeId");
            }

            try
            {
                Claims claims = new Claims().Values();

                var command = new InactivePersonCoWorkerCommand();
                command.CompanyListId = deleteCompanyDTO.CompanyListId;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }


        }

        #endregion

        #region Activate Employee
        /// <summary>
        /// Check Employee Active
        /// </summary>
        /// <remarks>Check Employee Active</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("api/enrollment/CheckEmployeeActive/{employeeId}")]
        public HttpResponseMessage CheckEmployeeActive(Guid employeeId)
        {
            try
            {
                EmployeeDTO employee = _userDao.GetEmployeeActive(employeeId);

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Set Employee Senha
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Add nova Senha</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(EmployeeActivationDTO))]
        [Authorize]
        [Route("api/enrollment/EmployeeNewPassWord/")]
        public HttpResponseMessage EmployeeNewPassWord(EmployeeActivationDTO employee)
        {
            //executa validacao sobre tipos basicos
            try
            {
                if (ModelState.IsValid)
                {
                    Claims claims = new Claims().Values();

                    var command = new ChangeUserPasswordCommand();
                    command.IntegrationCode = employee.EmployeeId;
                    command.Password = employee.PassWord;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Nova Senha", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }


        [HttpPost]
        [HttpGet]
        [Authorize]
        [Route("api/enrollment/ChangeOwnPassWord/{newPassword}")]
        public HttpResponseMessage ChangeOwnPassWord(string newPassword)
        {
            //executa validacao sobre tipos basicos
            if (string.IsNullOrEmpty(newPassword))
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "senha inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "a senha informada não é válida."
                };

                this.ThrowFormattedApiResponse(error, "Nova senha");
            }

            try
            {
                Claims claims = new Claims().Values();

                var command = new ChangeUserPasswordCommand();
                command.IntegrationCode = claims.userIntegrationCode;
                command.Password = newPassword;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Nova Senha", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        #endregion


        #region Password
        /// <summary>
        /// Resuperar Senha
        /// </summary>
        /// <param name="password">email</param>
        /// <remarks>Recuperar Senha</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(string))]
        [AllowAnonymous]
        [Route("api/enrollment/ForgotPassWord")]
        public HttpResponseMessage ForgotPassWord(string email)
        {

            //executa validacao sobre tipos basicos
            if (string.IsNullOrEmpty(email))
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "email inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Email informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error, "Email");
            }

            try
            {
                var command = new ForgotPasswordUserCommand();
                command.Email = email;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao recuperar senha", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Alterar Senha
        /// </summary>
        /// <param name="password">password</param>
        /// <remarks>Alterar Senha</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(PasswordDTO))]
        [AllowAnonymous]
        [Route("api/enrollment/NewPassWord")]
        public HttpResponseMessage NewPassWord(PasswordDTO password)
        {

            //executa validacao sobre tipos basicos
            if (password.IntegrationCode == Guid.Empty && string.IsNullOrEmpty(password.PassWord))
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "IntegrationCode inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O IntegrationCode informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error, "IntegrationCode");
            }

            try
            {
                Claims claims = new Claims().Values();

                var command = new ChangeUserPasswordCommand();
                command.IntegrationCode = password.IntegrationCode;
                command.Password = password.PassWord;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao recuperar senha", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Verificar Link Recuperar Senha
        /// </summary>
        /// <param name="integrationCode">Guid Token</param>
        /// <remarks>Verificar Link Recuperar Senha</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(Guid))]
        [AllowAnonymous]
        [Route("api/enrollment/ValidateTokenForgotPassword/{integrationCode}")]
        public HttpResponseMessage ValidateTokenForgotPassword(Guid integrationCode)
        {

            //executa validacao sobre tipos basicos
            if (integrationCode == Guid.Empty)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "IntegrationCode inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O IntegrationCode informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error, "IntegrationCode");
            }

            try
            {
                if (_userDao.ValidateTokenForgotPassword(integrationCode))
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, false);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao recuperar senha", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        #endregion


        #region Invite
        /// <summary>
        /// Convidar Empresa
        /// </summary>
        /// <param name="invite">invite</param>
        /// <remarks>Convidar Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(MarketingProspectDTO))]
        [Route("api/enrollment/InviteCompany")]
        public HttpResponseMessage InviteCompany(MarketingProspectDTO invite)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();
                    var command = new MarketingProspectCommand();
                    command.Name = invite.Name;
                    command.Email = invite.Email;
                    command.PhoneNumber = invite.PhoneNumber;
                    command.RecommendedBy = claims.userSystemId;
                    command.Message = invite.Message;
                    command.Address = invite.PlaceAddress;
                    this.bus.Send(command);
                }
                catch (Exception e)
                {
                    LogManager.Error("Erro mandar convite", e);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }


            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        /// <summary>
        /// Convidar Empresa
        /// </summary>
        /// <param name="requestContact">requestContact</param>
        /// <remarks>Convidar Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(MarketingProspectDTO))]
        [AllowAnonymous]
        [Route("api/enrollment/RequestContact")]
        public HttpResponseMessage RequestContact(MarketingProspectDTO requestContact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();
                    var command = new MarketingProspectCommand();
                    command.Name = requestContact.Name;
                    command.Email = requestContact.Email;
                    command.PhoneNumber = requestContact.PhoneNumber;
                    command.Message = requestContact.Message;
                    command.Address = requestContact.PlaceAddress;
                    this.bus.Send(command);
                }
                catch (Exception e)
                {
                    LogManager.Error("Erro mandar convite", e);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }


            }
            return Request.CreateResponse(HttpStatusCode.OK, 1);
        }



        #endregion


        #region Tarefas Administrativas
        /// <summary>
        /// Adiciona Nova empresa de Coworking
        /// </summary>
        /// <param name="coworking">coworking</param>
        /// <remarks>Add nova empresa de coworking</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize(Roles = "AdminHeeelp")]
        [Route("api/enrollment/SyncData")]
        public HttpResponseMessage SyncPersonData()
        {
            Claims claims = new Claims().Values();
            Guid IntegrationCode = Guid.NewGuid();

            try
            {
                AdminSyncAllPersonsCommand syncAllPersons = new AdminSyncAllPersonsCommand();
                this.bus.Send(syncAllPersons);

                return Request.CreateResponse(HttpStatusCode.OK, IntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao disparar rotina de sincronizacao de persons e users", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        #endregion


        



        //public async Task<HttpResponseMessage> AddPersonDocument()
        //{
        //    try
        //    {

        //        var provider = new MultipartMemoryStreamProvider();
        //        NameValueCollection parameters = HttpContext.Current.Request.Params;
        //        if (Request.Content.IsMimeMultipartContent())
        //        {
        //            try
        //            {
        //                await Request.Content.ReadAsMultipartAsync(provider);
        //                fs = new List<FIleServer>();
        //                int count = 0;

        //                foreach (var file in HttpContext.Current.Request.Files)
        //                {
        //                    var f = provider.Contents[count];
        //                    string fileName;
        //                    if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
        //                        fileName = parameters["FileName"].ToString();
        //                    else
        //                        fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");

        //                    string mimeType = new MimeType().GetMimeType(fileName);

        //                    var stream = (HttpContext.Current.Request.Files[count]).InputStream;
        //                    byte[] bytesInStream = new byte[stream.Length];
        //                    stream.Read(bytesInStream, 0, bytesInStream.Length);

        //                    Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
        //                    ft.FileIntegrationCode = Guid.NewGuid();
        //                    ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
        //                    ft.OriginalName = fileName;
        //                    try
        //                    {
        //                        using (Image img = Image.FromStream(stream: stream,
        //                       useEmbeddedColorManagement: false,
        //                       validateImageData: false))
        //                        {
        //                            ft.Width = img.PhysicalDimension.Width.ToString();
        //                            ft.Height = img.PhysicalDimension.Height.ToString();
        //                        }
        //                        count++;
        //                    }
        //                    catch (Exception ex)
        //                    { LogManager.Error("Erro ao recuperar dimensoes da imagen", ex); }


        //                    //salvo na tablea temporaria

        //                    _listFileTemp.Add(_FileTemp.SaveFileTemp(ft));

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                LogManager.Error(string.Format("PersonDocumentAdd image Error:{0}", ex));

        //            }


        //        }

        //        var personDocument = new AddPersonDocumentCommand()
        //        {
        //            DocumentTypeId = (short)GeneralEnumerators.EnumDocumentType.CNPJ,
        //            Number = parameters["CNPJ"].ToString(),
        //            Active = true,
        //            listFileTemp = _listFileTemp,
        //            PersonIntegrationId = Guid.Parse(parameters["PersonIntegrationID"]),
        //            UserSystemId = Convert.ToInt32(parameters["UserSystem"].ToString())

        //        };


        //        this.bus.Send(personDocument);


        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        LogManager.Error("Erro ao Add PersonDocumentAdd", e);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}

        //public HttpResponseMessage AddPersonExpertise(PersonExpertiseDTO expertise)
        //{
        //    try
        //    {
        //        Claims claims = new Claims().Values();

        //        if (ModelState.IsValid)
        //        {
        //            var personExpertise = new AddPersonExpertiseCommand()
        //            {
        //                ExpertiseListId = expertise.ExpertiseListId,
        //                InsertedDateUTC = DateTime.UtcNow,
        //                InsertedBy = expertise.InsertedBy,
        //                ExhibitionOrder = expertise.ExhibitionOrder,
        //                PersonIntegrationId = expertise.PersonIntegrationId
        //            };

        //            this.bus.Send(personExpertise);
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        LogManager.Error("Erro ao Add colaborador", e);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }


        //public HttpResponseMessage PersonInterestAdd(JObject jsonData)
        //{
        //    dynamic json = jsonData;
        //    try
        //    {

        //        var personInterest = new AddPersonInterestCommand()
        //        {

        //            PersonInterestId = (int)json.PersonInterestId,
        //            PersonId = (int)json.PersonId,
        //            ExpertiseId = (int)json.ExpertiseId,
        //            DisplayOrder = (int?)json.DisplayOrder,
        //            InsertedDateUTC = Convert.ToDateTime(json.InsertedDateUTC),
        //            InsertedBy = (int?)json.InsertedBy,
        //            ServerInstanceId = Convert.ToInt16(json.ServerInstanceId),
        //            Active = Convert.ToBoolean(json.Active)
        //        };

        //        this.bus.Send(personInterest);


        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        LogManager.Error("Erro ao Add PersonInterestAdd", e);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}



        //#endregion

        [HttpGet]
        [Route("api/enrollment/encript")]
        public HttpResponseMessage Encript()
        {
            CompanyReturnDTO ret = new CompanyReturnDTO()
            {
                CompanyID = Guid.NewGuid(),
                ManagerHash = "hash teste",
                ManagerEmployeeId = Guid.NewGuid()
            };

            //envia o objeto, retorna uma string
            string hash = Common.Utils.Crypt.Encrypt(ret);

            //envia string e recebe um objeto
            var obj = Common.Utils.Crypt.Decrypt<CompanyReturnDTO>(hash);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }

}
