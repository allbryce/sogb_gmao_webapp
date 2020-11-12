namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImputationAnalytique")]
    public partial class ImputationAnalytique
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImputationAnalytique()
        {
            FicheDeTravail = new HashSet<FicheDeTravail>();
        }

        public long ImputationAnalytiqueId { get; set; }

        [Required]
        public string LibelleImputationAnalytique { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FicheDeTravail> FicheDeTravail { get; set; }
    }
}
