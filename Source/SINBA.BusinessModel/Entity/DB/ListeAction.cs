using System.Collections.Generic;

namespace Sinba.BusinessModel.Entity
{
    /// <summary>
    /// Class Action (Liste)
    /// </summary>
    public partial class Action
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Action"/> class.
        /// </summary>
        public Action()
        {
            FonctionActions = new List<FonctionAction>();
        }
        public string Code { get; set; }
        public string Nom { get; set; }
        public bool? Log { get; set; }
        public virtual ICollection<FonctionAction> FonctionActions { get; set; }
    }
}
