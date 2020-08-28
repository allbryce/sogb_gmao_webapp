namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysTypeGeolocalisation")]
    public partial class SysTypeGeolocalisation
    {
        [Key]
        [StringLength(1)]
        public string sysTypeDonneesGeographique { get; set; }

        [StringLength(50)]
        public string sysLibelleTypeDonneesGeographique { get; set; }
    }
}
