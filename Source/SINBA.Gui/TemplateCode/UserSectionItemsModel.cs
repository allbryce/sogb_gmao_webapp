using Microsoft.AspNet.Identity;
using Sinba.BusinessModel.Entity;
using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for root tree node (Section Items)
    /// </summary>
    public class UserSectionItemsModel
    {
        #region Variables
        static SectionItemsModel instance;
        static SectionItemModel current;

        static readonly object userInstanceLock = new object();
        static readonly object currentLock = new object();
        static readonly object instanceLock = new object();

        static Dictionary<string, SectionItemsModel> userInstance;
        #endregion

        #region Properties
        public static Dictionary<string, SectionItemsModel> UserInstance
        {
            get
            {
                lock (userInstanceLock)
                {
                    if (userInstance == null)
                    {
                        userInstance = new Dictionary<string, SectionItemsModel>();
                    }
                    return userInstance;
                }
            }
        }

        public static SectionItemsModel Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        using (Stream stream = File.OpenRead(HttpContext.Current.Server.MapPath("~/App_Data/Sections.xml")))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(SectionItemsModel));
                            instance = (SectionItemsModel)serializer.Deserialize(stream);
                        }
                        foreach (var sectionItem in instance.SectionItems)
                        {
                            foreach (var group in sectionItem.Groups)
                            {
                                foreach (var section in group.GetAllSections())
                                {
                                    section.Group = group;
                                    section.Item = sectionItem;
                                }
                                group.SectionItem = sectionItem;
                            }
                        }
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        /// <exception cref="System.Exception">The current section is not found</exception>
        public static SectionItemModel Current
        {
            get
            {
                lock (currentLock)
                {
                    if (current == null)
                    {
                        current = UserSectionItemsModel.Instance.SectionItems.FirstOrDefault(dp => dp.Key == ConfigurationManager.AppSettings[XmlElementNames.SectionItem]);
                    }
                    if (current == null)
                    {
                        throw new Exception("The current section is not found");
                    }

                    return current;
                }
            }
        }
        #endregion

        #region Methods
        public static SectionItemsModel GetUserInstance()
        {
            var user = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            if (user == null || !user.IsAuthenticated)
            {
                return new SectionItemsModel();
            }

            var userId = user.GetUserId();
            if (UserInstance.ContainsKey(userId))
            {
                return UserInstance[userId];
            }
            else
            {
                var userInstance = Instance.CloneSerialize();
                RefreshUserInstance(userInstance, user, userId);
                userInstance.SortedSectionItems = userInstance.SectionItems.OrderBy(p => p.OrderIndex).ToList();
                UserInstance[userId] = userInstance;
                return userInstance;
            }
        }

        public static SectionItemModel GetUserCurrent()
        {
            var user = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            if (user == null || !user.IsAuthenticated)
            {
                return Current;
            }

            var userId = user.GetUserId();
            if (UserInstance.ContainsKey(userId))
            {
                RefreshUserInstance(UserInstance[userId], user, userId);
                return UserInstance[userId].Current;
            }
            return Current;
        }

        public static void SetUserCurrent(SectionItemModel sectionItem)
        {
            var user = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            if (user != null && user.IsAuthenticated)
            {
                var userId = user.GetUserId();
                if (UserInstance.ContainsKey(userId))
                {
                    UserInstance[userId].Current = sectionItem.CloneSerialize() as SectionItemModel;
                }
            }
        }

        private static void RefreshUserInstance(SectionItemsModel userInstance, ClaimsIdentity user, string userId = null)
        {
            List<string> authorizedMenuList = user.GetAuthorizedMenuListFromClaims();

            int visibleSections = 0;
            int visibleGroups = 0;
            string menuPath = null;
            bool visible = false;

            if (user != null)
            {
                for (int i = userInstance.SectionItems.Count - 1; i >= 0; i--)
                {
                    visibleGroups = 0;
                    var sectionItem = userInstance.SectionItems[i];
                    for (int j = sectionItem.Groups.Count - 1; j >= 0; j--)
                    {
                        visibleSections = 0;
                        var sectionGroup = sectionItem.Groups[j];
                        for (int k = sectionGroup.Sections.Count - 1; k >= 0; k--)
                        {
                            var section = sectionGroup.Sections[k];
                            menuPath = string.Format("{0}/{1}/{2}", sectionItem.Key, sectionGroup.Key, section.Key);
                            if (authorizedMenuList.Any(s => s.Equals(menuPath)))
                            {
                                visible = true;

                                section.Visible = visible;
                                visibleSections += visible ? 1 : 0;
                            }
                            else
                            {
                                section.Visible = false;
                            }
                        }
                        sectionGroup.Visible = visibleSections > 0;
                        visibleGroups += sectionGroup.Visible ? 1 : 0;
                    }
                    sectionItem.Visible = visibleGroups > 0;
                }
            }
        }
        #endregion
    }
}
