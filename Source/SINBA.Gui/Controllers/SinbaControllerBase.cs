using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.Resources;
using Sinba_Gui;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    /// <summary>
    /// Base Controller for Sinba
    /// </summary>
    [IHMLog()]
    [IhmExceptionHandler()]
    public abstract class SinbaControllerBase : Controller
    {
        #region Properties
        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        protected string IpAddress
        {
            get
            {
                string ip = this.Request != null ? this.Request.ServerVariables[Strings.REMOTE_ADDR] : null;
                return string.IsNullOrWhiteSpace(ip) ? Strings.IpNotFound : ip;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SinbaControllerBase"/> class.
        /// </summary>
        protected SinbaControllerBase()
        {
            LanguageConfig.ChangeLanguage("fr");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Traite le DTO provenant de l'appel à un service métier
        /// </summary>
        /// <param name="dto">Le DTO à traiter.</param>
        /// <returns>True si une erreur s'est produite false sinon</returns>
        public virtual bool TreatDto(SinbaDtoBase dto)
        {
            //TODO : AJOUTER ici la gestion de l'affichage des erreurs.
            return dto.HasError;
        }

        private string ConcatenateString(string initialString, string text, string value)
        {
            return initialString + text + value + Strings.CRLF;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Signs the in asynchronous.
        /// </summary>
        /// <param name="authenticationManager">The authentication manager.</param>
        /// <param name="applicationUserManager">The application user manager.</param>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <returns></returns>
        protected async Task SignInAsync(IAuthenticationManager authenticationManager, ApplicationUserManager applicationUserManager, SinbaUser user, bool isPersistent, string userSiteId = null, string userSocieteId = null,string userSocieteSite=null)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);

            Claim claimFullName = new Claim(SinbaClaims.Type.FullName, string.Format("{0} {1}", user.Prenom, user.Nom.ToUpper()));

            Claim claimIpAddress = new Claim(SinbaClaims.Type.IpAddress, IpAddress);

            Claim claimUserSiteId = string.IsNullOrWhiteSpace(userSiteId) ? null : new Claim(SinbaClaims.Type.UserSiteId, userSiteId);

            Claim claimUserSocieteId = string.IsNullOrWhiteSpace(userSocieteId) ? null : new Claim(SinbaClaims.Type.UserSocieteId, userSocieteId);

            Claim claimUserSocieteSite = string.IsNullOrWhiteSpace(userSocieteSite) ? null : new Claim(SinbaClaims.UserSocieteSite, userSocieteSite);

            // Chargement de la liste des menus autorisés
            List<string> authorizedMenuList = user.GetAuthorizedMenuListFromDb();

            List<Claim> authorizedMenuClaims = authorizedMenuList.Select(x => new Claim(SinbaClaims.Type.Menu, x)).ToList();

            var identity = await user.GenerateUserIdentityAsync(applicationUserManager);

            identity.AddClaim(claimIpAddress);

            identity.AddClaim(claimFullName);

            if (claimUserSiteId != null)
            {
                identity.AddClaim(claimUserSiteId);
            }

            if (claimUserSocieteId != null)
            {
                identity.AddClaim(claimUserSocieteId);
            }

            if (claimUserSocieteSite != null)
            {
                identity.AddClaim(claimUserSocieteSite);
            }

            // Ajout des menus
            authorizedMenuList.ForEach(x => identity.AddClaim(new Claim(SinbaClaims.Type.Menu, x)));

            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        protected string GetUserSiteId()
        {
            return GetClaimValue(SinbaClaims.Type.UserSiteId);
        }

        protected string GetUserSocieteId()
        {
            return GetClaimValue(SinbaClaims.Type.UserSocieteId);
        }

        protected string GetCurrentExercice()
        {
            return GetClaimValue(SinbaClaims.Type.Exercice);
        }

        protected string GetCurrentCodeVersionBudget()
        {
            return GetClaimValue(SinbaClaims.Type.CodeVersionBudget);
        }

        protected string GetUserId()
        {
            var user = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            return user.GetUserId();
        }

        protected List<string> GetUserRoles()
        {
            var identity = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            return identity.GetUerRoleList();
        }

        private string GetClaimValue(string claimType)
        {
            string ret = null;
            var identity = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            if (identity.IsAuthenticated)
            {
                var claim = identity.Claims.FirstOrDefault(c => c.Type.Equals(claimType));
                if (claim != null) ret = claim.Value;
            }
            return ret;
        }

        /// <summary>
        /// Adds the user claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userIdentity">The user identity.</param>
        /// <remarks>Old method. Not used for the moment.</remarks>
        private void AddUserClaims(SinbaUser user, ClaimsIdentity userIdentity)
        {
            if (user == null || userIdentity == null || !userIdentity.IsAuthenticated) { return; }

            List<Claim> claimsToRemove = new List<Claim>();

            // Add profil rights
            if (user.Profil != null && user.Profil.ProfilRights.Count > 0)
            {
                foreach (ProfilRight pr in user.Profil.ProfilRights)
                {
                    // Menu management
                    if (pr.CodeAction.Equals(SinbaClaims.Type.Menu, System.StringComparison.OrdinalIgnoreCase) &&
                        pr.FonctionAction != null && pr.FonctionAction.Fonction != null && !string.IsNullOrWhiteSpace(pr.FonctionAction.Fonction.MenuPath))
                    {
                        userIdentity.AddClaim(new Claim(SinbaClaims.Type.Menu, pr.FonctionAction.Fonction.MenuPath));
                    }
                    else
                    {
                        userIdentity.AddClaim(new Claim(pr.CodeFonction, pr.CodeAction));
                    }
                }
            }

            // Add user rights
            if (user.UserRights.Count > 0)
            {
                foreach (UserRight ur in user.UserRights)
                {
                    if (ur.CodeAction.Equals(SinbaClaims.Type.Menu, System.StringComparison.OrdinalIgnoreCase) &&
                        ur.FonctionAction != null && ur.FonctionAction.Fonction != null && !string.IsNullOrWhiteSpace(ur.FonctionAction.Fonction.MenuPath))
                    {
                        if (ur.DenyAccess)
                        {
                            claimsToRemove.Add(new Claim(SinbaClaims.Type.Menu, ur.FonctionAction.Fonction.MenuPath));
                        }
                        else
                        {
                            if (!userIdentity.HasClaim(SinbaClaims.Type.Menu, ur.FonctionAction.Fonction.MenuPath))
                            {
                                userIdentity.AddClaim(new Claim(SinbaClaims.Type.Menu, ur.FonctionAction.Fonction.MenuPath));
                            }
                        }
                    }
                    else
                    {
                        if (ur.DenyAccess)
                        {
                            claimsToRemove.Add(new Claim(ur.CodeFonction, ur.CodeAction));
                        }
                        else
                        {
                            if (!userIdentity.HasClaim(ur.CodeFonction, ur.CodeAction))
                            {
                                userIdentity.AddClaim(new Claim(ur.CodeFonction, ur.CodeAction));
                            }
                        }
                    }
                }

                // Remove restricted claims
                foreach (Claim c in claimsToRemove)
                {
                    userIdentity.RemoveClaim(userIdentity.FindFirst(c.Type));
                }
            }
        }
        #endregion
    }
}