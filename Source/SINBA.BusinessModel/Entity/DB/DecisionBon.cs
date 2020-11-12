namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DecisionBon")]
    public partial class DecisionBon
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NumeroBon { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime DateEtape { get; set; }

        public long DecisionId { get; set; }

        public long NumeroEtape { get; set; }

        public DateTime DateDecision { get; set; }

        [Column(TypeName = "text")]
        public string Commentaire { get; set; }

        public virtual BonMagasin BonMagasin { get; set; }

        public virtual Decision Decision { get; set; }

        public virtual Etape Etape { get; set; }
    }
}
