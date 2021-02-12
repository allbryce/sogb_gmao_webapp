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
    [RoutePrefix("DonneesDeBase/GestionMateriel/Marque")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class MarqueController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        long marqueid;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Marque; } }
        public MarqueController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        #region List

        [ClaimsAuthorize(SinbaConstants.Controllers.Marque, SinbaConstants.Actions.Index)]
        public ActionResult Index()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.ListPartial, GetMarqueList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Marque, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetMarqueList());
        }
       


        #endregion

        #region Add

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Marque marque)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertMarque(marque);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);       
        }
        [HttpGet]
        [ClaimsAuthorize]
        [Route(SinbaConstants.Routes.Add)]
        public ActionResult Add()
        {
            Marque marque = new Marque();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, marque);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                marqueid = id;
                var dto = donnesDeBaseService.GetMarque(id);
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
        public ActionResult Edit(Marque marque)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, marque);
            }
            var dto = donnesDeBaseService.UpdateMarque(marque);
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
                var dtoDelete = donnesDeBaseService.DeleteMarque(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Other Methods

        /// <summary>

        /// Determines whether [is identifier used] [the specified identifier].

        /// </summary>

        /// <param name="code">The identifier.</param>

        /// <returns></returns>




        #endregion


        #region ViewBag
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Marque);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }   
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.Domaine = GetDomaineList();
            ViewBag.AddMode = addMode;
        }
        #endregion

        #region List
        private List<Marque> GetMarqueList()
        {
            List<Marque> lst = new List<Marque>();
            var dto = donnesDeBaseService.GetMarqueList();       
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        private List<Domaine> GetDomaineList()
        {
            var lstDomaine = new List<Domaine>();
            var dto = donnesDeBaseService.GetDomaineList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstDomaine = dto.Value.ToList();
            }
            return lstDomaine;
        }

        #endregion

    }
}
