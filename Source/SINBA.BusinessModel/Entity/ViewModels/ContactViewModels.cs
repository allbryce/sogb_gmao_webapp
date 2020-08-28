using Dev.Tools.Common;
using Newtonsoft.Json;
using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sinba.BusinessModel.Entity
{

    public class ContactViewModels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactViewModels()
        {
            Contacts = new HashSet<Contact>();
        }

        [StringLength(15)]
        public string RechercheContactID { get; set; }
        [StringLength(100)]
        public string RechercheNomEtPrenom { get; set; }
        [StringLength(100)]
        public string RechercheLocaliteID { get; set; }
        [StringLength(100)]
        public string RechercheEmail { get; set; }
        [StringLength(100)]
        public string RechercheTelephone { get; set; }
        [StringLength(100)]
        public string RechercheAdresse { get; set; }

        public DateTime? RechercheDateNaissance { get; set; }
        [StringLength(50)]
        public string RechercheNumeroPiece { get; set; }
        [StringLength(20)]
        public string RechercheNumeroTicketIdentification { get; set; }

        [StringLength(20)]
        public string RechercheNumeroMatriculeNational { get; set; }

        [StringLength(1)]
        [Display(Name = ResourceNames.Entity.Genre, ResourceType = typeof(EntityColumnResource))]
        public string RechercheSexe { get; set; }

        [StringLength(25)]
        public string RechercheCompteContribuable { get; set; }

        public bool? PersonnePhysique { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }




    }

    public class ContactModalViewModels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactModalViewModels()
        {
            FonctionContacts = new HashSet<FonctionContactViewModels>();
            FonctionContact1s = new HashSet<FonctionContactViewModels>();
            Fournisseurs = new HashSet<Fournisseur>();
            ReferenceBancaires = new HashSet<ReferenceBancaire>();
        }

        [StringLength(15)]
        [Required]
        public string ContactID { get; set; }

        [StringLength(20)]
        public string NumeroTicketIdentification { get; set; }

        [StringLength(20)]
        public string NumeroMatriculeNational { get; set; }

        [StringLength(1)]
        [Required]
        [Display(Name = ResourceNames.Entity.Sexe, ResourceType = typeof(EntityColumnResource))]
        public string Sexe { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = ResourceNames.Entity.Nom, ResourceType = typeof(EntityColumnResource))]
        public string Nom { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = ResourceNames.Entity.Prenom, ResourceType = typeof(EntityColumnResource))]
        public string Prenom { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = ResourceNames.Entity.LocaliteID, ResourceType = typeof(EntityColumnResource))]
        public string LocaliteID { get; set; }

        [StringLength(25)]
        [Required]
        public string CompteContribuable { get; set; }

        [StringLength(10)]
        public string ActiviteID { get; set; }

        [StringLength(3)]
        [Required]
        public string PieceIdentiteID { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = ResourceNames.Entity.NumeroPiece, ResourceType = typeof(EntityColumnResource))]
        public string NumeroPiece { get; set; }

        [Required]
        [Display(Name = ResourceNames.Entity.DateDelivrancePiece, ResourceType = typeof(EntityColumnResource))]
        public DateTime? DateDelivrancePiece { get; set; }

        [StringLength(50)]
        [Display(Name = ResourceNames.Entity.LieuDelivrancePiece, ResourceType = typeof(EntityColumnResource))]
        public string LieuDelivrancePiece { get; set; }

        [Required]
        [Display(Name = ResourceNames.Entity.DateNaissance, ResourceType = typeof(EntityColumnResource))]
        public DateTime? DateNaissance { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = ResourceNames.Entity.LieuNaissance, ResourceType = typeof(EntityColumnResource))]
        public string LieuNaissance { get; set; }

        [StringLength(50)]
        [Display(Name = ResourceNames.Entity.Adresse, ResourceType = typeof(EntityColumnResource))]
        public string Adresse { get; set; }

        [StringLength(25)]
        public string NumeroTelephone1 { get; set; }

        [StringLength(25)]
        public string NumeroTelephone2 { get; set; }

        [StringLength(25)]
        public string NumeroTelephone3 { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Display(Name = ResourceNames.Entity.Email, ResourceType = typeof(EntityColumnResource))]
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string AdresseMail1 { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Display(Name = ResourceNames.Entity.Email, ResourceType = typeof(EntityColumnResource))]
        [RegularExpression(SinbaConstants.RegularExpressions.Email, ErrorMessageResourceName = ResourceNames.Error.ErrorInvalidEmail, ErrorMessageResourceType = typeof(EntityCommonResource))]
        public string AdresseMail2 { get; set; }

          [StringLength(10)]
        public string TypeContactID { get; set; }

        public bool Actif { get; set; }

        public bool PersonnePhysique { get; set; }

        public DateTime? DateCreation { get; set; }

        [StringLength(128)]
        public string UserCreation { get; set; }

        public DateTime? DateModification { get; set; }

        [StringLength(128)]
        public string UserModification { get; set; }

        [StringLength(100)]
        public string NomEtPrenom { get; set; }

        [Display(Name = ResourceNames.Entity.Culture, ResourceType = typeof(EntityColumnResource))]
        public string[] CultureArray { get; set; }

        [Display(Name = ResourceNames.Entity.ActiviteID, ResourceType = typeof(EntityColumnResource))]
        public string[] ActiviteArray { get; set; }

        public string FonctionContactString { get; set; }

        public string ReferenceBancaireString { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<FonctionContactViewModels> FonctionContacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<FonctionContactViewModels> FonctionContact1s { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Fournisseur> Fournisseurs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ReferenceBancaire> ReferenceBancaires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Contact ToContact()
        {
            Contact contact = new Contact();
            contact.Actif = this.Actif;
            contact.Adresse = this.Adresse;
            contact.AdresseMail1 = this.AdresseMail1;
            contact.AdresseMail2 = this.AdresseMail2;
            contact.CompteContribuable = this.CompteContribuable;
            contact.ContactID = this.ContactID;
            contact.DateCreation = this.DateCreation;
            contact.DateDelivrancePiece = this.DateDelivrancePiece;
            contact.DateModification = this.DateModification;
            contact.DateNaissance = this.DateNaissance;
            contact.LieuDelivrancePiece = this.LieuDelivrancePiece;
            contact.LieuNaissance = this.LieuNaissance;
            contact.Nom = this.Nom;
            contact.NumeroMatriculeNational = this.NumeroMatriculeNational;
            contact.NumeroPiece = this.NumeroPiece;
            contact.NumeroTelephone1 = this.NumeroTelephone1;
            contact.NumeroTelephone2 = this.NumeroTelephone2;
            contact.NumeroTelephone3 = this.NumeroTelephone3;
            contact.NumeroTicketIdentification = this.NumeroTicketIdentification;
            contact.PersonnePhysique = this.PersonnePhysique;
            contact.PieceIdentiteID = this.PieceIdentiteID;
            contact.Prenom = this.Prenom;
            contact.Sexe = this.Sexe;
            contact.UserCreation = this.UserCreation;
            contact.UserModification = this.UserModification;
            contact.LocaliteID = this.LocaliteID;

            CultureArray.ToList().ForEach(item => {
                contact.CulturePratiquees.Add(new CulturePratiquee()
                {
                    ContactID = contact.ContactID,
                    CultureID = item                   
                });
            });

            ActiviteArray.ToList().ForEach(item => {
                contact.ActivitePratiquees.Add(new ActivitePratiquee()
                {
                    ContactID = contact.ContactID,
                    ActiviteID = item
                });
            });

            contact.FonctionContacts = string.IsNullOrEmpty(FonctionContactString) ? new List<FonctionContact>() : JsonConvert.DeserializeObject<FonctionContact[]>(FonctionContactString).ToList();
            foreach (var item in contact.FonctionContacts)
            {
                item.ContactID = ContactID;
            }

            //création des fourniseurs et references bancaire 
            var refs = string.IsNullOrEmpty(ReferenceBancaireString) ? new List<ReferenceBancaire>() : JsonConvert.DeserializeObject<ReferenceBancaire[]>(ReferenceBancaireString).ToList();
            refs.DistinctBy(p => p.FournisseurID).ToList().ForEach(p => {
                contact.Fournisseurs.Add(new Fournisseur() {
                    ContactID = contact.ContactID,
                    FournisseurID = p.FournisseurID,
                    Actif =true,
                    ReferenceBancaires = refs.Where(r=>r.FournisseurID.Equals(p.FournisseurID,StringComparison.InvariantCultureIgnoreCase)).ToList() });
            });
            

            return contact;
        }


        /// <summary>
        /// Permet de renseigner les propriétés du  viewmodel à partir du model
        /// </summary>
        /// <param name="contact"></param>
        public void SetFromContact(Contact contact)
        {
            this.Actif = contact.Actif.Value;
            this.Adresse = contact.Adresse;
            this.AdresseMail1 = contact.AdresseMail1;
            this.AdresseMail2 = contact.AdresseMail2;
            this.CompteContribuable = contact.CompteContribuable;
            this.ContactID = contact.ContactID;
            this.DateCreation = contact.DateCreation;
            this.DateDelivrancePiece = contact.DateDelivrancePiece;
            this.DateModification = contact.DateModification;
            this.DateNaissance = contact.DateNaissance;
            this.LieuDelivrancePiece = contact.LieuDelivrancePiece;
            this.LieuNaissance = contact.LieuNaissance;
            this.Nom = contact.Nom;
            this.NumeroMatriculeNational = contact.NumeroMatriculeNational;
            this.NumeroPiece = contact.NumeroPiece;
            this.NumeroTelephone1 = contact.NumeroTelephone1;
            this.NumeroTelephone2 = contact.NumeroTelephone2;
            this.NumeroTelephone3 = contact.NumeroTelephone3;
            this.NumeroTicketIdentification = contact.NumeroTicketIdentification;
            this.PersonnePhysique = contact.PersonnePhysique.Value;
            this.PieceIdentiteID = contact.PieceIdentiteID;
            this.Prenom = contact.Prenom;
            this.Sexe = contact.Sexe;
            this.UserCreation = contact.UserCreation;
            this.UserModification = contact.UserModification;
            this.LocaliteID = contact.LocaliteID;
            this.NomEtPrenom = string.Format("{0} {1}",contact.Nom,contact.Prenom);

            if (contact.CulturePratiquees.Any())
                CultureArray = contact.CulturePratiquees.Select(item => item.CultureID).ToArray();

            if (contact.ActivitePratiquees.Any())
                ActiviteArray = contact.ActivitePratiquees.Select(item => item.ActiviteID).ToArray();

            foreach(var fonctioncontact in contact.FonctionContacts)
            {
                FonctionContacts.Add(new FonctionContactViewModels() {
                    ContactID = fonctioncontact.ContactID,
                    AutreContactID = fonctioncontact.AutreContactID,
                    FonctionID = fonctioncontact.FonctionID,
                    DescriptionContact = fonctioncontact.AutreContactID,
                    ContactDirigeant = fonctioncontact.Contact1
                });

            }

            Fournisseurs = contact.Fournisseurs;
            foreach(var item in contact.Fournisseurs)
            {
                item.ReferenceBancaires.ToList().ForEach( reference=> { ReferenceBancaires.Add(reference); });
            }


        }




    }

}
