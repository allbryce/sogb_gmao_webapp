using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Base class for Theme Model
    /// </summary>
    public class ThemeModelBase
    {
        #region Variables
        string name;
        string title;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        public string Name
        {
            get
            {
                if (name == null) { return string.Empty; }
                return name;
            }
            set { name = value; }
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
            set { title = value; }
        }
        #endregion
    }
}
