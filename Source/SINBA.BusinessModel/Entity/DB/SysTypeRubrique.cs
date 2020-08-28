namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysTypeRubrique")]
    public partial class SysTypeRubrique
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysTypeRubrique()
        {
            RubriquePaies = new HashSet<RubriquePaie>();
        }

        [Key]
        public byte TypeRubrique { get; set; }

        [StringLength(25)]
        public string LibelleTypeRubrique { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RubriquePaie> RubriquePaies { get; set; }
    }
}
