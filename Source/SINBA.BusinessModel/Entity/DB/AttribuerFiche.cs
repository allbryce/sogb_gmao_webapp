namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttribuerFiche")]
    public partial class AttribuerFiche
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FicheDeTravailId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SectionId { get; set; }

        public DateTime? Date { get; set; }

        public virtual FicheDeTravail FicheDeTravail { get; set; }

        public virtual Sections Sections { get; set; }
    }
}
