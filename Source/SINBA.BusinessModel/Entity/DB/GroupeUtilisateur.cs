namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupeUtilisateur")]
    public partial class GroupeUtilisateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupeUtilisateur()
        {
            Utilisateur = new HashSet<Utilisateur>();
        }

        [Key]
        public long GoupeUtilisateurId { get; set; }

        [Required]
        [StringLength(50)]
        public string LibelleGroupeUtilisateur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
