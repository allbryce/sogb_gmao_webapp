namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TacheEffectuee")]
    public partial class TacheEffectuee
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FicheDeTravailId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TacheId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long EmployeId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime DateDebut { get; set; }

        public DateTime? Dure { get; set; }

        [Column(TypeName = "text")]
        public string Commentaire { get; set; }

        public virtual Employe Employe { get; set; }

        public virtual FicheDeTravail FicheDeTravail { get; set; }

        public virtual Tache Tache { get; set; }
    }
}
