using System.Web.Optimization;

namespace Sinba_Gui
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.min.css",
                         "~/Content/Components.css",
                         "~/Content/Site.css",
                         "~/Content/Platform.css",
                         "~/Content/Exception.css"));
        }
    }
}