using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Snap_Bank.Filter
{
    public class AuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            String username = filterContext.HttpContext.Session["user"]?.ToString();
            if (String.IsNullOrEmpty(username))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary()
                    {
                        { "controller", "Login" },
                        { "action", "Signin" },
                        { "redirectUrl", filterContext.HttpContext.Request.Url.AbsolutePath }
                    }
                );
            }
        }
    }
}