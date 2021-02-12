namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupeInventaire")]
    public partial class GroupeInventaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupeInventaire()
        {
            Materiel = new HashSet<Materiel>();
        }

        public long GroupeInventaireId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleGroupeInventaire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Materiel> Materiel { get; set; }
        public bool IsUsed { get { return (Materiel.Count > 0); } }
    }
}
