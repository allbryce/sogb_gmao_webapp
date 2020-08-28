namespace Sinba.BusinessModel.Entity
{
    using Resources;
    using Resources.Resources.Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Site")]
    public partial class Site
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Site()
        {
            SiteUtilisateur = new HashSet<SiteUtilisateur>();
            SiteCulture = new HashSet<SiteCulture>();
        }

        [NotMapped]
        public string IdHidden { get; set; } //Voir pourquoi cette ligne a été ajoutée

        [Display(Name = ResourceNames.Entity.Code, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(25)]
        [Remote(SinbaConstants.Actions.IsIdUsed, SinbaConstants.Controllers.Site, ErrorMessageResourceName = ResourceNames.Error.ErrorCodeExists, ErrorMessageResourceType = typeof(EntityCommonResource), AdditionalFields = DbColumns.IdHidden)]
        public string Id { get; set; }

        [Display(Name = ResourceNames.Entity.Libelle, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(50)]
        public string Libelle { get; set; }

        [Display(Name = ResourceNames.Entity.IdSociete, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(10)]
        public string IdSociete { get; set; }

        public virtual Societe Societe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteUtilisateur> SiteUtilisateur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteCulture> SiteCulture { get; set; }

        [NotMapped]
        public bool IsUsed
        {
            get
            {
                return SiteUtilisateur.Count > 0; //Voir si ajouter CentreDeCoutSite et Bloc
            }
        }
    }
}
