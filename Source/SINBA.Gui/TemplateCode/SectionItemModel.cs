using System;
using System.Linq;
using Sinba.Resources;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class of Section Item
    /// </summary>
    [Serializable]
    public class SectionItemModel : ModelBase
    {
        #region Variables
        bool isRootSection;
        string key;
        string title;
        string navItemTitle;
        bool ie7CompatModeRequired;
        bool supportsTheming = true;
        bool hideNavItem = false;
        List<SectionGroupModel> groups = new List<SectionGroupModel>();
        int orderIndex = 1000;
        bool integrationHighlighted = false;
        List<SectionPageModel> highlighledSections;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is root section.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is root section; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool IsRootSection
        {
            get { return isRootSection; }
            set { isRootSection = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [hide nav item].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hide nav item]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool HideNavItem
        {
            get { return hideNavItem; }
            set { hideNavItem = value; }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [XmlAttribute]
        public string Key
        {
            get
            {
                if (key == null) { return string.Empty; }
                return key;
            }
            set { key = value; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [XmlAttribute]
        public string Title
        {
            get
            {
                if (title == null) { return string.Empty; }
                return title;
            }
            set { title = value; }
        }

        /// <summary>
        /// Gets or sets the nav item title.
        /// </summary>
        /// <value>
        /// The nav item title.
        /// </value>
        [XmlAttribute]
        public string NavItemTitle
        {
            get
            {
                if (navItemTitle == null) { return string.Empty; }
                return navItemTitle;
            }
            set { navItemTitle = value; }
        }

        /// <summary>
        /// Gets or sets the index of the order.
        /// </summary>
        /// <value>
        /// The index of the order.
        /// </value>
        [XmlAttribute]
        public int OrderIndex
        {
            get { return orderIndex; }
            set { orderIndex = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [integration highlighted].
        /// </summary>
        /// <value>
        /// <c>true</c> if [integration highlighted]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool IntegrationHighlighted
        {
            get { return integrationHighlighted; }
            set { integrationHighlighted = value; }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        [XmlElement(XmlElementNames.SectionGroup)]
        public List<SectionGroupModel> Groups
        {
            get { return groups; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [i e7 compat mode required].
        /// </summary>
        /// <value>
        /// <c>true</c> if [i e7 compat mode required]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool IE7CompatModeRequired
        {
            get { return ie7CompatModeRequired; }
            set { ie7CompatModeRequired = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [supports theming].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports theming]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool SupportsTheming
        {
            get { return supportsTheming; }
            set { supportsTheming = value; }
        }

        /// <summary>
        /// Gets the highlighted sections.
        /// </summary>
        /// <value>
        /// The highlighted sections.
        /// </value>
        [XmlIgnore]
        public List<SectionPageModel> HighlightedSections
        {
            get
            {
                if (highlighledSections == null)
                {
                    highlighledSections = CreateHighlightedSections();
                }
                return highlighledSections;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsCurrent
        {
            get { return this == UserSectionItemsModel.GetUserCurrent(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Finds the group.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The SectionGroup.</returns>
        public SectionGroupModel FindGroup(string key)
        {
            foreach (SectionGroupModel group in Groups)
            {
                if (key.ToLower().Equals(group.Key.ToLower()))
                    return group;
            }
            return null;
        }

        /// <summary>
        /// Creates the highlighted sections.
        /// </summary>
        /// <returns>SectionPage</returns>
        List<SectionPageModel> CreateHighlightedSections()
        {
            List<SectionPageModel> result = new List<SectionPageModel>();
            foreach (SectionGroupModel group in Groups)
            {
                foreach (SectionPageModel section in group.Sections)
                {
                    if (section.HighlightedIndex > -1)
                        result.Add(section);
                }
            }
            result.Sort(CompareHighlightedSections);
            return result;
        }

        /// <summary>
        /// Compares the highlighted sections.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The comparison result</returns>
        int CompareHighlightedSections(SectionModel x, SectionModel y)
        {
            return Comparer<int>.Default.Compare(x.HighlightedIndex, y.HighlightedIndex);
        }

        #endregion
    }
}
