namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysFonction")]
    public partial class SysFonction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysFonction()
        {
            FonctionContacts = new HashSet<FonctionContact>();
        }

        [Key]
        [StringLength(10)]
        public string FonctionID { get; set; }

        [StringLength(50)]
        public string LibelleFonction { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FonctionContact> FonctionContacts { get; set; }
    }
}
