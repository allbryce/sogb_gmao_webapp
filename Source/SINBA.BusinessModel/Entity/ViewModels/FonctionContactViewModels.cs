using Newtonsoft.Json;
using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sinba.BusinessModel.Entity
{

    public class FonctionContactViewModels
    {
        private Contact _dirigeant = new Contact();
        public FonctionContactViewModels()
        {

        }

        [StringLength(15)]
        public string ContactID { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = ResourceNames.Entity.ContactID, ResourceType = typeof(EntityColumnResource))]
        public string AutreContactID { get; set; }

        [StringLength(200)]
        [Display(Name = ResourceNames.Entity.ContactID, ResourceType = typeof(EntityColumnResource))]
        public string DescriptionContact { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = ResourceNames.Entity.Fonction, ResourceType = typeof(EntityColumnResource))]
        public string FonctionID { get; set; }

        [StringLength(50)]
        [Display(Name = ResourceNames.Entity.Fonction, ResourceType = typeof(EntityColumnResource))]
        public string LibelleFonction { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Contact ContactDirigeant { get { return _dirigeant; }
            set
            {
                _dirigeant = value;
                if (_dirigeant != null)
                {
                    DescriptionContact = string.Format("{0} {1}", _dirigeant.Nom,_dirigeant.Prenom);
                }
            }
        }

        public virtual SysFonction SysFonction { get; set; }


    }

}
