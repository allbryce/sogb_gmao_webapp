namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Composant")]
    public partial class Composant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Composant()
        {
            ComposerMateriel = new HashSet<ComposerMateriel>();
            PossederCaracteristiques = new HashSet<PossederCaracteristiques>();
            CaracteristiqueComposant = new HashSet<CaracteristiqueComposant>();
        }

        public long ComposantId { get; set; }

        public long DomaineId { get; set; }

        [Required]
        [StringLength(32)]
        public string LibelleComposant { get; set; }

        public int? OrdreComposant { get; set; }

        public virtual Domaine Domaine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComposerMateriel> ComposerMateriel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossederCaracteristiques> PossederCaracteristiques { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaracteristiqueComposant> CaracteristiqueComposant { get; set; }
    }
}
