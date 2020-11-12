namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Absence")]
    public partial class Absence
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UtilisateurId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime DateAbsence { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateReprise { get; set; }

        public virtual Utilisateur Utilisateur { get; set; }
    }
}
