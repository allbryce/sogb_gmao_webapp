using System.Web.Mvc;
using System.Web.Routing;

namespace Sinba_Gui
{
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapMvcAttributeRoutes();


            //routes.MapRoute(
            //    name: "Folder", // Route name
            //    url: "{SectionItem}/{SectionGroup}/{controller}/{action}/{id}", // URL with parameters
            //    defaults: new { controller = "{controller}", action = "{action}", id = UrlParameter.Optional } // Parameter defaults
            //);

            routes.MapRoute(
                name: "Default", // Route name
                url: "{controller}/{action}/{id}", // URL with parameters
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            ); 
        }
    }
}