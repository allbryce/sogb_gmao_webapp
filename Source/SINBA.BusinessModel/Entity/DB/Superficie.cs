namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Superficie")]
    public partial class Superficie
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string NouvellePlantationID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime DateEvolution { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string CloneID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short AnneeCulture { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(7)]
        public string AnciennePlantationID { get; set; }

        public byte? MotifEvolutionID { get; set; }

        [Column("Superficie")]
        public double Superficie1 { get; set; }

        public DateTime? DateMiseExploitation { get; set; }

        public DateTime? DateFin { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        public virtual DonneeGeographique DonneeGeographique { get; set; }

        public virtual MotifEvolution MotifEvolution { get; set; }

        public virtual Plantation Plantation { get; set; }
    }
}
