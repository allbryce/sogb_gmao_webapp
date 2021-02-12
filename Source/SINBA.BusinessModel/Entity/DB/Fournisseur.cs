namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fournisseur")]
    public partial class Fournisseur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fournisseur()
        {
            FicheDeTravail = new HashSet<FicheDeTravail>();
            Materiel = new HashSet<Materiel>();
        }

        public long FournisseurId { get; set; }

        [Required]
        [StringLength(50)]
        public string NomFournisseur { get; set; }

        public string Adresse { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FicheDeTravail> FicheDeTravail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Materiel> Materiel { get; set; }
        public bool IsUsed { get { return (Materiel.Count > 0 || FicheDeTravail.Count > 0); } }
    }
}
