namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Panne")]
    public partial class Panne
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Panne()
        {
            ConstaterPanne = new HashSet<ConstaterPanne>();
            Signaler = new HashSet<Signaler>();
        }

        public long PanneId { get; set; }

        public long? TypePanneId { get; set; }

        public long? SousFamilleId { get; set; }

        [Required]
        public string LibellePanne { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConstaterPanne> ConstaterPanne { get; set; }

        public virtual SousFamille SousFamille { get; set; }

        public virtual TypePanne TypePanne { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Signaler> Signaler { get; set; }
    }
}
