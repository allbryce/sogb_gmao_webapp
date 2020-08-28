using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Transactions;
using System.Data.Entity;
using Sinba.BusinessModel.Enums;
using static Sinba.Resources.DatabaseConstants;
using Newtonsoft.Json;
using Dev.Tools.Common;
using Sinba.Resources.Resources.Entity;
using System.Data.Entity.Spatial;
using System.Globalization;

namespace Sinba.BusinessLogic.Services
{
    /// <summary>
    /// Classe de base des services métier de l'application Sinba
    /// </summary>
    [ServiceLog]
    [SinbaServiceExceptionHandler]
    public class ContactService : SinbaServiceBase, IContactService
    {


        #region Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ContactService(ISinbaUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        #endregion

        #region Methods

        #region PiecedIdentite
        public ListDto<Contact> GetContactList()
        {
            ListDto<Contact> lst = new ListDto<Contact>();
            this.UnitOfWork.ContactRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ContactRepository.Get();
            return lst;
        }

        
        public ListDto<Contact> GetContactList(ContactViewModels contactViewModels)
        {
            ListDto<Contact> lst = new ListDto<Contact>();
            this.UnitOfWork.ContactRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.RequestsRepository.GetContactListSQL(contactViewModels).Value;
            return lst;
        }

        public ListDto<Contact> GetContactListWithDependancies()
        {
            throw new NotImplementedException();
        }
        public SimpleDto<Contact> GetContact(string contactID)
        {
            SimpleDto<Contact> contact = new SimpleDto<Contact>();
            this.UnitOfWork.ContactRepository.EnableTracking = false;
            contact.Value = this.UnitOfWork.ContactRepository.Get(p => p.ContactID.Equals(contactID,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return contact;
        }
        public SimpleDto<Contact> GetContactWithDependancies(string contactID)
        {
            SimpleDto<Contact> contact = new SimpleDto<Contact>();
            this.UnitOfWork.ContactRepository.EnableTracking = false;
            contact.Value = this.UnitOfWork.ContactRepository.Get(p => p.ContactID.Equals(contactID,StringComparison.InvariantCultureIgnoreCase)
            ,p=> p.Include(fa=>fa.Fournisseurs.Select(rb=>rb.ReferenceBancaires))
                .Include(fc=>fc.FonctionContacts.Select(c=>c.Contact1)).OrderBy(u=>u.ContactID)
            ,p => p.ActivitePratiquees, p => p.CulturePratiquees).FirstOrDefault();
            return contact;
        }
        public BoolDto InsertContact(ContactModalViewModels contactModalViewModels)
        {
            BoolDto dto = new BoolDto();
            contactModalViewModels.Actif = true;
            contactModalViewModels.DateCreation = DateTime.Now;
            if (!ContatExiste(contactModalViewModels))
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    this.UnitOfWork.ContactRepository.EnableTracking = false;
                    var lastcontact = this.UnitOfWork.ContactRepository.Get().OrderByDescending(p => p.ContactID).FirstOrDefault();
                    var lastfournisseur = this.UnitOfWork.FournisseurRepository.Get().OrderByDescending(p => p.FournisseurID).FirstOrDefault();
                    string lastFournisseurID = lastfournisseur != null ? lastfournisseur.FournisseurID : string.Empty;
                    // conversion du viewmodel 
                    var contact = contactModalViewModels.ToContact();
                    //générer l'identifiant puis le met à jour dans les objets  les listes associées au contact
                   contact.GenererID(lastcontact != null ? lastcontact.ContactID : string.Empty);

                    foreach (var fournisseur in contact.Fournisseurs)
                    {
                        fournisseur.ContactID = contact.ContactID;
                        fournisseur.DateCreation = DateTime.Now;
                        fournisseur.UserCreation = contactModalViewModels.UserCreation;
                        fournisseur.GenererID(lastFournisseurID);
                        lastFournisseurID = fournisseur.FournisseurID;
                    }
                    //inserer 
                    this.UnitOfWork.ContactRepository.Insert(contact);
                    this.UnitOfWork.Commit();
                    scope.Complete();
                    dto.Value = true;
                }
                if (dto.HasError)
                {
                    dto.Errors.Add(EntityCommonResource.msgFailure);
                }
            }
            else
            {
                dto.Errors.Add(EntityCommonResource.errorContactExiste);
                dto.Value = false;
            }

            return dto;

        }
        public BoolDto UpdateContact(ContactModalViewModels contactModalViewModels)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ContactRepository.EnableTracking = true;
            Contact dbContact = this.UnitOfWork.ContactRepository.Get(p => p.ContactID.Equals(contactModalViewModels.ContactID,StringComparison.InvariantCultureIgnoreCase), p => p.Include(fa => fa.Fournisseurs.Select(rb => rb.ReferenceBancaires)).OrderBy(u => u.ContactID), p => p.ActivitePratiquees, p => p.CulturePratiquees, p => p.FonctionContacts ).FirstOrDefault();
            using (TransactionScope scope = new TransactionScope())
            {
                // conversion du viewmodel 
                var contact = contactModalViewModels.ToContact();

                if (dbContact != null)
                {
                    dbContact.NumeroTicketIdentification = contactModalViewModels.NumeroTicketIdentification;
                    dbContact.NumeroMatriculeNational = contactModalViewModels.NumeroMatriculeNational;
                    dbContact.Sexe = contactModalViewModels.Sexe;
                    dbContact.Nom = contactModalViewModels.Nom;
                    dbContact.Prenom = contactModalViewModels.Prenom;
                    dbContact.LocaliteID = contactModalViewModels.LocaliteID;
                    dbContact.CompteContribuable = contactModalViewModels.CompteContribuable;
                    dbContact.PieceIdentiteID = contactModalViewModels.PieceIdentiteID;
                    dbContact.NumeroPiece = contactModalViewModels.NumeroPiece;
                    dbContact.DateDelivrancePiece = contactModalViewModels.DateDelivrancePiece;
                    dbContact.LieuDelivrancePiece = contactModalViewModels.LieuDelivrancePiece;
                    dbContact.DateNaissance = contactModalViewModels.DateNaissance;
                    dbContact.LieuNaissance = contactModalViewModels.LieuNaissance;
                    dbContact.Adresse = contactModalViewModels.Adresse;
                    dbContact.NumeroTelephone1 = contactModalViewModels.NumeroTelephone1;
                    dbContact.NumeroTelephone2 = contactModalViewModels.NumeroTelephone2;
                    dbContact.NumeroTelephone3 = contactModalViewModels.NumeroTelephone3;
                    dbContact.AdresseMail1 = contactModalViewModels.AdresseMail1;
                    dbContact.AdresseMail2 = contactModalViewModels.AdresseMail2;
                    //dbContact.Actif = contactModalViewModels.Actif;
                    dbContact.PersonnePhysique = contactModalViewModels.PersonnePhysique;
                    dbContact.DateModification = DateTime.Now;
                    dbContact.UserModification = contactModalViewModels.UserModification;

                    dbContact.FonctionContacts = contact.FonctionContacts;
                    dbContact.CulturePratiquees = contact.CulturePratiquees;
                    dbContact.ActivitePratiquees = contact.ActivitePratiquees;
                    var arrayFournisseur = contact.Fournisseurs.Select(f => f.FournisseurID).ToArray();
                    //cas 1: supprimer toutes les references bancaires qui n'existent plus
                    foreach (var fournisseur in dbContact.Fournisseurs)
                    {
                        if (!arrayFournisseur.Contains(fournisseur.FournisseurID))
                        {
                            fournisseur.ReferenceBancaires = new List<ReferenceBancaire>();
                        }
                        else
                        {
                            //modification de references
                            var referencetoDelete = new List<ReferenceBancaire>();
                            foreach (var reference in fournisseur.ReferenceBancaires)
                            {
                                //if(contact.Fournisseurs.Select(p=> 
                                //    p.ReferenceBancaires.Where(r=>r.FournisseurID.Equals(reference.FournisseurID, StringComparison.InvariantCultureIgnoreCase))).
                                //))
                                //referencetoDelete.Add(reference);
                            }
                        }
                    }

                    var arraydbFournisseur = dbContact.Fournisseurs.Select(f => f.FournisseurID).ToArray();
                    //cas2: i
                    foreach (var fournisseur in contact.Fournisseurs)
                    {
                        if (!arraydbFournisseur.Contains(fournisseur.FournisseurID))
                        {
                            fournisseur.DateCreation = DateTime.Now;
                            fournisseur.UserCreation = contactModalViewModels.UserCreation;
                            dbContact.Fournisseurs.Add(fournisseur);
                        }
                        else
                        {
                            //modification de references
                            foreach (var reference in fournisseur.ReferenceBancaires)
                            {
                                //
                            }
                        }
                    }
                    this.UnitOfWork.Commit();
                }
                scope.Complete();
                dto.Value = true;
            }
            return dto;
        }
        public BoolDto DeleteContact(string contactID)
        {
            BoolDto dto = new BoolDto();
            Contact contactToDelete = UnitOfWork.ContactRepository.Get(p => p.ContactID.Equals(contactID, StringComparison.InvariantCultureIgnoreCase),null,
                p=>p.ActivitePratiquees,prop=>prop.CulturePratiquees, prop=>prop.FonctionContacts).FirstOrDefault();
            if (contactToDelete != null && !contactToDelete.IsUsed)
            {
                UnitOfWork.ContactRepository.Delete(contactToDelete);
                UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }
        public BoolDto IsContactUsed(string nom, string prenom, DateTime datenaissance)
        {
            BoolDto dto = new BoolDto();
            UnitOfWork.ContactRepository.EnableTracking = false;
            var query = UnitOfWork.ContactRepository.Get(p => p.Nom.Equals(nom, System.StringComparison.InvariantCultureIgnoreCase) && p.Prenom.Equals(prenom, System.StringComparison.InvariantCultureIgnoreCase));
            dto.Value = query.Any();
            return dto;
        }
        private bool ContatExiste(ContactModalViewModels contact)
        {
            this.UnitOfWork.ContactRepository.EnableTracking = false;
            bool returnValue = this.UnitOfWork.ContactRepository.Get(p => p.Nom.Equals(contact.Nom,StringComparison.InvariantCultureIgnoreCase) 
                && p.Prenom.Equals(contact.Prenom, StringComparison.InvariantCultureIgnoreCase)
                && DbFunctions.TruncateTime(p.DateNaissance) == DbFunctions.TruncateTime(contact.DateNaissance)
            ).Any();
            
            return returnValue;
        }


        #endregion

        #region FonctionContact

        public ListDto<FonctionContactViewModels> GetFonctionContactList(string contactID)
        {
            ListDto<FonctionContactViewModels> lst = new ListDto<FonctionContactViewModels>();
            lst.Value = this.UnitOfWork.RequestsRepository.GetFonctionContactListSQL(contactID).Value;
            return lst;
        }

        #endregion

        #region Fournisseur

        public ListDto<Fournisseur> GetFournisseurList(string contactID)
        {
            ListDto<Fournisseur> lst = new ListDto<Fournisseur>();
            this.UnitOfWork.FournisseurRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.FournisseurRepository.Get(p=> p.ContactID.Equals(contactID,StringComparison.InvariantCultureIgnoreCase),null, p=>p.ReferenceBancaires);
            return lst;
        }

        #endregion

        #region ReferenceBancaire

        public ListDto<ReferenceBancaire> GetReferenceBancaireList(string fournisseurID)
        {
            ListDto<ReferenceBancaire> lst = new ListDto<ReferenceBancaire>();
            this.UnitOfWork.ReferenceBancaireRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ReferenceBancaireRepository.Get(p=>p.FournisseurID.Equals(fournisseurID,StringComparison.InvariantCultureIgnoreCase));
            return lst;
        }

        public ListDto<ReferenceBancaire> GetReferenceBancaireListByContact(string contactID)
        {
            ListDto<ReferenceBancaire> lst = new ListDto<ReferenceBancaire>();
            this.UnitOfWork.FournisseurRepository.EnableTracking = false;
            var fournisseur = this.UnitOfWork.FournisseurRepository.Get(p => p.ContactID.Equals(contactID, StringComparison.InvariantCultureIgnoreCase), null, p => p.ReferenceBancaires);
            if (fournisseur != null)
            {
                foreach (var item in fournisseur)
                {
                    lst.Value.ToList().AddRange(item.ReferenceBancaires);
                }
            }
            return lst;
        }

        #endregion

        #endregion

    }
}
