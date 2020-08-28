using Microsoft.AspNet.Identity;
using Sinba.BusinessModel.Entity;
using Sinba.DataAccess;
using Sinba.Resources;
using Sinba.Resources.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Sinba.Gui
{
    /// <summary>
    /// Classe d'extention
    /// </summary>
    public static partial class ExtensionClass
    {
        #region Identity

        public static string IdCulture(this IIdentity user)
        {
            var claimCultureId = user.GetClaim(SinbaClaims.UserCultureId);
            return claimCultureId != null ? claimCultureId.Value : string.Empty;
        }

        public static string IdSite(this IIdentity user)
        {
            var claimSiteId = user.GetClaim(SinbaClaims.UserSiteId);
            return claimSiteId != null ? claimSiteId.Value : string.Empty;
        }

        public static string SocieteSite(this IIdentity user)
        {
            var claimSocieteSite = user.GetClaim(SinbaClaims.UserSocieteSite);
            return claimSocieteSite != null ? claimSocieteSite.Value : string.Empty;
        }




        public static Claim GetClaim(this SinbaUser user, string claimType)
        {
            var identity = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            return identity.Claims.FirstOrDefault(c => c.Type.Equals(claimType));
        }

        public static Claim GetClaim(this IIdentity user, string claimType)
        {
            var identity = user as ClaimsIdentity;
            return identity.Claims.Where(c => c.Type.Equals(claimType)).FirstOrDefault();
        }



        public static List<string> GetAuthorizedActions(this IIdentity user, string fonction)
        {
            List<string> ret = new List<string>();
            SinbaUser sinbaUser;

            string userId = user.GetUserId();
            using (SinbaContext context = new SinbaContext())
            {
                sinbaUser = context.Users.Where(u => u.Id == userId).Include(u => u.Profil.ProfilRights).Include(u => u.UserRights).ToList().FirstOrDefault();
                if (sinbaUser != null)
                {
                    if (sinbaUser.Profil != null)
                    {
                        ret = sinbaUser.UserRights.Where(ur => ur.CodeFonction == fonction && (!ur.DenyAccess))
                            .Select(ur => ur.CodeAction)
                            .Union((sinbaUser.Profil.ProfilRights.Where(pr => pr.CodeFonction == fonction)
                            .Select(pr => pr.CodeAction))).ToList();
                    }
                    else
                    {
                        ret = sinbaUser.UserRights.Where(ur => ur.CodeFonction == fonction && (!ur.DenyAccess))
                            .Select(ur => ur.CodeAction).ToList();
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Determines whether [has claim access] [the specified type].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool HasClaimAccess(this IIdentity user, string type, string value)
        {
            bool hasAccess = false;

            if (user == null || !user.IsAuthenticated)
            {
                return hasAccess;
            }

            SinbaUser sinbaUser = new SinbaUser();
            using (SinbaContext context = new SinbaContext())
            {
                string userId = user.GetUserId();
                sinbaUser = context.Users.Where(u => u.Id == userId).Include(u => u.Profil.ProfilRights).Include(u => u.UserRights).ToList().FirstOrDefault();
                if (sinbaUser != null)
                {
                    if (type.Equals(SinbaClaims.Type.Menu))
                    {
                        hasAccess = HasMenuAccess(sinbaUser, value);
                    }
                    else
                    {
                        hasAccess = HasFonctionActionAccess(sinbaUser, type, value);
                    }
                }
            }
            return hasAccess;
        }

        /// <summary>
        /// Gets the authorized menu list.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static List<string> GetAuthorizedMenuListFromDb(this IIdentity user, string userId = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                userId = user.GetUserId();
            }

            SinbaUser sinbaUser = new SinbaUser() { Id = userId };

            return sinbaUser.GetAuthorizedMenuListFromDb();
        }

        public static List<string> GetAuthorizedMenuListFromDb(this SinbaUser user)
        {
            List<string> authorizedMenu = new List<string>();
            List<string> userRightList = new List<string>();
            List<string> profilRightList = new List<string>();
            SinbaUser sinbaUser = new SinbaUser();

            using (SinbaContext context = new SinbaContext())
            {
                sinbaUser = context.Users.Where(u => u.Id == user.Id).Include(u => u.Profil.ProfilRights).Include(u => u.UserRights).ToList().FirstOrDefault();
                if (sinbaUser != null)
                {
                    userRightList = sinbaUser.UserRights.Where(ur => ur.CodeAction == SinbaClaims.Type.Menu && (!ur.DenyAccess))
                        .Join(context.Fonctions, ur => ur.CodeFonction, f => f.Code, (ur, f) => new { UserRight = ur, Fonction = f })
                        .Select(f => f.Fonction.MenuPath).ToList();

                    if (sinbaUser.Profil != null)
                    {
                        profilRightList = sinbaUser.Profil.ProfilRights.Where(pr => pr.CodeAction == SinbaClaims.Type.Menu)
                            .Join(context.Fonctions, pr => pr.CodeFonction, f => f.Code, (pr, f) => new { ProfilRight = pr, Fonction = f })
                            .Select(f => f.Fonction.MenuPath).OrderBy(s => s).ToList();
                    }

                    authorizedMenu = userRightList.Union(profilRightList).ToList();
                }
            }
            return authorizedMenu;
        }

        /// <summary>
        /// Gets the uer role list.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static List<string> GetUerRoleList(this IIdentity user)
        {
            List<string> userRoleList = new List<string>();
            SinbaUser sinbaUser = new SinbaUser();
            using (SinbaContext context = new SinbaContext())
            {
                string userId = user.GetUserId();
                sinbaUser = context.Users.Where(u => u.Id == userId).Include(u => u.Roles).Where(u => u.Roles.Count > 0).ToList().FirstOrDefault();
                if (sinbaUser != null && sinbaUser.Roles.Count > 0)
                {
                    userRoleList = sinbaUser.Roles.Select(r => r.RoleId).ToList();
                }
            }
            return userRoleList;
        }

        /// <summary>
        /// Existses the in roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        public static bool ExistsInRoles(this IIdentity user, List<string> roles)
        {
            bool existInRoles = false;

            if (user == null || !user.IsAuthenticated)
            {
                return existInRoles;
            }

            SinbaUser sinbaUser = new SinbaUser();
            using (SinbaContext context = new SinbaContext())
            {
                string userId = user.GetUserId();
                sinbaUser = context.Users.Where(u => u.Id == userId).Include(u => u.Roles).ToList().FirstOrDefault();
                if (sinbaUser != null && sinbaUser.Roles.Any())
                {
                    existInRoles = sinbaUser.Roles.Any(ur => roles.Contains(ur.RoleId));
                }
            }
            return existInRoles;
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static string GetFullName(this IIdentity user)
        {
            string fullName = string.Empty;

            if (user == null || !user.IsAuthenticated)
            {
                return fullName;
            }

            var claim = ((ClaimsIdentity)user).Claims.FirstOrDefault(c => c.Type.Equals(SinbaClaims.Type.FullName));
            if (claim != null)
            {
                fullName = claim.Value;
            }

            return fullName;
        }

        /// <summary>
        /// Gets the authorized menu list.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static List<string> GetAuthorizedMenuListFromClaims(this IIdentity user)
        {
            List<string> menuList = new List<string>();
            if (user != null && user.IsAuthenticated)
            {
                menuList = ((ClaimsIdentity)user).Claims.Where(c => c.Type == SinbaClaims.Type.Menu).Select(c => c.Value).Distinct().ToList();
            }
            return menuList;
        }

        /// <summary>
        /// Determines whether [has menu access] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="menuPath">The menu path.</param>
        /// <returns></returns>
        private static bool HasMenuAccess(SinbaUser user, string menuPath)
        {
            bool hasAccess = false;
            if (user != null && !string.IsNullOrWhiteSpace(menuPath))
            {
                bool? profilAccess = null;
                if (user.Profil != null)
                {
                    if (user.Profil.ProfilRights.Any(pr => pr.FonctionAction != null &&
                     pr.FonctionAction.Fonction != null &&
                     pr.FonctionAction.Fonction.MenuPath.Equals(menuPath) &&
                     pr.CodeAction.Equals(SinbaClaims.Type.Menu)))
                    {
                        profilAccess = true;
                    }
                }

                UserRight userRight = user.UserRights.Where(ur => ur.FonctionAction != null &&
                    ur.FonctionAction.Fonction != null && ur.FonctionAction.Fonction.MenuPath.Equals(menuPath) &&
                    ur.CodeAction.Equals(SinbaClaims.Type.Menu)).FirstOrDefault();

                bool? userAccess = null;
                if (userRight != null)
                {
                    userAccess = !userRight.DenyAccess;
                }

                if (profilAccess.HasValue)
                {
                    hasAccess = profilAccess.Value;
                    if (userAccess.HasValue)
                    {
                        hasAccess = profilAccess.Value && userAccess.Value;
                    }
                }
                else
                {
                    if (userAccess.HasValue)
                    {
                        hasAccess = userAccess.Value;
                    }
                }
            }
            return hasAccess;
        }

        /// <summary>
        /// Determines whether [has fonction action access] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="codeFonction">The code fonction.</param>
        /// <param name="codeAction">The code action.</param>
        /// <returns></returns>
        private static bool HasFonctionActionAccess(SinbaUser user, string codeFonction, string codeAction)
        {
            bool hasAccess = false;
            if (user != null)
            {
                bool? profilAccess = null;
                if (user.Profil != null)
                {
                    if (user.Profil.ProfilRights.Any(pr => pr.CodeFonction == codeFonction &&
                    pr.CodeAction == codeAction))
                    {
                        profilAccess = true;
                    }
                }

                UserRight userRight = user.UserRights.Where(ur => ur.CodeFonction == codeFonction &&
                    ur.CodeAction == codeAction).FirstOrDefault();

                bool? userAccess = null;
                if (userRight != null)
                {
                    userAccess = !userRight.DenyAccess;
                }

                if (profilAccess.HasValue)
                {
                    hasAccess = profilAccess.Value;
                    if (userAccess.HasValue)
                    {
                        hasAccess = profilAccess.Value && userAccess.Value;
                    }
                }
                else
                {
                    if (userAccess.HasValue)
                    {
                        hasAccess = userAccess.Value;
                    }
                }
            }
            return hasAccess;
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Gets the java script date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static long GetJavaScriptDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToJavaScriptDate() : 0;
        }

        /// <summary>
        /// Gets the java script date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static long ToJavaScriptDate(this DateTime date)
        {
            return Convert.ToInt64(date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
        }
        #endregion

        /// <summary>
        /// Determines whether the specified property name has property.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        /// <summary>
        /// Sorts the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>
        /// Uses reflection to sort any type of list using string values
        /// </returns>
        public static IEnumerable<T> SortList<T>(this IEnumerable<T> list, string sortBy, string sortOrder) where T : class
        {
            IEnumerable<T> ret = list;
            if (list.Count() > 0 && list.First().HasProperty(sortBy))
            {
                if (sortOrder.Equals("Descending", StringComparison.OrdinalIgnoreCase))
                {
                    ret = list.OrderByDescending(x => x.GetType()
                    .GetProperty(sortBy)
                    .GetValue(x, null));
                }
                else
                {
                    ret = list.OrderBy(x => x.GetType()
                    .GetProperty(sortBy)
                    .GetValue(x, null));
                }
            }
            return ret;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="numberOfDecimals">The number of decimals.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this decimal? value, int numberOfDecimals)
        {
            string ret = string.Empty;
            if (!value.HasValue)
            {
                return ret;
            }

            string format = "{0:0}";

            if (numberOfDecimals < 0)
            {
                numberOfDecimals = 0;
            }
            if (numberOfDecimals > 0)
            {
                format = "{0:0.";
                for (int i = 0; i < numberOfDecimals; i++)
                {
                    format += "#";
                }
                format += "}";
            }

            return string.Format(format, value.Value);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="numberOfDecimals">The number of decimals.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this decimal value, int numberOfDecimals)
        {
            return new decimal?(value).ToString(numberOfDecimals);
        }

        public static string Add(this string value, string text)
        {
            return string.Format("{0}{1}", value, text);
        }
    }
}
