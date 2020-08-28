using Sinba.BusinessModel.Entity;
using Sinba.Resources;
using System;
using System.Linq;
using System.Security.Claims;

namespace Sinba.BusinessModel.Helpers
{
    public class ModelHelper
    {
        public static string GetClaimValue(string claimType)
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

        public static string GetUserDisplayLanguage()
        {
            var identity = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            string userDisplayLanguage = identity.IsAuthenticated ? GetClaimValue(SinbaClaims.DisplayLanguage) : System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            return (userDisplayLanguage ?? DatabaseConstants.DefaultLanguage).FirstCharToUpper();
        }

        public static string GetUserSiteId()
        {
            return GetClaimValue(SinbaClaims.UserSiteId);
        }

        private static void SetValue<T>(T instance, string propertyName, object value)
        {
            var property = typeof(T).GetProperty(propertyName);
            property.SetValue(instance, value, null);
        }

        private static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static void SetMultilangualLabel<T>(T instance, string value, string codeIso = null)
        {
            codeIso = codeIso ?? GetUserDisplayLanguage();
            if (string.IsNullOrWhiteSpace(codeIso))
            {
                return;
            }
            var property = typeof(T).GetProperty($"{DbColumns.Libelle}{codeIso}");
            if (property != null)
            {
                property.SetValue(instance, value, null);
            }
        }

        public static T CreateMultilingualEntity<T>(string value, string codeIso = null)
        {
            T instance = CreateInstance<T>();
            SetMultilangualLabel(instance, value, codeIso);
            return instance;
        }

    }
}
