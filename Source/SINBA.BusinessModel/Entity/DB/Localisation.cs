using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Sinba.BusinessModel.Entity.ViewModels;

namespace Sinba.BusinessModel.Entity
{  
    [Table("Localisation")]
    public partial class Localisation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Localisation()
        {
            Affectation = new HashSet<Affectation>();
            InventorierMaterield = new HashSet<InventorierMaterield>();
            LocaliserMateriel = new HashSet<LocaliserMateriel>();
        }

        public long LocalisationId { get; set; }

        [Required]
        public string LibelleLocalisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affectation> Affectation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventorierMaterield> InventorierMaterield { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocaliserMateriel> LocaliserMateriel { get; set; }
        public bool IsUsed { get { return (LocaliserMateriel.Count > 0); } }

        public LocalisationViewModel ToViewModel(Localisation localiser)
        {
            var localisation = new LocalisationViewModel()
            {
                LocalisationId = localiser.LocalisationId,
                LibelleLocalisation = localiser.LibelleLocalisation
            };
            return localisation;
        }
    }
}
