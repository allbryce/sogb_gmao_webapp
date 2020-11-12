namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventaire")]
    public partial class Inventaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventaire()
        {
            InventorierMaterield = new HashSet<InventorierMaterield>();
        }

        [Key]
        public long NumeroIventaire { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateIventaire { get; set; }

        [Column(TypeName = "text")]
        public string Commentaire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventorierMaterield> InventorierMaterield { get; set; }
    }
}
