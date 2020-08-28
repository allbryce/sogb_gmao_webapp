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

    [Table("Societe")]
    public partial class Societe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Societe()
        {
            Site = new HashSet<Site>();
        }

        [NotMapped]
        public string IdHidden { get; set; } //A voir!!!

        [Display(Name = ResourceNames.Entity.Code, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(10)]
        [Remote(SinbaConstants.Actions.IsIdUsed, SinbaConstants.Controllers.Societe, ErrorMessageResourceName = ResourceNames.Error.ErrorCodeExists, ErrorMessageResourceType = typeof(EntityCommonResource), AdditionalFields = DbColumns.IdHidden)]
        public string Id { get; set; }

        [Display(Name = ResourceNames.Entity.Libelle, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(50)]
        public string Libelle { get; set; }

        [StringLength(50)]
        [Display(Name = ResourceNames.Entity.LibelleEn, ResourceType = typeof(EntityColumnResource))]
        public string LibelleEn { get; set; }

        public bool? Actif { get; set; }
        [StringLength(5)]
        public string MonnaieDeBase { get; set; }

    

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Site { get; set; }

        [NotMapped]
        public bool IsUsed
        {
            get
            {
                return  Site.Count > 0;
            }
        }
    }
}
