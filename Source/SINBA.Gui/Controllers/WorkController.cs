using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    [Authorize]
    public class WorkController : SectionController
    {
        public override string ItemName { get { return ""; } }
        public override string GroupName { get { return ""; } }
        public override string ControllerName { get { return "Test"; } }
        
        
        public WorkController()
        {

        }
        
        
        // GET: Test
        public ActionResult Index()
        {
            return SinbaView("Index");
        }
    }
}