using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Dto;
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
    [RoutePrefix("Administration/Acces/Profil")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin, SinbaRoles.AdminSite)]
    #endregion
    public class ProfilController : AdministrationAccesController
    {
        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        #endregion

        #region Overrides
        // Set the Controller Name here (used for Navigation).
        public override string ControllerName { get { return SinbaConstants.Controllers.Profil; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilController" /> class.
        /// </summary>
        /// <param name="listeService">The liste service.</param>
        public ProfilController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        #region List
        // GET: Profil
        public ActionResult Index()
        {
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetProfilList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Profil, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetProfilList());
        }

        /// <summary>
        /// Gets the profil list.
        /// </summary>
        /// <returns></returns>
        private List<Profil> GetProfilList()
        {
            List<Profil> lst = new List<Profil>();
            var dto = this.rightManagementService.GetProfilListWithDependencies();
            TreatDto(dto);
            return dto.Value.ToList();
        }

        #endregion

        #region Add / Edit
        [HttpGet]
        public ActionResult Add()
        {
            return SinbaView(ViewNames.EditPartial, new Profil() { Id = -1 });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Profil profil)
        {
            if (!ModelState.IsValid) return SinbaView(ViewNames.EditPartial, profil);

            if (profil == null) return SinbaErrorView();

            if (profil.Id < 0)
            {
                // Add mode (Insert Entity)
                var dto = rightManagementService.InsertProfil(profil);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long? id)
        {
            if (id.HasValue)
            {
                var dto = rightManagementService.GetProfil(id.Value);
                TreatDto(dto);
                var profil = dto.Value;

                if (profil != null)
                {
                    return SinbaView(ViewNames.EditPartial, profil);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Profil profil)
        {
            if (!ModelState.IsValid) return SinbaView(ViewNames.EditPartial, profil);

            if (profil == null) return SinbaErrorView();

            if (profil.Id > 0)
            {
                // Edit mode (Update Entity)
                var dto = rightManagementService.UpdateProfil(profil, false);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteId)]
        public ActionResult Delete(long? id)
        {
            if (id.HasValue)
            {
                var profil = this.rightManagementService.GetProfil(id.Value);
                if (!TreatDto(profil))
                {
                    if (profil.Value.IsUsed)
                    {
                        ViewBag.errorMessage = RightManagementResource.errorProfilUsed;
                        return SinbaErrorView();
                    }
                    else
                    {
                        var dto = this.rightManagementService.DeleteProfil(id.Value);
                        TreatDto(dto);
                    }
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
        public ActionResult IsNameUsed(long id, string nom)
        {
            bool ret = true;
            SimpleDto<Profil> dto = new SimpleDto<Profil>();
            dto = this.rightManagementService.GetProfil(nom);

            if (!TreatDto(dto) && dto.Value != null)
            {
                if (dto.Value.Id != id)
                {
                    ret = false;
                }
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
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Profil);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }
        #endregion
    }
}