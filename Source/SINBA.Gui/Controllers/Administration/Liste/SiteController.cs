using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.Resources;
using Sinba.Gui.Security;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    #region Attributes
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("Administration/Liste/Site")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin, SinbaRoles.AdminSite)]
    #endregion
    public class SiteController : AdministrationListeController
    {
        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        #endregion

        #region Overrides
        // Set the Controller Name here (used for Navigation).
        public override string ControllerName { get { return SinbaConstants.Controllers.Site; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteController" /> class.
        /// </summary>
        /// <param name="listeService">The liste service.</param>
        public SiteController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        #region List
        // GET: Site
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetSiteList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Site, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetSiteList());
        }

        /// <summary>
        /// Gets the profil list.
        /// </summary>
        /// <returns></returns>
        private List<Site> GetSiteList()
        {            
            List<Site> lst = new List<Site>();
            var dto = rightManagementService.GetSiteListWithDependencies();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        #endregion

        #region Add / Edit
        [HttpGet]
        public ActionResult Add()
        {
            FillViewBag(true);
            return SinbaView(ViewNames.EditPartial, new Site());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Site site)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                return SinbaView(ViewNames.EditPartial, site);
            }

            // Add mode (Insert Entity)

            var dto = rightManagementService.InsertSite(site);
            TreatDto(dto);

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var dto = rightManagementService.GetSite(id);
                TreatDto(dto);
                var site = dto.Value;

                if (site != null)
                {
                    site.IdHidden = id;
                    FillViewBag();
                    return SinbaView(ViewNames.EditPartial, site);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Site site)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, site);
            }

            // Edit mode (Update Entity)
            if (!string.IsNullOrEmpty(site.IdHidden))
            {
                var dto = rightManagementService.UpdateSite(site);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteId)]
        public ActionResult Delete(string id)
        {
            var dtoSite = rightManagementService.IsSiteUsed(id);
            if (!TreatDto(dtoSite))
            {
                if (dtoSite.Value)
                {
                    ViewBag.errorMessage = CommonResource.errorDeleteRecordUsed;
                    return SinbaErrorView();
                }
                else
                {
                    var dtoDelete = rightManagementService.DeleteSite(id);
                    TreatDto(dtoDelete);
                }
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
        [AllowAnonymous]
        public ActionResult IsIdUsed(string id, string idHidden)
        {
            bool ret = true;
            var dto = rightManagementService.IsSiteIdUsed(id, idHidden);

            if (!TreatDto(dto))
            {
                ret = !dto.Value;
            }

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ViewBag
        /// <summary>
        /// Fills the authorized actions view bag.
        /// </summary>
        /// 24-May-16 - Rene: creation
        /// Change history:
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Site);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }

        /// <summary>
        /// Charger Conseil0 view bag.
        /// </summary>
        private void FillViewBag(bool addMode = false)
        {
            List<Societe> lst = new List<Societe>();
            var dto = this.rightManagementService.GetSocieteList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            ViewBag.Societe = lst;

            ViewBag.AddMode = addMode;
        }

        #endregion
    }
}
