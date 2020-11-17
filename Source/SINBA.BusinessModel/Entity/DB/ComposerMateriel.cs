namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ComposerMateriel")]
    public partial class ComposerMateriel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ComposerMateriel()
        {
            PossederCaracteristiques = new HashSet<PossederCaracteristiques>();
        }

        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime DateInsertion { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ComposantId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielId { get; set; }
        public int? Quantite { get; set; }
        public int? Plafond { get; set; }

        public virtual Composant Composant { get; set; }
        public virtual Materiel Materiel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossederCaracteristiques> PossederCaracteristiques { get; set; }
    }
}
