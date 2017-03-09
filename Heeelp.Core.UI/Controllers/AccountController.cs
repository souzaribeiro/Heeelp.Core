using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel;
using Heeelp.Core.Logging;
using Heeelp.Core.UI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Heeelp.Core.UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Authentication
        protected string user_session = CustomConfiguration.UserSessionName;
        private HttpClient _client;


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoGon(LoginViewModel login)
        {

            string email = login.Email;
            string password = login.Password;

            var user = new User { Email = email, Password = password };

            try
            {


                HttpCookie cookie = Request.Cookies[user_session];
                int userSession = cookie == null ? 0 : int.Parse(cookie.Value);


                LogManager.Info(string.Format("Account User:{0}, UserSession:{1}", user, userSession));

                //int userSession = int.Parse(Request.Cookies[user_session].Value);
                _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiCore);
                var response = _client.PostAsJsonAsync("/api/Authentication/ValidateUser", new { user }).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var authenticationResponse = response.Content.ReadAsAsync<Authentication>().Result;
                    AuthenticateUser(authenticationResponse.UserId, userSession, email, password);
                    //if(model.KeepLogged)
                    //    UserSession.SelectedProduct = EnumProducts.GetConference;
                    //else
                    //    UserSession.SelectedProduct = EnumProducts.GetDoc;

                    //var x = authenticationResponse;
                    LogManager.Info(string.Format("Account User logado:{0}, UserSession:{1}", authenticationResponse.UserId, userSession));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Response.StatusCode = (int)response.StatusCode;
                    string result = response.Content.ReadAsAsync<string>().Result;
                    ModelState.AddModelError("ErrorApplication", result);
                    LogManager.Error(string.Format("Account Error:{0} ", new { ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).ToList(), IsError = true }));
                    LogManager.Error(string.Format("Call Validate User address: [{0}], Error : [{1}] ", _client.BaseAddress, response));
                    ViewBag.MsgError = "Erro Ao Fazer Login";
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("Error:{0},  Account User:{1}", ex, user));
                ViewBag.MsgError = "Erro Ao Fazer Login";
                return RedirectToAction("Index", "Home");
            }
        }

        private void AuthenticateUser(int idUser, int idUserSession, string email, string password)
        {

            try
            {


                var x = HttpContext.Request.Browser.Platform;

                var IntegrationCode = Guid.NewGuid();


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(CustomConfiguration.WebApiContab);

                var AddUserSession = new
                {
                    IntegrationCode = IntegrationCode,
                    UserId = idUser,
                    AuthStartDateUTC = DateTime.UtcNow,
                    IP = GetUserIP(),
                    //Localization = "POINT(50.0 30.0)",
                    AuthenticationTypeId = 0,//???
                    ClientApplicationId = (int)GeneralEnumerators.EnumClientApplication.WebBrowser,
                    //Origin= null,
                    //InviteId=null,
                    LanguageId = 1,         // enum?
                                            // AuthToken = null,
                    UserSessionStatusId = 1,// enum?
                    Active = false,
                    ClientId = 0,
                    Product = HttpContext.Request.Browser.Type,
                    Version = HttpContext.Request.Browser.Version,
                    OperationalSystem = HttpContext.Request.Browser.Platform
                    //,         Resolution="40x50"
                };

                var response = client.PostAsJsonAsync(string.Format("{0}api/UserSession/AddUserSession", CustomConfiguration.WebApiContab), AddUserSession).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    LogManager.Info(string.Format("UserSession Response: [{0}], Error : [{1}] ", _client.BaseAddress, response));
                    var res = response.Content.ReadAsAsync<dynamic>().Result;
                    
                    //var cookiedateOfExpires = new HttpCookie("User", idUser.ToString());
                    //Response.Cookies.Add(cookiedateOfExpires);




                    LogManager.Info(string.Format("Created User Session ", Request.Cookies[user_session]));
                }
                else
                {
                    LogManager.Error(string.Format("Call Validate User address: [{0}], Error : [{1}] ", CustomConfiguration.WebApiContab, response));
                    LogManager.Error(string.Format("Erro ao criar Sesseion: {0}, Message: {1}", AddUserSession, response));
                }


                FormsAuthentication.SetAuthCookie(idUserSession.ToString(), true);
                var dateOfExpires = DateTime.UtcNow.AddHours(1);

                var usersessionCookie = new HttpCookie(user_session, IntegrationCode.ToString());
                Response.Cookies.Add(usersessionCookie);

                string CoreToken = GenerateTokenWebApiCore(email, password);
                var s = new System.Web.Script.Serialization.JavaScriptSerializer();
                var userProfile = s.Serialize(new { idUserSession = IntegrationCode, Name = "UserName", CoreToken });
                var profileId = new HttpCookie("profile", userProfile.ToString());
                Response.Cookies.Add(profileId);


            }
            catch (Exception ex)
            {

                LogManager.Error(string.Format("Erro ao criar Sesseion [{0}]", ex));

            }

            //if (FormsAuthentication.CookieDomain.ToLower() != "localhost") cookieUserSession.Domain = FormsAuthentication.CookieDomain;
            //Response.Cookies.Add(cookieUserSession);
        }

        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }

        private string GenerateTokenWebApiCore(string email, string password)
        {
            string token = string.Empty;
            _client = new HttpClient();

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", email));
            postData.Add(new KeyValuePair<string, string>("password", password));
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));

            HttpContent content = new FormUrlEncodedContent(postData);
            var response = _client.PostAsync(CustomConfiguration.WebApiCore + "TokenInternal", content).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var authenticationResponse = response.Content.ReadAsAsync<dynamic>().Result;
                token = authenticationResponse["access_token"].ToString();
            }
            return token;

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
           
            ClearSession();
            return View();
        }
        public void ClearSession()
        {
            FormsAuthentication.SignOut();
            var erasableCookies = new[] {

                user_session,
                FormsAuthentication.FormsCookieName,
                "ASP.NET_SessionId"   ,
                "profile"  ,
                "User"
            };
            foreach (string nameCookie in Request.Cookies.AllKeys)
            {
                if (erasableCookies.Contains(nameCookie))
                {
                    HttpCookie cookie = new HttpCookie(nameCookie);
                    cookie.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            FormsAuthentication.SignOut();
        }

        //[Authorize]
        //public ActionResult Logout()
        //{
        //    Request.GetOwinContext().Authentication.SignOut();
        //    return View();
        //}

    }
}