


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

    public class CessionViewModels
    {


        public CessionViewModels()
        {
            ListContat = new HashSet<Contact>();
            DonneeGeographiques = new HashSet<DonneeGeographique>();
           DonneeGeographiquesViewModel = new HashSet<DonneeGeographiqueViewModels>();
            Superficies = new HashSet<Superficie>();
        }
        

        [Display(Name = ResourceNames.Entity.PlantationID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(7)]
        public string PlantationID { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroApromac, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string NumeroApromac { get; set; }

        [Display(Name = ResourceNames.Entity.CultureID, ResourceType = typeof(EntityColumnResource))]
       // [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
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

        [Display(Name = ResourceNames.Entity.MatriculeOperateur, ResourceType = typeof(EntityColumnResource))]

        [StringLength(8)]
        public string MatriculeOperateur { get; set; }

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
        
       
        [Display(Name = ResourceNames.Entity.DateFin, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateFin { get; set; }

       [StringLength(10)]
        public string TypeContactID { get; set; }

        public string DonneesGeographiqueString { get; set; }

        // Fournisseur

        [StringLength(7)]
        public string FournisseurID { get; set; }
       
        [StringLength(7)]
        public string NouvellePlantationID { get; set; }

        [Display(Name = ResourceNames.Entity.DateEvolution, ResourceType = typeof(EntityColumnResource))]
        public DateTime DateEvolution { get; set; }

        [Display(Name = ResourceNames.Entity.CloneID, ResourceType = typeof(EntityColumnResource))]
        public string CloneID { get; set; }

        [Display(Name = ResourceNames.Entity.AnneeCulture, ResourceType = typeof(EntityColumnResource))]
        public short? AnneeCulture { get; set; }

       
        [StringLength(7)]
        public string AnciennePlantationID { get; set; }

        [Display(Name = ResourceNames.Entity.MotifEvolutionID, ResourceType = typeof(EntityColumnResource))]
        public byte? MotifEvolutionID { get; set; }


        [Display(Name = ResourceNames.Entity.LibelleMotifEvolution, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LibelleMotifEvolution { get; set; }


        [Display(Name = ResourceNames.Entity.Superficie, ResourceType = typeof(EntityColumnResource))]
        public double? Superficie { get; set; }

        public DateTime? DateMiseExploitation { get; set; }

        public bool AddMode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonneeGeographique> DonneeGeographiques { get; set; }

       public virtual ICollection<DonneeGeographiqueViewModels> DonneeGeographiquesViewModel { get; set; }
        public virtual Groupement Groupement { get; set; }
        public virtual Localite Localite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReferenceBancaire> ReferenceBancaires { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Superficie> Superficies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Intervenant> Intervenants { get; set; }
        public virtual ICollection<CessionViewModels> ListPlantation { get; set; }


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

    }



    public class DonneeGeographiqueViewModels
    {


        public DonneeGeographiqueViewModels()
        {

            //ListCessionViewModel = new HashSet<CessionViewModels>();
            DonneeGeographiquesViewModel = new HashSet<DonneeGeographiqueViewModels>();

        }


        [Display(Name = ResourceNames.Entity.PlantationID, ResourceType = typeof(EntityColumnResource))]
         [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(7)]
        public string NouvellePlantationID { get; set; }

        [Display(Name = ResourceNames.Entity.PlantationID, ResourceType = typeof(EntityColumnResource))]
        [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(7)]
        public string AnciennePlantationID { get; set; }

        [Display(Name = ResourceNames.Entity.NumeroGeolocalisation, ResourceType = typeof(EntityColumnResource))]
        public long? NumeroGeolocalisation { get; set; }


        [Display(Name = ResourceNames.Entity.Latitude, ResourceType = typeof(EntityColumnResource))]
        public string Latitude { get; set; }

        [Display(Name = ResourceNames.Entity.Longitude, ResourceType = typeof(EntityColumnResource))]
        public string Longitude { get; set; }

        [Display(Name = ResourceNames.Entity.Altitude, ResourceType = typeof(EntityColumnResource))]
        public double? Altitude { get; set; }

        [Display(Name = ResourceNames.Entity.DonneesGeolocalisation, ResourceType = typeof(EntityColumnResource))]
        public string DonneesGeolocalisation { get; set; }

        [Display(Name = ResourceNames.Entity.Superficie, ResourceType = typeof(EntityColumnResource))]
        public double? Superficie { get; set; }

        ///Les Contacts
        [Display(Name = ResourceNames.Entity.ContactID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(15)]
        public string ContactID { get; set; }


        [Display(Name = ResourceNames.Entity.Noms, ResourceType = typeof(EntityColumnResource))]
        [StringLength(200)]
        public string Noms { get; set; }

        public DateTime? DateMiseExploitation { get; set; }

        public virtual ICollection<DonneeGeographiqueViewModels> DonneeGeographiquesViewModel { get; set; }

    //    public virtual ICollection<CessionViewModels> ListCessionViewModel { get; set; }

    }


}



