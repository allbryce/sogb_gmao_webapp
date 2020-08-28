namespace Sinba.BusinessModel.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Resources.Resources.Entity;
    using Resources;
    using System.Linq;
    

    [Table("TypeContact")]
    public partial class TypeContact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeContact()
        {
            Intervenants = new HashSet<Intervenant>();
            TypeContactActivite = new HashSet<TypeContactActivite>();
        }

        [StringLength(10)]
        [Display(Name = ResourceNames.Entity.Code, ResourceType = typeof(EntityColumnResource))]
        public string TypeContactID { get; set; }

        [StringLength(50)]
        [Display(Name = ResourceNames.Entity.Libelle, ResourceType = typeof(EntityColumnResource))]
        public string LibelleTypeContact { get; set; }

        [Display(Name = ResourceNames.Entity.NbOccurencePlantation, ResourceType = typeof(EntityColumnResource))]
        public int? NbOccurencePlantation { get; set; }

        [Display(Name = ResourceNames.Entity.NbOccurenceContact, ResourceType = typeof(EntityColumnResource))]
        public int? NbOccurenceContact { get; set; }

        [Display(Name = ResourceNames.Entity.SaisieAutorise, ResourceType = typeof(EntityColumnResource))]
        public bool? SaisieAutorise { get; set; }
        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Intervenant> Intervenants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypeContactActivite> TypeContactActivite { get; set; }

        public bool IsUsed
        {
            get
            {
                return Intervenants.Count > 0 || TypeContactActivite.Count > 0;
            }
        }

        #region Token
        [NotMapped]
        private List<string> typeContactsToken;

        [NotMapped]
       // [Display(Name = ResourceNames.Entity.Actions, ResourceType = typeof(EntityColumnResource))]
        public List<string> TypeContactsToken
        {
            get
            {
                if (TypeContactActivite.Any())
                {
                    typeContactsToken = TypeContactActivite.Select(fa => fa.ActiviteID).ToList();
                }
                return typeContactsToken;
            }
            set
            {
                typeContactsToken = value;
                if (typeContactsToken.Count > 0)
                {
                    TypeContactActivite = new List<TypeContactActivite>();
                    foreach (string idActivite in typeContactsToken)
                    {
                        TypeContactActivite.Add(new TypeContactActivite()
                        {
                           TypeContactID = this.TypeContactID,
                          ActiviteID= idActivite
                        });
                    }
                }
            }
        }

        //public List<string> FonctionActionsToken
        //{
        //    get
        //    {
        //        if (FonctionActions.Any())
        //        {
        //            fonctionActionsToken = FonctionActions.Select(fa => fa.CodeAction).ToList();
        //        }
        //        return fonctionActionsToken;
        //    }
        //    set
        //    {
        //        fonctionActionsToken = value;
        //        if (fonctionActionsToken.Count > 0)
        //        {
        //            FonctionActions = new List<FonctionAction>();
        //            foreach (string codeAction in fonctionActionsToken)
        //            {
        //                FonctionActions.Add(new FonctionAction()
        //                {
        //                    CodeFonction = this.Code,
        //                    CodeAction = codeAction
        //                });
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}
