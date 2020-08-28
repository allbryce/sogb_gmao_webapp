using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for section management.
    /// </summary>
    [Serializable]
    public class SectionModel : SectionModelBase
    {
        #region Variables
        string links = null;
        string metaDescription;
        bool hideSourceCode;
        List<string> sourceFiles = new List<string>();
        bool ie7CompatModeRequired;

        int highlightedIndex = -1;
        string highlightedImageUrl;
        string highlightedTitle;
        string highlightedLink;
        bool isErrorPage = false;

        SectionItemModel item;
        bool linksProcessed;

        bool useSecondTitle = false;
        string secondTitle;

        private string culture;
        #endregion

        #region Properties
        [XmlAttribute]
        public virtual string Culture
        {
            get
            {
                if (culture == null) { culture = string.Empty; }
                return culture;
            }
            set
            {
                culture = value;
            }
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [XmlIgnore]
        public SectionItemModel Item
        {
            get { return item; }
            set { item = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is error page.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is error page; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsErrorPage
        {
            get { return isErrorPage; }
            set { isErrorPage = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [hide source code].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hide source code]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public virtual bool HideSourceCode
        {
            get { return hideSourceCode; }
            set { hideSourceCode = value; }
        }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        [XmlIgnore]
        public string Links
        {
            get
            {
                if (!linksProcessed)
                {
                    ParseLinks();
                }
                return links;
            }
        }
        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        /// <value>
        /// The meta description.
        /// </value>
        [XmlElement]
        public string MetaDescription
        {
            get
            {
                if (metaDescription == null) { return string.Empty; }
                return metaDescription;
            }
            set
            {
                if (value != null)
                {
                    value = value.Trim();
                }
                metaDescription = value;
            }
        }

        /// <summary>
        /// Gets the source files.
        /// </summary>
        /// <value>
        /// The source files.
        /// </value>
        [XmlElement(XmlElementNames.SourceFile)]
        public List<string> SourceFiles
        {
            get { return sourceFiles; }
        }

        /// <summary>
        /// Gets or sets the index of the highlighted.
        /// </summary>
        /// <value>
        /// The index of the highlighted.
        /// </value>
        [XmlAttribute]
        public int HighlightedIndex
        {
            get { return highlightedIndex; }
            set { highlightedIndex = value; }
        }

        /// <summary>
        /// Gets or sets the highlighted image URL.
        /// </summary>
        /// <value>
        /// The highlighted image URL.
        /// </value>
        [XmlAttribute]
        public string HighlightedImageUrl
        {
            get
            {
                if (highlightedImageUrl == null) { return string.Empty; }
                return highlightedImageUrl;
            }
            set { highlightedImageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the highlighted title.
        /// </summary>
        /// <value>
        /// The highlighted title.
        /// </value>
        [XmlAttribute]
        public string HighlightedTitle
        {
            get
            {
                if (highlightedTitle == null) { return string.Empty; }
                return highlightedTitle;
            }
            set { highlightedTitle = value; }
        }

        /// <summary>
        /// Gets or sets the highlighted link.
        /// </summary>
        /// <value>
        /// The highlighted link.
        /// </value>
        [XmlAttribute]
        public string HighlightedLink
        {
            get { return highlightedLink; }
            set { highlightedLink = value; }
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

        public bool UseSecondTitle 
        {
            get { return useSecondTitle; }
            set { useSecondTitle = value; }
        }

        public string SecondTitle
        {
            get { return secondTitle; }
            set { secondTitle = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the highlighted title.
        /// </summary>
        /// <returns></returns>
        public string GetHighlightedTitle()
        {
            if (!String.IsNullOrEmpty(HighlightedTitle))
            {
                return HighlightedTitle;
            }
            return Title;
        }

        /// <summary>
        /// Parses the links.
        /// </summary>
        void ParseLinks()
        {
            linksProcessed = true;
        }

        #endregion
    }
}
