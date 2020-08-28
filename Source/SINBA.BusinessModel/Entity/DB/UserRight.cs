using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinba.BusinessModel.Entity
{
    /// <summary>
    /// Class UserRight
    /// </summary>
    public partial class UserRight
    {
        [NotMapped]
        public string IdHidden { get; set; }

        public string IdUser { get; set; }

        [Display(Name = ResourceNames.Entity.Fonction, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string CodeFonction { get; set; }

        [Display(Name = ResourceNames.Entity.Action, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string CodeAction { get; set; }

        [Display(Name = ResourceNames.Entity.DenyAccess, ResourceType = typeof(EntityColumnResource))]
        public bool DenyAccess { get; set; }

        public virtual FonctionAction FonctionAction { get; set; }

        public virtual SinbaUser SinbaUser { get; set; }

        [NotMapped]
        public bool? FromProfil { get; set; }
    }
}
