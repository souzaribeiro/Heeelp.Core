using System.Web.Mvc;
using System.Web.Routing;

namespace Heeelp.Core.UI.Security
{
    public sealed class IsAuthenticatedAttribute : AuthorizeAttribute
    {
        private bool _isAuthenticated;



        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            _isAuthenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;


            base.OnAuthorization(filterContext);

            if (!_isAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                                                    { "action", "Login" },
                                                                    { "controller", "Account" },
                                                                });

            }

        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (_isAuthenticated)
            {

            }

            return isAuthorized;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 403;
            filterContext.Result = new JsonResult()
            {
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}