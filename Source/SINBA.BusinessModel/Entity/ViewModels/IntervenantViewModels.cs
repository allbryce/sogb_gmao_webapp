


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

    public class IntervenantViewModels
    {


        public IntervenantViewModels()
        {
            ListContat = new HashSet<Contact>();
            ListPlantations = new HashSet<Plantation>();
            Intervenants = new HashSet<Intervenant>();
        }

        public long IntervenantID { get; set; }

        [Display(Name = ResourceNames.Entity.PlantationID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(7)]
        public string PlantationID { get; set; }

        
        [Display(Name = ResourceNames.Entity.CultureID, ResourceType = typeof(EntityColumnResource))]
      //  [Required(ErrorMessageResourceName = ResourceNames.Error.ErrorRequiredField, ErrorMessageResourceType = typeof(EntityCommonResource))]
        [StringLength(10)]
        public string CultureID { get; set; }


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
                
        [Display(Name = ResourceNames.Entity.Noms, ResourceType = typeof(EntityColumnResource))]
        [StringLength(200)]
        public string Noms { get; set; }


        [Display(Name = ResourceNames.Entity.ActiviteID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(10)]
        public string ActiviteID { get; set; }
        
        [Display(Name = ResourceNames.Entity.Actif, ResourceType = typeof(EntityColumnResource))]
        public bool? Actif { get; set; }

        //Groupe Contact

        [Display(Name = ResourceNames.Entity.LibelleActivite, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LibelleActivite { get; set; }

        [Display(Name = ResourceNames.Entity.LibelleTypeContact, ResourceType = typeof(EntityColumnResource))]
        [StringLength(50)]
        public string LibelleTypeContact { get; set; }

        [Display(Name = ResourceNames.Entity.DateFin, ResourceType = typeof(EntityColumnResource))]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateFin { get; set; }

        public string IntervenantString { get; set; }

        // Plantation Contact

        [Display(Name = ResourceNames.Entity.TypeContactID, ResourceType = typeof(EntityColumnResource))]
        [StringLength(10)]
        public string TypeContactID { get; set; }

       // [Display(Name = ResourceNames.Entity.DateFin, ResourceType = typeof(EntityColumnResource))]
        public int? NbOccurencePlantation { get; set; }

       // [Display(Name = ResourceNames.Entity.DateFin, ResourceType = typeof(EntityColumnResource))]
        public int? NbOccurenceContact { get; set; }

       // [Display(Name = ResourceNames.Entity.DateFin, ResourceType = typeof(EntityColumnResource))]
        public bool? SaisieAutorise { get; set; }

        [Display(Name = ResourceNames.Entity.ModeSaisie, ResourceType = typeof(EntityColumnResource))]
        public bool ModeSaisie { get; set; }

        [Display(Name = ResourceNames.Entity.DateAdhesion, ResourceType = typeof(EntityColumnResource))]
        public DateTime? DateAdhesion { get; set; }


        [Display(Name = ResourceNames.Entity.DateSortie, ResourceType = typeof(EntityColumnResource))]
        public DateTime? DateSortie { get; set; } 

        public bool AddMode { get; set; }
     
        public virtual ICollection<Intervenant> Intervenants { get; set; }

        private string[] _ContactArray = new string[] {};
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


        private string[] _PlantationArray = new string[] {};
        public virtual ICollection<Plantation> ListPlantations { get; set; }

        [Display(Name = ResourceNames.Entity.Plantations, ResourceType = typeof(EntityColumnResource))]
        public string[] Plantations
        {
            get
            {
                if (ListPlantations.Any())
                {
                    _PlantationArray = ListPlantations.Select(p => p.PlantationID).ToArray();
                }
                return _PlantationArray;
            }
            set { _PlantationArray = value; }
        }
    }


    public class ContactGridLookupViewModel
    {
        public List<ContactModalViewModels> Liste { get; set; }
        public string[] Values { get; set; }
    }

    public class PlantationGridLookupViewModel
    {
       // public List<PlantationViewModel> Liste { get; set; }
        public List<Plantation> Liste { get; set; }
        public string[] Values { get; set; }
    }


}





