using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Theme Model
    /// </summary>
    public class ThemeModel : ThemeModelBase
    {
        #region Variables
        string spriteCssClass;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sprite CSS class.
        /// </summary>
        /// <value>
        /// The sprite CSS class.
        /// </value>
        [XmlAttribute]
        public string SpriteCssClass
        {
            get
            {
                if (spriteCssClass == null) { return string.Empty; }
                return spriteCssClass;
            }
            set { spriteCssClass = value; }
        }
        #endregion
    }
}
