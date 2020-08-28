using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinba.BusinessModel.Entity
{
    public class SiteUtilisateurViewModel
    {
        public string IdUser { get; set; }

        public string NomPrenom { get; set; }
        
        #region Sites Token

        public string SitesToken { get; set; }

        public string IdUserModif { get; set; }
        #endregion
    }
}
