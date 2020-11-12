namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FicheDeTravail")]
    public partial class FicheDeTravail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FicheDeTravail()
        {
            AttribuerFiche = new HashSet<AttribuerFiche>();
            ConstaterPanne = new HashSet<ConstaterPanne>();
            DecisionFiche = new HashSet<DecisionFiche>();
            Signaler = new HashSet<Signaler>();
            Sortir = new HashSet<Sortir>();
            TacheEffectuee = new HashSet<TacheEffectuee>();
        }

        public long FicheDeTravailId { get; set; }

        public long EmployeId { get; set; }

        [Required]
        public long EtatFicheId { get; set; }

        public long DelaiId { get; set; }

        public long? FournisseurId { get; set; }

        public long TypeFicheId { get; set; }

        public long ServiceId { get; set; }

        public long DocumentId { get; set; }

        [StringLength(50)]
        public string FicheDeTravailParentId { get; set; }

        public long MaterielId { get; set; }

        public long? ServiceEmmetteurId { get; set; }

        public long ImputationAnalytiqueId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateEmission { get; set; }

        [Column(TypeName = "text")]
        public string Observation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttribuerFiche> AttribuerFiche { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConstaterPanne> ConstaterPanne { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionFiche> DecisionFiche { get; set; }

        public virtual Delai Delai { get; set; }

        public virtual Document Document { get; set; }

        public virtual Employe Employe { get; set; }

        public virtual EtatFiche EtatFiche { get; set; }

        public virtual Fournisseur Fournisseur { get; set; }

        public virtual ImputationAnalytique ImputationAnalytique { get; set; }

        public virtual Materiel Materiel { get; set; }

        public virtual Service Service { get; set; }

        public virtual Service Service1 { get; set; }

        public virtual TypeFiche TypeFiche { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Signaler> Signaler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sortir> Sortir { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TacheEffectuee> TacheEffectuee { get; set; }
    }
}
