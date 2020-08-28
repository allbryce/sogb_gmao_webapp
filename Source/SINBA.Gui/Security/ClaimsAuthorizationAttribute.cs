using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sinba.Gui.Security
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string claimType;
        private string claimValue;
        public ClaimsAuthorizeAttribute(string type, string value)
        {
            this.claimType = type;
            this.claimValue = value;
        }
        public ClaimsAuthorizeAttribute()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var controllerName = string.Empty;
            var actionName = string.Empty;
            bool hasAccess = false;

            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                hasAccess = true;
            }
            else
            {
                var user = filterContext.HttpContext.User as ClaimsPrincipal;

                if (user == null || !user.Identity.IsAuthenticated)
                {
                    CustomAttributesHelper.RedirectToLogin(filterContext);
                    return;
                }
                else
                {
                    if (filterContext.ActionDescriptor != null)
                    {
                        this.claimType = null;
                        this.claimValue = null;

                        if (filterContext.ActionDescriptor.IsDefined(typeof(ClaimsAuthorizeAttribute), false))
                        {
                            var attributes = filterContext.ActionDescriptor.GetFilterAttributes(false);
                            var claimsAuthorizeAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(ClaimsAuthorizeAttribute));
                            if (claimsAuthorizeAttribute != null)
                            {
                                this.claimType = ((ClaimsAuthorizeAttribute)claimsAuthorizeAttribute).claimType;
                                this.claimValue = ((ClaimsAuthorizeAttribute)claimsAuthorizeAttribute).claimValue;
                            }
                        }

                        actionName = filterContext.ActionDescriptor.ActionName;
                        if (filterContext.ActionDescriptor.ControllerDescriptor != null)
                        {
                            controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                        }
                    }

                    // Ces valeurs sont prioritaires.
                    if (!string.IsNullOrEmpty(claimType) && !string.IsNullOrEmpty(claimValue))
                    {
                        controllerName = claimType;
                        actionName = claimValue;
                    }

                    hasAccess = user.Identity.HasClaimAccess(controllerName, actionName);
                }
            }

            if (hasAccess)
            {
                SetCachePolicy(filterContext);
                base.OnAuthorization(filterContext);
            }
            else
            {
                CustomAttributesHelper.RedirectToUnauthorized(filterContext);
            }
        }

        protected void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }

    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RolesAttribute : AuthorizeAttribute
    {
        List<string> rolesList = new List<string>();

        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
            rolesList = roles.ToList();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;

            if (user != null && user.Identity.IsAuthenticated && !user.Identity.ExistsInRoles(rolesList))
            {
                CustomAttributesHelper.RedirectToUnauthorized(filterContext);
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }
    }

    public class CustomAttributesHelper
    {
        public static void RedirectToUnauthorized(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = SinbaConstants.Controllers.Account, action = SinbaConstants.Actions.NotAuthorized }));
        }

        public static void RedirectToLogin(AuthorizationContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url;
            filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = SinbaConstants.Controllers.Account, action = SinbaConstants.Actions.Login, returnUrl = url }));
        }
    }
}