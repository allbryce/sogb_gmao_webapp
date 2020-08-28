using Sinba.Resources;
using System;

namespace Sinba.Gui.Controllers
{
    public class DonneesDeBaseDonneesDeBaseController : SectionController
    {
        public override string ItemName { get { return Strings.DonneesDeBase; } }
        public override string GroupName { get { return Strings.DonneesDeBase; } }
        public override string ControllerName
        {
            get { throw new NotImplementedException(); }
        }
    }
}