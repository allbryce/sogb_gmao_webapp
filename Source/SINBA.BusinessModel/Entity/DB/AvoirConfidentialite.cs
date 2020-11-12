namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AvoirConfidentialite")]
    public partial class AvoirConfidentialite
    {
        [Key]
        [Column(Order = 0)]
        public long ConfidentialiteId { get; set; }

        [Key]
        [Column(Order = 1)]
        public long UtilisateurId { get; set; }
    }
}
