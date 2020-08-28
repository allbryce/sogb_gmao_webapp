using System;
using Sinba.Resources;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Synonyms in search model.
    /// </summary>
    [Serializable]
    public class SynonymsSearchModel
    {
        #region Variables
        List<string> groups = new List<string>();
        #endregion

        #region Properties
        [XmlElement(XmlElementNames.Group)]
        public List<string> Groups
        {
            get { return groups; }
            set { groups = value; }
        }
        #endregion
    }
}
