using Heeelp.Core.Command.Person;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using Newtonsoft.Json.Linq;
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
using System.Web.Script.Serialization;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PersonController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly IPersonDao _PersonDao;
        private readonly IPersonSkinDao _PersonSkinDao;
        private readonly IPersonOriginTypeDao _PersonOriginTypeDao;
        private readonly IPersonProfileDao _PersonProfileDao;
        private readonly IPersonStatusDao _PersonStatusDao;
        private readonly IPersonTypeDao _PersonTypeDao;
        private long fileId;
        private readonly IPersonAddressDao _PersonAddressDao;
        private IStorage _storage;
        private string _containerName;
        private readonly IFileTempDao _FileTemp;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;
        private long? campaignID;
        private long? inviteId;
        private IPersonExpertiseDao _PersonExpertiseDao;

        public PersonController(ICommandBus bus, IFileTempDao fileTemp, IPersonDao _PersonDao, IPersonOriginTypeDao _personOriginTypeDao, IPersonProfileDao _personProfileDao
            , IPersonStatusDao _personStatusDao, IPersonTypeDao _personTypeDao, IPersonAddressDao _personAddressDao, IPersonExpertiseDao _personExpertiseDao    ,
            IPersonSkinDao personSkinDao) : base()
        {
            this.bus = bus;
            this._PersonDao = _PersonDao;
            this._PersonOriginTypeDao = _personOriginTypeDao;
            this._PersonProfileDao = _personProfileDao;
            this._PersonStatusDao = _personStatusDao;
            this._PersonTypeDao = _personTypeDao;
            this._PersonAddressDao = _personAddressDao;
            this._PersonSkinDao = personSkinDao;
            this._listFileTemp = new List<int>();
            this._FileTemp = fileTemp;
            this._PersonExpertiseDao = _personExpertiseDao;
            _storage = new Storage.StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
        }

        [HttpPost]
        public HttpResponseMessage PersonAdd(JObject jsonData)
        {

            dynamic json = jsonData;
            try
            {
                var Person = new AddPersonCommand();
                //{
                Person.IntegrationCode = Guid.NewGuid();
                Person.Name = json.Name.ToString();
                Person.FantasyName = json.FantasyName.ToString();
                Person.NameFromSecurityCheck = json.NameFromSecurityCheck.ToString();
                Person.SecuritySourceId = string.IsNullOrEmpty((json.SecuritySourceId).Value) ? null : int.Parse(json.SecuritySourceId.ToString());
                Person.IsSafe = string.IsNullOrEmpty((json.IsSafe).Value) ? null : Boolean.Parse((json.IsSafe).Value);
                Person.FriendlyNameURL = json.FriendlyNameURL.ToString();
                Person.PersonOriginTypeId = Convert.ToByte(json.PersonOriginTypeId);
                Person.PersonOriginDetails = json.PersonOriginDetails.ToString();
                Person.CampaignId = string.IsNullOrEmpty((json.CampaignId).Value) ? null : Convert.ToInt64(json.CampaignId);
                Person.CountryId = Convert.ToByte(json.CountryId);
                Person.LanguageId = Convert.ToByte(json.LanguageId);
                Person.PersonTypeId = Convert.ToByte(json.PersonTypeId);
                Person.PersonProfileId = Convert.ToByte(json.PersonProfileId);
                Person.PersonStatusId = Convert.ToByte(json.PersonStatusId);
                Person.PersonalWebSite = json.PersonalWebSite.ToString();
                Person.CurrencyId = Convert.ToByte(json.CurrencyId);
                Person.CreationDateUTC = DateTime.UtcNow;
                Person.ActivationCode = json.ActivationCode.ToString();
                Person.ActivationDateUTC = DateTime.UtcNow;
                Person.PhoneNumber = json.PhoneNumber.ToString();
                Person.PersonFatherId = string.IsNullOrEmpty((json.PersonFatherId).Value) ? null : int.Parse(json.PersonFatherId.ToString());
                Person.InviteId = string.IsNullOrEmpty((json.InviteId).Value) ? null : Convert.ToInt64(json.InviteId);
                Person.ServerInstanceId = Convert.ToInt16(json.ServerInstanceId);
                Person.Active = true;
                Person.UserSystemId = Convert.ToInt64(json.UserSystem);
                Person.PersonIntegrationID = Guid.NewGuid();

                //};


                this.bus.Send(Person);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonAdd ", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        [HttpPost]
        public async Task<HttpResponseMessage> PersonProspectAdd()
        {

            //recebo as imagens se tiver               
            try
            {
                var provider = new MultipartMemoryStreamProvider();
                NameValueCollection parameters = HttpContext.Current.Request.Params;
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
                            string fileName;
                            if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
                                fileName = parameters["FileName"].ToString();
                            else
                                fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                            string mimeType = new MimeType().GetMimeType(fileName);
                            var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                            byte[] bytesInStream = new byte[stream.Length];
                            stream.Read(bytesInStream, 0, bytesInStream.Length);

                            Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
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


                            //salvo na tablea temporaria
                            try
                            {
                                _listFileTemp.Add(_FileTemp.SaveFileTemp(ft));
                            }
                            catch (Exception ex)
                            {
                                LogManager.Info("PersonProspectAdd sem Imagem");
                                throw;
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(string.Format("PersonProspectAdd image Error:{0}", ex));
                    }

                }



                var Person = new AddPersonCommand()
                {
                    IntegrationCode = Guid.NewGuid(),
                    Name = parameters["Name"],
                    FantasyName = parameters["FantasyName"].ToString(),
                    //NameFromSecurityCheck = parameters["NameFromSecurityCheck"].ToString(),
                    //SecuritySourceId =  parameters["SecuritySourceId"],
                    //IsSafe = parameters["IsSafe"],
                    //FriendlyNameURL = parameters["FriendlyNameURL"].ToString(),
                    PersonOriginTypeId = Convert.ToByte(parameters["PersonOriginTypeId"]),
                    //PersonOriginDetails = parameters["PersonOriginDetails"].ToString(),
                    CampaignId = long.Parse(parameters["CampaignId"].ToString()),
                    CountryId = Convert.ToByte(parameters["CountryId"]),
                    LanguageId = Convert.ToByte(parameters["LanguageId"]),
                    PersonTypeId = Convert.ToByte(parameters["PersonTypeId"]),
                    PersonProfileId = Convert.ToByte(parameters["PersonProfileId"]),
                    PersonStatusId = Convert.ToByte(parameters["PersonStatusId"]),
                    //PersonalWebSite = parameters["PersonalWebSite.ToString(),
                    CurrencyId = Convert.ToByte(parameters["CurrencyId"].ToString()),
                    CreationDateUTC = DateTime.UtcNow,
                    //ActivationCode = parameters["ActivationCode.ToString(),
                    //ActivationDateUTC = DateTime.Parse( parameters["ActivationDateUTC"].ToString()),
                    PhoneNumber = parameters["PhoneNumber"].ToString(),
                    //PersonFatherId = parameters["PersonFatherId,
                    InviteId = long.Parse(parameters["InviteId"].ToString()),
                    ServerInstanceId = Convert.ToInt16(1),
                    Active = true,
                    listFileTemp = _listFileTemp,
                    UserSystemId = long.Parse(parameters["UserSystem"].ToString()),
                    PersonIntegrationID = Guid.NewGuid()
                    //,ImgListId = _listFileTemp
                };


                this.bus.Send(Person);


                return Request.CreateResponse(HttpStatusCode.OK, new { Person.PersonIntegrationID });
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("ValidateUser Error:{0}", ex));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PersonProspectDesktopAdd()
        {

            //recebo as imagens se tiver               
            try
            {
                var provider = new MultipartMemoryStreamProvider();
                NameValueCollection parameters = HttpContext.Current.Request.Params;
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
                            string fileName;
                            if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
                                fileName = parameters["FileName"].ToString();
                            else
                                fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                            string mimeType = new MimeType().GetMimeType(fileName);
                            var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                            byte[] bytesInStream = new byte[stream.Length];
                            stream.Read(bytesInStream, 0, bytesInStream.Length);

                            Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
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


                            //salvo na tablea temporaria
                            try
                            {
                                _listFileTemp.Add(_FileTemp.SaveFileTemp(ft));
                            }
                            catch (Exception ex)
                            {
                                LogManager.Info("PersonProspectAdd sem Imagem");
                                throw;
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(string.Format("PersonProspectAdd image Error:{0}", ex));
                    }

                }
                if (!string.IsNullOrEmpty(parameters["CampaignId"].ToString()))
                {
                    campaignID = Convert.ToInt64(parameters["CampaignId"].ToString());
                }
                else
                {
                    campaignID = null;
                }

                if (!string.IsNullOrEmpty(parameters["InviteId"].ToString()))
                {
                    inviteId = long.Parse(parameters["InviteId"].ToString());
                }
                else
                {
                    inviteId = null;
                }


                var Person = new AddPersonCommand();

                Person.IntegrationCode = Guid.NewGuid();
                Person.Name = parameters["Name"];
                Person.FantasyName = parameters["FantasyName"].ToString();
                Person.PersonOriginTypeId = Convert.ToByte(parameters["PersonOriginTypeId"]);
                Person.CampaignId = campaignID;
                Person.CountryId = Convert.ToByte(parameters["CountryId"]);
                Person.LanguageId = Convert.ToByte(parameters["LanguageId"]);
                Person.PersonTypeId = Convert.ToByte(parameters["PersonTypeId"]);
                Person.PersonProfileId = Convert.ToByte(parameters["PersonProfileId"]);
                Person.PersonStatusId = Convert.ToByte(parameters["PersonStatusId"]);
                Person.CurrencyId = Convert.ToByte(parameters["CurrencyId"].ToString());
                Person.CreationDateUTC = DateTime.UtcNow;
                Person.PhoneNumber = parameters["PhoneNumber"].ToString();
                Person.InviteId = inviteId;
                Person.ServerInstanceId = Convert.ToInt16(1);
                Person.Active = true;
                Person.listFileTemp = _listFileTemp;
                Person.UserSystemId = long.Parse(parameters["UserSystem"].ToString());
                Person.PersonIntegrationID = Guid.NewGuid();
                Person.Number = parameters["CNPJ"].ToString();//cnpj



                this.bus.Send(Person);


                List<int> expertiseId = new List<int>() { Convert.ToInt32(parameters["ExpertiseId"].ToString()) };
                var personExpertise = new AddPersonExpertiseCommand()
                {
                    ExpertiseListId = expertiseId,
                    InsertedDateUTC = DateTime.UtcNow,
                    InsertedBy = Convert.ToInt32(parameters["UserSystem"].ToString()),
                    CustomDescription = parameters["CustomDescription"].ToString(),
                    ServerInstanceId = 1,//Convert.ToInt16(json.ServerInstanceId),
                    //CustomPhotoFileId = string.IsNullOrEmpty(json.CustomPhotoFileId) ? null : json.CustomPhotoFileId,
                    //CustomDescription = json.CustomDescription,
                    ExhibitionOrder = 1,// Convert.ToByte(json.ExhibitionOrder),
                    PersonIntegrationId = Person.PersonIntegrationID,
                    Active = true //Convert.ToBoolean(json.Active)

                };

                this.bus.Send(personExpertise);




                return Request.CreateResponse(HttpStatusCode.OK, new { Person.PersonIntegrationID });
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("ValidateUser Error:{0}", ex));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]

        public HttpResponseMessage PersonProspectAdrdressAdd()
        {

            try
            {


                NameValueCollection parameters = HttpContext.Current.Request.Params;


                var personAddress = new AddPersonAddressCommand()
                {
                    //PersonAddressId = (int)json.PersonAddressId,
                    //PersonId = (int)json.PersonId,
                    AddressTypeId = 1,//commerce
                    StartDateUTC = DateTime.UtcNow,
                    StreetName = parameters["StreetName"],
                    Number = parameters["Number"].ToString(),
                    //NeighbourhoodId = int.Parse(parameters["NeighbourhoodId"]),
                    Neighbourhood = parameters["Neighbourhood"].ToString(),
                    PostCode = parameters["PostCode"],
                    Coordinates = parameters["Coordinates"],
                    ContactPhoneNumber = parameters["ContactPhoneNumber"],
                    ServerInstanceId = 1,
                    CreatedBy = int.Parse(parameters["CreatedBy"]),
                    ContactEMail = parameters["ContactEMail"],
                    Active = true,
                    PersonIntegrationId = Guid.Parse(parameters["PersonIntegrationID"])
                };

                this.bus.Send(personAddress);


                return Request.CreateResponse(HttpStatusCode.OK, new { OK = "OK" });
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("Add Image Address Error:{0}", ex));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        /// <summary>
        /// Send PersonAddress Prospect
        /// </summary>
        /// <param name="address">PersonAddressProspectDTO</param>
        /// <remarks>Send PersonAddress Prospect</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(PersonAddressProspectDTO))]
        public HttpResponseMessage PersonAdrdressAdd(PersonAddressProspectDTO address)
        {

            try
            {

                var personAddress = new AddPersonAddressCommand()
                {
                    //PersonAddressId = address.PersonAddressId,
                    //PersonId = address.PersonId,
                    AddressTypeId = 1,// Comercial
                    StartDateUTC = DateTime.UtcNow,
                    StreetName = address.StreetName,
                    Number = address.Number,
                    NeighbourhoodId = address.NeighbourhoodId,
                    Neighbourhood = address.Neighbourhood,
                    PostCode = address.PostCode,
                    Coordinates = address.Coordinates,
                    ContactPhoneNumber = address.ContactPhoneNumber,
                    ServerInstanceId = 1,// address.ServerInstanceId,
                    CreatedBy = address.CreatedBy,
                    ContactEMail = address.ContactEMail,
                    Active = true,
                    PersonIntegrationId = address.PersonIntegrationID,
                    State = address.State,
                    City = address.City,
                    Country = address.Country
                };

                this.bus.Send(personAddress);


                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonAdrdressAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        public HttpResponseMessage PersonContractAdd(JObject jsonData)
        {
            dynamic json = jsonData;
            try
            {

                var personContract = new AddPersonContractCommand()
                {
                    PersonContractId = (int)json.PersonContractId,
                    PersonId = (int)json.PersonId,
                    ContractId = Convert.ToInt16(json.ContractId),
                    UserId = (int)json.UserId
                };

                this.bus.Send(personContract);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonContractAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public async Task<HttpResponseMessage> PersonDocumentAdd()
        {
            try
            {

                var provider = new MultipartMemoryStreamProvider();
                NameValueCollection parameters = HttpContext.Current.Request.Params;
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
                            string fileName;
                            if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
                                fileName = parameters["FileName"].ToString();
                            else
                                fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");

                            string mimeType = new MimeType().GetMimeType(fileName);

                            var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                            byte[] bytesInStream = new byte[stream.Length];
                            stream.Read(bytesInStream, 0, bytesInStream.Length);

                            Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
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
                            { LogManager.Error("Erro ao recuperar dimensoes da imagen", ex); }


                            //salvo na tablea temporaria

                            _listFileTemp.Add(_FileTemp.SaveFileTemp(ft));

                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(string.Format("PersonDocumentAdd image Error:{0}", ex));

                    }

                }

                var personDocument = new AddPersonDocumentCommand()
                {
                    DocumentTypeId = (short)GeneralEnumerators.EnumDocumentType.CNPJ,
                    Number = parameters["CNPJ"].ToString(),
                    Active = true,
                    listFileTemp = _listFileTemp,
                    PersonIntegrationId = Guid.Parse(parameters["PersonIntegrationID"]),
                    UserSystemId = Convert.ToInt32(parameters["UserSystem"].ToString())

                };


                this.bus.Send(personDocument);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonDocumentAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage PersonExpertiseAdd(PersonExpertiseDTO personExpertiseAdd)
        {
            try
            {

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(personExpertiseAdd);

                var personExpertise = new AddPersonExpertiseCommand()
                {
                    ExpertiseListId = personExpertiseAdd.ExpertiseListId,
                    InsertedDateUTC = DateTime.UtcNow,
                    InsertedBy = personExpertiseAdd.InsertedBy,
                    ServerInstanceId = 1,//Convert.ToInt16(json.ServerInstanceId),
                    ExhibitionOrder = personExpertiseAdd.ExhibitionOrder,// Convert.ToByte(json.ExhibitionOrder),
                    PersonIntegrationId = personExpertiseAdd.PersonIntegrationId,
                    Active = true //Convert.ToBoolean(json.Active)

                };

                this.bus.Send(personExpertise);

                return Request.CreateResponse(HttpStatusCode.OK, new { ok = "ok" });
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonExpertiseAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage PersonFileAdd(JObject jsonData)
        {
            dynamic json = jsonData;
            try
            {

                var personFile = new AddPersonFileCommand()
                {
                    PersonFileId = (int)json.PersonFileId,
                    PersonId = (int)json.PersonId,
                    FileId = Convert.ToInt64(json.FileId),
                    AssociatedDateUTC = Convert.ToDateTime(json.AssociatedDateUTC),
                    AssocietedBy = (int?)json.AssocietedBy,
                    Active = Convert.ToBoolean(json.Active)

                };

                this.bus.Send(personFile);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonFileAdd ", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage PersonInterestAdd(JObject jsonData)
        {
            dynamic json = jsonData;
            try
            {

                var personInterest = new AddPersonInterestCommand()
                {

                    PersonInterestId = (int)json.PersonInterestId,
                    PersonId = (int)json.PersonId,
                    ExpertiseId = (int)json.ExpertiseId,
                    DisplayOrder = (int?)json.DisplayOrder,
                    InsertedDateUTC = Convert.ToDateTime(json.InsertedDateUTC),
                    InsertedBy = (int?)json.InsertedBy,
                    ServerInstanceId = Convert.ToInt16(json.ServerInstanceId),
                    Active = Convert.ToBoolean(json.Active)
                };

                this.bus.Send(personInterest);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonInterestAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage PersonHistoricAdd(JObject jsonData)
        {
            dynamic json = jsonData;
            try
            {

                var personHistoric = new AddPersonHistoricCommand()
                {

                    PersonHistoricId = Convert.ToInt64(json.PersonHistoricId),
                    PersonId = (int)json.PersonId,
                    IntegrationCode = Guid.NewGuid(),
                    Name = json.Name,
                    FantasyName = json.FantasyName,
                    NameFromSecurityCheck = json.NameFromSecurityCheck,
                    SecuritySourceId = (int?)json.SecuritySourceId,
                    IsSafe = string.IsNullOrEmpty(json.IsSafe) ? null : Convert.ToByte(json.IsSafe),
                    FriendlyNameURL = json.FriendlyNameURL,
                    PersonOriginTypeId = Convert.ToByte(json.PersonOriginTypeId),
                    PersonOriginDetails = json.PersonOriginDetails,
                    CountryId = Convert.ToByte(json.CountryId),
                    LanguageId = Convert.ToByte(json.LanguageId),
                    PersonTypeId = Convert.ToByte(json.PersonTypeId),
                    PersonProfileId = Convert.ToByte(json.PersonProfileId),
                    PersonStatusId = Convert.ToByte(json.PersonStatusId),
                    PersonalWebSite = json.PersonalWebSite,
                    CurrencyId = Convert.ToByte(json.CurrencyId),
                    CreationDateUTC = DateTime.UtcNow,
                    ActivationCode = json.ActivationCode,
                    ActivationDateUTC = string.IsNullOrEmpty(json.ActivationDateUTC) ? null : Convert.ToDateTime(json.ActivationDateUTC),
                    PhoneNumber = json.PhoneNumber,
                    PersonFatherId = (int?)json.PersonFatherId,
                    InviteId = string.IsNullOrEmpty(json.InviteId) ? null : Convert.ToInt64(json.InviteId),
                    ServerInstanceId = Convert.ToInt16(json.ServerInstanceId),
                    Active = Convert.ToBoolean(json.Active)
                };

                this.bus.Send(personHistoric);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add PersonHistoricAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersons")]
        public HttpResponseMessage ListPersons()
        {
            IEnumerable<PersonListDTO> PersonList = _PersonDao.ListPersons();

            return Request.CreateResponse(HttpStatusCode.OK, PersonList);
        }

        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonsPromotion")]
        public HttpResponseMessage ListPersonsPromotion()
        {
            IEnumerable<PersonListDTO> PersonList = _PersonDao.ListPersonsPromotion();

            return Request.CreateResponse(HttpStatusCode.OK, PersonList);
        }
        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonsNotAddress")]
        public HttpResponseMessage ListPersonsNotAddress()
        {
            IEnumerable<PersonListDTO> PersonList = _PersonDao.ListPersonsNotAdrress();

            return Request.CreateResponse(HttpStatusCode.OK, PersonList);
        }
        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonStatus")]
        public HttpResponseMessage ListPersonStatus()
        {
            IEnumerable<PersonStatusListDTO> PersonStatusList = _PersonStatusDao.ListPersonStatus();

            return Request.CreateResponse(HttpStatusCode.OK, PersonStatusList);
        }
        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonProfiles")]
        public HttpResponseMessage ListPersonProfiles()
        {
            IEnumerable<PersonProfileListDTO> PersonProfileList = _PersonProfileDao.ListPersonProfiles();

            return Request.CreateResponse(HttpStatusCode.OK, PersonProfileList);
        }
        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonTypes")]
        public HttpResponseMessage ListPersonTypes()
        {
            IEnumerable<PersonTypeListDTO> PersonTypeList = _PersonTypeDao.ListPersonTypes();

            return Request.CreateResponse(HttpStatusCode.OK, PersonTypeList);
        }
        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonOriginTypes")]
        public HttpResponseMessage ListPersonOriginTypes()
        {
            IEnumerable<PersonOriginTypeListDTO> PersonOriginTypeList = _PersonOriginTypeDao.ListPersonOriginTypes();

            return Request.CreateResponse(HttpStatusCode.OK, PersonOriginTypeList);
        }
        [Route("api/person/GetPersonAddressClassified/{id}")]
        public HttpResponseMessage GetPersonAddressClassified(int id)
        {

            PersonAddressClassifiedDTO personAddressClassified = _PersonAddressDao.GetPersonAddressClassified(id);

            return Request.CreateResponse(HttpStatusCode.OK, personAddressClassified);
        }
        [Route("api/person/GetPersonClassified/{id}")]
        public HttpResponseMessage GetPersonClassified(int id)
        {
            PersonClassifiedDTO personClassified = _PersonDao.GetPersonClassified(id);

            return Request.CreateResponse(HttpStatusCode.OK, personClassified);
        }

        [HttpPost]
        [HttpGet]
        [Route("api/person/ListPersonExpertiseByPerson/{id}")]
        public HttpResponseMessage ListPersonExpertiseByPerson(Guid id)
        {
            IEnumerable<PersonExpertiseListDTO> personExpertiseList = _PersonExpertiseDao.ListPersonExpertiseByPerson(id);

            return Request.CreateResponse(HttpStatusCode.OK, personExpertiseList);
        }

        [HttpPost]
        [Route("api/person/CompleteRegistrationExternalPerson")]
        public async Task<HttpResponseMessage> CompleteRegistrationExternalPerson()
        {

            //se tiver logo
            var provider = new MultipartMemoryStreamProvider();
            NameValueCollection parameters = HttpContext.Current.Request.Params;
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
                        string fileName;
                        if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
                            fileName = parameters["FileName"].ToString();
                        else
                            fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");


                        string mimeType = new MimeType().GetMimeType(fileName);
                        var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                        byte[] bytesInStream = new byte[stream.Length];
                        stream.Read(bytesInStream, 0, bytesInStream.Length);

                        Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
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


                        //salvo na tablea temporaria
                        try
                        {
                            _listFileTemp.Add(_FileTemp.SaveFileTemp(ft));
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Erro ao salvar Logo do Person");
                        }

                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("CompleteRegistrationExternalPerson image Error:{0}", ex));
                }

            }
            try
            {


                //var commandPersonCompleteRegistration = new UpdatePersonCompleteRegistrationCommand();

                //commandPersonCompleteRegistration.IntegrationCode = Guid.NewGuid();
                //commandPersonCompleteRegistration.PhoneNumber = parameters["PhoneNumber"].ToString();
                //commandPersonCompleteRegistration.UserSystemId = long.Parse(parameters["UserId"].ToString());
                //commandPersonCompleteRegistration.PersonIntegrationID = Guid.NewGuid();
                //commandPersonCompleteRegistration.Document = parameters["Document"].ToString();

                //this.bus.Send(commandPersonCompleteRegistration);



                //var UpdatePersonAddressCompleteRegistration = new UpdatePersonAddressCompleteRegistrationCommand();

                //UpdatePersonAddressCompleteRegistration.PersonIntegrationId = commandPersonCompleteRegistration.PersonIntegrationID;
                //UpdatePersonAddressCompleteRegistration.StreetName = parameters["StreetName"].ToString();
                //UpdatePersonAddressCompleteRegistration.Number = parameters["Number"].ToString();
                //UpdatePersonAddressCompleteRegistration.State = parameters["State"].ToString();
                //UpdatePersonAddressCompleteRegistration.Country = parameters["Country"].ToString();
                //UpdatePersonAddressCompleteRegistration.City = parameters["City"].ToString();
                //UpdatePersonAddressCompleteRegistration.PostCode = parameters["PostCode"].ToString();
                //UpdatePersonAddressCompleteRegistration.ContactEMail = parameters["SecundaryEmail"].ToString();
                //UpdatePersonAddressCompleteRegistration.ContactPhoneNumber = parameters["SmartPhoneNumber"].ToString();
                //UpdatePersonAddressCompleteRegistration.CreatedBy = Convert.ToInt32(parameters["UserId"].ToString());
                //UpdatePersonAddressCompleteRegistration.ServerInstanceId = Convert.ToInt16(1);
                //this.bus.Send(UpdatePersonAddressCompleteRegistration);


                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("CompleteRegistartionPerson: {0}", parameters));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("api/person/GetPersonDetails/{id}")]
        public HttpResponseMessage GetPersonDetails(int id)
        {
            PersonDetailsDTO personDetails = _PersonDao.GetPersonDetails(id);

            return Request.CreateResponse(HttpStatusCode.OK, personDetails);
        }


      



        [HttpGet]
        [Route("api/person/GetPersonSkin/{PersonIntegrationCodeId}")]
        public HttpResponseMessage GetPersonSkin(Guid PersonIntegrationCodeId)
        {
            PersonSkinDTO personSkin = _PersonSkinDao.GetPersonSkin(PersonIntegrationCodeId);

            return Request.CreateResponse(HttpStatusCode.OK, personSkin);
        }


        [HttpGet]
        [Route("api/person/GetPersonSkinByUser/{UserId}")]
        public HttpResponseMessage GetPersonSkinByUser(int UserId)
        {
            PersonSkinDTO personSkin = _PersonSkinDao.GetPersonSkin(UserId);

            return Request.CreateResponse(HttpStatusCode.OK, personSkin);
        }




    }
}
