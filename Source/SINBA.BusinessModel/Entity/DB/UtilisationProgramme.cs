namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilisationProgramme")]
    public partial class UtilisationProgramme
    {
        [Key]
        [Column(Order = 0)]
        public DateTime DateUtilisation { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Utilisateur { get; set; }

        [StringLength(8)]
        public string HDéb { get; set; }

        [StringLength(8)]
        public string HFin { get; set; }
    }
}
