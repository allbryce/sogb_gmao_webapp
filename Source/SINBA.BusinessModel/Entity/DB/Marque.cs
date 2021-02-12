namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Marque")]
    public partial class Marque
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Marque()
        {
            Model = new HashSet<Model>();
        }

        public long MarqueId { get; set; }

        [Required]
        public string LibelleMarque { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Model> Model { get; set; }
        public bool IsUsed { get { return (Model.Count > 0); } }
    }
}
