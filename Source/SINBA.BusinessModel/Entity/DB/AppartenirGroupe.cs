namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppartenirGroupe")]
    public partial class AppartenirGroupe
    {
        [Key]
        [Column(Order = 0)]
        public long UtilisateurId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public long GroupeUtilisateurId { get; set; }
    }
}
