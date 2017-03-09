using System.Web.Mvc;
using System.Web.Routing;

namespace Heeelp.Core.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home SPA",
                url: "Expertise/{*catchall}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
               name: "Promotion SPA",
               url: "Promotion/{*catchall}",
               defaults: new { controller = "Home", action = "Index" }
           );
            routes.MapRoute(
               name: "Person SPA",
               url: "Person/{*catchall}",
               defaults: new { controller = "Home", action = "Index" }
           );
            routes.MapRoute(
               name: "PersonAddress SPA",
               url: "Person/{*catchall}",
               defaults: new { controller = "Home", action = "Index" }
           );
            routes.MapRoute(
               name: "PersonDocument SPA",
               url: "Person/{*catchall}",
               defaults: new { controller = "Home", action = "Index" }
           );
            routes.MapRoute(
                name: "Notification SPA",
                url: "Notification/{*catchall}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "User SPA",
                url: "User/{*catchall}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                 name: "Account",
                 url: "Account/Login",
                 defaults: new { controller = "Account", action = "Login" }
             );

            routes.MapRoute(
             name: "Logout",
             url: "Account/Logout",
             defaults: new { controller = "Account", action = "LogOut" }
         );
        }
    }
}
