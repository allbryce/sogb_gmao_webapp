namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaracteriserComposant")]
    public partial class CaracteriserComposant
    {
        [Key]
        [Column(Order = 0)]
        public long ComposantId { get; set; }

        [Key]
        [Column(Order = 1)]
        public long CaracteristiqueComposantId { get; set; }
    }
}
