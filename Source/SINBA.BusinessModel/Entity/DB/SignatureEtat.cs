namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SignatureEtat")]
    public partial class SignatureEtat
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Etat { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RéfSign { get; set; }
    }
}
