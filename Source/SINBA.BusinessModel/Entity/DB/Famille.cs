namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Famille")]
    public partial class Famille
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Famille()
        {
            SousFamille = new HashSet<SousFamille>();
        }

        public long FamilleId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleFamille { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SousFamille> SousFamille { get; set; }
        public bool IsUsed { get { return (SousFamille.Count > 0); } }
    }
}
