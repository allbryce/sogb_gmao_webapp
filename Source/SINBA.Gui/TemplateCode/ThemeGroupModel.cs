using Sinba.Resources;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Theme Group Model.
    /// </summary>
    public class ThemeGroupModel : ThemeModelBase
    {
        #region Variables
        List<ThemeModel> themes = new List<ThemeModel>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the themes.
        /// </summary>
        /// <value>
        /// The themes.
        /// </value>
        [XmlElement(ElementName = XmlElementNames.Theme)]
        public List<ThemeModel> Themes
        {
            get { return themes; }
        }

        /// <summary>
        /// Gets or sets the float.
        /// </summary>
        /// <value>
        /// The float.
        /// </value>
        [XmlAttribute(XmlElementNames.Float)]
        public string Float { get; set; }
        #endregion
    }
}
