using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.Security;
using Sinba.Gui.Resources;
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
    [RoutePrefix("Administration/Liste/Societe")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin)]
    #endregion
    public class SocieteController : AdministrationListeController
    {
        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        //private IDonneesDeBaseService donnesDeBaseService;
        #endregion

        #region Overrides
        // Set the Controller Name here (used for Navigation).
        public override string ControllerName { get { return SinbaConstants.Controllers.Societe; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SocieteController" /> class.
        /// </summary>
        /// <param name="rightManagementService">rightManagementService.</param>
        public SocieteController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;            
        }
        #endregion

        #region List
        // GET: Societe
        public ActionResult Index()
        {
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetSocieteList());
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Societe, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetSocieteList());
        }

        /// <summary>
        /// Gets the Societe list.
        /// </summary>
        /// <returns></returns>
        private List<Societe> GetSocieteList()
        {
            List<Societe> lst = new List<Societe>();
            //var dto = rightManagementService.GetSocieteListWithDependencies();
            var dto = rightManagementService.GetSocieteList();
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
            return SinbaView(ViewNames.EditPartial, new Societe());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Societe societe)
        {
            if (!ModelState.IsValid)
            {
                return SinbaView(ViewNames.EditPartial, societe);
            }

            if (string.IsNullOrEmpty(societe.IdHidden))
            {
                // Add mode (Insert Entity)
                var dto = rightManagementService.InsertSociete(societe);
                TreatDto(dto);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var dto = rightManagementService.GetSociete(id);
                TreatDto(dto);
                var societe = dto.Value;

                if (societe != null)
                {
                    societe.IdHidden = id;
                    return SinbaView(ViewNames.EditPartial, societe);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(Societe societe)
        {
            if (!ModelState.IsValid)
            {
                return SinbaView(ViewNames.EditPartial, societe);
            }

            if (!string.IsNullOrEmpty(societe.IdHidden))
            {
                // Edit mode (Update Entity)
                var dto = rightManagementService.UpdateSociete(societe);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteId)]
        public ActionResult Delete(string id)
        {
            var dtoSociete = rightManagementService.IsSocieteUsed(id);
            if (!TreatDto(dtoSociete))
            {
                if (dtoSociete.Value)
                {
                    ViewBag.errorMessage = CommonResource.errorDeleteRecordUsed;
                    return SinbaErrorView();
                }
                else
                {
                    var dtoDelete = rightManagementService.DeleteSociete(id);
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
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IsIdUsed(string id, string idHidden)
        {
            //var dto = rightManagementService.IsSocieteIdUnique(id);
            //TreatDto(dto);

            //bool codeUnique = string.IsNullOrEmpty(idHidden) ? dto.Value : id.ToLower().Equals(idHidden.ToLower()) ? !dto.Value : dto.Value;

            //return Json(codeUnique, JsonRequestBehavior.AllowGet);

            bool ret = true;
            var dto = rightManagementService.IsSocieteIdUsed(id, idHidden);

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
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Societe);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }

        #endregion
    }
}
