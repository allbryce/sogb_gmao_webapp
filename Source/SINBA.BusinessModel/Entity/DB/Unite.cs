namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Unite")]
    public partial class Unite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unite()
        {
            PossederCaracteristiques = new HashSet<PossederCaracteristiques>();
        }

        public int UniteId { get; set; }

        [Required]
        public string LibelleUnite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossederCaracteristiques> PossederCaracteristiques { get; set; }
        public bool IsUsed { get { return (PossederCaracteristiques.Count > 0); } }
    }
}
