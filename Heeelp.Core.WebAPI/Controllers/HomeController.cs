using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Heeelp.Core.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "usuario1", DateTime.Now, DateTime.Now.AddMinutes(30), true, String.Empty, FormsAuthentication.FormsCookiePath);
            //string encryptedCookie = FormsAuthentication.Encrypt(ticket);
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
            //cookie.Expires = DateTime.Now.AddMinutes(30);
            //Response.Cookies.Add(cookie);
            //FormsAuthentication.RedirectFromLoginPage(userName, false);


            //Context.User.Identity.Name

            ViewBag.Title = "Heeelp!";

            return View();
        }
    }
}
