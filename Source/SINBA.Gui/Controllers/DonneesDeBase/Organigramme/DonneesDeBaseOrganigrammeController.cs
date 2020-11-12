using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    public class DonneesDeBaseOrganigrammeController : SectionController
    {
        public override string ItemName { get { return Strings.DonneesDeBase; } }
        public override string GroupName { get { return Strings.Organigramme; } }
        public override string ControllerName
        {
            get { throw new NotImplementedException(); }
        }
    }
}