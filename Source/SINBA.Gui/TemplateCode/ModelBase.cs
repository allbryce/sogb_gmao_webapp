using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Base class of Navigation model
    /// </summary>
    [Serializable]
    public class ModelBase
    {
        #region Variables
        private string keywords;
        private Dictionary<string, int> keywordsRankList;
        private bool visible;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        [XmlElement]
        public string Keywords
        {
            get
            {
                if (keywords == null) { return string.Empty; }
                return keywords;
            }
            set
            {
                if (value != null)
                    value = value.Trim();
                keywords = value;
            }
        }

        /// <summary>
        /// Gets the keywords rank list.
        /// </summary>
        /// <value>
        /// The keywords rank list.
        /// </value>
        [XmlIgnore]
        public Dictionary<string, int> KeywordsRankList
        {
            get
            {
                if (keywordsRankList == null)
                    keywordsRankList = SearchTools.GetKeywordsRankList(this);
                return keywordsRankList;
            }
            set
            {
                this.keywordsRankList = value;
            }
        }

        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
        #endregion
    }
}
