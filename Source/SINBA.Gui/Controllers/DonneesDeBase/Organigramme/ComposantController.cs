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
    [RoutePrefix("DonneesDeBase/Organigramme/Composant")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class ComposantController : DonneesDeBaseOrganigrammeController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Composant; } }
        public ComposantController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }

        #region List
        public ActionResult Index()

        {

            FillViewBag();

            FillAuthorizedActionsViewBag();

            return SinbaView(ViewNames.ListPartial, GetComposantList());

        }
        [ClaimsAuthorize(SinbaConstants.Controllers.Composant, SinbaConstants.Actions.Index)]

        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ComposantPartial, GetComposantList());
        }
       


        #endregion

        #region Add

        [HttpPost, ValidateInput(false)]

        public ActionResult Add(Composant composant)
        {

            if (!ModelState.IsValid)

            {

                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);

            }
            var dto = donnesDeBaseService.InsertComposant(composant);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);

        }
        [Route(SinbaConstants.Routes.EditId)]

        public ActionResult Edit(long id)

        {

            if (id != 0)
            {

                var dto = donnesDeBaseService.GetComposant(id);

                TreatDto(dto);

                var composant = dto.Value;
                if (composant != null)

                {

                    FillViewBag();

                    return SinbaView(ViewNames.ListPartial, composant);

                }

            }
            return RedirectToAction(SinbaConstants.Actions.Index);

        }
        [HttpPost, ValidateInput(false)]

        [Route(SinbaConstants.Routes.EditId)]

        public ActionResult Edit(Composant composant)

        {

            if (!ModelState.IsValid)

            {

                FillViewBag();

                return SinbaView(ViewNames.EditPartial, composant);

            }
            var dto = donnesDeBaseService.UpdateComposant(composant);

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
                var dtoDelete = donnesDeBaseService.DeleteComposant(id);
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

            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Composant);
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
            ViewBag.Materiel = lstDirection;
            ViewBag.AddMode = addMode;
        }

        #endregion

        #region List
        private List<Composant> GetComposantList()

        {

            List<Composant> lst = new List<Composant>();
            var dto = donnesDeBaseService.GetComposantList();

            if (!TreatDto(dto) && dto.Value != null)

            {

                lst = dto.Value.ToList();

            }

            return lst;

        }
        #endregion

    }
}
