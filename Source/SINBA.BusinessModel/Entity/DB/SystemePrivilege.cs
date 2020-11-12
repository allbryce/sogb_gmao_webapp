namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemePrivilege")]
    public partial class SystemePrivilege
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemePrivilege()
        {
            PouvoirSigner = new HashSet<PouvoirSigner>();
        }

        [Key]
        public long SystemPrivilegeId { get; set; }

        [Required]
        public string LibelleSystemPrivilege { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PouvoirSigner> PouvoirSigner { get; set; }
    }
}
