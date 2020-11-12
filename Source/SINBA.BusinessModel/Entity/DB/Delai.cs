namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Delai")]
    public partial class Delai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Delai()
        {
            FicheDeTravail = new HashSet<FicheDeTravail>();
        }

        public long DelaiId { get; set; }

        [Column(TypeName = "date")]
        public DateTime LibelleDelai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FicheDeTravail> FicheDeTravail { get; set; }
    }
}
