namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Signature")]
    public partial class Signature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RéfSign { get; set; }

        [StringLength(50)]
        public string NomSign { get; set; }

        [StringLength(50)]
        public string PrénomSign { get; set; }

        [StringLength(50)]
        public string FonctionSign { get; set; }
    }
}
