using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using DevExpress.Web;
using DevExpress.Web.Internal;
using Sinba.BusinessModel.Entity;
using Sinba.Gui.Resources;
using Sinba.Gui.TemplateCode;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    /// <summary>
    /// Controller for Sections
    /// </summary>
    [IHMLog()]
    [IhmExceptionHandler()]
    public abstract class SectionController : SinbaControllerBase
    {
        #region Constants
        public const string ViewTitleSuffix = "Sinba application";
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the item (Navigation).
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public abstract string ItemName { get; }

        /// <summary>
        /// Gets the name of the group (ItemGroup).
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        public abstract string GroupName { get; }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public abstract string ControllerName { get; }

        /// <summary>
        /// Gets the ie compatibility version.
        /// </summary>
        /// <value>
        /// The ie compatibility version.
        /// </value>
        protected virtual int IECompatibilityVersion { get { return -1; } }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Methods
        public ActionResult SinbaView(string viewName)
        {
            return SinbaView(viewName, null);
        }

        public ActionResult SinbaInfoView()
        {
            return SinbaInfoView(null);
        }

        public ActionResult SinbaErrorView()
        {
            return SinbaErrorView(null);
        }

        public ActionResult SinbaView(string viewName, string title, bool useSectionSecondTitle = false)
        {
            return SinbaView(viewName, null, title, useSectionSecondTitle);
        }

        public ActionResult SinbaView(string viewName, object model)
        {
            return SinbaView(viewName, model, null);
        }

        public ActionResult SinbaView(string viewName, object model, string title, bool useSectionSecondTitle = false)
        {
            Utils.RegisterCurrentMvcSection(ItemName, GroupName, ControllerName, title, useSectionSecondTitle);
            return (model != null) ? View(viewName, model) : View(viewName);
        }

        public ActionResult SinbaInfoView(object model)
        {
            return SinbaView(ViewNames.Info, model, SharedResource.InfoTitle);
        }

        public ActionResult SinbaErrorView(object model)
        {
            return SinbaView(ViewNames.Error, model, SharedResource.ErrorTitle);
        }

        /// <summary>
        /// Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (RenderUtils.Browser.IsIE)
            {

                bool isDemoRequiredCompatibilityMode = IsDemoRequiredCompatibilityMode() && !Utils.IsIE10();
                if (isDemoRequiredCompatibilityMode)
                {
                    ASPxWebControl.SetIECompatibilityMode(IECompatibilityVersion);
                }
                else
                {
                    ASPxWebControl.SetIECompatibilityModeEdge();
                }
            }
        }

        /// <summary>
        /// Determines whether [is demo required compatibility mode].
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsDemoRequiredCompatibilityMode()
        {
            return false;
        }

        /// <summary>
        /// Maps the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        protected internal string MapPath(string path)
        {
            return System.Web.HttpContext.Current.Request.MapPath(path);
        }

        public Uri WebsiteUrl
        {
            get { return System.Web.HttpContext.Current.Request.Url; }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        protected bool IsSuperAdmin(List<string> userRoles = null)
        {
            bool ret = false;
            if (userRoles == null)
            {
                userRoles = GetUserRoles();
            }
            if (userRoles != null && userRoles.Count > 0)
            {
                ret = userRoles.Contains(SinbaRoles.SuperAdmin);
            }
            return ret;
        }

        protected bool AccessDenied(SinbaUser utilisateur, List<string> userRoles = null)
        {
            if (userRoles == null)
            {
                userRoles = GetUserRoles();
            }

            if (userRoles != null)
            {
                if (utilisateur.Roles.Any(r => r.RoleId.Equals(SinbaRoles.SuperAdmin, System.StringComparison.OrdinalIgnoreCase) ||
                    r.RoleId.Equals(SinbaRoles.AdminSite, System.StringComparison.OrdinalIgnoreCase) || r.RoleId.Equals(SinbaRoles.Admin)))
                {
                    if (!userRoles.Contains(SinbaRoles.SuperAdmin))
                    {
                        ViewBag.errorMessage = CommonResource.errorDataInaccessible;
                        return true;
                    }
                }
            }
            return false;
        }

        protected List<string> GetYearList(int startYear = 1950, int? endYear = null)
        {
            List<string> anneeList = new List<string>();

            for (int i = endYear ?? DateTime.Now.Year; i >= startYear; i--)
            {
                anneeList.Add(i.ToString());
            }

            return anneeList;
        }

        protected string GetViewName(string controllerName, string viewName)
        {
            return $"~/Views/{controllerName}/{viewName}.cshtml";
        }

        protected string GetReportViewName(string viewName)
        {
            return string.Format("~/Views/Rapports/{0}.cshtml", viewName);
        }

        protected string GetReportViewNameWithParameters(string viewName)
        {
            return string.Format("~/Views/Rapports/{0}/{1}/{2}.cshtml", GroupName, ControllerName, viewName);
        }
        
        #endregion
    }
}