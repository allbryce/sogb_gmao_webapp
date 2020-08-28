using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Class for Text rank
    /// </summary>
    public class TextRank
    {
        #region Properties
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public int Rank
        {
            get;
            set;
        }
        #endregion

        #region Constructers
        /// <summary>
        /// Initializes a new instance of the <see cref="TextRank"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="rank">The rank.</param>
        public TextRank(string text, int rank)
        {
            this.Text = text;
            this.Rank = rank;
        }
        #endregion
    }
}
