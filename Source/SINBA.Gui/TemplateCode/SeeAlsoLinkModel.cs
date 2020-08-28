using System;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for displaying additional Links.
    /// </summary>
    [Serializable]
    public class SeeAlsoLinkModel
    {
        #region Variables
        string src;
        string title;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [XmlAttribute]
        public string Url { get { return src; } set { src = value; } }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [XmlAttribute]
        public string Title { get { return title; } set { title = value; } }
        #endregion
    }
}
