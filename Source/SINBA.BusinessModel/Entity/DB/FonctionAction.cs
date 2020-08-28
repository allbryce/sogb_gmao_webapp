using System.Collections.Generic;

namespace Sinba.BusinessModel.Entity
{
    /// <summary>
    /// Class FonctionAction
    /// </summary>
    public partial class FonctionAction
    {
        public FonctionAction()
        {
            UserRights = new List<UserRight>();
            ProfilRights = new List<ProfilRight>();
        }

        public string CodeFonction { get; set; }

        public string CodeAction { get; set; }

        public virtual Fonction Fonction { get; set; }

        public virtual Action Action { get; set; }

        public virtual ICollection<UserRight> UserRights { get; set; }

        public virtual ICollection<ProfilRight> ProfilRights { get; set; }
    }
}
