namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utilisateur")]
    public partial class Utilisateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilisateur()
        {
            Absence = new HashSet<Absence>();
            Suppleer = new HashSet<Suppleer>();
            Suppleer1 = new HashSet<Suppleer>();
            Tache = new HashSet<Tache>();
            SignataireParNiveau = new HashSet<SignataireParNiveau>();
            GroupeUtilisateur = new HashSet<GroupeUtilisateur>();
            Confidentialite = new HashSet<Confidentialite>();
            Domaine = new HashSet<Domaine>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UtilisateurId { get; set; }

        public long? EmployeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public string NomComplet { get; set; }

        public int GroupeUtilisateurId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Absence> Absence { get; set; }

        public virtual Employe Employe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Suppleer> Suppleer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Suppleer> Suppleer1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tache> Tache { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignataireParNiveau> SignataireParNiveau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupeUtilisateur> GroupeUtilisateur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Confidentialite> Confidentialite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Domaine> Domaine { get; set; }
    }
}
