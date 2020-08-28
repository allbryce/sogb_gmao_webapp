using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sinba_Gui
{
    public class LanguageConfig
    {
        public static void ChangeLanguage(string iso)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture(iso);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}