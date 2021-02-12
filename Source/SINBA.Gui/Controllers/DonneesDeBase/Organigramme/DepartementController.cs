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
    [RoutePrefix("DonneesDeBase/Organigramme/Departement")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class DepartementController : DonneesDeBaseOrganigrammeController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Departement; } }

        public DepartementController(IDonneesDeBaseService donnesDeBaseService)
        {
            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetDepartementList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Departement, SinbaConstants.Actions.Index)]   
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetDepartementList());
        }

        private List<Departement> GetDepartementList()
        {
            List<Departement> lst = new List<Departement>();
            var dto = donnesDeBaseService.GetDepartementListWithDependencies();
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
            Departement departement = new Departement();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, departement);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Departement departement)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, departement);
            }
            var dto = donnesDeBaseService.InsertDepartement(departement);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        [Route(SinbaConstants.Routes.EditId)]
       public ActionResult Edit(long id)
       {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetDepartement(id);
                TreatDto(dto);
                var departement = dto.Value;
                if (departement != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, departement);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
       }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Departement departement)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, departement);
            }
            var dto = donnesDeBaseService.UpdateDepartement(departement);
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
                var dtoDelete = donnesDeBaseService.DeleteDepartement(id);
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
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Departement);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }

        private void FillViewBag(bool addMode = false)
        {
            var lstDirection = new List<Direction>();
            var dtoDirection = donnesDeBaseService.GetDirectionList();
            if (!TreatDto(dtoDirection) && dtoDirection.Value != null)
            {
                lstDirection = dtoDirection.Value.ToList();
            }
            ViewBag.Direction = lstDirection;
            ViewBag.AddMode = addMode;
        }

        #endregion

    }
}