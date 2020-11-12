namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SignataireParNiveau")]
    public partial class SignataireParNiveau
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UtilisateurId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long niveauSignataireId { get; set; }

        public bool? Resigner { get; set; }

        public virtual NiveauSignataire NiveauSignataire { get; set; }

        public virtual Utilisateur Utilisateur { get; set; }
    }
}
