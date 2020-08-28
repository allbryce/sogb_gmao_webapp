namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeContactActivite")]
    public partial class TypeContactActivite
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string TypeContactID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ActiviteID { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        public virtual Activite Activite { get; set; }

        public virtual TypeContact TypeContact { get; set; }
    }
}
