using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web;
using Heeelp.Core.Logging;
using System.Collections.Generic;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Messaging;
using System.Web.Http.Description;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Common.Utils;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly IAutenticationUserDao _autenticationUserDao;
        private HttpClient _clientNotification;

        public AuthenticationController(IAutenticationUserDao autenticationUserDao, ICommandBus bus) : base()
        {
            this.bus = bus;
            this._autenticationUserDao = autenticationUserDao;
        }


        [HttpPost]
        [ResponseType(typeof(UserFirstAccessDTO))]
        //[Authorize]
        [Route("api/Authentication/ActiveNewUser")]
        public HttpResponseMessage ActiveNewUser(UserFirstAccessDTO userInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bus.Send(new ActivateUserCommand
                    {
                        IntegrationCode = userInfo.IntegrationCode,
                        Password = userInfo.Password,
                        PhoneNumber = userInfo.PhoneNumber
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        [HttpPost]
        //[Authorize]
        [Route("api/Authentication/AuthFistAccess")]
        public HttpResponseMessage AuthFistAccess(TemporaryAccessdDTO userPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var auntentication = _autenticationUserDao.CheckAuthenticationFirstAccess(userPassword.IntegrationCode);
                        var response = new HttpResponseMessage();


                        if (auntentication != null)
                        {
                            response = Request.CreateResponse(HttpStatusCode.OK, auntentication);
                            LogManager.Info(string.Format("ActiveNewUser Success User IntegrationCode:{0}", userPassword.IntegrationCode));
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.Conflict);
                            LogManager.Warn(string.Format("ActiveNewUser Success Fail User IntegrationCode:{0}", userPassword.IntegrationCode));
                        }
                        return response;
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(string.Format("ActiveNewUser Error:{0}", ex.Message));
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                    }

                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        [HttpGet]
        public HttpResponseMessage ValidateAccessToken(Guid UserIntegrationCode)
        {
            try
            {
                var response = new HttpResponseMessage();
                var canActivateUser = _autenticationUserDao.CheckActivationUser(UserIntegrationCode);
                response = Request.CreateResponse(HttpStatusCode.OK, canActivateUser);
                LogManager.Info(string.Format("ValidateAccessToken Success UserToken:{0}", UserIntegrationCode));
                return response;
            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("ValidateAccessToken Error:{0}", ex.Message));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        [HttpPost]
        public HttpResponseMessage ValidateUser(JObject jsonData)
        {
            dynamic json = jsonData;
            string email = json.user.Email;
            string password = json.user.Password;



            var response = new HttpResponseMessage();
            try
            {
                var auntentication = _autenticationUserDao.CheckAuthentication(email, password);


                if (auntentication != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, auntentication);
                    LogManager.Info(string.Format("ValidateUser Success User:{0}, password:{1}", email, password));
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.Conflict);
                    LogManager.Warn(string.Format("ValidateUser Fail User:{0}, password:{1}", email, password));
                }
                return response;
            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("ValidateUser Error:{0}", ex.Message));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpPost]
        public HttpResponseMessage ValidateUserHash(UserHashDTO user)
        {
            if (user.Hash == Crypt.GerarHashMd5(user.IntegrationCode.ToString()))
            {

                var response = new HttpResponseMessage();
                try
                {
                    Authentication auntentication = _autenticationUserDao.CheckAuthenticationHash(user.IntegrationCode);


                    if (auntentication != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, auntentication);
                        LogManager.Info(string.Format("ValidateUser Success User:{0}, Hash:{1}", auntentication.UserId, user.Hash));
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.Conflict);
                        LogManager.Warn(string.Format("ValidateUser Fail Email:{0} Hash:{1}", user.IntegrationCode, user.Hash));
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("ValidateUser Error:{0}", ex.Message));
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                LogManager.Error(string.Format("ValidateUser Error: Invalid Hash"));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Hash");
            }





        }


        [HttpPost]
        public HttpResponseMessage ActiveUserEMail(JObject jsonData)
        {
            dynamic json = jsonData;
            var password = json.user.Domain.ToString();
            var email = json.user.Email.ToString();
            var token = json.user.Token.ToString();


            var response = new HttpResponseMessage();
            try
            {
                bool active = _autenticationUserDao.CheckAuthenticationActive(email, password, token);


                if (active)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    LogManager.Warn(string.Format("Erro ao ativar email, Email:{0}, Senha:{1}, Token:{2}", email, password, token));
                }
                return response;
            }
            catch (Exception ex)
            {
                LogManager.Warn(string.Format("Erro ao ativar email, Email:{0}, Senha:{1}, Token:{2}", email, password, token));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        private void SendMailWelcome(int userId, string user, string email, string password)
        {

            _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);


            Dictionary<string, string> listKeys = new Dictionary<string, string>();
            listKeys.Add("usuario", user);
            listKeys.Add("Email", email);
            listKeys.Add("psw", password);

            var message = new { UserFromId = userId, UserToId = userId, LanguageId = (int)GeneralEnumerators.EnumLanguage.Portuguese, MessageCodeType = "LoginWellGoWork", ListKeys = listKeys };
            var resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultNotification.IsSuccessStatusCode)
            {
                LogManager.Info(string.Format("Erro ao enviar msg de boas vindas para o userId: {0}, user: {1}, email: {2}", userId, user, email));

            }
        }

    }
}
