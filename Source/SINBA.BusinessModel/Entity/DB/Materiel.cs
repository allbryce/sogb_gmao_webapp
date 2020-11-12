namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Materiel")]
    public partial class Materiel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materiel()
        {
            Affectation = new HashSet<Affectation>();
            AssocierMateriel = new HashSet<AssocierMateriel>();
            AssocierMateriel1 = new HashSet<AssocierMateriel>();
            ComposerMateriel = new HashSet<ComposerMateriel>();
            FicheDeTravail = new HashSet<FicheDeTravail>();
            InventorierMaterield = new HashSet<InventorierMaterield>();
            LocaliserMateriel = new HashSet<LocaliserMateriel>();
            PossederCaracteristiques = new HashSet<PossederCaracteristiques>();
        }

        public long MaterielId { get; set; }

        public long? DomaineId { get; set; }

        public long? ClasseMaterielId { get; set; }

        public long FournisseurId { get; set; }

        public long? TypeMaterielId { get; set; }

        public long? GroupeInventaireId { get; set; }

        public long? NumeroModel { get; set; }

        [Required]
        public string LibelleMateriel { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateMiseEnService { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateAcquisition { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrixAchat { get; set; }

        [StringLength(50)]
        public string NumeroBonCommande { get; set; }

        public string NumeroSerie { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateSortie { get; set; }

        public bool Actif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Garantie { get; set; }

        public bool? Immobilise { get; set; }

        [StringLength(50)]
        public string NumeroImmobilisation { get; set; }

        public long? SousFamilleId { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affectation> Affectation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssocierMateriel> AssocierMateriel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssocierMateriel> AssocierMateriel1 { get; set; }

        public virtual Classemateriel Classemateriel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComposerMateriel> ComposerMateriel { get; set; }

        public virtual Domaine Domaine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FicheDeTravail> FicheDeTravail { get; set; }

        public virtual Fournisseur Fournisseur { get; set; }

        public virtual GroupeInventaire GroupeInventaire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventorierMaterield> InventorierMaterield { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocaliserMateriel> LocaliserMateriel { get; set; }

        public virtual Model Model { get; set; }

        public virtual SousFamille SousFamille { get; set; }

        public virtual TypeMateriel TypeMateriel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossederCaracteristiques> PossederCaracteristiques { get; set; }
    }
}
