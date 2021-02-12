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
using Sinba.BusinessModel.Entity.ViewModels;

namespace Sinba.Gui.Controllers
{
    #region Attributes
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("DonneesDeBase/GestionMateriel/Localisation")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class LocalisationController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Localisation; } }
        public LocalisationController(IDonneesDeBaseService donnesDeBaseService)
        {
            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        [ClaimsAuthorize(SinbaConstants.Controllers.Localisation, SinbaConstants.Actions.Index)]
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            List<Localisation> lst = new List<Localisation>();
            return SinbaView(ViewNames.ListPartial,GetLocalisationList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Localisation, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            List<Localisation> lst = new List<Localisation>();
            lst = GetLocalisationList();
            return PartialView(ViewNames.ListPartial, lst);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Localisation, SinbaConstants.Actions.Index)]
        public ActionResult AffectationModal(long id)
        {
            //AffectationPartial(id);
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.AffectationModal);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Localisation, SinbaConstants.Actions.Index)]
        public ActionResult AffectationPartial(long id)
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            List<LocaliserMateriel> lst = new List<LocaliserMateriel>();
            var dto = donnesDeBaseService.GetAffecterMaterielList(id);
            lst = dto.Value.ToList();
            return PartialView(ViewNames.AffectationPartial, lst);
        }

        private List<Localisation> GetLocalisationList()
        {
            List<Localisation> lst = new List<Localisation>();
            var dto = donnesDeBaseService.GetLocalisationList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        #region Add
        [HttpGet]
        public ActionResult Add()
        {
            LocalisationViewModel localisation = new LocalisationViewModel();
            FillViewBag(true);            
            return SinbaView(ViewNames.EditPartial, localisation);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(LocalisationViewModel localisation)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, localisation);
            }
            var localiser = new Localisation();
            localiser = localisation.ToLocalisation();
            var dto = donnesDeBaseService.InsertLocalisation(localiser);    
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetLocalisation(id);
                TreatDto(dto);
                var localisation = dto.Value;
                var localiser = new LocalisationViewModel();
                localiser = localisation.ToViewModel(localisation);
                if (localisation != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, localiser);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(LocalisationViewModel localisation)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, localisation);
            }
            var localiser = new Localisation();
            localiser.LocalisationId = localisation.LocalisationId;
            localiser.LibelleLocalisation = localisation.LibelleLocalisation;
            var dto = donnesDeBaseService.UpdateLocalisation(localiser);
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
                var dtoDelete = donnesDeBaseService.DeleteLocalisation(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.AddMode = addMode;
            ViewBag.Service = donnesDeBaseService.GetServiceList().Value.ToList();
            ViewBag.Departement = donnesDeBaseService.GetDepartementList().Value.ToList();           
            ViewBag.Localisation = donnesDeBaseService.GetMaterielList().Value.ToList();
        }

        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Localisation);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);           
        }
   
        #endregion
    }
}