namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Direction")]
    public partial class Direction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Direction()
        {
            Departement = new HashSet<Departement>();
        }

        public long DirectionId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleDirection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Departement> Departement { get; set; }
        public bool IsUsed { get { return Departement.Count > 0; } }
    }
}
