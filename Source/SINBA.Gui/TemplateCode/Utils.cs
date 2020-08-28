using DevExpress.Web;
using DevExpress.Web.Internal;
using Sinba.Resources;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Sinba.Gui.TemplateCode
{
    public static class Utils
    {
        #region Constants
        const string
            CurrentSectionKey = "DXCurrentSection",
            CurrentThemeCookieKeyPrefix = "DXCurrentTheme",
            DefaultTheme = "Sinba",
            BogusSectionTitle = "";
        #endregion

        #region Variables
        static bool? _isSiteMode;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        static HttpContext Context { get { return HttpContext.Current; } }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        static HttpRequest Request { get { return Context.Request; } }

        /// <summary>
        /// Gets a value indicating whether this instance is embed required client libraries.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is embed required client libraries; otherwise, <c>false</c>.
        /// </value>
        public static bool IsEmbedRequiredClientLibraries
        {
            get
            {
                var section = SettingsConfigurationSection.Get();
#pragma warning disable CS0618 // 'SettingsConfigurationSection.EmbedRequiredClientLibraries' est obsolète : 'This property is now obsolete. Use the devExpress section's resources option in web.config instead.'
                return section != null ? section.EmbedRequiredClientLibraries : false;
#pragma warning restore CS0618 // 'SettingsConfigurationSection.EmbedRequiredClientLibraries' est obsolète : 'This property is now obsolete. Use the devExpress section's resources option in web.config instead.'
            }
        }

        /// <summary>
        /// Gets the current section.
        /// </summary>
        /// <value>
        /// The current section.
        /// </value>
        public static SectionModel CurrentSection
        {
            get
            {
                return (SectionModel)Context.Items[CurrentSectionKey];
            }
        }

        /// <summary>
        /// Gets the root item.
        /// </summary>
        /// <value>
        /// The root item.
        /// </value>
        public static SectionItemModel RootItem
        {
            get
            {
                return UserSectionItemsModel.GetUserInstance().SortedSectionItems.Find(p => p.IsRootSection);
            }
        }

        /// <summary>
        /// Gets the name of the current section node.
        /// </summary>
        /// <value>
        /// The name of the current section node.
        /// </value>
        public static string CurrentSectionNodeName
        {
            get
            {
                if (CurrentSectionPage != null && CurrentSectionPage.Item != null)
                    return string.Format("{0}_{1}_{2}", CurrentSectionPage.Item.Key, CurrentSectionPage.Group.Key, CurrentSectionPage.Key);
                if (CurrentSection != null && CurrentSection.Item != null)
                    return CurrentSection.Item.Key;
                return null;
            }
        }

        /// <summary>
        /// Gets the current section page.
        /// </summary>
        /// <value>
        /// The current section page.
        /// </value>
        public static SectionPageModel CurrentSectionPage
        {
            get
            {
                return CurrentSection as SectionPageModel;
            }
        }

        /// <summary>
        /// Gets the current theme cookie key.
        /// </summary>
        /// <value>
        /// The current theme cookie key.
        /// </value>
        public static string CurrentThemeCookieKey
        {
            get
            {
                return CurrentThemeCookieKeyPrefix;
            }
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        /// <value>
        /// The current theme.
        /// </value>
        public static string CurrentTheme
        {
            get
            {
                if (Request.Cookies[CurrentThemeCookieKey] != null)
                    return HttpUtility.UrlDecode(Request.Cookies[CurrentThemeCookieKey].Value);
                return DefaultTheme;
            }
        }

        /// <summary>
        /// Gets the current theme title.
        /// </summary>
        /// <value>
        /// The current theme title.
        /// </value>
        public static string CurrentThemeTitle
        {
            get
            {
                var theme = CurrentTheme;
                var themeModel = ThemesModel.Current.Groups.SelectMany(g => g.Themes).FirstOrDefault(t => t.Name == theme);
                return themeModel != null ? themeModel.Title : theme;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is bogus section.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is bogus section; otherwise, <c>false</c>.
        /// </value>
        public static bool IsBogusSection
        {
            get
            {
                return CurrentSection != null ? CurrentSection.Title == BogusSectionTitle : false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is site mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is site mode; otherwise, <c>false</c>.
        /// </value>
        public static bool IsSiteMode
        {
            get
            {
                if (!_isSiteMode.HasValue)
                {
                    _isSiteMode = ConfigurationManager.AppSettings[Strings.SiteMode].Equals(Strings.True, StringComparison.InvariantCultureIgnoreCase);
                }
                return _isSiteMode.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can change theme.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can change theme; otherwise, <c>false</c>.
        /// </value>
        public static bool CanChangeTheme
        {
            get
            {
                return !IsIE6() && UserSectionItemsModel.GetUserCurrent().SupportsTheming;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the version text.
        /// </summary>
        /// <returns>The application version.</returns>
        public static string GetVersionText()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return string.Format(" {0}.{1}", version.Major, version.Minor);
        }

        /// <summary>
        /// Gets the version suffix.
        /// </summary>
        /// <returns></returns>
        public static string GetVersionSuffix()
        {
            return AssemblyInfo.Version.Replace(".", "_");
        }

        /// <summary>
        /// Registers the current MVC section on callback.
        /// </summary>
        public static void RegisterCurrentMvcSectionOnCallback()
        {
            string controllerName = string.Empty;
            string actionName = string.Empty;
            string[] sectionUriParts = Request.UrlReferrer.Segments;
            int controllerNamePartIndex = sectionUriParts.Length > 2 ? sectionUriParts.Length - 2 : -1;
            if (controllerNamePartIndex > -1)
            {
                string controllerNamePart = sectionUriParts[controllerNamePartIndex];
                if (Request.AppRelativeCurrentExecutionFilePath.Contains(controllerNamePart))
                {
                    controllerName = controllerNamePart.Replace("/", string.Empty);
                    actionName = sectionUriParts[controllerNamePartIndex + 1];
                }
            }
            RegisterCurrentMvcSection(null, null, !string.IsNullOrEmpty(controllerName) ? controllerName : SinbaConstants.Controllers.Home);
        }

        /// <summary>
        /// Registers the current MVC section.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action.</param>
        public static void RegisterCurrentMvcSection(string itemKey, string groupKey, string controllerName, string title, bool useSectionSecondTitle = false)
        {
            RegisterCurrentSection(itemKey, groupKey, controllerName, title, useSectionSecondTitle);
        }

        /// <summary>
        /// Registers the current MVC section.
        /// </summary>
        /// <param name="itemKey">The item key.</param>
        /// <param name="groupKey">The group key.</param>
        /// <param name="controllerName">Name of the controller.</param>
        public static void RegisterCurrentMvcSection(string itemKey, string groupKey, string controllerName)
        {
            RegisterCurrentMvcSection(itemKey, groupKey, controllerName, null);
        }

        /// <summary>
        /// Registers the current section.
        /// </summary>
        /// <param name="groupKey">The group key.</param>
        /// <param name="sectionKey">The section key.</param>
        static void RegisterCurrentSection(string itemKey, string groupKey, string sectionKey, string title, bool useSectionSecondTitle = false)
        {
            SectionModel section = null;
            if (!string.IsNullOrWhiteSpace(title) && !useSectionSecondTitle)
            {
                section = CreateTitleSectionModel(title);
            }
            else
            {
                if (IsErrorPage(sectionKey))
                {
                    section = CreateErrorPageSectionModel();
                }
                else
                {
                    SectionGroupModel group = UserSectionItemsModel.GetUserInstance().FindGroup(itemKey, groupKey);
                    if (group != null)
                    {
                        section = group.FindSection(sectionKey);
                        section.UseSecondTitle = useSectionSecondTitle;
                        section.SecondTitle = useSectionSecondTitle ? title : null;
                    }
                }

                if (section == null)
                {
                    section = CreateBogusSectionModel();
                }
                else
                {
                    UserSectionItemsModel.SetUserCurrent(UserSectionItemsModel.GetUserInstance().SectionItems.FirstOrDefault(s => s.Key.ToLower() == itemKey.ToLower()));
                }
            }
            Context.Items[CurrentSectionKey] = section;
        }

        /// <summary>
        /// Creates the bogus section model (In case no section is detected).
        /// </summary>
        /// <returns></returns>
        static SectionPageModel CreateBogusSectionModel()
        {
            SectionGroupModel group = new SectionGroupModel();
            group.Title = string.Empty;

            SectionPageModel result = new SectionPageModel();
            result.Group = group;
            result.HideSourceCode = true;
            result.Title = BogusSectionTitle;

            return result;
        }

        /// <summary>
        /// Creates the title section model.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        static SectionPageModel CreateTitleSectionModel(string sectionTitle)
        {
            SectionGroupModel group = new SectionGroupModel();
            group.Title = string.Empty;

            SectionPageModel result = new SectionPageModel();
            result.Group = group;
            result.HideSourceCode = true;
            result.Title = sectionTitle;

            return result;
        }

        /// <summary>
        /// Creates the error page section model.
        /// </summary>
        /// <returns></returns>
        static SectionPageModel CreateErrorPageSectionModel()
        {
            SectionPageModel result = new SectionPageModel
            {
                IsErrorPage = true,
                Item = UserSectionItemsModel.GetUserCurrent(),
                Group = new SectionGroupModel()
            };
            return result;
        }

        /// <summary>
        /// Gets the current section page title.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentSectionPageTitle()
        {
            StringBuilder builder = new StringBuilder();
            if (CurrentSection.IsErrorPage)
            {
                builder.Append("Error Page - ");
                builder.Append(UserSectionItemsModel.GetUserCurrent().Title);
                if (!UserSectionItemsModel.GetUserCurrent().IsRootSection)
                    builder.Append(" Section");
            }
            else if (CurrentSection is SectionPageModel)
            {
                string item = UserSectionItemsModel.GetUserCurrent().Title;
                SectionGroupModel sectionGroup = ((SectionPageModel)CurrentSection).Group;
                string group = sectionGroup != null ? sectionGroup.Title : null;

                string sectionTitle = CurrentSection.UseSecondTitle ? CurrentSection.SecondTitle : CurrentSection.Title;
                builder.Append(sectionTitle);
                builder.Append(string.IsNullOrEmpty(sectionTitle) ? string.Empty : " - ");
                builder.Append(string.IsNullOrEmpty(item) ? group : item);
                builder.Append(" > ");
                builder.Append(sectionGroup.Title);
            }
            builder.Append(" | PV");
            return builder.ToString();
        }

        /// <summary>
        /// Gets the section navigation title.
        /// </summary>
        /// <returns></returns>
        public static string GetSectionNavigationTitle()
        {
            string result = string.Empty;
            if (Utils.CurrentSection != null && Utils.CurrentSection.Item != null)
                result = Utils.CurrentSection.Item.NavItemTitle;
            if (Utils.CurrentSectionPage.Group != null && !string.IsNullOrWhiteSpace(Utils.CurrentSectionPage.Group.Title))
            {
                result += " - " + Utils.CurrentSectionPage.Group.Title;
            }

            return result;
        }

        /// <summary>
        /// Generates the section URL.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns></returns>
        public static string GenerateSectionUrl(SectionModel section)
        {
            if (!string.IsNullOrEmpty(section.HighlightedLink))
                return section.HighlightedLink;
            StringBuilder str = new StringBuilder();

            // RTO: Add only website address
            str.Append("~/");

            //if(section.Item.IsCurrent) {
            //    str.Append("~/");
            //} else {
            //    var url = HttpContext.Current.Request.Url.AbsolutePath;
            //    var itemUrl = "/";
            //    url = url.Substring(0, url.IndexOf(itemUrl, StringComparison.InvariantCultureIgnoreCase) + 1);
            //    str.AppendFormat("{0}/", url);
            //}

            SectionGroupModel sectionGroup = section is SectionPageModel ? ((SectionPageModel)section).Group : null;
            if (sectionGroup != null && !string.IsNullOrEmpty(sectionGroup.Key))
            {
                if (sectionGroup.SectionItem != null && !string.IsNullOrEmpty(sectionGroup.SectionItem.Key))
                {
                    str.Append(sectionGroup.SectionItem.Key);
                    str.Append("/");
                }
                str.Append(sectionGroup.Key);
                str.Append("/");
            }
            if (!string.Equals("Index", section.Key))
                str.Append(section.Key);
            return str.ToString();
        }

        public static bool IsIE6()
        {
            return RenderUtils.Browser.IsIE && RenderUtils.Browser.Version < 7;
        }

        public static bool IsIE10()
        {
            return RenderUtils.Browser.IsIE && RenderUtils.Browser.Version > 9;
        }

        public static string EncodeUrl(string url)
        {
            return Uri.EscapeUriString(url.Trim());
        }

        private static bool IsErrorPage(string sectionKey)
        {
            return sectionKey.Equals("Error404", StringComparison.OrdinalIgnoreCase) ||
                sectionKey.Equals("Error500", StringComparison.OrdinalIgnoreCase);
        }
        #endregion
    }
}
