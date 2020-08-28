using Sinba.Resources;
using System;

namespace Sinba.Gui.Controllers
{
    public class AdministrationListeController : SectionController
    {
        public override string ItemName { get { return Strings.Administration; } }
        public override string GroupName { get { return Strings.Liste; } }
        public override string ControllerName
        {
            get { throw new NotImplementedException(); }
        }
    }
}