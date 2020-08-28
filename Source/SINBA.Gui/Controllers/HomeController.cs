using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using DevExpress.Web.Mvc;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.Controllers;
using Sinba.Gui.TemplateCode;
using Sinba.Gui.UIModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;


namespace Sinba_Gui.Controllers
{
    [IHMLog()]
    [IhmExceptionHandler()]
    public class HomeController : SectionController
    {
        public override string ItemName { get { return ""; } }
        public override string GroupName { get { return ""; } }
        public override string ControllerName { get { return "Home"; } }

        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        private string idSite;
        private string idUser;
        #endregion

        public HomeController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
            idSite = GetUserSiteId();
            idUser = GetUserId();
        }

        public ActionResult SearchListPartial(string text)
        {
            if (DevExpressHelper.IsCallback)
                Thread.Sleep(500);
            ViewData["RequestText"] = text;
            return PartialView(ViewNames.SearchListPartial, SearchTools.DoSearch(text));
        }


        public ActionResult Index()
        {
            return SinbaView("Index");
        }


    }
}

public enum HeaderViewRenderMode { Full, Menu, Title }