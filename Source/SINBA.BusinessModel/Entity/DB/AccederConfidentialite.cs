namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccederConfidentialite")]
    public partial class AccederConfidentialite
    {
        [Key]
        [Column(Order = 0)]
        public int ConfidentialiteID { get; set; }

        [Key]
        [Column(Order = 1)]     
        public int ConfidentialiteAccederID { get; set; }
    }
}
