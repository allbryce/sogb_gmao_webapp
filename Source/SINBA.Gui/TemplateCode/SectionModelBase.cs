using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Base class model for sections
    /// </summary>
    [Serializable]
    public class SectionModelBase : ModelBase
    {
        #region Variables
        private string key;
        private string title;
        private string helpurl;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [XmlAttribute]
        public virtual string Key
        {
            get
            {
                if (key == null) { return string.Empty; }
                return key;
            }
            set
            {
                key = value;
            }
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
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// HelpUrl.
        /// </summary>
        /// <value>
        /// The help url.
        /// </value>
        [XmlAttribute]
        public string HelpUrl
        {
            get
            {
                if (helpurl == null) { return string.Empty; }
                return helpurl;
            }
            set
            {
                helpurl = value;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }
        #endregion
    }

    
}
