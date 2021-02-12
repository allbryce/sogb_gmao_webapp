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
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("DonneesDeBase/GestionMateriel/Domaine")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    public class DomaineController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Domaine; } }
        public DomaineController(IDonneesDeBaseService donnesDeBaseService)
        {
            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Domaine, SinbaConstants.Actions.Index)]
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetDomaineList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Domaine, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetDomaineList());
        }

        [Route(SinbaConstants.Routes.EditId)]
        [ClaimsAuthorize(SinbaConstants.Controllers.Domaine, SinbaConstants.Actions.Edit)]
        public ActionResult Edit(long? Id)
        {
            if (Id != 0)
            {
                var dto = donnesDeBaseService.GetDomaine(Id??0);
                TreatDto(dto);
                var domaine = dto.Value;
                if (domaine != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, domaine);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Domaine domaine)
        {
            if (domaine != null)
            {
                var dto = donnesDeBaseService.UpdateDomaine(domaine);
                TreatDto(dto);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpGet]
        [ClaimsAuthorize(SinbaConstants.Controllers.Domaine,SinbaConstants.Actions.Add)]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            Domaine domaine = new Domaine();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, domaine);
        }
        #region Delete

        [Route(SinbaConstants.Routes.DeleteId)]

        public ActionResult Delete(long id)
        {
            if (id != 0)
            {
                var dtoDelete = donnesDeBaseService.DeleteDomaine(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion
        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Domaine domaine)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertDomaine(domaine);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        public List<Domaine> GetDomaineList()
        {
            List<Domaine> lst = new List<Domaine>();
            var dto = donnesDeBaseService.GetDomaineList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }
        #region ViewBag
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Domaine);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.AddMode = addMode;
        }
        #endregion
    }
}