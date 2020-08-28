using Sinba.Resources;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Themes management
    /// </summary>
    /// <remarks>
    /// A Theme was created for SINBA application
    /// </remarks>
    [XmlRoot(XmlElementNames.Themes)]
    public class ThemesModel
    {
        #region Variables
        static ThemesModel current;
        static readonly object currentLock = new object();

        List<ThemeGroupModel> groups = new List<ThemeGroupModel>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static ThemesModel Current
        {
            get
            {
                lock (currentLock)
                {
                    if (current == null)
                    {
                        using (Stream stream = File.OpenRead(HttpContext.Current.Server.MapPath("~/App_Data/Themes.xml")))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(ThemesModel));
                            current = (ThemesModel)serializer.Deserialize(stream);
                        }
                    }
                    return current;
                }
            }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        [XmlElement(XmlElementNames.ThemeGroup)]
        public List<ThemeGroupModel> Groups
        {
            get { return groups; }
        }

        /// <summary>
        /// Gets the left groups.
        /// </summary>
        /// <value>
        /// The left groups.
        /// </value>
        public List<ThemeGroupModel> LeftGroups
        {
            get { return (from g in Groups where g.Float == "Left" select g).ToList(); }
        }

        /// <summary>
        /// Gets the right groups.
        /// </summary>
        /// <value>
        /// The right groups.
        /// </value>
        public List<ThemeGroupModel> RightGroups
        {
            get { return (from g in Groups where g.Float == "Right" select g).ToList(); }
        }
        #endregion
    }
}
