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
        ListDto<LocaliserMateriel> GetLocaliserMaterielList();

        //ListDto<Departement> GetDepartementList(string idSociete);

        ListDto<Departement> GetDepartementListWithDependencies();

        ListDto<LocaliserMateriel> GetLocaliserMaterielListWithDependencies();

        //ListDto<Departement> GetDepartementListWithDependencies(string idSociete);
        SimpleDto<Departement> GetDepartement(long id);
        BoolDto InsertDepartement(Departement departement);
        BoolDto InsertLocaliserMateriel(LocaliserMateriel localiser);
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
        ListDto<LocaliserMateriel> GetAffecterMaterielList(long localisationid);

        //ListDto<Departement> GetDepartementListWithDependencies(string idSociete);

        SimpleDto<Materiel> GetMateriel(long id);
        SimpleDto<PossederCaracteristiques> GetPossederCaracteristiques(long composantid, long materielid);
        BoolDto InsertMateriel(Materiel materiel);
//      BoolDto InsertPossederCaracteristique(PossederCaracteristiques Possedercaracteristique);

        BoolDto UpdateMateriel(Materiel materiel);
        BoolDto DeleteMateriel(long id);
        BoolDto RemoveComposant(long materielid, long composantid, DateTime dateinsertion);  
        //object UpdateMateriel(Materiel materiel);

        //BoolDto IsMaterielCodeUsed(string code, int id, string idSociete);



        #endregion

        #region Composant

        ListDto<Composant> GetComposantList();
        ListDto<Famille> GetFamilleList();
        ListDto<Marque> GetMarqueList();
        ListDto<Localisation> GetLocalisationList();
        SimpleDto<Domaine> GetDomaine(long id);
        SimpleDto<Marque> GetMarque(long id);
        SimpleDto<Localisation> GetLocalisation(long id);
        SimpleDto<Model> GetModel(long id);
        SimpleDto<Classemateriel> GetClasseMateriel(long id);
        SimpleDto<Famille> GetFamille(long id);
        SimpleDto<Fournisseur> GetFournisseur(long id);
        SimpleDto<GroupeInventaire> GetGroupeInventaire(long id);
        SimpleDto<SousFamille> GetSousFamille(long id);
        ListDto<Unite> GetUniteList();
        ListDto<CaracteristiqueComposant> GetCaracteristiqueComposantList();
        ListDto<ComposerMateriel> GetComposantMateriel(long id);
        ListDto<ComposerMateriel> GetComposerMaterielListWithDependencies(long id);
        ListDto<AssocierMateriel> GetAssocierMaterielListWithDependencies(long materielid); 
        ListDto<Materiel> GetAssocierMaterielList(long materielid); 
        ListDto<Composant> GetComposantListWithDependencies();
        //ListDto<Composant> GetComposantListWithDependencies(string id);

        SimpleDto<Composant> GetComposant(long id);
        SimpleDto<Unite> GetUnite(long id);
        BoolDto InsertComposant(Composant composant);
        BoolDto InsertMarque(Marque marque);
        BoolDto InsertLocalisation(Localisation localisation);
        BoolDto InsertFamille(Famille famille);
        BoolDto InsertFournisseur(Fournisseur famille);
        BoolDto InsertGroupeInventaire(GroupeInventaire groupe);
        BoolDto InsertSousFamille(SousFamille famille);
        BoolDto InsertModel(Model model);
        BoolDto InsertClasseMateriel(Classemateriel classe);
        BoolDto InsertUnite(Unite unite);
        BoolDto InsertDomaine(Domaine domaine);
        BoolDto Insertcaracteristique(CaracteristiqueComposant caracteristique);
        BoolDto UpdateComposant(Composant composant);
        BoolDto UpdateMarque(Marque marque);
        BoolDto UpdateLocalisation(Localisation marque);
        BoolDto UpdateFamille(Famille famille);
        BoolDto UpdateFournisseur(Fournisseur fournisseur);
        BoolDto UpdateSousFamille(SousFamille sousFamille);
        BoolDto UpdateModel(Model model);
        BoolDto UpdateClasseMateriel(Classemateriel classe);
        BoolDto UpdateUnite(Unite unite);
        BoolDto UpdateDomaine(Domaine domaine);
        BoolDto UpdateGroupeInventaire(GroupeInventaire groupe);
        BoolDto UpdateCaracteristiqueComposant(CaracteristiqueComposant caracteristique);
        BoolDto DeleteComposant(long id);
        BoolDto DeleteMarque(long id);
        BoolDto DeleteLocalisation(long id);
        BoolDto DeleteFamille(long id);
        BoolDto DeleteFournisseur(long id);
        BoolDto DeleteSousFamille(long id);
        BoolDto DeleteModel(long id);
        BoolDto DeleteClasseMateriel(long id);
        BoolDto DeleteCaracteristique(long id);
        BoolDto DeleteUnite(long id);
        BoolDto DeleteGroupeInventaire(long id);
        BoolDto DeleteDomaine(long id);
        ////object UpdateMateriel(Materiel materiel);

        ////BoolDto IsMaterielCodeUsed(string code, int id, string idSociete);

        #endregion

        #region Composer,caracteristique

        BoolDto InsertComposerMateriel(ComposerMateriel Composermateriel);
        BoolDto UpdateCaracteristique(ComposerMateriel Composermateriel);
        BoolDto InsertAssocieMateriel(List<AssocierMateriel> Composermateriel);
        //BoolDto InsertPossederCaracteristiques(PossederCaracteristiques possedercaracteristique);
        //BoolDto UpdateComposerMateriel(ComposerMateriel composermateriel);
        //BoolDto UpdatePossederCaracteristiques(PossederCaracteristiques possedercaracteristiques);

        #endregion

        #region Lists

        ListDto<Domaine> GetDomaineList();
        SimpleDto<CaracteristiqueComposant> GetCaracteristique(long caracteristique);
        ListDto<CaracteristiqueComposant> GetCaracteristiqueList();
        ListDto<GroupeInventaire> GetGroupeInventaireList();
        ListDto<Classemateriel> GetClassematerielList();
        ListDto<SousFamille> GetSousFamilleList();
        ListDto<Fournisseur> GetFournisseurList();
        ListDto<TypeMateriel> GetTypeMaterielList();
        ListDto<Model> GetModelList();

        #endregion
    }
}
