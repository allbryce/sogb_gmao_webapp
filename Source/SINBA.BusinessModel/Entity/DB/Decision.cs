namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Decision")]
    public partial class Decision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Decision()
        {
            DecisionAffectation = new HashSet<DecisionAffectation>();
            DecisionBon = new HashSet<DecisionBon>();
            DecisionFiche = new HashSet<DecisionFiche>();
        }

        public long DecisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleDecision { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionAffectation> DecisionAffectation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionBon> DecisionBon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionFiche> DecisionFiche { get; set; }
    }
}
