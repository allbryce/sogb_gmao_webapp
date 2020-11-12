namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Departement")]
    public partial class Departement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departement()
        {
            Service = new HashSet<Service>();
        }

        public long DepartementId { get; set; }

        public long DirectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string LibelleDepartement { get; set; }

        public virtual Direction Direction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Service { get; set; }

        public bool IsUsed { get { return Service.Count > 0; } }

    }
}
    
    
