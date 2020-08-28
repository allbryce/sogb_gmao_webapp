using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.Enums;
using System;
using System.Collections.Generic;
namespace Sinba.BusinessModel.ServiceInterface
{
    public interface IContactService
    {

        #region Contact

        ListDto<Contact> GetContactList();
        ListDto<Contact> GetContactList(ContactViewModels contactViewModels);
      
        ListDto<Contact> GetContactListWithDependancies();
        SimpleDto<Contact> GetContact(string contactID);
        SimpleDto<Contact> GetContactWithDependancies(string contactID);
        BoolDto InsertContact(ContactModalViewModels contactModalViewModels);
        BoolDto UpdateContact(ContactModalViewModels contactModalViewModels);
        BoolDto DeleteContact(string contactID);
        BoolDto IsContactUsed(string nom, string prenom, DateTime datenaissance );

        #endregion

        #region FonctionContact

        ListDto<FonctionContactViewModels> GetFonctionContactList(string contactID);

        #endregion

        #region Fournisseur
        ListDto<Fournisseur> GetFournisseurList(string contactID);
        #endregion

        #region ReferenceBancaire
        ListDto<ReferenceBancaire> GetReferenceBancaireList(string fournisseurID);
        ListDto<ReferenceBancaire> GetReferenceBancaireListByContact(string contactID);
        #endregion


    }
}
