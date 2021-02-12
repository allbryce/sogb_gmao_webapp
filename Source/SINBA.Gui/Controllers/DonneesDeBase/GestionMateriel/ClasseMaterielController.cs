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
    [RoutePrefix("DonneesDeBase/GestionMateriel/ClasseMateriel")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class ClasseMaterielController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        long composantid;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.ClasseMateriel; } }
        public ClasseMaterielController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        #region List

        [ClaimsAuthorize(SinbaConstants.Controllers.ClasseMateriel, SinbaConstants.Actions.Index)]
        public ActionResult Index()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetClasseMaterielList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.ClasseMateriel, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetClasseMaterielList());
        }
       


        #endregion

        #region Add

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Classemateriel classemateriel)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertClasseMateriel(classemateriel);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);       
        }
        [HttpGet]
        [ClaimsAuthorize]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            Classemateriel composant = new Classemateriel();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, composant);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                composantid = id;
                var dto = donnesDeBaseService.GetClasseMateriel(id);
                TreatDto(dto);
                var composant = dto.Value;
                if (composant != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, composant);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Classemateriel classe)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, classe);
            }
            var dto = donnesDeBaseService.UpdateClasseMateriel(classe);
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
                var dtoDelete = donnesDeBaseService.DeleteClasseMateriel(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.ClasseMateriel);
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
        private List<Classemateriel> GetClasseMaterielList()
        {
            List<Classemateriel> lst = new List<Classemateriel>();
            var dto = donnesDeBaseService.GetClassematerielList();       
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

       

        #endregion

    }
}
