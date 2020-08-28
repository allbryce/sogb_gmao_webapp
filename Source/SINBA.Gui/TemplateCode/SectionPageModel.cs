using System;
using System.Linq;
using Sinba.Resources;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Model Class for Section Page.
    /// </summary>
    [Serializable]
    public class SectionPageModel : SectionModel
    {
        #region Variables
        SectionGroupModel group;
        List<SeeAlsoLinkModel> seeAlsoLinks = new List<SeeAlsoLinkModel>();
        string highlightedDescription = string.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        [XmlIgnore]
        public SectionGroupModel Group
        {
            get { return group; }
            set { group = value; }
        }

        /// <summary>
        /// Gets or sets the see also links.
        /// </summary>
        /// <value>
        /// The see also links.
        /// </value>
        [XmlElement(XmlElementNames.SeeAlso)]
        public List<SeeAlsoLinkModel> SeeAlsoLinks { get { return seeAlsoLinks; } set { seeAlsoLinks = value; } }

        /// <summary>
        /// Gets or sets the highlighted description.
        /// </summary>
        /// <value>
        /// The highlighted description.
        /// </value>
        [XmlElement(XmlElementNames.HighlightedDescription)]
        public string HighlightedDescription { get { return highlightedDescription; } set { highlightedDescription = value; } }
        #endregion
    }
}
