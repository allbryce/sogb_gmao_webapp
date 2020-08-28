using System;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class to manage search inside the navigation
    /// </summary>
    [Serializable]
    public class SearchModel
    {
        #region Variables
        private SynonymsSearchModel synonyms;
        private ExclusionsSearchModel exclusions;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the synonyms.
        /// </summary>
        /// <value>
        /// The synonyms.
        /// </value>
        [XmlElement]
        public SynonymsSearchModel Synonyms
        {
            get { return synonyms; }
            set { synonyms = value; }
        }

        /// <summary>
        /// Gets or sets the exclusions.
        /// </summary>
        /// <value>
        /// The exclusions.
        /// </value>
        [XmlElement]
        public ExclusionsSearchModel Exclusions
        {
            get { return exclusions; }
            set { exclusions = value; }
        }
        #endregion
    }
}
