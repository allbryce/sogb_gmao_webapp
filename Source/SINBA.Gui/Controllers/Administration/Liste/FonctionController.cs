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
    [RoutePrefix("Administration/Liste/Fonction")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin)]
    #endregion
    public class FonctionController : AdministrationListeController
    {
        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        #endregion

        #region Overrides
        // Set the Controller Name here (used for Navigation).
        public override string ControllerName { get { return SinbaConstants.Controllers.Fonction; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FonctionController" /> class.
        /// </summary>
        /// <param name="listeService">The liste service.</param>
        public FonctionController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        #region List
        // GET: Fonction
        public ActionResult Index()
        {
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetFonctionList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Fonction, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetFonctionList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Fonction, SinbaConstants.Actions.Index)]
        public ActionResult ActionsFonctionPartial(string codeFonction)
        {
            ViewData[DbColumns.CodeFonction] = codeFonction;
            return PartialView(ViewNames.ActionsFonctionPartial, GetFonctionActionList(codeFonction));
        }

        /// <summary>
        /// Gets the fonction list.
        /// </summary>
        /// <returns></returns>
        private List<Fonction> GetFonctionList()
        {
            List<Fonction> lst = new List<Fonction>();
            var dto = this.rightManagementService.GetFonctionListWithDependencies(IsSuperAdmin());
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }

        private List<Action> GetFonctionActionList(string codeFonction)
        {
            List<Action> lst = new List<Action>();
            var dto = this.rightManagementService.GetFonctionActionList(codeFonction);
            TreatDto(dto);
            if (dto.Value != null)
            {
                lst = dto.Value.Select(fa => fa.Action).ToList();
            }
            return lst;
        }
        #endregion

        #region Add / Edit
        [HttpGet]
        public ActionResult Add()
        {
            Fonction fonction = new Fonction();

            FillViewBagLists();

            return SinbaView(ViewNames.EditPartial, fonction);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Fonction fonction)
        {
            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                return SinbaView(ViewNames.EditPartial, fonction);
            }

            if (string.IsNullOrEmpty(fonction.CodeHidden))
            {
                // Add mode (Insert Entity)
                var dto = rightManagementService.InsertFonction(fonction);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpGet]
        [Route(SinbaConstants.Routes.EditCode)]
        public ActionResult Edit(string code)
        {
            Fonction fonction = null;

            var dto = rightManagementService.GetFonction(code);
            TreatDto(dto);
            fonction = dto.Value;

            if (fonction != null)
            {
                fonction.CodeHidden = code;
                FillViewBagLists();
                return SinbaView(ViewNames.EditPartial, fonction);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditCode)]
        public ActionResult Edit(Fonction fonction)
        {
            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                return SinbaView(ViewNames.EditPartial, fonction);
            }

            if (!string.IsNullOrEmpty(fonction.CodeHidden))
            {
                // Edit mode (Update Entity)
                var dto = rightManagementService.UpdateFonction(fonction);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteCode)]
        public ActionResult Delete(string code)
        {
            var dtoFonctionUsed = this.rightManagementService.IsFonctionUsed(code);
            if (!TreatDto(dtoFonctionUsed))
            {
                if (dtoFonctionUsed.Value)
                {
                    ViewBag.errorMessage = RightManagementResource.errorFonctionUsed;
                    return SinbaErrorView();
                }
                else
                {
                    var dto = this.rightManagementService.DeleteFonction(code);
                    TreatDto(dto);
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
        public ActionResult IsCodeUsed(string code, string codeHidden)
        {
            var dto = this.rightManagementService.IsFonctionCodeUnique(code);
            TreatDto(dto);

            bool codeUnique = string.IsNullOrEmpty(codeHidden) ? dto.Value : code.ToLower().Equals(codeHidden.ToLower()) ? !dto.Value : dto.Value;

            return Json(codeUnique, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ViewBag
        /// <summary>
        /// Fills the view bag lists.
        /// </summary>
        private void FillViewBagLists()
        {
            // Actions
            List<FonctionAction> lst = new List<FonctionAction>();
            ListDto<Action> dtoActions = this.rightManagementService.GetActionList();
            TreatDto(dtoActions);
            if (dtoActions.Value != null)
            {
                lst = dtoActions.Value.Select(a => new FonctionAction() { CodeAction = a.Code }).ToList();
            }

            ViewBag.ActionList = lst;
        }

        /// <summary>
        /// Fills the authorized actions view bag.
        /// </summary>
        /// 24-May-16 - Rene: creation
        /// Change history:
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Fonction);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }
        #endregion
    }
}