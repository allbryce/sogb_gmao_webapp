using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Xml.Serialization;
using Sinba.BusinessModel.Entity;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for root tree node (Section Items)
    /// </summary>
    [XmlRoot(XmlElementNames.SectionItems)]
    [Serializable]
    public class SectionItemsModel
    {
        #region Variables
        SectionItemModel current;
        GlobalHeaderModel globalHeader = new GlobalHeaderModel();

        bool expandAllSectionsAtFirstTime;
        bool disableTextWrap;

        List<SectionItemModel> sectionItems = new List<SectionItemModel>();
        List<SectionItemModel> sortedSectionItems;
        SearchModel search;
        #endregion

        #region Properties
        public SectionItemModel Current
        {
            get 
            {
                if(current == null)
                {
                    current = UserSectionItemsModel.Current.CloneSerialize() as SectionItemModel;
                }
                return current; 
            }
            set { current = value; }
        }

        /// <summary>
        /// Gets the section items.
        /// </summary>
        /// <value>
        /// The section items.
        /// </value>
        [XmlElement(XmlElementNames.SectionItem)]
        public List<SectionItemModel> SectionItems { get { return sectionItems; } }

        /// <summary>
        /// Gets the sorted section items.
        /// </summary>
        /// <value>
        /// The sorted section items.
        /// </value>
        [XmlIgnore]
        public List<SectionItemModel> SortedSectionItems
        {
            get
            {
                if (sortedSectionItems == null)
                {
                    sortedSectionItems = sectionItems.OrderBy(p => p.OrderIndex).ToList();
                }
                return sortedSectionItems;
            }
            set
            {
                sortedSectionItems = value;
            }
        }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>
        /// The search.
        /// </value>
        [XmlElement(XmlElementNames.Search)]
        public SearchModel Search
        {
            get { return search; }
            set { search = value; }
        }

        /// <summary>
        /// Gets or sets the global header.
        /// </summary>
        /// <value>
        /// The global header.
        /// </value>
        [XmlElement(XmlElementNames.GlobalHeader)]
        public GlobalHeaderModel GlobalHeader
        {
            get { return globalHeader; }
            set { globalHeader = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [expand all sections at first time].
        /// </summary>
        /// <value>
        /// <c>true</c> if [expand all sections at first time]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool ExpandAllSectionsAtFirstTime
        {
            get { return expandAllSectionsAtFirstTime; }
            set { expandAllSectionsAtFirstTime = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disable text wrap].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disable text wrap]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool DisableTextWrap
        {
            get { return disableTextWrap; }
            set { disableTextWrap = value; }
        }
        #endregion

        #region Methods
        public SectionGroupModel FindGroup(string sectionItemKey, string sectionGroupKey)
        {
            SectionGroupModel sectionGroup = null;

            sectionItemKey = string.IsNullOrEmpty(sectionItemKey) ? string.Empty : sectionItemKey.ToLower();
            sectionGroupKey = string.IsNullOrEmpty(sectionGroupKey) ? string.Empty : sectionGroupKey.ToLower();
            
            foreach(SectionItemModel item in sectionItems)
            {
                if(item.Key.ToLower().Equals(sectionItemKey.ToLower()))
                {
                    foreach(SectionGroupModel group in item.Groups)
                    {
                        if(group.Key.ToLower().Equals(sectionGroupKey.ToLower()))
                        {
                            sectionGroup = group;
                            break;
                        }
                    }
                    if (sectionGroup != null) { break; }
                }
            }
            return sectionGroup;
        }
        #endregion
    }
}
