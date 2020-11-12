namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DecisionAffectation")]
    public partial class DecisionAffectation
    {
        [Key]
        [Column(Order = 0)]
        public DateTime DateEtape { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AffectationId { get; set; }

        public long DecisionId { get; set; }

        public DateTime? Datedecision { get; set; }

        [Column(TypeName = "text")]
        public string Commentaire { get; set; }

        public long NumeroEtape { get; set; }

        public virtual Affectation Affectation { get; set; }

        public virtual Decision Decision { get; set; }

        public virtual Etape Etape { get; set; }
    }
}
