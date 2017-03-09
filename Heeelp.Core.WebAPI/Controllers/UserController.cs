using Heeelp.Core.Command.User;
using Heeelp.Core.Domain;
using Heeelp.Core.Domain.ReadModel.DTO;

using Heeelp.Core.Domain.ReadModel;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using Heeelp.Core.Common;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using System.Collections.Specialized;
using Heeelp.Core.Command.Person;
using System.Web.Http.Description;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly IUserDao _UserDao;
        private readonly IUserProfileDao _UserProfileDao;
        private readonly IUserStatusDao _UserStatusDao;
        private readonly IFileTempDao _FileTemp;
        private long fileId;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;
        private IStorage _storage;
        private string _containerName;

        public UserController(ICommandBus bus, IFileTempDao fileTemp, IUserDao userDao, IUserProfileDao userProfileDao, IUserStatusDao userStatusDao) : base()
        {

            this.bus = bus;
            this._UserDao = userDao;
            this._UserProfileDao = userProfileDao;
            this._UserStatusDao = userStatusDao;
            this._FileTemp = fileTemp;
            this._listFileTemp = new List<int>();
            _storage = new Storage.StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
        }

        //[HttpPost]
        //public async Task<HttpResponseMessage> UserAdd(string jsonData)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    var json = serializer.Deserialize<dynamic>(jsonData);

        //    try
        //    {
        //        //recebo as imagens se tiver
        //        var provider = new MultipartMemoryStreamProvider();
        //        if (Request.Content.IsMimeMultipartContent())
        //        {

        //            try
        //            {
        //                await Request.Content.ReadAsMultipartAsync(provider);
        //                fs = new List<FIleServer>();
        //                int count = 0;


        //                foreach (var f in provider.Contents)
        //                {
        //                    var fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");

        //                    string mimeType = new MimeType().GetMimeType(fileName);

        //                    var stream = (HttpContext.Current.Request.Files[count]).InputStream;

        //                    byte[] bytesInStream = new byte[stream.Length];
        //                    stream.Read(bytesInStream, 0, bytesInStream.Length);

        //                    Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
        //                    ft.FileIntegrationCode = Guid.NewGuid();
        //                    ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
        //                    ft.OriginalName = f.Headers.ContentDisposition.FileName.Replace("\"", "");
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
        //                LogManager.Info("UserAdd sem imagen", ex);

        //            }


        //        }



        //        var command = new AddUserCommand()
        //        {

        //            IntegrationCode = Guid.NewGuid(),
        //            Name = json["Name"],
        //            Email = json["Email"],
        //            SecundaryEmail = json["SecundaryEmail"],
        //            SmartPhoneNumber = json["SmartPhoneNumber"],
        //            PersonId = Convert.ToInt32(json["PersonId"]),
        //            IsDefaultUser = Convert.ToBoolean(json["IsDefaultUser"]),
        //            UserProfileId = Convert.ToByte(json["UserProfileId"]),
        //            UserStatusId = Convert.ToByte(json["UserStatusId"]),
        //            AuthenticationModeId = Convert.ToByte(json["AuthenticationModeId"]),
        //            CreationDateUTC = DateTime.UtcNow,
        //            ValidationDateUTC = DateTime.UtcNow,
        //            ValidationToken = json["ValidationToken"],
        //            EnrollmentIP = json["EnrollmentIP"],
        //            ValidationIP = json["ValidationIP"],
        //            ServerInstanceId = Convert.ToInt16(json["ServerInstanceId"]),
        //            listFileTemp = _listFileTemp,

        //            CreatedBy = string.IsNullOrEmpty((json["CreatedBy"])) ? null : int.Parse(json["CreatedBy"]),
        //            LanguageId = string.IsNullOrEmpty(json["LanguageId"]) ? null : Convert.ToByte(json["LanguageId"]),
        //            FormFillTime = string.IsNullOrEmpty(json["FormFillTime"]) ? null : Convert.ToInt16(json["FormFillTime"]),
        //            SecurityCheckNecessary = string.IsNullOrEmpty(json["SecurityCheckNecessary"]) ? null : Convert.ToBoolean(json["SecurityCheckNecessary"]),
        //            IsPerpetual = string.IsNullOrEmpty(json["IsPerpetual"]) ? null : Convert.ToBoolean(json["IsPerpetual"]),
        //            LoginPassword = CustomConfiguration.DefaultPasswordNewUser
        //        };

        //        this.bus.Send(command);


        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        LogManager.Error("Erro ao Add UserAdd", e);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage UserProspectAdd(UserAddDTO userDto)
        {
            try
            {
                var personAddress = new AddUserProspectCommand()
                {
                    IntegrationCode = Guid.NewGuid(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    SecundaryEmail = userDto.SecundaryEmail,
                    SmartPhoneNumber = userDto.SmartPhoneNumber,
                    //PersonId = Convert.ToInt32(json["PersonId"]),
                    IsDefaultUser = true,
                    UserProfileId = (byte)GeneralEnumerators.EnumUserProfile.Administrador,
                    UserStatusId = (byte)GeneralEnumerators.EnumUserStatus.Ativo,
                    AuthenticationModeId = 1,//Convert.ToInt32(json["AuthenticationModeId"]),
                    CreationDateUTC = DateTime.UtcNow,
                    ValidationDateUTC = DateTime.UtcNow,
                    //ValidationToken = json["ValidationToken"],
                    EnrollmentIP = userDto.EnrollmentIP,
                    ValidationIP = userDto.ValidationIP,
                    ServerInstanceId = 1,//Convert.ToInt16(json["ServerInstanceId"]),
                    //listFileTemp = _listFileTemp,

                    CreatedBy = userDto.CreatedBy,
                    LanguageId = userDto.LanguageId,
                    //FormFillTime = 1,//string.IsNullOrEmpty(json["FormFillTime"]) ? null : Convert.ToInt16(json["FormFillTime"]),
                    //SecurityCheckNecessary = string.IsNullOrEmpty(json["SecurityCheckNecessary"]) ? null : Convert.ToBoolean(json["SecurityCheckNecessary"]),
                    IsPerpetual = true,//string.IsNullOrEmpty(json["IsPerpetual"]) ? null : Convert.ToBoolean(json["IsPerpetual"]),
                    LoginPassword = CustomConfiguration.DefaultPasswordNewUser,
                    PersonIntegrationId = userDto.PersonIntegrationId

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
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage UserFileAdd(JObject jsonData)
        {
            dynamic json = jsonData;

            try
            {
                var userFile = new AddUserFileCommand()
                {
                    UserFileId = (int)json.UserFileId,
                    UserId = (int)json.UserId,
                    FileId = Convert.ToInt64(json.FileId),
                    AssociatedDateUTC = DateTime.UtcNow,
                    Active = true
                };

                this.bus.Send(userFile);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserFileAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListUsers()
        {
            IEnumerable<UserListDTO> UserList = _UserDao.ListUsers();

            return Request.CreateResponse(HttpStatusCode.OK, UserList);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListUserProfiles()
        {
            IEnumerable<UserProfileListDTO> UserProfilesList = _UserProfileDao.ListUserProfiles();

            return Request.CreateResponse(HttpStatusCode.OK, UserProfilesList);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListUserStatus()
        {
            IEnumerable<UserStatusListDTO> UserStatusList = _UserStatusDao.ListUserStatus();

            return Request.CreateResponse(HttpStatusCode.OK, UserStatusList);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage ListUserByPerson(Guid id)
        {
            IEnumerable<UserListDTO> UserList = _UserStatusDao.ListUserByPerson(id);

            return Request.CreateResponse(HttpStatusCode.OK, UserList);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public HttpResponseMessage CompleteRegistrationExternalUser()
        {
            //se tiver logo
            var provider = new MultipartMemoryStreamProvider();
            NameValueCollection parameters = HttpContext.Current.Request.Params;

            try
            {
                var command = new UpdateUserCommand();
                command.UserId = Convert.ToInt32(parameters["UserId"].ToString());
                command.SecundaryEmail = parameters["SecundaryEmail"].ToString();
                command.SmartPhoneNumber = parameters["SmartPhoneNumber"].ToString();
                command.UserStatusId = Heeelp.Core.Common.GeneralEnumerators.EnumUserStatus.Ativo;

                bus.Send(command);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("CompleteRegistartionUser: {0}", parameters));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage UserPreferenceAdd(JObject jsonData)
        {
            dynamic json = jsonData;

            try
            {
                var userPreference = new AddUserPreferenceCommand()
                {
                    UserPreferenceId = (int)json.UserPreferenceId,
                    UserId = (int)json.UserId,
                    CouponEstatisticModeId = Convert.ToByte(json.CouponEstatisticModeId),
                    CouponPeriodModeId = Convert.ToByte(json.CouponPeriodModeId),
                    SaveRecentQueries = json.SaveRecentQueries,
                    ShowRecentQueries = Convert.ToByte(json.ShowRecentQueries),
                    ShowRecentCoupons = Convert.ToBoolean(json.ShowRecentCoupons),
                    ShowRecentReviews = Convert.ToBoolean(json.ShowRecentReviews),
                    ShowRecentCheckins = Convert.ToBoolean(json.ShowRecentCheckins),
                    ShowPendentActions = Convert.ToBoolean(json.ShowPendentActions),
                    AcceptReceiveAlertAboutInterests = Convert.ToBoolean(json.AcceptReceiveAlertAboutInterests),
                    AcceptReceiveAlertAboutContent = Convert.ToBoolean(json.AcceptReceiveAlertAboutContent),
                    AcceptReceiveAlertAboutEvents = Convert.ToBoolean(json.AcceptReceiveAlertAboutEvents),
                    AcceptReceiveWizardSuggestions = Convert.ToBoolean(json.AcceptReceiveWizardSuggestions),
                    AcceptReceiveNewsletterOffers = Convert.ToBoolean(json.AcceptReceiveNewsletterOffers),
                    ShowFastProfileBar = Convert.ToBoolean(json.ShowFastProfileBar),
                    ToggleLeftNavigationMenu = Convert.ToBoolean(json.ToggleLeftNavigationMenu),
                    SelectedSkin = Convert.ToByte(json.SelectedSkin),
                    ConfigurationDateUTC = Convert.ToDateTime(json.ConfigurationDateUTC),
                    ShowFriendsActivity = Convert.ToBoolean(json.ShowFriendsActivity),
                    ShareActivitiesWithFriends = Convert.ToBoolean(json.ShareActivitiesWithFriends),
                    SearchDistance = Convert.ToByte(json.SearchDistance),
                    Active = Convert.ToBoolean(json.Active)
                };

                this.bus.Send(userPreference);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserPreferenceAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        public HttpResponseMessage UserInvited(UserInvitedDTO userInvited)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new AddUserInvitedCommand();

                    command.Name = userInvited.Name;
                    command.InviteCode = userInvited.InviteCode;
                    command.Email = userInvited.Email;
                    command.EnrollmentIP = userInvited.EnrollmentIP;

                    this.bus.Send(command);

                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
