namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PouvoirSigner")]
    public partial class PouvoirSigner
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NiveauSignataireId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NumeroEtape { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SystemePrivilegeId { get; set; }

        public virtual Etape Etape { get; set; }

        public virtual NiveauSignataire NiveauSignataire { get; set; }

        public virtual SystemePrivilege SystemePrivilege { get; set; }
    }
}
