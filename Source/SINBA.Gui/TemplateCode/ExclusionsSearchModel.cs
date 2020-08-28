using System;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class of Search Model Exclusions
    /// </summary>
    [Serializable]
    public class ExclusionsSearchModel
    {
        #region Variables
        private string words;
        private string prefixes;
        private string postfixes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the words.
        /// </summary>
        /// <value>
        /// The words.
        /// </value>
        [XmlElement]
        public string Words
        {
            get { return words; }
            set { words = value; }
        }
        /// <summary>
        /// Gets or sets the prefixes.
        /// </summary>
        /// <value>
        /// The prefixes.
        /// </value>
        [XmlElement]
        public string Prefixes
        {
            get { return prefixes; }
            set { prefixes = value; }
        }
        /// <summary>
        /// Gets or sets the postfixes.
        /// </summary>
        /// <value>
        /// The postfixes.
        /// </value>
        [XmlElement]
        public string Postfixes
        {
            get { return postfixes; }
            set { postfixes = value; }
        }
        #endregion
    }
}
