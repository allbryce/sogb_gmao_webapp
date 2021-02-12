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
    [RoutePrefix("DonneesDeBase/GestionMateriel/SousFamille")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class SousFamilleController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        long marqueid;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.SousFamille; } }
        public SousFamilleController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        #region List

        [ClaimsAuthorize(SinbaConstants.Controllers.SousFamille, SinbaConstants.Actions.Index)]
        public ActionResult Index()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetSousFamilleList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.SousFamille, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetSousFamilleList());
        }
      
        #endregion

        #region Add

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(SousFamille famille)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertSousFamille(famille);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);       
        }
        [HttpGet]
        [ClaimsAuthorize]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            SousFamille famille = new SousFamille();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, famille);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                marqueid = id;
                var dto = donnesDeBaseService.GetSousFamille(id);
                TreatDto(dto);
                var marque = dto.Value;
                if (marque != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, marque);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(SousFamille marque)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, marque);
            }
            var dto = donnesDeBaseService.UpdateSousFamille(marque);
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
                var dtoDelete = donnesDeBaseService.DeleteSousFamille(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.SousFamille);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }   
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.AddMode = addMode;
            ViewBag.Famille = GetFamilleList();
        }
        #endregion

        #region List
        private List<SousFamille> GetSousFamilleList()
        {
            List<SousFamille> lst = new List<SousFamille>();
            var dto = donnesDeBaseService.GetSousFamilleList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }
        private List<Famille> GetFamilleList()
        {
            List<Famille> lst = new List<Famille>();
            var dto = donnesDeBaseService.GetFamilleList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        #endregion
    }
}
