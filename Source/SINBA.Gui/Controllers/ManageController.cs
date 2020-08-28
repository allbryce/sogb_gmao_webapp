using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Sinba.BusinessModel.Entity;
using Sinba.Gui.Resources;
using Sinba.Gui.Security;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using Sinba_Gui;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Sinba.Gui.Controllers
{
    #region Attributes
    [IHMLog()]
    [IhmExceptionHandler()]
    [Authorize]
    #endregion
    public class ManageController : SectionController
    {
        #region Variables
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
        public override string ItemName { get { return string.Empty; } }
        public override string GroupName { get { return string.Empty; } }
        public override string ControllerName { get { return SinbaConstants.Controllers.Manage; } }
        #endregion

        #region Constructors
        public ManageController()
        {
        }
        #endregion

        #region Methods
        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? ManageResource.IndexChangePasswordSuccess
                : message == ManageMessageId.SetPasswordSuccess ? ManageResource.IndexSetPasswordSuccess
                : message == ManageMessageId.Error ? ManageResource.IndexError
                : string.Empty;

            var user = UserManager.FindById(User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(user),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId())
            };

            return SinbaView(ViewNames.Index, model, ManageResource.IndexTitle);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return SinbaView(ViewNames.ChangePassword, ManageResource.ChangePasswordTitle);
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return SinbaView(ViewNames.ChangePassword, model, ManageResource.ChangePasswordTitle);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(AuthenticationManager, UserManager, user, isPersistent: false);
                }
                return RedirectToAction(SinbaConstants.Actions.Index, new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return SinbaView(ViewNames.ChangePassword, model, ManageResource.ChangePasswordTitle);
        }

        //
        // GET: /Manage/SetPassword
        [ClaimsAuthorize]
        public ActionResult SetPassword()
        {
            return SinbaView(ViewNames.SetPassword, ManageResource.SetPasswordTitle);
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ClaimsAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(AuthenticationManager, UserManager, user, isPersistent: false);
                    }
                    return RedirectToAction(SinbaConstants.Actions.Index, new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return SinbaView(ViewNames.SetPassword, model, ManageResource.SetPasswordTitle);
        }

        //
        // GET: /Manage/EditAccountInfo
        public async Task<ActionResult> EditAccountInfo()
        {
            AccountInfoViewModel model = new AccountInfoViewModel();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                // Set user properties here.
            }

            return SinbaView(ViewNames.EditAccountInfo, model, ManageResource.EditAccountInfoTitle);
        }

        //
        // POST: /Manage/EditAccountInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAccountInfo(AccountInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(SinbaConstants.Actions.Index, new { Message = ManageMessageId.EditAccountInfoSuccess });
                    }
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return SinbaView(ViewNames.EditAccountInfo, model, ManageResource.EditAccountInfoTitle);
        }
        #endregion

        #region Helpers
        private bool HasPassword(SinbaUser user)
        {
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            EditAccountInfoSuccess,
            Error
        }

        #endregion
    }
}