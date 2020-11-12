namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sortir")]
    public partial class Sortir
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NumeroBon { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FicheDeTravailId { get; set; }

        public long ArticleId { get; set; }

        public DateTime DateSortie { get; set; }

        public double QuantiteSortie { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrixUnitaire { get; set; }

        public double QuantiteDemande { get; set; }

        public virtual Article Article { get; set; }

        public virtual BonMagasin BonMagasin { get; set; }

        public virtual FicheDeTravail FicheDeTravail { get; set; }
    }
}
