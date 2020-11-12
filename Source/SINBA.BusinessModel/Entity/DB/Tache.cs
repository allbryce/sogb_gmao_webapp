namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tache")]
    public partial class Tache
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tache()
        {
            TacheEffectuee = new HashSet<TacheEffectuee>();
        }

        public long TacheId { get; set; }

        public long TypeTacheId { get; set; }

        public long EmployeId { get; set; }

        public long UtilisateurId { get; set; }

        [Required]
        public string LibelleTache { get; set; }

        public virtual Employe Employe { get; set; }

        public virtual TypeTache TypeTache { get; set; }

        public virtual Utilisateur Utilisateur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TacheEffectuee> TacheEffectuee { get; set; }
    }
}
