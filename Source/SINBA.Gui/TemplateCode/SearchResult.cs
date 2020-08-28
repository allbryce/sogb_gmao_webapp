using System;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class to manage search results
    /// </summary>
    [Serializable]
    public class SearchResult : IComparable<SearchResult>
    {
        #region Variables
        private int rank = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public SectionItemModel Item { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public SectionModel Section  { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public SectionGroupModel Group  { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text  { get; set; }

        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        /// <value>
        /// The item text.
        /// </value>
        public string ItemText  { get; set; }

        /// <summary>
        /// Gets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public int Rank { get { return rank; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResult"/> class.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="rank">The rank.</param>
        public SearchResult(SectionModel section, int rank)
        {
            this.Section = section;
            this.rank = rank;
            Item = section.Item;
            if (section is SectionPageModel)
            {
                Group = (section as SectionPageModel).Group;
            }
        }
        #endregion

        #region IComparable<SearchResult> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(SearchResult other)
        {
            return other.Rank.CompareTo(Rank);
        }

        #endregion
    }
}
