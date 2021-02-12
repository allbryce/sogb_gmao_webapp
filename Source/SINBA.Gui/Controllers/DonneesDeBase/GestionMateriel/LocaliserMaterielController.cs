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
    [RoutePrefix("DonneesDeBase/GestionMateriel/LocaliserMateriel")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class LocaliserMaterielController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;

        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.LocaliserMateriel; } }
        public LocaliserMaterielController(IDonneesDeBaseService donnesDeBaseService)
        {
            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            List<LocaliserMateriel> lst = new List<LocaliserMateriel>();
            lst = GetLocaliserMaterielList();
            return SinbaView(ViewNames.Index,lst);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.LocaliserMateriel, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            List<LocaliserMateriel> lst = new List<LocaliserMateriel>();
            lst = GetLocaliserMaterielList();
            return PartialView(ViewNames.ListPartial, lst);
        }

        

        private List<LocaliserMateriel> GetLocaliserMaterielList()
        {
            List<LocaliserMateriel> lst = new List<LocaliserMateriel>();
            var dto = donnesDeBaseService.GetLocaliserMaterielList();
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
            LocaliserMateriel localisation = new LocaliserMateriel();
            FillViewBag(true);            
            return SinbaView(ViewNames.EditPartial, localisation);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(LocaliserMateriel localisation)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, localisation);
            }
            
            var dto = donnesDeBaseService.InsertLocaliserMateriel(localisation);    
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetLocaliserMateriel(id);
                TreatDto(dto);
                var localisation = dto.Value;
                var localiser = new LocaliserMateriel();
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
        public ActionResult Edit(LocaliserMateriel localisation)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, localisation);
            }
            //localisation.LocaliserMaterielId = long.Parse(ViewData[DbColumns.LocaliserMaterielId].ToString());
            var localiser = new LocaliserMateriel();
            localiser.LocaliserMaterielId = localisation.LocaliserMaterielId;
            localiser.LibelleLocaliserMateriel = localisation.LibelleLocaliserMateriel;
            var dto = donnesDeBaseService.UpdateLocaliserMateriel(localiser);
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
                var dtoDelete = donnesDeBaseService.DeleteLocaliserMateriel(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag

        private void FillViewBag(bool addMode = false)
        {
            List<Service> service = new List<Service>();
            List<Departement> departement = new List<Departement>();
            var dto = donnesDeBaseService.GetServiceList();
            var dto1 = donnesDeBaseService.GetDepartementList();
            service = dto.Value.ToList();
            departement = dto1.Value.ToList();
            ViewBag.AddMode = addMode;
            ViewBag.Service = service;
            ViewBag.Departement = departement;
        }

        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.LocaliserMateriel);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);           
        }

        #endregion
    }
}