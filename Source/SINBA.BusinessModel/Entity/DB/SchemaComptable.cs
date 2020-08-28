namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SchemaComptable")]
    public partial class SchemaComptable
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string Projet { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Ligne { get; set; }

        [StringLength(15)]
        public string CatRubrique { get; set; }

        [StringLength(6)]
        public string NatUO { get; set; }

        public bool Analyse { get; set; }

        [StringLength(30)]
        public string Libellécompta { get; set; }

        [StringLength(6)]
        public string CIA { get; set; }

        [StringLength(4)]
        public string RubAnalytique { get; set; }

        [StringLength(15)]
        public string CG { get; set; }

        [StringLength(1)]
        public string Sens { get; set; }

        [StringLength(5)]
        public string JournalID { get; set; }

        public short? Signe { get; set; }

        [StringLength(1)]
        public string ModeCumul { get; set; }

        public virtual Projet Projet1 { get; set; }

        public virtual RubriquePaie RubriquePaie { get; set; }
    }
}
