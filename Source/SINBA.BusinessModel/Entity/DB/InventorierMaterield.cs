namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InventorierMaterield")]
    public partial class InventorierMaterield
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NumeroInventaire { get; set; }
        public long LocalisationId { get; set; }

        [Required]
        public string LocalisationActuelle { get; set; }
        public virtual Inventaire Inventaire { get; set; }
        public virtual Localisation Localisation { get; set; }
        public virtual Materiel Materiel { get; set; }
    }
}
