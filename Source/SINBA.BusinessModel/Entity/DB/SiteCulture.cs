namespace Sinba.BusinessModel.Entity
{
    using Resources;
    using Resources.Resources.Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("SiteCulture")]
    public partial class SiteCulture
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(25)]
        public string IdSite { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string IdSysCulture { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        public virtual Site Site { get; set; }

    }
}
