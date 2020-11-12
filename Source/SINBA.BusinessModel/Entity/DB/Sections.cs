namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sections
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sections()
        {
            AttribuerFiche = new HashSet<AttribuerFiche>();
        }

        public long SectionsId { get; set; }

        public long ServiceId { get; set; }

        [Required]
        public string LibelleSections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttribuerFiche> AttribuerFiche { get; set; }

        public virtual Service Service { get; set; }
    }
}
