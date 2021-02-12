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
    [RoutePrefix("DonneesDeBase/GestionMateriel/Caracteristique")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    public class CaracteristiqueController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Caracteristique; } }
        public CaracteristiqueController(IDonneesDeBaseService donnesDeBaseService)
        {
            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        // GET: Caractéristique
        [ClaimsAuthorize(SinbaConstants.Controllers.Caracteristique, SinbaConstants.Actions.Index)]
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetCaracteristiqueList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Caracteristique, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetCaracteristiqueList());
        }

        #region Add
        [HttpGet]
        [ClaimsAuthorize(SinbaConstants.Controllers.Caracteristique, SinbaConstants.Actions.Add)]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            CaracteristiqueComposant caracteristique = new CaracteristiqueComposant();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, caracteristique);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Add(CaracteristiqueComposant caracteristique)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
            }
            var dto = donnesDeBaseService.Insertcaracteristique(caracteristique);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Edit
       
        [ClaimsAuthorize(SinbaConstants.Controllers.Caracteristique, SinbaConstants.Actions.Edit)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long Id)
        {
            if (Id != 0)
            {
                var dto = donnesDeBaseService.GetCaracteristique(Id);
                TreatDto(dto);
                var caracteristique = dto.Value;
                if (caracteristique != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, caracteristique);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(CaracteristiqueComposant caracteristique)
        {
            if (caracteristique != null)
            {
                var dto = donnesDeBaseService.UpdateCaracteristiqueComposant(caracteristique);
                TreatDto(dto);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        #region Delete

        [Route(SinbaConstants.Routes.DeleteId)]

        public ActionResult Delete(long id)
        {
            if (id != 0)
            {
                var dtoDelete = donnesDeBaseService.DeleteCaracteristique(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion


        public List<CaracteristiqueComposant> GetCaracteristiqueList()
        {
            List<CaracteristiqueComposant> lst = new List<CaracteristiqueComposant>();
            var dto = donnesDeBaseService.GetCaracteristiqueList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }
        #endregion


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