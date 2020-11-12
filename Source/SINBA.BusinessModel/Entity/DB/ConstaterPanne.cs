namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConstaterPanne")]
    public partial class ConstaterPanne
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PanneId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FicheDeTravailId { get; set; }

        [Column(TypeName = "text")]
        public string Commentaire { get; set; }

        public virtual FicheDeTravail FicheDeTravail { get; set; }

        public virtual Panne Panne { get; set; }
    }
}
