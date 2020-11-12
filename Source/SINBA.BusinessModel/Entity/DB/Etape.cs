namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Etape")]
    public partial class Etape
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etape()
        {
            DecisionAffectation = new HashSet<DecisionAffectation>();
            DecisionBon = new HashSet<DecisionBon>();
            DecisionFiche = new HashSet<DecisionFiche>();
            PouvoirSigner = new HashSet<PouvoirSigner>();
            Suppleer = new HashSet<Suppleer>();
        }

        [Key]
        public long NumeroEtape { get; set; }

        [Required]
        public long DocumentId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleEtape { get; set; }

        public int? Ordre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionAffectation> DecisionAffectation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionBon> DecisionBon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionFiche> DecisionFiche { get; set; }

        public virtual Document Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PouvoirSigner> PouvoirSigner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Suppleer> Suppleer { get; set; }
    }
}
