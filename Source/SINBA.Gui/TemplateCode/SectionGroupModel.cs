using System;
using System.Linq;
using Sinba.Resources;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Group Section
    /// </summary>
    [Serializable]
    public class SectionGroupModel : SectionModelBase
    {
        #region Variales
        List<SectionPageModel> sections = new List<SectionPageModel>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <value>
        /// The sections.
        /// </value>
        [XmlElement(Type = typeof(SectionPageModel), ElementName = XmlElementNames.Section)]
        public List<SectionPageModel> Sections
        {
            get { return sections; }
        }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [XmlIgnore]
        public SectionItemModel SectionItem { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Finds the section.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The sectionModel.</returns>
        public SectionModel FindSection(string key)
        {
            foreach (SectionModel section in Sections)
            {
                if (key.ToLower().Equals(section.Key.ToLower()))
                    return section;
            }
            return null;
        }

        /// <summary>
        /// Gets all sections.
        /// </summary>
        /// <returns>The list of Sections.</returns>
        public List<SectionPageModel> GetAllSections()
        {
            List<SectionPageModel> result = new List<SectionPageModel>();
            result.AddRange(Sections);
            return result;
        }
        #endregion
    }

}
