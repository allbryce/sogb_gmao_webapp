namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PossederCaracteristiques
    {
        [Key]
        [Column(Order = 0)]
        
        public int UniteId { get; set; }

        [Key]
        [Column(Order = 1)]
              
        public long CaracteristiqueComposantId { get; set; }

        public double Valeur { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime DateInsertion { get; set; }


        [Key]
        [Column(Order = 3)]
        public long ComposantId { get; set; }

        [Key]
        [Column(Order = 4)]
        public long MaterielId { get; set; }

        public virtual CaracteristiqueComposant CaracteristiqueComposant { get; set; }

        public virtual Composant Composant { get; set; }

        public virtual ComposerMateriel ComposerMateriel { get; set; }

        public virtual Materiel Materiel { get; set; }

        public virtual Unite Unite { get; set; }

    }
}
