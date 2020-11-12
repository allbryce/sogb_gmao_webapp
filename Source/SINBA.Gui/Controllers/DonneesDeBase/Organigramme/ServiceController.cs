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
    [RoutePrefix("DonneesDeBase/Organigramme/Service")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class ServiceController : DonneesDeBaseOrganigrammeController



    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;


        private string userId;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Service; } }


        public ServiceController(IDonneesDeBaseService donnesDeBaseService)

        {

            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        public ActionResult Index()

        {

            FillViewBag();

            FillAuthorizedActionsViewBag();

            return SinbaView(ViewNames.Index, GetServiceList());

        }




        [ClaimsAuthorize(SinbaConstants.Controllers.Service, SinbaConstants.Actions.Index)]

        public ActionResult ListPartial()

        {

            FillViewBag();

            FillAuthorizedActionsViewBag();

            return PartialView(ViewNames.ListPartial, GetServiceList());

        }



        /// <summary>

        /// Gets the Direction list.

        /// </summary>

        /// <returns></returns>

        private List<Service> GetServiceList()

        {

            List<Service> lst = new List<Service>();

            var dto = donnesDeBaseService.GetServiceListWithDependencies();

            if (!TreatDto(dto) && dto.Value != null)

            {

                lst = dto.Value.ToList();

            }

            return lst;

        }
        #region ViewBag
        private void FillAuthorizedActionsViewBag()

        {

            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Service);

            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);

            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);

            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);

        }



        /// <summary>

        /// </summary>

        private void FillViewBag(bool addMode = false)

        {
                           
            var lstDepartement= new List<Departement>();
                var dtoDepartement = donnesDeBaseService.GetDepartementList();
                if (!TreatDto(dtoDepartement) && dtoDepartement.Value != null)
                {
                     lstDepartement = dtoDepartement.Value.ToList();
                }
                ViewBag.Departement = lstDepartement;
                ViewBag.AddMode = addMode;
        }



        #endregion

        #region Add
        [HttpGet]
        public ActionResult Add()
        {
            Service service = new Service();
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, service);

        }
        [HttpPost, ValidateInput(false)]

        public ActionResult Add(Service service)

        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, service);
            }
            var dto = donnesDeBaseService.InsertService(service);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)

            {
                var dto = donnesDeBaseService.GetService(id);
                TreatDto(dto);
                var service = dto.Value;
                if (service != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, service);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Service service)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, service);
            }
            var dto = donnesDeBaseService.UpdateService(service);
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

                var dtoDelete = donnesDeBaseService.DeleteService(id);

                TreatDto(dtoDelete);

            }



            return RedirectToAction(SinbaConstants.Actions.Index);

        }

        #endregion
    }
}