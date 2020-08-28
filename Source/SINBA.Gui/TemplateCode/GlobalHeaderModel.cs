using System;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Global Header
    /// </summary>
    [Serializable]
    public class GlobalHeaderModel
    {
        #region Variables
        private string logoPlatformSubject = "SINBA";
        private string logoPlatformDescription = "version 1.0";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the logo platform subject.
        /// </summary>
        /// <value>
        /// The logo platform subject.
        /// </value>
        [XmlAttribute]
        public string LogoPlatformSubject
        {
            get { return logoPlatformSubject; }
            set { logoPlatformSubject = value; }
        }

        /// <summary>
        /// Gets or sets the logo platform description.
        /// </summary>
        /// <value>
        /// The logo platform description.
        /// </value>
        [XmlAttribute]
        public string LogoPlatformDescription
        {
            get { return logoPlatformDescription; }
            set { logoPlatformDescription = value; }
        }
        #endregion
    }
}
