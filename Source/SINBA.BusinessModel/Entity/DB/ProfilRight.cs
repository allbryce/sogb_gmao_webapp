using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinba.BusinessModel.Entity
{
    public partial class ProfilRight
    {
        [NotMapped]
        public long IdHidden { get; set; }

        public long IdProfil { get; set; }

        [Display(Name = ResourceNames.Entity.Fonction, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string CodeFonction { get; set; }

        public string CodeAction { get; set; }

        public virtual FonctionAction FonctionAction { get; set; }

        public virtual Profil Profil { get; set; }

        #region Actions Token
        [NotMapped]
        public string ActionsToken { get; set; }
        #endregion
    }
}
