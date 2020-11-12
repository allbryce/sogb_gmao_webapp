namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocaliserMateriel")]
    public partial class LocaliserMateriel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LocalisationId { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime DateAffectation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateFin { get; set; }

        public virtual Localisation Localisation { get; set; }

        public virtual Materiel Materiel { get; set; }
    }
}
