namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteUtilisateur")]
    public partial class SiteUtilisateur
    {
        [Key]
        [Column(Order = 0)]
        public string IdUser { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(25)]
        public string IdSite { get; set; }

        public DateTime? DateModif { get; set; }

        [StringLength(128)]
        public string IdUserModif { get; set; }

        public virtual Site Site { get; set; }

        public virtual SinbaUser SinbaUser { get; set; }
    }
}
