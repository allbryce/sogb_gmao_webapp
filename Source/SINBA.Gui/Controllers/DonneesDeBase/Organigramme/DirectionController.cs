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
    [RoutePrefix("DonneesDeBase/Organigramme/Direction")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class DirectionController : DonneesDeBaseOrganigrammeController

    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;


        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Direction; } }
        public DirectionController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        public ActionResult Index()

        {

            FillViewBag();

            FillAuthorizedActionsViewBag();

            return SinbaView(ViewNames.Index, GetDirectionList());

        }
        [ClaimsAuthorize(SinbaConstants.Controllers.Direction, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()

        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetDirectionList());
        }
        private List<Direction> GetDirectionList()
        {
            List<Direction> lst = new List<Direction>();
            var dto = donnesDeBaseService.GetDirectionListWithDependencies();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }
        #region ViewBag
        private void FillAuthorizedActionsViewBag()

        {

            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Direction);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);     
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);

        }
        private void FillViewBag(bool addMode = false)
        {
            ViewBag.AddMode = addMode;
        }

        #endregion

        #region Add
        [HttpGet]
        public ActionResult Add()
        {
            Direction direction  = new Direction();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, direction);

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Direction direction)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, direction);
            }
            var dto = donnesDeBaseService.InsertDirection(direction);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id!=0)
            {
                var dto = donnesDeBaseService.GetDirection(id);
                TreatDto(dto);
                var direction = dto.Value;
                if (direction != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, direction);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);     
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Direction direction)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, direction);
            }
            var dto = donnesDeBaseService.UpdateDirection(direction);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        #endregion



        #region Delete

        [Route(SinbaConstants.Routes.DeleteId)]

        public ActionResult Delete(long id)
        {
            if (id!=0)
            {
                var dtoDelete = donnesDeBaseService.DeleteDirection(id);
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

        /// <summary>

        /// Fills the authorized actions view bag.

        /// </summary>

        /// 24-May-16 - Rene: creation

        /// Change history:

        



        /// <summary>

        /// </summary>

       // private void FillViewBag(bool addMode = false)

        //{
        
        //    ViewBag.AddMode = addMode;

        //}



        #endregion
}
}