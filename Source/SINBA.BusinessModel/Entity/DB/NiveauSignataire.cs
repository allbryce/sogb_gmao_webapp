namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NiveauSignataire")]
    public partial class NiveauSignataire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NiveauSignataire()
        {
            PouvoirSigner = new HashSet<PouvoirSigner>();
            SignataireParNiveau = new HashSet<SignataireParNiveau>();
        }

        public long NiveauSignataireId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleNiveauSignataire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PouvoirSigner> PouvoirSigner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignataireParNiveau> SignataireParNiveau { get; set; }
    }
}
