namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysTypeBlocage")]
    public partial class SysTypeBlocage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysTypeBlocage()
        {
            BlocagePlantations = new HashSet<BlocagePlantation>();
        }

        [Key]
        [Column("SysTypeBlocage")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SysTypeBlocage1 { get; set; }

        [StringLength(50)]
        public string LibelleSysTypeBlocage { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlocagePlantation> BlocagePlantations { get; set; }
    }
}
