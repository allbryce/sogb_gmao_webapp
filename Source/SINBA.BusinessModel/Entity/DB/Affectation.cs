namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Affectation")]
    public partial class Affectation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Affectation()
        {
            DecisionAffectation = new HashSet<DecisionAffectation>();
        }

        public long MaterielId { get; set; }

        public long EmployeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAfffectation { get; set; }

        public long DocumentId { get; set; }

        public long? ServiceId { get; set; }

        public long LocalisationId { get; set; }

        public long? SerciceResponsableId { get; set; }

        public long TypeAffectationId { get; set; }

        [Column(TypeName = "text")]
        public string Commentaire { get; set; }

        public long AffectationId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Employe Employe { get; set; }

        public virtual Localisation Localisation { get; set; }

        public virtual Materiel Materiel { get; set; }

        public virtual Service Service { get; set; }

        public virtual Service Service1 { get; set; }

        public virtual TypeAffectation TypeAffectation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DecisionAffectation> DecisionAffectation { get; set; }
    }
}
