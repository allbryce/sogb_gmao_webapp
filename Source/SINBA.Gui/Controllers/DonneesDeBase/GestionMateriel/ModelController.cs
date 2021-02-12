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
    [RoutePrefix("DonneesDeBase/GestionMateriel/Model")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class ModelController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Model; } }
        public ModelController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        #region List

        [ClaimsAuthorize(SinbaConstants.Controllers.Model, SinbaConstants.Actions.Index)]
        public ActionResult Index()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetModelList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Model, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetModelList());
        }
       


        #endregion

        #region Add

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Model model)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertModel(model);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);       
        }
        [HttpGet]
        [ClaimsAuthorize]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            Model model = new Model();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, model);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetModel(id);
                TreatDto(dto);
                var model = dto.Value;
                if (model != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, model);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Model model)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, model);
            }
            var dto = donnesDeBaseService.UpdateModel(model);
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
                var dtoDelete = donnesDeBaseService.DeleteModel(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Model);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }   
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.Marque = GetMarqueList();
            ViewBag.AddMode = addMode;
        }
        #endregion

        #region List
        private List<Model> GetModelList()
        {
            List<Model> lst = new List<Model>();
            var dto = donnesDeBaseService.GetModelList();       
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        private List<Marque> GetMarqueList()
        {
            var lstMarque = new List<Marque>();
            var dto = donnesDeBaseService.GetMarqueList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstMarque = dto.Value.ToList();
            }
            return lstMarque;
        }

        #endregion

    }
}
