using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    public class AdministrationAccesController : SectionController
    {
        public override string ItemName { get { return Strings.Administration; } }
        public override string GroupName { get { return Strings.Acces; } }
        public override string ControllerName
        {
            get { throw new NotImplementedException(); }
        }
    }
}