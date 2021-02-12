namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaracteristiqueComposant")]
    public partial class CaracteristiqueComposant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaracteristiqueComposant()
        {
            PossederCaracteristiques = new HashSet<PossederCaracteristiques>();
            Composant = new HashSet<Composant>();
        }


        public long CaracteristiqueComposantId { get; set; }

        [Required]
        [StringLength(100)]
        public string LibelleCaracteristique { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossederCaracteristiques> PossederCaracteristiques { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Composant> Composant { get; set; }
        public bool IsUsed { get { return (Composant.Count>0||PossederCaracteristiques.Count>0); } }
    }
}
