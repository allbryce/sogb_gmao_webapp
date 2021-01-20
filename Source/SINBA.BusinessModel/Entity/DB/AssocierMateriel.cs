namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Sinba.BusinessModel.Entity.ViewModels;

    [Table("AssocierMateriel")]
    public partial class AssocierMateriel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime DateInstallation { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaterielAssocieId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateRetrait { get; set; }
        public virtual Materiel Materiel { get; set; }
        public virtual Materiel Materiel1 { get; set; }

        public AssociematerielViewModel ToViewModel()
        {
            var associemateriel = new AssociematerielViewModel(){
                MaterielId = this.MaterielId,
                MaterielAssocieId = this.MaterielAssocieId,
                LibelleMaterielAssocie = this.Materiel1?.LibelleMateriel,
                LibelleMateriel =this. Materiel?.LibelleMateriel,
                DateInstallation = this.DateInstallation,
                DateRetrait = this.DateRetrait
            };        
            return associemateriel;
        }
    }
}
