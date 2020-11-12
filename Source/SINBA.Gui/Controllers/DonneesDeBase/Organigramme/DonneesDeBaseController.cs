using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sinba.Resources;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.UIModels;
using Sinba.BusinessModel.Entity;
using Sinba.Gui.Security;
using Dev.Tools.Exceptions;
using Dev.Tools.Logger;

namespace Sinba.Gui.Controllers
{
    #region Attribut
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("DonneesDeBase/Organigramme/DonneesDeBase")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion

    public class DonneesDeBaseController : DonneesDeBaseOrganigrammeController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.DonneesDeBase; } }
        public DonneesDeBaseController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        [ClaimsAuthorize(SinbaConstants.Controllers.DonneesDeBase, SinbaConstants.Actions.Index)]
        public ActionResult Index()
        {
            return SinbaView(ViewNames.Index);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.DonneesDeBase, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial);
        }

        private void FillAuthorizedActionsViewBag()

        {

            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.DonneesDeBase);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);

        }

        private void FillViewBag(bool addMode = false)

        {
        }
    }
}