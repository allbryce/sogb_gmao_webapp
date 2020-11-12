namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Suppleer")]
    public partial class Suppleer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UtilisateurId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SuppleantId { get; set; }

        [Key]
        [Column(Order = 2)]
        public long NumeroEtape { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroOrdre { get; set; }

        public virtual Etape Etape { get; set; }

        public virtual Utilisateur Utilisateur { get; set; }

        public virtual Utilisateur Utilisateur1 { get; set; }
    }
}
