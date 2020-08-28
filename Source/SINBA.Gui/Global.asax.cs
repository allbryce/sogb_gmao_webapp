using DevExpress.Web.Mvc;
using Sinba.Gui.TemplateCode;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sinba_Gui
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AuthConfig.RegisterAuth();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LanguageConfig.ChangeLanguage("fr");
            
            ModelBinders.Binders.DefaultBinder = new DevExpressEditorsBinder();

            DevExpress.Web.ASPxWebControl.CallbackError += Application_Error;

        }

        protected void Application_Error(object sender, EventArgs e) 
        {
            Exception exception = System.Web.HttpContext.Current.Server.GetLastError();
            //TODO: Handle Exception
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            DevExpressHelper.Theme = Utils.CurrentTheme;
            if (DevExpressHelper.IsCallback)
                Utils.RegisterCurrentMvcSectionOnCallback();
        }
    }
}