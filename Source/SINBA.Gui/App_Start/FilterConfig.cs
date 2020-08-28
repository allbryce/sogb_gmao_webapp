using System.Web;
using System.Web.Mvc;
using Sinba.Gui.Security;

namespace Sinba_Gui {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}