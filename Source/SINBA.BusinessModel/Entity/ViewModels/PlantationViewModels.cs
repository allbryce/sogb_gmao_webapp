


namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Resources.Resources.Entity;
    using Resources;
    using System.Linq;

    public class PlantationViewModel
    {
        public PlantationViewModel()
        {
            ListContat = new HashSet<Contact>();
            Intervenants = new HashSet<Intervenant>();
            DonneeGeographiques = new HashSet<DonneeGeographique>();
            Superficies = new HashSet<Superficie>();
            ListGroupement = new HashSet<Groupement>();
            ListLocalite = new HashSet<Localite>();
            ListCulture = new HashSet<Culture>();
            ListTypeContact = new HashSet<TypeContact>();
            IntervenantPlantaionViewModel = new HashSet<IntervenantPlantaionViewModels>();
            ListSuperficie = new HashSet<CessionViewModels>();
            ListIntervenant = new HashSet<IntervenantViewModels>();
        }

        [Display(Name = ResourceNames.Entity.PlantationID, ResourceType = typeof(EntityColumnResource))]
      //  [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(7)]
        public string PlantationID { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroApromac, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NumeroApromac { get; set; }

        [Display(Name = ResourceNames.Entity.CultureID, ResourceType = typeof(EntityColumnResource))]
      //  [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(10)]
        public string CultureID { get; set; }

        [Display(Name = ResourceNames.Entity.DistanceUsine, ResourceType = typeof(EntityColumnResource))]
        public double? DistanceUsine { get; set; }

        [Display(Name = ResourceNames.Entity.GroupementID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(6)]
        public string GroupementID { get; set; }

        [Display(Name = ResourceNames.Entity.DateAttestationPropriete, ResourceType = typeof(EntityColumnResource))]
        public DateTime? DateAttestationPropriete { get; set; }

        [Display(Name = ResourceNames.Entity.LocaliteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string LocaliteID { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        ///Les Contacts
        [Display(Name = ResourceNames.Entity.ContactID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string ContactID { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroTicketIdentification, ResourceType = typeof(EntityColumnResource))]
        [StringLength(20)]
        public string NumeroTicketIdentification { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroMatriculeNational, ResourceType = typeof(EntityColumnResource))]
        [StringLength(20)]
        public string NumeroMatriculeNational { get; set; }

        [Display(Name = ResourceNames.Entity.Sexe, ResourceType = typeof(EntityColumnResource))]
        [StringLength(1)]
        public string Sexe { get; set; }

        [Display(Name = ResourceNames.Entity.Noms, ResourceType = typeof(EntityColumnResource))]
        [StringLength(200)]
        public string Noms { get; set; }

        [Display(Name = ResourceNames.Entity.Nom, ResourceType = typeof(EntityColumnResource))]
        [StringLength(200)]
        public string Nom { get; set; }

        [Display(Name = ResourceNames.Entity.Prenom, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string Prenom { get; set; }

        //[Display(Name = ResourceNames.Entity.Prenom, ResourceType = typeof(EntityColumnResource))]
        [StringLength(30)]
        public string Lot { get; set; }

        [Display(Name = ResourceNames.Entity.CompteContribuable, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string CompteContribuable { get; set; }

        [Display(Name = ResourceNames.Entity.ActiviteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(10)]
        public string ActiviteID { get; set; }

        [Display(Name = ResourceNames.Entity.PieceIdentiteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(3)]
        public string PieceIdentiteID { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroPiece, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NumeroPiece { get; set; }

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

        [Display(Name = ResourceNames.Entity.NumeroTelephones, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NumeroTelephones { get; set; }


        [Display(Name = ResourceNames.Entity.NumeroTelephone1, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string NumeroTelephone1 { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroTelephone2, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string NumeroTelephone2 { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroTelephone3, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string NumeroTelephone3 { get; set; }

        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Display(Name = ResourceNames.Entity.AdresseMails, ResourceType = typeof(EntityColumnResource))]
        [StringLength(300)]
        public string AdresseMails { get; set; }

        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Display(Name = ResourceNames.Entity.AdresseMail1, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string AdresseMail1 { get; set; }

        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Display(Name = ResourceNames.Entity.AdresseMail2, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string AdresseMail2 { get; set; }

        [Display(Name = ResourceNames.Entity.Actif, ResourceType = typeof(EntityColumnResource))]
        public bool Actif { get; set; }

        [Display(Name = ResourceNames.Entity.Actif, ResourceType = typeof(EntityColumnResource))]
        public string Activer { get; set; }

        //Groupe Contact

        [Display(Name = ResourceNames.Entity.LibelleActivite, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LibelleActivite { get; set; }

        [Display(Name = ResourceNames.Entity.PersonnePhysique, ResourceType = typeof(EntityColumnResource))]
        public bool PersonnePhysique { get; set; }


        //Donnees géographique
        [Display(Name = ResourceNames.Entity.DateGeolocalisation, ResourceType = typeof(EntityColumnResource))]
        public DateTime DateGeolocalisation { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroGeolocalisation, ResourceType = typeof(EntityColumnResource))]
        public long? NumeroGeolocalisation { get; set; }

        //[Display(Name = ResourceNames.Entity.CoordonneesGPS, ResourceType = typeof(EntityColumnResource))]
        //public DbGeography CoordonneesGPS { get; set; }

        [Display(Name = ResourceNames.Entity.Latitude, ResourceType = typeof(EntityColumnResource))]
        public string Latitude { get; set; }

        [Display(Name = ResourceNames.Entity.Longitude, ResourceType = typeof(EntityColumnResource))]
        public string Longitude { get; set; }

        [Display(Name = ResourceNames.Entity.Altitude, ResourceType = typeof(EntityColumnResource))]
        public double? Altitude { get; set; }

        [Display(Name = ResourceNames.Entity.DonneesGeolocalisation, ResourceType = typeof(EntityColumnResource))]
        public string DonneesGeolocalisation { get; set; }

        [Display(Name = ResourceNames.Entity.DateAdhesion, ResourceType = typeof(EntityColumnResource))]
        public DateTime DateAdhesion { get; set; }


        [StringLength(8)]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [Display(Name = ResourceNames.Entity.MatriculeOperateur, ResourceType = typeof(EntityColumnResource))]
        public string MatriculeOperateur { get; set; }

        [Display(Name = ResourceNames.Entity.SuperficieGeolocalisee, ResourceType = typeof(EntityColumnResource))]
        public double? SuperficieGeolocalisee { get; set; }

        [Display(Name = ResourceNames.Entity.DateFin, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateFin { get; set; }

        public string SuperficieString { get; set; }
        public string IntervenantString { get; set; }

        // Plantation Contact

        [StringLength(10)]
        public string TypeContactID { get; set; }

        [StringLength(50)]
        //[Display(Name = ResourceNames.Entity.Libelle, ResourceType = typeof(EntityColumnResource))]
        public string LibelleTypeContact { get; set; }


        [Display(Name = ResourceNames.Entity.MotifEvolutionID, ResourceType = typeof(EntityColumnResource))]
        public byte? MotifEvolutionID { get; set; }


        [Display(Name = ResourceNames.Entity.LibelleMotifEvolution, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LibelleMotifEvolution { get; set; }

        // Fournisseur

        [StringLength(7)]
        public string FournisseurID { get; set; }



        [Display(Name = ResourceNames.Entity.ProjetID, ResourceType = typeof(EntityColumnResource))]

        [StringLength(15)]
        public string ProjetID { get; set; }

        [Display(Name = ResourceNames.Entity.LibelleProjet, ResourceType = typeof(EntityColumnResource))]

        [StringLength(30)]
        public string LibelleProjet { get; set; }

        [Display(Name = ResourceNames.Entity.OrganismeID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(10)]
        public string OrganismeID { get; set; }

        [Display(Name = ResourceNames.Entity.LibelleOrganisme, ResourceType = typeof(EntityColumnResource))]
        [StringLength(25)]
        public string LibelleOrganisme { get; set; }


        [Display(Name = ResourceNames.Entity.GroupeOrganismeID, ResourceType = typeof(EntityColumnResource))]

        [StringLength(15)]
        public string GroupeOrganismeID { get; set; }

        [Display(Name = ResourceNames.Entity.LibelleGroupeOrganisme, ResourceType = typeof(EntityColumnResource))]

        [StringLength(50)]
        public string LibelleGroupeOrganisme { get; set; }




        public bool FournisseurPlantation { get; set; }
        public bool AddMode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonneeGeographique> DonneeGeographiques { get; set; }
        public virtual Groupement Groupement { get; set; }
        public virtual Localite Localite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReferenceBancaire> ReferenceBancaires { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Superficie> Superficies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Intervenant> Intervenants { get; set; }
        public virtual ICollection<PlantationViewModel> ListPlantation { get; set; }
        public virtual ICollection<CessionViewModels> ListSuperficie { get; set; }

        public virtual ICollection<IntervenantViewModels> ListIntervenant { get; set; }


        public virtual ICollection<IntervenantPlantaionViewModels> IntervenantPlantaionViewModel { get; set; }


        private string[] _ContactArray = new string[] { };
        public virtual ICollection<Contact> ListContat { get; set; }

        [Display(Name = ResourceNames.Entity.Contacts, ResourceType = typeof(EntityColumnResource))]
        public string[] Contacts
        {
            get
            {
                if (ListContat.Any())
                {
                    _ContactArray = ListContat.Select(p => p.ContactID).ToArray();
                }
                return _ContactArray;
            }
            set { _ContactArray = value; }
        }


        private string[] _GroupementArray = new string[] { };
        public virtual ICollection<Groupement> ListGroupement { get; set; }

        [Display(Name = ResourceNames.Entity.Groupements, ResourceType = typeof(EntityColumnResource))]
        public string[] Groupements
        {
            get
            {
                if (ListGroupement.Any())
                {
                    _GroupementArray = ListGroupement.Select(p => p.GroupementID).ToArray();
                }
                return _GroupementArray;
            }
            set { _GroupementArray = value; }
        }

        private string[] _LocaliteArray = new string[] { };
        public virtual ICollection<Localite> ListLocalite { get; set; }

         [Display(Name = ResourceNames.Entity.Localites, ResourceType = typeof(EntityColumnResource))]
        public string[] Localites
        {
            get
            {
                if (ListLocalite.Any())
                {
                    _LocaliteArray = ListLocalite.Select(p => p.LocaliteID).ToArray();
                }
                return _LocaliteArray;
            }
            set { _LocaliteArray = value; }
        }

        private string[] _CultureArray = new string[] { };
        public virtual ICollection<Culture> ListCulture { get; set; }

        [Display(Name = ResourceNames.Entity.CultureID, ResourceType = typeof(EntityColumnResource))]
        public string[] Cultures
        {
            get
            {
                if (ListCulture.Any())
                {
                    _CultureArray = ListCulture.Select(p => p.CultureID).ToArray();
                }
                return _CultureArray;
            }
            set { _CultureArray = value; }
        }



        private string[] _TypeContactArray = new string[] { };
        public virtual ICollection<TypeContact> ListTypeContact { get; set; }

          [Display(Name = ResourceNames.Entity.TypeContactID, ResourceType = typeof(EntityColumnResource))]
        public string[] TypeContacts
        {
            get
            {
                if (ListTypeContact.Any())
                {
                    _TypeContactArray = ListTypeContact.Select(p => p.TypeContactID).ToArray();
                }
                return _TypeContactArray;
            }
            set { _TypeContactArray = value; }
        }


        public bool IsUsed
        {
            get
            {
                return Superficies.Count > 0 || Intervenants.Count > 0 || DonneeGeographiques.Count > 0;
            }
        }

       }


    public class GroupementGridLookupViewModel
    {
        public List<Groupement> Liste { get; set; }
        public string[] Values { get; set; }
    }

    public class LocaliteGridLookupViewModel
    {
        public List<Localite> Liste { get; set; }
        public string[] Values { get; set; }
    }


    public class TypeContactGridLookupViewModel
    {
        public List<TypeContact> Liste { get; set; }
        public string[] Values { get; set; }
    }

    public class CulturesGridLookupViewModel
    {
        public List<Culture> Liste { get; set; }
        public string[] Values { get; set; }
    }

    public class IntervenantPlantaionViewModels
    {

        public IntervenantPlantaionViewModels()
        {
            IntervenantPlantaionViewModel = new HashSet<IntervenantPlantaionViewModels>();
        }

        //public long IntervenantID { get; set; }

        [Display(Name = ResourceNames.Entity.PlantationID, ResourceType = typeof(EntityColumnResource))]
       [StringLength(7)]
        [Required]
        public string PlantationID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = ResourceNames.Entity.TypeContactID, ResourceType = typeof(EntityColumnResource))]
        public string TypeContactID { get; set; }

        //[Display(Name = ResourceNames.Entity.Libelle, ResourceType = typeof(EntityColumnResource))]
        public string LibelleTypeContact { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = ResourceNames.Entity.ContactID, ResourceType = typeof(EntityColumnResource))]
        public string ContactID { get; set; }

        [Display(Name = ResourceNames.Entity.Noms, ResourceType = typeof(EntityColumnResource))]
        [StringLength(200)]
        public string Noms { get; set; }

        public DateTime? DateSortie { get; set; }
        

        [Display(Name = ResourceNames.Entity.DateAdhesion, ResourceType = typeof(EntityColumnResource))]
        public DateTime DateAdhesion { get; set; }

               
        public virtual ICollection<IntervenantPlantaionViewModels> IntervenantPlantaionViewModel { get; set; }

    }

}




