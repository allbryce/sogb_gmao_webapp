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
    [RoutePrefix("DonneesDeBase/Organigramme/Sections")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class SectionsController : DonneesDeBaseOrganigrammeController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;

        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Sections; } }
        public SectionsController(IDonneesDeBaseService donnesDeBaseService)
        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetSectionsList());
        }




        [ClaimsAuthorize(SinbaConstants.Controllers.Sections, SinbaConstants.Actions.Index)]

        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetSectionsList());
        }


        private List<Sections> GetSectionsList()
        {
            List<Sections> lst = new List<Sections>();
            var dto = donnesDeBaseService.GetSectionsListWithDependencies();
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
            Sections sections = new Sections();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, sections);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Sections sections)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, sections);
            }
            var dto = donnesDeBaseService.InsertSections(sections);    
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetSections(id);
                TreatDto(dto);
                var sections = dto.Value;           
                if (sections != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, sections);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Sections sections)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, sections);
            }
            var dto = donnesDeBaseService.UpdateSections(sections);
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
                var dtoDelete = donnesDeBaseService.DeleteSections(id);
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

      

        /// <summary>

        /// Fills the authorized actions view bag.

        /// </summary>

        /// 24-May-16 - Rene: creation

        /// Change history:

        private void FillAuthorizedActionsViewBag()

        {

            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Sections);

            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);

            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);

            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);

        }



        /// <summary>

        /// </summary>

        private void FillViewBag(bool addMode = false)

        {
            var lstService = new List<Service>();
            var dtoService = donnesDeBaseService.GetServiceList();
            if (!TreatDto(dtoService) && dtoService != null)
            {
                lstService = dtoService.Value.ToList();
            }
            ViewBag.Service = lstService;
            ViewBag.AddMode = addMode;

        }



        #endregion

    






    }
}