


namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Resources.Resources.Entity;
    using Resources;
    public class PlanteurViewModel
    {

        private ICollection<ProjetPlanteurViewModel> _ProjetPlanteur = new List<ProjetPlanteurViewModel>();

        public PlanteurViewModel()
        {
            Plantation = new HashSet<Plantation>();
            this.ListProjetPlanteur = _ProjetPlanteur;
        }

        [Display(Name = ResourceNames.Entity.PlanteurID, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(15)]
        public string PlanteurID { get; set; }

        [Display(Name = ResourceNames.Entity.SexePlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(1)]
        public string SexePlanteur { get; set; }


        [Display(Name = ResourceNames.Entity.NomPlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NomPlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.PrenomPlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string PrenomPlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.VillagePlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string VillagePlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.PieceIdentiteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(3)]
        public string PieceIdentiteID { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroPiecePlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NumeroPiecePlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.DateDelivrancePiece, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateDelivrancePiece { get; set; }

        [Display(Name = ResourceNames.Entity.LieuDelivrancePiece, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LieuDelivrancePiece { get; set; }

        [Display(Name = ResourceNames.Entity.DateNaissance, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateNaissance { get; set; }

        [Display(Name = ResourceNames.Entity.LieuNaissance, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LieuNaissance { get; set; }

        [Display(Name = ResourceNames.Entity.Adresse, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string Adresse { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroTelephone, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string NumeroTelephone { get; set; }

        [Display(Name = ResourceNames.Entity.IDTbPlanteursID, ResourceType = typeof(EntityColumnResource))]
        public int? IDTbPlanteursID { get; set; }

        [Display(Name = ResourceNames.Entity.NumIdentification, ResourceType = typeof(EntityColumnResource))]
        public long? NumIdentification { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroTelephone1, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string NumeroTelephone1 { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroTelephone2, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string NumeroTelephone2 { get; set; }

    
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Display(Name = ResourceNames.Entity.AdresseMail1, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string AdresseMail1 { get; set; }

        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Display(Name = ResourceNames.Entity.AdresseMail2, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string AdresseMail2 { get; set; }

        [Display(Name = ResourceNames.Entity.ActeurID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string ActeurID { get; set; }

        [Display(Name = ResourceNames.Entity.TypeActeurID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(10)]
        public string TypeActeurID { get; set; }


        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }


        public virtual PieceIdentite PieceIdentite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantation> Plantation { get; set; }

        public virtual ICollection<ProjetPlanteurViewModel> ListProjetPlanteur { get; set; }

        public bool AddMode { get; set; }
        public bool IsUsed
        {
            get
            {
                return Plantation.Count > 0;
            }
        }

    }


    public  class ProjetPlanteurViewModel
    {
        private ICollection<SuperficieViewModel> _Superficie = new List<SuperficieViewModel>();

        public ProjetPlanteurViewModel()
        {
            CulturePratiquee = new HashSet<CulturePratiquee>();
            DonneeGeographique = new HashSet<DonneeGeographique>();
            PlantationContact = new HashSet<PlantationContact>();
            Superficie = new HashSet<Superficie>();
            this.ListSuperficie = _Superficie;
        }



        [Display(Name = ResourceNames.Entity.ProjetPlanteurID, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(15)]
        public string ProjetPlanteurID { get; set; }

        [Required]
        [Display(Name = ResourceNames.Entity.NumeroPlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string NumeroPlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.GroupementID, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(6)]
        public string GroupementID { get; set; }

        [Display(Name = ResourceNames.Entity.CompteGeneral, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string CompteGeneral { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroCompte, ResourceType = typeof(EntityColumnResource))]
        [StringLength(12)]
        public string NumeroCompte { get; set; }

        [Display(Name = ResourceNames.Entity.ModePaiementID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(1)]
        public string ModePaiementID { get; set; }

        [Display(Name = ResourceNames.Entity.BanqueID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(10)]
        public string BanqueID { get; set; }

        [Display(Name = ResourceNames.Entity.ClefPlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(2)]
        public string ClefPlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.RibPlanteur, ResourceType = typeof(EntityColumnResource))]
        [StringLength(2)]
        public string RibPlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.KilometrePlanteur, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? KilometrePlanteur { get; set; }

        [Display(Name = ResourceNames.Entity.CodeSecuriteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(5)]
        public string CodeSecuriteID { get; set; }

        [Display(Name = ResourceNames.Entity.CompteContribuable, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string CompteContribuable { get; set; }

        [Display(Name = ResourceNames.Entity.LocaliteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string LocaliteID { get; set; }

        [Display(Name = ResourceNames.Entity.PayeBordChamps, ResourceType = typeof(EntityColumnResource))]
        public bool? PayeBordChamps { get; set; }

        [Display(Name = ResourceNames.Entity.EstDelegue, ResourceType = typeof(EntityColumnResource))]
        public bool? EstDelegue { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroDelegue, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string NumeroDelegue { get; set; }

        [Display(Name = ResourceNames.Entity.PrixTpKg, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrixTpKg { get; set; }

        [Display(Name = ResourceNames.Entity.DistanceEstimee, ResourceType = typeof(EntityColumnResource))]
        public int? DistanceEstimee { get; set; }

        [Display(Name = ResourceNames.Entity.KmFacture, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? KmFacture { get; set; }

        [Display(Name = ResourceNames.Entity.PrimeExceptionnelle, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrimeExceptionnelle { get; set; }

        [Display(Name = ResourceNames.Entity.Analytique, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string Analytique { get; set; }

        [Display(Name = ResourceNames.Entity.PrixTpKgTpt, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrixTpKgTpt { get; set; }

        [Display(Name = ResourceNames.Entity.ReversPreCollecte, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? ReversPreCollecte { get; set; }

        [Display(Name = ResourceNames.Entity.RetenuePreCollecte, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? RetenuePreCollecte { get; set; }

        [Display(Name = ResourceNames.Entity.ReversActionnaire, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? ReversActionnaire { get; set; }

        [Display(Name = ResourceNames.Entity.PlanfondPoids, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "numeric")]
        public decimal? PlanfondPoids { get; set; }

        [Display(Name = ResourceNames.Entity.PrimePlanfondPoids, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrimePlanfondPoids { get; set; }

        [Display(Name = ResourceNames.Entity.RetSubCollecte, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? RetSubCollecte { get; set; }

        [Display(Name = ResourceNames.Entity.RetSubPreCollecte, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? RetSubPreCollecte { get; set; }

        [Display(Name = ResourceNames.Entity.RevSubCollecte, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? RevSubCollecte { get; set; }

        [Display(Name = ResourceNames.Entity.RevSubPreCollecte, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? RevSubPreCollecte { get; set; }

        [Display(Name = ResourceNames.Entity.PlanfondPoids2, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "numeric")]
        public decimal? PlanfondPoids2 { get; set; }

        [Display(Name = ResourceNames.Entity.PrimePlanfondPoids2, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrimePlanfondPoids2 { get; set; }

        [Display(Name = ResourceNames.Entity.PlanfondPoids3, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "numeric")]
        public decimal? PlanfondPoids3 { get; set; }

        [Display(Name = ResourceNames.Entity.PrimePlanfondPoids3, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrimePlanfondPoids3 { get; set; }

        [Display(Name = ResourceNames.Entity.PlanfondPoids4, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "numeric")]
        public decimal? PlanfondPoids4 { get; set; }

        [Display(Name = ResourceNames.Entity.PrimePlanfondPoids4, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "money")]
        public decimal? PrimePlanfondPoids4 { get; set; }

        [Display(Name = ResourceNames.Entity.Bloque, ResourceType = typeof(EntityColumnResource))]
        [StringLength(1)]
        public string Bloque { get; set; }

        [Display(Name = ResourceNames.Entity.ZonePlanteurID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string ZonePlanteurID { get; set; }

        [Display(Name = ResourceNames.Entity.P2ASaisir, ResourceType = typeof(EntityColumnResource))]
        public bool? P2ASaisir { get; set; }

        [Display(Name = ResourceNames.Entity.NoApromac, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NoApromac { get; set; }

        //[StringLength(15)]
        //public string ProjetPlanteurID { get; set; }

        //[Required]
        //[StringLength(15)]
        //public string NumeroPlanteur { get; set; }

        [StringLength(100)]
        public string NomProjetPlanteur { get; set; }


        //[Required]
        //[StringLength(6)]
        //public string GroupementID { get; set; }

        //[StringLength(15)]
        //public string CompteGeneral { get; set; }

        //[StringLength(12)]
        //public string NumeroCompte { get; set; }

        //[StringLength(1)]
        //public string ModePaiementID { get; set; }

        //[StringLength(10)]
        //public string BanqueID { get; set; }

        //[StringLength(2)]
        //public string ClefPlanteur { get; set; }

        //[StringLength(2)]
        //public string RibPlanteur { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? KilometrePlanteur { get; set; }

        //[StringLength(5)]
        //public string CodeSecuriteID { get; set; }

        //[StringLength(25)]
        //public string CompteContribuable { get; set; }

        //[StringLength(15)]
        //public string LocaliteID { get; set; }

        //public bool? PayeBordChamps { get; set; }

        //public bool? EstDelegue { get; set; }

        //[StringLength(15)]
        //public string NumeroDelegue { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrixTpKg { get; set; }

        //public int? DistanceEstimee { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? KmFacture { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrimeExceptionnelle { get; set; }

        //[StringLength(15)]
        //public string Analytique { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrixTpKgTpt { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? ReversPreCollecte { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? RetenuePreCollecte { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? ReversActionnaire { get; set; }

        //[Column(TypeName = "numeric")]
        //public decimal? PlanfondPoids { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrimePlanfondPoids { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? RetSubCollecte { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? RetSubPreCollecte { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? RevSubCollecte { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? RevSubPreCollecte { get; set; }

        //[Column(TypeName = "numeric")]
        //public decimal? PlanfondPoids2 { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrimePlanfondPoids2 { get; set; }

        //[Column(TypeName = "numeric")]
        //public decimal? PlanfondPoids3 { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrimePlanfondPoids3 { get; set; }

        //[Column(TypeName = "numeric")]
        //public decimal? PlanfondPoids4 { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? PrimePlanfondPoids4 { get; set; }

        //[StringLength(1)]
        //public string Bloque { get; set; }

        //[StringLength(15)]
        //public string ZonePlanteurID { get; set; }

        //public bool? P2ASaisir { get; set; }

        //[StringLength(50)]
        //public string NoApromac { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }
        public bool AddMode { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }


       
        [StringLength(15)]
        public string CloneID { get; set; }
        
        public string SuperficieString { get; set; }
        public string CulturePratiqueeString { get; set; }
       public string DonneesGeographiqueString { get; set; }

        public virtual Banque Banque { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CulturePratiquee> CulturePratiquee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonneeGeographique> DonneeGeographique { get; set; }

        public virtual Groupement Groupement { get; set; }

        public virtual ModePayement ModePayement { get; set; }

        public virtual Contact Contact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlantationContact> PlantationContact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Superficie> Superficie { get; set; }

        public virtual ICollection<SuperficieViewModel> ListSuperficie { get; set; }


        public bool IsUsed
        {
            get
            {
                return Superficie.Count > 0 || DonneeGeographique.Count > 0 || Superficie.Count > 0 ;
            }
        }
    }

    public  class SuperficieViewModel
    {
        public SuperficieViewModel()
        {

        }

        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string ProjetPlanteurID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string CloneID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short AnneeCultureID { get; set; }

        [Column("Superficie")]
        public decimal? Superficie1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DebutExploitation { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }


        public virtual Clone Clone { get; set; }

        public virtual Plantation Plantation { get; set; }
    }

}



