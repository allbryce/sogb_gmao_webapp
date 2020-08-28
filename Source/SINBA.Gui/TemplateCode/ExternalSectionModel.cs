using System;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for External Sections.
    /// </summary>
    [Serializable]
    public class ExternalSectionModel
    {
        #region Variables
        string imageUrl;
        string url;
        string title;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        [XmlAttribute]
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [XmlAttribute]
        public string Url
        {
            get { return url; }
            set { url = value; }
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
            get { return title; }
            set { title = value; }
        }
        #endregion
    }

}
