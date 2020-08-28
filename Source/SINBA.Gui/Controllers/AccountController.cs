using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Microsoft.AspNet.Identity;
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
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PostSharp;
using Microsoft.Practices.Unity;
using System;
using Sinba.Resources.Resources.Entity;

namespace Sinba.Gui.Controllers
{
    #region Attributes
    [Authorize]
    [IHMLog()]
    [IhmExceptionHandler()]
    #endregion
    public class AccountController : SectionController
    {
        #region Variables
        private ApplicationUserManager userManager;
        private ApplicationSignInManager signInManager;
        private ApplicationRoleManager roleManager;
        private IRightManagementService rightManagementService;

        private string idSite;
        private string idUser;
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

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { signInManager = value; }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this.roleManager = value; }
        }
        #endregion

        #region Overrides
        public override string ItemName { get { return string.Empty; } }
        public override string GroupName { get { return string.Empty; } }
        public override string ControllerName { get { return SinbaConstants.Controllers.Account; } }
        #endregion

        #region Constructors
        public AccountController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
            idSite = GetUserSiteId();
            idUser = GetUserId();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;

        }
        #endregion

        #region Methods
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var user = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            if (user.IsAuthenticated)
            {
                return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Home);
            }
            var model = new LoginViewModel() { ReturnUrl = returnUrl };

            FillViewBagLists();
            ViewBag.ReturnUrl = returnUrl;
            return SinbaView(ViewNames.Login, model, AccountResource.LoginTitle);
        }

        //
        // POST: /Account/Login
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return SinbaView(ViewNames.Login, model, AccountResource.LoginTitle);
            }

            // Require the user to have a confirmed email before they can log on.
            // var user = await UserManager.FindByNameAsync(model.Email);
            var user = UserManager.Find(model.UserName, model.Password);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, AccountResource.LoginEmailConfirmationSubject);

                    // Uncomment to debug locally  
                    ViewBag.Link = callbackUrl;
                    ViewBag.errorMessage = AccountResource.errorLoginMustConfirmEmail;
                    return SinbaErrorView();
                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe.GetValue(), shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    // Add UserSite Claim
                    return RedirectToAction(SinbaConstants.Actions.SetUserSiteAndCulture, SinbaConstants.Controllers.Account, new { isPersistent = model.RememberMe.GetValue(), returnUrl = returnUrl, userSite = model.IdSite});
                case SignInStatus.LockedOut:
                    return SinbaView(ViewNames.Lockout, AccountResource.LoginLockoutTitle);
                case SignInStatus.RequiresVerification:
                    return RedirectToAction(SinbaConstants.Actions.SendCode, new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError(string.Empty, AccountResource.errorLoginInvalidLoginAttempt);
                    return SinbaView(ViewNames.Login, model, AccountResource.LoginTitle);
            }
        }

        /// <summary>
        /// permet de stocker les variables publiques dans les cookies
        /// </summary>
        /// <param name="isPersistent"></param>
        /// <param name="userSite"></param>
        /// <param name="userCulture"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<ActionResult> SetUserSiteAndCulture(bool isPersistent, string userSite, string userCulture, string returnUrl)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                string userSocieteId = null;
                string UserSocieteSite = null;
                var dto = rightManagementService.GetSite(userSite);
                TreatDto(dto);
                var site = dto.Value;
                if (site != null)
                {
                    userSocieteId = site.IdSociete;
                    UserSocieteSite = site.Libelle;
                }

                await SignInAsync(AuthenticationManager, UserManager, user, isPersistent, userSite, userSocieteId, UserSocieteSite);
            }

            return RedirectToLocal(returnUrl);
        }

        //
        // GET: /Account/Register
        [ClaimsAuthorize]
        public ActionResult Register()
        {
            return SinbaView(ViewNames.Register, new RegisterViewModel(), AccountResource.RegisterTitle);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ClaimsAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new SinbaUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //  Comment the following line to prevent log in until the user is confirmed.
                    //  await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, AccountResource.RegisterEmailConfirmationSubject);


                    ViewBag.Message = AccountResource.errorRegisterMustConfirmEmail;

                    // For local debug only
                    ViewBag.Link = callbackUrl;

                    return SinbaInfoView();
                    //return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return SinbaView(ViewNames.Register, model, AccountResource.RegisterTitle);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return SinbaErrorView();
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return result.Succeeded ? SinbaView(ViewNames.ConfirmEmail, AccountResource.ConfirmEmailTitle) : SinbaErrorView();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return SinbaView(ViewNames.ForgotPassword, new ForgotPasswordViewModel(), AccountResource.ForgotPasswordTitle);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return SinbaView(ViewNames.ForgotPasswordConfirmation, AccountResource.ForgotPasswordConfirmationTitle);
                }

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action(SinbaConstants.Actions.ResetPassword, SinbaConstants.Controllers.Account, new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, AccountResource.ForgotPasswordEmailSubject,
                    StringHelper.FormatString(AccountResource.ForgotPasswordEmailBody, callbackUrl));

                TempData[SinbaConstants.ConfigKeys.ViewBagLink] = callbackUrl;
                return RedirectToAction(SinbaConstants.Actions.ForgotPasswordConfirmation, SinbaConstants.Controllers.Account);
            }

            // If we got this far, something failed, redisplay form
            return SinbaView(ViewNames.ForgotPassword, model, AccountResource.ForgotPasswordTitle);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            ViewBag.Link = TempData[SinbaConstants.ConfigKeys.ViewBagLink];
            return SinbaView(ViewNames.ForgotPasswordConfirmation, AccountResource.ForgotPasswordConfirmationTitle);
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? SinbaErrorView() : SinbaView(ViewNames.ResetPassword, AccountResource.ResetPasswordTitle);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(SinbaConstants.Actions.ResetPasswordConfirmation, SinbaConstants.Controllers.Account);
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(SinbaConstants.Actions.ResetPasswordConfirmation, SinbaConstants.Controllers.Account);
            }
            AddErrors(result);
            return SinbaView(ViewNames.ResetPassword, AccountResource.ResetPasswordTitle);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return SinbaView(ViewNames.ResetPasswordConfirmation, AccountResource.ResetPasswordConfirmationTitle);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Home);
        }

        [AllowAnonymous]
        public ActionResult NotAuthorized()
        {
            return SinbaView(ViewNames.NotAuthorized, AccountResource.NotAuthorizedTitle);
        }

        [AllowAnonymous]
        public JsonResult GetUserSites(string username, string password)
        {
            JsonResult result = new JsonResult();
            result.Data = null;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var user = UserManager.Find(username, password);
            if (user != null)
            {

                if (user.SiteUtilisateurs.Count > 0)
                {
                    var sites = user.SiteUtilisateurs.Select(s => new { Id = s.IdSite, Libelle = s.Site.Libelle });
                    if (sites.ToList().Count == 1) Session[DbColumns.IdSite] = sites.FirstOrDefault().Id;
                    return Json(new {HasError=false, Data = sites, Message = Sinba.Resources.Resources.Entity.EntityCommonResource.msgSuccess }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { HasError = true, Data =new List<Site>(), Message = EntityCommonResource.errorLoginPassword }, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<Site>(), JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            //if (Url.IsLocalUrl(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            if(!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Home);
        }

        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action(SinbaConstants.Actions.ConfirmEmail ,SinbaConstants.Controllers.Account ,new { userId = userID, code = code }, protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(userID, subject,
               StringHelper.FormatString(AccountResource.SendEmailConfirmationTokenEmailBody, callbackUrl));

            return callbackUrl;
        }

        #region DecryptPassword
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        static string DecryptPassword(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            //if (cipherText == null || cipherText.Length <= 0)
            //    throw new ArgumentNullException("cipherText");
            //if (Key == null || Key.Length <= 0)
            //    throw new ArgumentNullException("Key");
            //if (IV == null || IV.Length <= 0)
            //    throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
        #endregion

        #endregion

        #region ViewBag
        private void FillViewBagLists()
        {


        }
        #endregion
    }
}