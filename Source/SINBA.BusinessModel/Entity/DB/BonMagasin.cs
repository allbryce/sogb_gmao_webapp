namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BonMagasin")]
    public partial class BonMagasin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BonMagasin()
        {
            DecisionBon = new HashSet<DecisionBon>();
            Sortir = new HashSet<Sortir>();
        }

        [Key]
        public long NumeroBon { get; set; }

        public long DocumentId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleBon { get; set; }

        public DateTime? DateExecution { get; set; }

        public virtual Document Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionBon> DecisionBon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sortir> Sortir { get; set; }
    }
}
