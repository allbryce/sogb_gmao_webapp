using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    public class DonneesDeBaseGestionMaterielController : SectionController
    {
        public override string ItemName { get { return Strings.DonneesDeBase; } }
        public override string GroupName { get { return Strings.GestionMateriel; } }
        public override string ControllerName
        {
            get { throw new NotImplementedException(); }
        }
    }
}