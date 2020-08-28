using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba_Gui;
using Microsoft.AspNet.Identity.Owin;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.Resources;
using Sinba.Gui.Security;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    #region Attributes
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("Administration/Acces/UtilisateurRight")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin, SinbaRoles.AdminSite)]
    #endregion
    public class UtilisateurRightController : AdministrationAccesController
    {
        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        private ApplicationUserManager userManager;
        #endregion

        #region Properties
        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }
        #endregion

        #region Overrides
        // Set the Controller Name here (used for Navigation).
        public override string ControllerName { get { return SinbaConstants.Controllers.Utilisateur; } }
        #endregion

        #region Constructors
        public UtilisateurRightController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        #region List
        // GET: Profil rights
        [Route(SinbaConstants.Routes.IndexIdUtilisateur)]
        public async Task<ActionResult> Index(string idUtilisateur)
        {

            SinbaUser utilisateur = await GetUtilisateur(idUtilisateur);

            if (utilisateur != null)
            {
                if (AccessDenied(utilisateur)) return SinbaErrorView();
                FillViewBag(utilisateur);
                FillAuthorizedActionsViewBag();
                return SinbaView(ViewNames.Index, GetAllUserRights(utilisateur), PageTitle(utilisateur.UserName), true);
            }
            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Utilisateur);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.UtilisateurRight, SinbaConstants.Actions.Index)]
        public async Task<ActionResult> ListPartial(string idUtilisateur)
        {
            SinbaUser utilisateur = await UserManager.FindByIdAsync(idUtilisateur);
            if (utilisateur != null)
            {
                if (AccessDenied(utilisateur)) return SinbaErrorView();
                FillViewBag(utilisateur);
                FillAuthorizedActionsViewBag();
                return PartialView(ViewNames.ListPartial, GetAllUserRights(utilisateur));
            }
            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Profil);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.UtilisateurRight, SinbaConstants.Actions.Index)]
        public ActionResult UtilisateurRightActionsPartial(string codeFonction)
        {
            ViewData[DbColumns.CodeFonction] = codeFonction;
            ViewBag.ActionList = GetUtilisateurRightActionList(codeFonction);
            return PartialView(ViewNames.ActionsFonctionPartial, new UserRight());
        }

        private List<FonctionAction> GetUtilisateurRightActionList(string codeFonction)
        {
            ListDto<FonctionAction> dtoLst = new ListDto<FonctionAction>(new List<FonctionAction>());
            if (!string.IsNullOrWhiteSpace(codeFonction))
            {
                dtoLst = this.rightManagementService.GetFonctionActionList(codeFonction);
                TreatDto(dtoLst);
            }

            return dtoLst.Value != null ? dtoLst.Value.ToList() : new List<FonctionAction>();
        }

        #endregion

        #region Add / Edit
        [HttpGet]
        [Route(SinbaConstants.Routes.AddIdUtilisateur)]
        public async Task<ActionResult> Add(string idUtilisateur)
        {
            SinbaUser utilisateur = await GetUtilisateur(idUtilisateur);

            if (utilisateur == null)
            {
                return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Utilisateur);
            }

            if (AccessDenied(utilisateur)) return SinbaErrorView();

            UserRight userRight = new UserRight()
            {
                IdHidden = string.Empty,
                IdUser = utilisateur.Id
            };

            FillViewBag(utilisateur);

            return SinbaView(ViewNames.EditPartial, userRight, PageTitle(utilisateur.UserName), true);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.AddIdUtilisateur)]
        public async Task<ActionResult> Add(UserRight utilisateurRight)
        {
            SinbaUser utilisateur = await GetUtilisateur(utilisateurRight.IdUser);

            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                FillViewBag(utilisateur);
                return SinbaView(ViewNames.EditPartial, utilisateurRight, PageTitle(utilisateur.UserName), true);
            }

            if (string.IsNullOrWhiteSpace(utilisateurRight.IdHidden) && utilisateur != null)
            {
                if (AccessDenied(utilisateur)) return SinbaErrorView();

                // Add mode (Insert Entity)
                await AddUtilisateurRight(utilisateur, utilisateurRight);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpGet]
        [Route(SinbaConstants.Routes.EditIdUtilisateurCodeFonctionCodeAction)]
        public async Task<ActionResult> Edit(string idUtilisateur, string codeFonction, string codeAction)
        {
            SinbaUser utilisateur = await GetUtilisateur(idUtilisateur);

            if (utilisateur != null && !string.IsNullOrWhiteSpace(codeFonction) && !string.IsNullOrWhiteSpace(codeAction))
            {
                if (AccessDenied(utilisateur)) return SinbaErrorView();

                FillViewBag(utilisateur);

                UserRight utilisateurRight = utilisateur.UserRights.Where(ur => ur.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase) &&
                    ur.CodeAction.Equals(codeAction, System.StringComparison.OrdinalIgnoreCase) &&
                    ur.IdUser.Equals(idUtilisateur, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (utilisateurRight != null)
                {
                    utilisateurRight.IdHidden = utilisateurRight.IdUser;
                    return SinbaView(ViewNames.EditPartial, utilisateurRight, PageTitle(utilisateur.UserName), true);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditIdUtilisateurCodeFonctionCodeAction)]
        public async Task<ActionResult> Edit(UserRight utilisateurRight)
        {
            SinbaUser utilisateur = await GetUtilisateur(utilisateurRight.IdUser);

            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                FillViewBag(utilisateur);
                return SinbaView(ViewNames.EditPartial, utilisateurRight);
            }

            if (utilisateur != null && !string.IsNullOrWhiteSpace(utilisateurRight.IdHidden))
            {
                if (AccessDenied(utilisateur)) return SinbaErrorView();

                await AddUtilisateurRight(utilisateur, utilisateurRight);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteIdUtilisateurCodeFonctionCodeAction)]
        public async Task<ActionResult> Delete(string idUtilisateur, string codeFonction, string codeAction)
        {
            SinbaUser utilisateur = await GetUtilisateur(idUtilisateur);

            if (utilisateur == null || string.IsNullOrWhiteSpace(codeFonction) || string.IsNullOrWhiteSpace(codeAction))
            {
                return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Utilisateur);
            }

            if (AccessDenied(utilisateur)) return SinbaErrorView();

            var userRightToDelete = utilisateur.UserRights.Where(ur => ur.IdUser.Equals(idUtilisateur, System.StringComparison.OrdinalIgnoreCase) &&
                ur.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase) &&
                ur.CodeAction.Equals(codeAction, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (userRightToDelete != null)
            {
                utilisateur.UserRights.Remove(userRightToDelete);
                var result = await UserManager.UpdateAsync(utilisateur);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index, new { idUtilisateur = idUtilisateur });
        }
        #endregion

        #region ViewBag
        /// <summary>
        /// Fills the view bag lists.
        /// </summary>
        private void FillViewBag(SinbaUser user = null)
        {
            // Liste des fonction
            List<Fonction> lst = new List<Fonction>();

            if (user != null)
            {
                var dto = this.rightManagementService.GetFonctionList(IsSuperAdmin());
                TreatDto(dto);
                if (dto.Value != null)
                {
                    lst = dto.Value.ToList();
                }


                ViewBag.IdUtilisateur = user.Id;
                ViewBag.FonctionList = lst;
            }
        }

        /// <summary>
        /// Fills the authorized actions view bag.
        /// </summary>
        /// 25-May-16 - Rene: creation
        /// Change history:
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.UtilisateurRight);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }
        #endregion

        #region Other Methods
        private string PageTitle(string utilisateurUserName)
        {
            return string.Format(RightManagementResource.UtilisateurRightsTitle, utilisateurUserName);
        }

        /// <summary>
        /// Gets the utilisateur.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private async Task<SinbaUser> GetUtilisateur(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return user;
        }

        private async Task AddUtilisateurRight(SinbaUser utilisateur, UserRight utilisateurRight)
        {
            var dbUserRight = utilisateur.UserRights.Where(ur => ur.CodeFonction.Equals(utilisateurRight.CodeFonction, System.StringComparison.OrdinalIgnoreCase) &&
                    ur.CodeAction.Equals(utilisateurRight.CodeAction)).FirstOrDefault();

            if (dbUserRight == null)
            {
                utilisateur.UserRights.Add(new UserRight()
                {
                    CodeFonction = utilisateurRight.CodeFonction,
                    CodeAction = utilisateurRight.CodeAction,
                    DenyAccess = utilisateurRight.DenyAccess
                });
            }
            else
            {
                dbUserRight.CodeFonction = utilisateurRight.CodeFonction;
                dbUserRight.CodeAction = utilisateurRight.CodeAction;
                dbUserRight.DenyAccess = utilisateurRight.DenyAccess;
            }

            var result = await UserManager.UpdateAsync(utilisateur);
            if (!result.Succeeded)
            {
                AddErrors(result);
            }
        }

        private List<UserRight> GetAllUserRights(SinbaUser utilisateur)
        {
            List<UserRight> UserRights = new List<UserRight>();

            UserRights = utilisateur.UserRights.Select(ur => new UserRight()
            {
                IdUser = ur.IdUser,
                CodeAction = ur.CodeAction,
                CodeFonction = ur.CodeFonction,
                DenyAccess = ur.DenyAccess,
            }).ToList();

            if (utilisateur.Profil != null && utilisateur.Profil.ProfilRights.Any())
            {
                foreach (ProfilRight profilRight in utilisateur.Profil.ProfilRights)
                {
                    UserRights.Add(new UserRight()
                    {
                        IdUser = utilisateur.Id,
                        CodeFonction = profilRight.CodeFonction,
                        CodeAction = profilRight.CodeAction,
                        FromProfil = true
                    });
                }
            }
            return UserRights.OrderBy(ur => ur.CodeFonction).ToList();
        }
        #endregion
    }
}