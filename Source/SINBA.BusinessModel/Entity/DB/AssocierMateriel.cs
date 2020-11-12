namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssocierMateriel")]
    public partial class AssocierMateriel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime DateInstallation { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielAssocieId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateRetrait { get; set; }

        public virtual Materiel Materiel { get; set; }

        public virtual Materiel Materiel1 { get; set; }
    }
}
