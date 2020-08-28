using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Microsoft.AspNet.Identity.Owin;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.Helpers;
using Sinba.Gui.Resources;
using Sinba.Gui.Security;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using Sinba_Gui;
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
    [RoutePrefix("Administration/Acces/Utilisateur")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin, SinbaRoles.AdminSite)]
    #endregion
    public class UtilisateurController : AdministrationAccesController
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
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilisateurController" /> class.
        /// </summary>
        /// <param name="listeService">The liste service.</param>
        public UtilisateurController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        #region List
        // GET: Profil
        public ActionResult Index()
        {
            List<string> userRoles = GetUserRoles();
            FillViewBagList(userRoles);
            FillAuthorizedActionsViewBag();
            return SinbaView(ViewNames.Index, GetUtilisateurList(userRoles));
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Utilisateur, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            List<string> userRoles = GetUserRoles();
            FillViewBagList(userRoles);
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetUtilisateurList(userRoles));
        }

        /// <summary>
        /// Gets the utilisateur list.
        /// </summary>
        /// <returns></returns>
        private List<SinbaUser> GetUtilisateurList(List<string> userRoles)
        {
            List<SinbaUser> lst = new List<SinbaUser>();
            List<SinbaUser> ret = new List<SinbaUser>();

            var dto = this.rightManagementService.GetSinbaUserList();
            if (!TreatDto(dto))
            {
                lst = dto.Value.ToList();
                foreach (SinbaUser user in lst)
                {
                    if (!AccessDenied(user, userRoles))
                    {
                        ret.Add(user);
                    }
                }
            }
            return ret;
        }

        #endregion

        #region Add / Edit
        [HttpGet]
        public ActionResult Add()
        {
            SinbaUser sinbaUser = new SinbaUser();
            FillViewBagList();

            return SinbaView(ViewNames.EditPartial, sinbaUser);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Add(SinbaUser sinbaUser)
        {
            // Checking if the returned model is valid
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(sinbaUser.Email))
            {
                if (string.IsNullOrWhiteSpace(sinbaUser.Email))
                {
                    ModelState.AddModelError(DbColumns.Email, RightManagementResource.EmailRequired);
                }
                FillViewBagList();
                return SinbaView(ViewNames.EditPartial, sinbaUser);
            }

            if (string.IsNullOrWhiteSpace(sinbaUser.UserName))
            {
                var user = new SinbaUser
                {
                    UserName = sinbaUser.Email,
                    Email = sinbaUser.Email,
                    IdProfil = sinbaUser.IdProfil,
                    EmailConfirmed = true,
                    Nom = sinbaUser.Nom.ToUpper(),
                    Prenom = sinbaUser.Prenom
                };

                if (IsSuperAdmin() && sinbaUser.AdminSite)
                {
                    user.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { UserId = user.Id, RoleId = SinbaRoles.AdminSite });
                }

                // Add mode (Insert Entity)
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    string callbackUrl = await SendEmailPasswordCreationTokenAsync(user.Id, AccountResource.SendUserPasswordCreationTokenEmailSubject);
                }
                else
                {
                    AddErrors(result);
                    FillViewBagList();
                    return SinbaView(ViewNames.EditPartial, sinbaUser);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpGet]
        [Route(SinbaConstants.Routes.EditId)]
        public async Task<ActionResult> Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                SinbaUser user = await UserManager.FindByIdAsync(id);

                if (user != null)
                {
                    FillViewBagList();

                    if (user.Roles.Any(r => r.RoleId.Equals(SinbaRoles.AdminSite)))
                    {
                        user.AdminSite = true;
                    }

                    return SinbaView(ViewNames.EditPartial, user);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        [Route(SinbaConstants.Routes.EditId)]
        public async Task<ActionResult> Edit(SinbaUser user)
        {
            SinbaUser dbUser;

            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                FillViewBagList();
                return SinbaView(ViewNames.EditPartial, user);
            }

            dbUser = await UserManager.FindByIdAsync(user.Id);

            if (dbUser != null && !string.IsNullOrWhiteSpace(dbUser.UserName))
            {
                // Edit mode (Update Entity)
                dbUser.IdProfil = user.IdProfil;
                dbUser.Nom = user.Nom.ToUpper();
                dbUser.Prenom = user.Prenom;

                if (IsSuperAdmin())
                {
                    if (user.AdminSite)
                    {
                        if (!dbUser.Roles.Any(r => r.RoleId.Equals(SinbaRoles.AdminSite, System.StringComparison.OrdinalIgnoreCase)))
                        {
                            dbUser.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { RoleId = SinbaRoles.AdminSite, UserId = dbUser.Id });
                        }
                    }
                    else
                    {
                        if (dbUser.Roles.Any(r => r.RoleId.Equals(SinbaRoles.AdminSite, System.StringComparison.OrdinalIgnoreCase)))
                        {
                            dbUser.Roles.Remove(dbUser.Roles.First(r => r.RoleId.Equals(SinbaRoles.AdminSite, System.StringComparison.OrdinalIgnoreCase)));
                        }
                    }
                }


                var result = await UserManager.UpdateAsync(dbUser);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                    return SinbaView(ViewNames.EditPartial, user);
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteId)]
        public async Task<ActionResult> Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await UserManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                    }
                }
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Other Methods
        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action(SinbaConstants.Actions.ConfirmEmail, SinbaConstants.Controllers.Account,
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(userID, subject,
               StringHelper.FormatString(AccountResource.SendEmailConfirmationTokenEmailBody, callbackUrl));

            return callbackUrl;
        }

        private async Task<string> SendEmailPasswordCreationTokenAsync(string userID, string subject)
        {
            string code = await UserManager.GeneratePasswordResetTokenAsync(userID);
            var callbackUrl = Url.Action(SinbaConstants.Actions.SetPasswordUser, SinbaConstants.Controllers.Utilisateur,
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(userID, subject,
               StringHelper.FormatString(AccountResource.SendUserPasswordCreationTokenEmailBody, callbackUrl));

            return callbackUrl;
        }

        #endregion

        #region ViewBag
        private void FillViewBagList(List<string> userRoles = null)
        {
            List<Profil> lstProfil = new List<Profil>();
            var dtoProfil = this.rightManagementService.GetProfilList();
            if (!TreatDto(dtoProfil))
            {
                lstProfil = dtoProfil.Value.ToList();
            }

            ViewBag.ProfilList = lstProfil;

            ViewBag.IsSuperAdmin = IsSuperAdmin(userRoles);
        }

        /// <summary>
        /// Fills the authorized actions view bag.
        /// </summary>
        /// 25-May-16 - Rene: creation
        /// Change history:
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Utilisateur);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }
        #endregion

        #region User Password
        [AllowAnonymous]
        public async Task<ActionResult> SetPasswordUser(string userId, string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(userId))
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    SetPasswordUserViewModel model = new SetPasswordUserViewModel() { UserId = userId };
                    ViewBag.Username = user.UserName;
                    return SinbaView(ViewNames.SetPasswordUser, model, ManageResource.SetPasswordTitle);
                }
            }

            return SinbaErrorView();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPasswordUser(SetPasswordUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(model.UserId, model.NewPassword);
                if (result.Succeeded)
                {
                    ViewBag.Message = ManageResource.IndexSetPasswordSuccess;
                    return SinbaInfoView();
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return SinbaView(ViewNames.SetPasswordUser, model, ManageResource.SetPasswordTitle);
        }
        #endregion
    }
}