namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Model")]
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            Materiel = new HashSet<Materiel>();
        }

        [Key]
        public long NumeroModel { get; set; }

        public long? MarqueId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleModel { get; set; }

        public virtual Marque Marque { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Materiel> Materiel { get; set; }
    }
}
