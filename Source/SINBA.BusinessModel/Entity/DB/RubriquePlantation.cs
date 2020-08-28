namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RubriquePlantation")]
    public partial class RubriquePlantation
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string PlantationID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string RubriqueID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DateDebut { get; set; }

        public bool? Exclu { get; set; }

        public decimal? ValeurPaie { get; set; }

        public DateTime? DateFin { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        public virtual Plantation Plantation { get; set; }

        public virtual RubriquePaie RubriquePaie { get; set; }
    }
}
