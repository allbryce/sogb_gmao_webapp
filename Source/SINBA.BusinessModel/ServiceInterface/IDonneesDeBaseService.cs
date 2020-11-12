using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.Enums;
using System;
using System.Collections.Generic;
namespace Sinba.BusinessModel.ServiceInterface
{
    public interface IDonneesDeBaseService
    {
        #region Direction

        ListDto<Direction> GetDirectionList();

        //ListDto<Direction> GetDirectionList(string idSociete);

        ListDto<Direction> GetDirectionListWithDependencies();
       // ListDto<Direction> GetDirectionListWithDependencies(string idSociete);
        SimpleDto<Direction> GetDirection(long id);
        //SimpleDto<ComposerMateriel> GetComposerMateriel(long composantid,long materielid);
        SimpleDto<ComposerMateriel> GetComposerMaterielwithdependencies(long composantid, long materielid, DateTime dateinsertion);

        BoolDto InsertDirection(Direction direction);

        BoolDto UpdateDirection(Direction direction);

        BoolDto DeleteDirection(long id);

        //BoolDto IsDirectionCodeUsed(string code, int id, string idSociete);



        #endregion

        #region Departement

        ListDto<Departement> GetDepartementList();

        //ListDto<Departement> GetDepartementList(string idSociete);

        ListDto<Departement> GetDepartementListWithDependencies();

        //ListDto<Departement> GetDepartementListWithDependencies(string idSociete);

        SimpleDto<Departement> GetDepartement(long id);

        BoolDto InsertDepartement(Departement departement);

        BoolDto UpdateDepartement(Departement departement);

        BoolDto DeleteDepartement(long id);
        //object UpdateDirection(Departement departement);

        //BoolDto IsDepartementCodeUsed(string code, int id, string idSociete);



        #endregion


        #region Service

        ListDto<Service> GetServiceList();

        //ListDto<Departement> GetDepartementList(string idSociete);

        ListDto<Service> GetServiceListWithDependencies();

        //ListDto<Departement> GetDepartementListWithDependencies(string idSociete);

        SimpleDto<Service> GetService(long id);

        BoolDto InsertService(Service service);

        BoolDto UpdateService(Service service);

        BoolDto DeleteService(long id);
        //object UpdateDirection(Departement departement);

        //BoolDto IsDepartementCodeUsed(string code, int id, string idSociete);



        #endregion

        #region Sections

        ListDto<Sections> GetSectionsList();

        //ListDto<Departement> GetDepartementList(string idSociete);

        ListDto<Sections> GetSectionsListWithDependencies();

        //ListDto<Departement> GetDepartementListWithDependencies(string idSociete);

        SimpleDto<Sections>GetSections(long id);

        BoolDto InsertSections(Sections sections);

        BoolDto UpdateSections(Sections sections);

        BoolDto DeleteSections(long id);
        //object UpdateDirection(Departement departement);

        //BoolDto IsDepartementCodeUsed(string code, int id, string idSociete);



        #endregion

        #region Materiel

        ListDto<Materiel> GetMaterielList();

        //ListDto<Materiel> GetMaterielList(string idSociete);

        ListDto<Materiel> GetMaterielListWithDependencies();
        ListDto<Materiel> GetMaterielListWithDependencies(long domaineid);

        //ListDto<Departement> GetDepartementListWithDependencies(string idSociete);

        SimpleDto<Materiel> GetMateriel(long id);

        SimpleDto<PossederCaracteristiques> GetPossederCaracteristiques(long composantid, long materielid);

        BoolDto InsertMateriel(Materiel materiel);
//        BoolDto InsertPossederCaracteristique(PossederCaracteristiques Possedercaracteristique);

        BoolDto UpdateMateriel(Materiel materiel);

        BoolDto DeleteMateriel(long id);
        //object UpdateMateriel(Materiel materiel);

        //BoolDto IsMaterielCodeUsed(string code, int id, string idSociete);



        #endregion

        #region Composant

        ListDto<Composant> GetComposantList();
        ListDto<Unite> GetUniteList();
         ListDto<CaracteristiqueComposant> GetCaracteristiqueComposantList();

        ListDto<ComposerMateriel> GetComposantMateriel(long id);
        ListDto<ComposerMateriel> GetComposerMaterielListWithDependencies(long id);
        ListDto<AssocierMateriel> GetAssocierMaterielListWithDependencies(long materielid); 
        ListDto<Materiel> GetAssocierMaterielList(long materielid); 

        ListDto<Composant> GetComposantListWithDependencies();

        //ListDto<Composant> GetComposantListWithDependencies(string id);

        SimpleDto<Composant> GetComposant(long id);

        BoolDto InsertComposant(Composant composant);

        BoolDto UpdateComposant(Composant composant);

        BoolDto DeleteComposant(long id);
        ////object UpdateMateriel(Materiel materiel);

        ////BoolDto IsMaterielCodeUsed(string code, int id, string idSociete);

        #endregion

        #region Composer,caracteristique

        BoolDto InsertComposerMateriel(ComposerMateriel Composermateriel);
        //BoolDto InsertPossederCaracteristiques(PossederCaracteristiques possedercaracteristique);
        //BoolDto UpdateComposerMateriel(ComposerMateriel composermateriel);
        //BoolDto UpdatePossederCaracteristiques(PossederCaracteristiques possedercaracteristiques);

        #endregion

        #region Lists

        ListDto<Domaine> GetDomaineList();
        ListDto<GroupeInventaire> GetGroupeInventaireList();
        ListDto<Classemateriel> GetClassematerielList();
        ListDto<SousFamille> GetSousFamilleList();
        ListDto<Fournisseur> GetFournisseurList();
        ListDto<TypeMateriel> GetTypeMaterielList();
        ListDto<Model> GetModelList();

        #endregion
    }
}
