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
    #region Attributes

    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("DonneesDeBase/GestionMateriel/Fournisseur")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class FournisseurController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Fournisseur; } }
        public FournisseurController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        #region List

        [ClaimsAuthorize(SinbaConstants.Controllers.Fournisseur, SinbaConstants.Actions.Index)]
        public ActionResult Index()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetFournisseurList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Fournisseur, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetFournisseurList());
        }
      
        #endregion

        #region Add

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Fournisseur fournisseur)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertFournisseur(fournisseur);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);       
        }
        [HttpGet]
        [ClaimsAuthorize]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            Fournisseur fournisseur = new Fournisseur();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, fournisseur);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetFournisseur(id);
                TreatDto(dto);
                var fournisseur = dto.Value;
                if (fournisseur != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial,fournisseur);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Fournisseur fournisseur)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, fournisseur);
            }
            var dto = donnesDeBaseService.UpdateFournisseur(fournisseur);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete

        [Route(SinbaConstants.Routes.DeleteId)]

        public ActionResult Delete(long id)
        {
            if (id != 0)
            {
                var dtoDelete = donnesDeBaseService.DeleteFournisseur(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Fournisseur);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }   
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.AddMode = addMode;
        }
        #endregion

        #region List
        private List<Fournisseur> GetFournisseurList()
        {
            List<Fournisseur> lst = new List<Fournisseur>();
            var dto = donnesDeBaseService.GetFournisseurList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        #endregion
    }
}
