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
using System.Text.RegularExpressions;

namespace Sinba.BusinessLogic.Services
{
      [ServiceLog]
    [SinbaServiceExceptionHandler]
    public class DonneesDeBaseService : SinbaServiceBase, IDonneesDeBaseService
    {


        #region Contructors

        public DonneesDeBaseService(ISinbaUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
        #endregion

       
        #region Direction

        public ListDto<Direction> GetDirectionList()
        {
            ListDto<Direction> lst = new ListDto<Direction>();
            this.UnitOfWork.DirectionRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DirectionRepository.Get();
            return lst;
        }
        public ListDto<Direction> GetDirectionListWithDependencies()
        {

            ListDto<Direction> lst = new ListDto<Direction>();
            this.UnitOfWork.DirectionRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DirectionRepository.Get(null, null,x => x.Departement);
            return lst;
        }

        public SimpleDto<Direction> GetDirection(long id)
        {

            SimpleDto<Direction> dto = new SimpleDto<Direction>();

            this.UnitOfWork.DirectionRepository.EnableTracking = false;

            dto.Value = this.UnitOfWork.DirectionRepository.Get(p => p.DirectionId == id).FirstOrDefault();

            return dto;
        }


        public BoolDto InsertDirection(Direction direction)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.DirectionRepository.Insert(direction);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }


        public BoolDto UpdateDirection(Direction direction)
        {

            BoolDto dto = new BoolDto();
            this.UnitOfWork.DirectionRepository.EnableTracking = true;
            Direction dbDirection = this.UnitOfWork.DirectionRepository.Get(p => p.DirectionId == direction.DirectionId).FirstOrDefault();
            if (dbDirection != null)
            {
                dbDirection.DirectionId = direction.DirectionId;
                dbDirection.LibelleDirection = direction.LibelleDirection;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }
        public BoolDto DeleteDirection(long id)
        {
            BoolDto dto = new BoolDto();
            Direction directionToDelete = UnitOfWork.DirectionRepository.Get(n => n.DirectionId == id, null, n => n.Departement).FirstOrDefault();
            if (directionToDelete != null )
            {
                if (!directionToDelete.IsUsed)
                {
                    UnitOfWork.DirectionRepository.Delete(id);
                    UnitOfWork.Commit();
                    dto.Value = true;
                }
                else
                {
                    dto.Errors.Add("En cours d'utilisation");
                }
            }
            return dto;
        }
        #endregion

        #region Departement
        public ListDto<Departement> GetDepartementListWithDependencies()
        {
            ListDto<Departement> lst = new ListDto<Departement>();
            this.UnitOfWork.DepartementRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DepartementRepository.Get(null, null, x=> x.Direction);
            return lst;
        }
        public BoolDto InsertDepartement(Departement departement)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.DepartementRepository.Insert(departement);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }
        public SimpleDto<Departement> GetDepartement(long id)
        {
            SimpleDto<Departement> dto = new SimpleDto<Departement>();
            this.UnitOfWork.DepartementRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.DepartementRepository.Get(p => p.DepartementId == id).FirstOrDefault();
            return dto;
        }
        public ListDto<Departement> GetDepartementList()
        {
            ListDto<Departement> lst = new ListDto<Departement>();
            this.UnitOfWork.DepartementRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DepartementRepository.Get();
            return lst;
        }
        public BoolDto UpdateDepartement(Departement departement)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.DepartementRepository.EnableTracking = true;
            Departement dbDepartement = this.UnitOfWork.DepartementRepository.Get(p => p.DepartementId == departement.DepartementId).FirstOrDefault();
            if (dbDepartement != null)
            {
                dbDepartement.DepartementId = departement.DepartementId;
                dbDepartement.LibelleDepartement = departement.LibelleDepartement;
                dbDepartement.DirectionId = departement.DirectionId;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto DeleteDepartement(long id)
        {
            BoolDto dto = new BoolDto();      
            Departement departementToDelete = UnitOfWork.DepartementRepository.Get(n => n.DepartementId == id, null, n => n.Service).FirstOrDefault();
            if (departementToDelete != null)
            {
                if (!departementToDelete.IsUsed)
                { 
                   UnitOfWork.DepartementRepository.Delete(id);
                   UnitOfWork.Commit();
                   dto.Value = true;
                }
                else
                {
                    dto.Errors.Add("Le département en question est lié à un service");
                }
            }
            return dto;
       
        }
        #endregion

        #region ComposerMateriel
        public ListDto<ComposerMateriel> GetComposerMaterielListWithDependencies(long id)
        {
            ListDto<ComposerMateriel> lst = new ListDto<ComposerMateriel>();
            this.UnitOfWork.ComposerMaterielRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ComposerMaterielRepository.Get(p =>p.MaterielId == id, null, x=> x.Composant,x=>x.Materiel);
            return lst;
        }
        public ListDto<Materiel> GetAssocierMaterielList(long domaineid)
        {
            ListDto<Materiel> lst = new ListDto<Materiel>();
            this.UnitOfWork.MaterielRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.MaterielRepository.Get(p=>p.DomaineId == domaineid, null,null);
            return lst;
        }
        public ListDto<AssocierMateriel> GetAssocierMaterielListWithDependencies(long materielid)
        {
            ListDto<AssocierMateriel> lst = new ListDto<AssocierMateriel>();
            this.UnitOfWork.AssocierMaterielRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.AssocierMaterielRepository.Get(p => p.MaterielId == materielid, null, x => x.Materiel1, x => x.Materiel);
            return lst;
        }
    
        public BoolDto InsertComposerMateriel(ComposerMateriel composermateriel)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ComposerMaterielRepository.Insert(composermateriel);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }
        public BoolDto InsertAssocieMateriel(List<AssocierMateriel> associer)
        {
            AssocierMateriel materiel= associer[0];           
            BoolDto dto = new BoolDto();
           List<AssocierMateriel> associerTodelete = this.UnitOfWork.AssocierMaterielRepository.Get(p => p.MaterielId == materiel.MaterielId).ToList();
            if (associerTodelete != null)
            {
                foreach(AssocierMateriel item in associerTodelete)
                {
                  UnitOfWork.AssocierMaterielRepository.Delete(item);
                  UnitOfWork.Commit();

                }
            }
            foreach (AssocierMateriel item in associer)
            {
            this.UnitOfWork.AssocierMaterielRepository.Insert(item);
            this.UnitOfWork.Commit();
            }
            dto.Value = true;
            return dto;
        }
        public BoolDto UpdateCaracteristique(ComposerMateriel composerMateriel)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ComposerMaterielRepository.EnableTracking = true;
            ComposerMateriel dbcomposermateriel = this.UnitOfWork.ComposerMaterielRepository.Get(p => p.MaterielId == composerMateriel.MaterielId && p.ComposantId == composerMateriel.ComposantId && p.DateInsertion == composerMateriel.DateInsertion).FirstOrDefault();
            PossederCaracteristiques possedercaracteristiquesToDelete = UnitOfWork.PossederCaracteristiquesRepository.Get(n => n.MaterielId == composerMateriel.MaterielId && n.ComposantId ==composerMateriel.ComposantId && n.DateInsertion ==composerMateriel.DateInsertion, null,null).FirstOrDefault();
            if (possedercaracteristiquesToDelete != null)
            {
                UnitOfWork.PossederCaracteristiquesRepository.Delete(possedercaracteristiquesToDelete);
                UnitOfWork.Commit();
            }

              if (dbcomposermateriel != null)
              {
                dbcomposermateriel.Quantite = composerMateriel.Quantite;
                dbcomposermateriel.Plafond = composerMateriel.Plafond;    
                dbcomposermateriel.PossederCaracteristiques = composerMateriel.PossederCaracteristiques;
                this.UnitOfWork.Commit();
                dto.Value = true;
              }
         return dto;
        }
        public BoolDto InsertPossederCaracteristique(PossederCaracteristiques PossederCaracteristique)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.PossederCaracteristiquesRepository.Insert(PossederCaracteristique);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public SimpleDto<ComposerMateriel> GetComposerMateriel(long id)
        {
            SimpleDto<ComposerMateriel> dto = new SimpleDto<ComposerMateriel>();
            this.UnitOfWork.ComposerMaterielRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.ComposerMaterielRepository.Get(p => p.ComposantId == id).FirstOrDefault();
            return dto;
        }

        public ListDto<Departement> GetComposerMaterielList()
        {
            ListDto<Departement> lst = new ListDto<Departement>();
            this.UnitOfWork.DepartementRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DepartementRepository.Get();
            return lst;
        }

        //public BoolDto DeleteDepartement(string id)

        //{

        //    BoolDto dto = new BoolDto();

        //    Departement departementToDelete = UnitOfWork.DepartementRepository.Get(n => n.DepartementId == id, null, n => n.Service).FirstOrDefault();

        //    if (departementToDelete != null)

        //    {
        //        if (!departementToDelete.IsUsed)
        //        { 
        //           UnitOfWork.DepartementRepository.Delete(id);
        //           UnitOfWork.Commit();
        //           dto.Value = true;
        //        }
        //        else
        //        {
        //            dto.Errors.Add("Le département en question est lié à un service");
        //        }

        //    }

        //    return dto;

        //}
        #endregion

        #region Service
        public ListDto<Service> GetServiceListWithDependencies()

        {

            ListDto<Service> lst = new ListDto<Service>();

            this.UnitOfWork.ServiceRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.ServiceRepository.Get(null, null,

              x => x.Departement);

            return lst;

        }


        public BoolDto InsertService(Service service)

        {

            BoolDto dto = new BoolDto();

            this.UnitOfWork.ServiceRepository.Insert(service);

            this.UnitOfWork.Commit();

            dto.Value = true;

            return dto;

        }
        public SimpleDto<Service> GetService(long id)
        {
            SimpleDto<Service> dto = new SimpleDto<Service>();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.ServiceRepository.Get(p => p.ServiceId == id).FirstOrDefault();
            return dto;
        }
        public ListDto<Service> GetServiceList()
        {
            ListDto<Service> lst = new ListDto<Service>();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ServiceRepository.Get();
            return lst;
        }

        public BoolDto UpdateService(Service service)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ServiceRepository.EnableTracking = true;      
            Service dbService = this.UnitOfWork.ServiceRepository.Get(p => p.ServiceId == service.ServiceId).FirstOrDefault();
            if (dbService != null)
            {
                dbService.ServiceId = service.ServiceId;
                dbService.LibelleService = service.LibelleService;
                dbService.DepartementId = service.DepartementId;               
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }
        public BoolDto DeleteService(long id)
        {
            BoolDto dto = new BoolDto();
            Service serviceToDelete = UnitOfWork.ServiceRepository.Get(n => n.ServiceId == id, null, n => n.Sections).FirstOrDefault();
            if (serviceToDelete != null)
            {
                UnitOfWork.ServiceRepository.Delete(id);
                UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }
        #endregion

        #region Sections
        public ListDto<Sections> GetSectionsListWithDependencies()
        {
            ListDto<Sections> lst = new ListDto<Sections>();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.SectionsRepository.Get(null, null,x => x.Service);
            return lst;

        }
        public BoolDto InsertSections(Sections sections)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SectionsRepository.Insert(sections);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }
        public SimpleDto<Sections> GetSections(long id)
        {
            SimpleDto<Sections> dto = new SimpleDto<Sections>();
            this.UnitOfWork.SectionsRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.SectionsRepository.Get(p => p.SectionsId == id).FirstOrDefault();
            return dto;
        }
        public ListDto<Sections> GetSectionsList()
        {
            ListDto<Sections> lst = new ListDto<Sections>();
            this.UnitOfWork.SectionsRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.SectionsRepository.Get();
            return lst;

        }

        public BoolDto UpdateSections(Sections sections)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SectionsRepository.EnableTracking = true;
            Sections dbSections = this.UnitOfWork.SectionsRepository.Get(p => p.SectionsId == sections.SectionsId).FirstOrDefault();
            if (dbSections != null)
            {
                dbSections.SectionsId = sections.SectionsId;
                dbSections.LibelleSections = sections.LibelleSections;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
           return dto;
        }
    
        public BoolDto DeleteSections(long id)
        {
            BoolDto dto = new BoolDto();
            Sections sectionsToDelete = UnitOfWork.SectionsRepository.Get(n => n.SectionsId == id, null ).FirstOrDefault();
            if (sectionsToDelete != null)
            {
                UnitOfWork.SectionsRepository.Delete(id);
                UnitOfWork.Commit();
                dto.Value = true;
            }       
            return dto;
        }
        #endregion

        #region Materiel
        public ListDto<Materiel> GetMaterielListWithDependencies()
        {

            ListDto<Materiel> lst = new ListDto<Materiel>();

            this.UnitOfWork.ServiceRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.MaterielRepository.Get(null, null,x => x.ComposerMateriel,x=>x.Domaine,x=>x.Fournisseur,x=>x.Model,m=>m.TypeMateriel);

            return lst;

        }
        public ListDto<Materiel> GetMaterielListWithDependencies(long domaineid)
        {
            ListDto<Materiel> lst = new ListDto<Materiel>();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.MaterielRepository.Get(m =>m.DomaineId == domaineid, null);
            return lst;
        }

        public BoolDto InsertMateriel(Materiel materiel)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.MaterielRepository.Insert(materiel);          
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }
        public SimpleDto<Materiel> GetMateriel(long id)
        {
            SimpleDto<Materiel> dto = new SimpleDto<Materiel>();
            this.UnitOfWork.MaterielRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.MaterielRepository.Get(p => p.MaterielId == id).FirstOrDefault();
            return dto;
        }

        public ListDto<Materiel> GetMaterielList()
        {
            ListDto<Materiel> lst = new ListDto<Materiel>();
            this.UnitOfWork.MaterielRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.MaterielRepository.Get();
            return lst;
        }
    
        public BoolDto UpdateMateriel(Materiel materiel)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.MaterielRepository.EnableTracking = true;
            Materiel dbMateriel = this.UnitOfWork.MaterielRepository.Get(p => p.MaterielId == materiel.MaterielId).FirstOrDefault();         
            if (dbMateriel != null)
            {
                dbMateriel.LibelleMateriel = materiel.LibelleMateriel;
                dbMateriel.DomaineId = materiel.DomaineId;
                dbMateriel.Classemateriel = materiel.Classemateriel;
                dbMateriel.FournisseurId = materiel.FournisseurId;
                dbMateriel.TypeMaterielId = materiel.TypeMaterielId;
                dbMateriel.GroupeInventaireId = materiel.GroupeInventaireId;
                dbMateriel.NumeroModel = materiel.NumeroModel;
                dbMateriel.DateMiseEnService = materiel.DateMiseEnService;
                dbMateriel.DateAcquisition = materiel.DateAcquisition;
                dbMateriel.PrixAchat = materiel.PrixAchat;
                dbMateriel.NumeroBonCommande = materiel.NumeroBonCommande;
                dbMateriel.NumeroSerie = materiel.NumeroSerie;
                dbMateriel.DateSortie = materiel.DateSortie;
                dbMateriel.Actif = materiel.Actif;
                dbMateriel.Garantie = materiel.Garantie;
                dbMateriel.Immobilise = materiel.Immobilise;
                dbMateriel.SousFamilleId = materiel.SousFamilleId;
                dbMateriel.Note = materiel.Note;              
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto DeleteMateriel(long id)
        {
            BoolDto dto = new BoolDto();
            Materiel MaterielToDelete = UnitOfWork.MaterielRepository.Get(n => n.MaterielId == id,null, x=>x.ComposerMateriel, x => x.PossederCaracteristiques, x => x.AssocierMateriel, x => x.AssocierMateriel1).FirstOrDefault();
            if (MaterielToDelete != null)
            {
                UnitOfWork.MaterielRepository.Delete(MaterielToDelete);
                UnitOfWork.Commit();
                dto.Value = true;
            }      
            return dto;
        }
        public BoolDto RemoveComposant(long materielid, long composantid, DateTime dateinsertion)
        {
            ComposerMateriel ComposantTodelete = UnitOfWork.ComposerMaterielRepository.Get(n => n.MaterielId == materielid && n.ComposantId == composantid && n.DateInsertion == dateinsertion, null, x => x.PossederCaracteristiques).FirstOrDefault();
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;
               if(ComposantTodelete != null )
               {                   
                    UnitOfWork.ComposerMaterielRepository.Delete(ComposantTodelete);
                    UnitOfWork.Commit();
                    dto.Value = true;
               }
            return dto;
        }

        #endregion #region Materiel

        #region Domaine
        public ListDto<Domaine> GetDomaineListWithDependencies()
        {
            ListDto<Domaine> lst = new ListDto<Domaine>();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DomaineRepository.Get(null, null,
            x => x.Materiel, x=>x.Composant);       
            return lst;
        }

        public BoolDto InsertDomaine(Domaine domaine)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.DomaineRepository.Insert(domaine);          
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }
        public SimpleDto<Domaine> GetDomaine(long id)
        {
            SimpleDto<Domaine> dto = new SimpleDto<Domaine>();
            this.UnitOfWork.DomaineRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.DomaineRepository.Get(p => p.DomaineId == id).FirstOrDefault();
            return dto;
        }

        public ListDto<Domaine> GetDomaineList()
        {
            ListDto<Domaine> lst = new ListDto<Domaine>();
            this.UnitOfWork.DomaineRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.DomaineRepository.Get();
            return lst;
        }


        public BoolDto UpdateDomaine(Domaine domaine)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.DomaineRepository.EnableTracking = true;
            Domaine dbDomaine = this.UnitOfWork.DomaineRepository.Get(p => p.DomaineId == domaine.DomaineId).FirstOrDefault();
            if (dbDomaine != null)
            {
                dbDomaine.DomaineId = domaine.DomaineId;
                dbDomaine.LibelleDomaine = domaine.LibelleDomaine;               
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;        
        }

        public BoolDto DeleteDomaine(long id)
        {
            BoolDto dto = new BoolDto();
            Domaine DomaineToDelete = UnitOfWork.DomaineRepository.Get(n => n.DomaineId == id, null).FirstOrDefault();
            if (DomaineToDelete != null)
            {
               UnitOfWork.DomaineRepository.Delete(id);
                UnitOfWork.Commit();
                dto.Value = true;
            }       
            return dto;

        }

        #endregion

        #region loading lists

        public ListDto<GroupeInventaire> GetGroupeInventaireList()
        {

            ListDto<GroupeInventaire> lst = new ListDto<GroupeInventaire>();

            this.UnitOfWork.GroupeInventaireRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.GroupeInventaireRepository.Get();

            return lst;

        }


        public ListDto<Classemateriel> GetClassematerielList()

        {

            ListDto<Classemateriel> lst = new ListDto<Classemateriel>();

            this.UnitOfWork.ClassematerielRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.ClassematerielRepository.Get();

            return lst;

        }

        public ListDto<SousFamille> GetSousFamilleList()

        {

            ListDto<SousFamille> lst = new ListDto<SousFamille>();

            this.UnitOfWork.SousFamilleRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.SousFamilleRepository.Get();

            return lst;

        }

        public ListDto<Fournisseur> GetFournisseurList()

        {

            ListDto<Fournisseur> lst = new ListDto<Fournisseur>();

            this.UnitOfWork.FournisseurRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.FournisseurRepository.Get();

            return lst;

        }
        public ListDto<Model> GetModelList()

        {

            ListDto<Model> lst = new ListDto<Model>();

            this.UnitOfWork.ModelRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.ModelRepository.Get();

            return lst;

        }
        public ListDto<TypeMateriel> GetTypeMaterielList()

        {

            ListDto<TypeMateriel> lst = new ListDto<TypeMateriel>();

            this.UnitOfWork.TypeMaterielRepository.EnableTracking = false;

            lst.Value = this.UnitOfWork.TypeMaterielRepository.Get();

            return lst;

        }

        #endregion

        #region Composant
        public ListDto<Composant> GetComposantListWithDependencies()
        {
            ListDto<Composant> lst = new ListDto<Composant>();
            this.UnitOfWork.ServiceRepository.EnableTracking = false;      
            lst.Value = this.UnitOfWork.ComposantRepository.Get(null, null, x => x.ComposerMateriel);
            // x => x.CaracteristiqueComposant,
           // x => x.Domaine
            return lst;
        }

        public BoolDto InsertComposant(Composant composant)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ComposantRepository.Insert(composant);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }
        public SimpleDto<Composant> GetComposant(long id)
        {
            SimpleDto<Composant> dto = new SimpleDto<Composant>();
            this.UnitOfWork.ComposantRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.ComposantRepository.Get(p => p.ComposantId == id).FirstOrDefault();
            return dto;
        }

        public ListDto<Composant> GetComposantList()
        {
            ListDto<Composant> lst = new ListDto<Composant>();
            this.UnitOfWork.ComposantRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ComposantRepository.Get();
            return lst;
        }

        public ListDto<ComposerMateriel> GetComposantMateriel(long id)
        {            
            ListDto<ComposerMateriel> dtomaterielcomposant = new ListDto<ComposerMateriel>();
            this.UnitOfWork.ComposerMaterielRepository.EnableTracking = true;
            dtomaterielcomposant.Value = this.UnitOfWork.ComposerMaterielRepository.Get(p => p.MaterielId == id);
            return dtomaterielcomposant;            
        }

        public BoolDto UpdateComposant(Composant composant)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ComposantRepository.EnableTracking = true;
            Composant dbComposant = this.UnitOfWork.ComposantRepository.Get(p => p.ComposantId == composant.ComposantId).FirstOrDefault();            
            if (dbComposant != null)
            {
                dbComposant.ComposantId = composant.ComposantId;
                dbComposant.LibelleComposant = composant.LibelleComposant;
                dbComposant.DomaineId = composant.DomaineId;
                dbComposant.OrdreComposant = composant.OrdreComposant;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto DeleteComposant(long id)
        {
            BoolDto dto = new BoolDto();
            Composant ComposantToDelete = UnitOfWork.ComposantRepository.Get(n => n.ComposantId == id, null).FirstOrDefault();
            if (ComposantToDelete != null)
            {
                UnitOfWork.ComposantRepository.Delete(id);
                UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public ListDto<CaracteristiqueComposant> GetCaracteristiqueComposantList()
        {
            ListDto<CaracteristiqueComposant> lst = new ListDto<CaracteristiqueComposant>();
            this.UnitOfWork.CaracteristiqueComposantRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.CaracteristiqueComposantRepository.Get();
            return lst;
        }

        public ListDto<Unite> GetUniteList()
        {
            ListDto<Unite> lst = new ListDto<Unite>();
            this.UnitOfWork.UniteRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.UniteRepository.Get();
            return lst;
        }

        public SimpleDto<PossederCaracteristiques> GetPossederCaracteristiques(long composantid,long materielid)
        {
            SimpleDto<PossederCaracteristiques> dto = new SimpleDto<PossederCaracteristiques>();
            this.UnitOfWork.PossederCaracteristiquesRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.PossederCaracteristiquesRepository.Get(p => p.ComposantId == composantid && p.MaterielId == materielid).FirstOrDefault();
            return dto;
        }
       
       public SimpleDto<ComposerMateriel> GetComposerMaterielwithdependencies(long composantid, long materielid, DateTime dateinsertion)
        {
            SimpleDto<ComposerMateriel> dto = new SimpleDto<ComposerMateriel>();
            this.UnitOfWork.PossederCaracteristiquesRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.ComposerMaterielRepository.Get(p => p.ComposantId == composantid && p.MaterielId == materielid && p.DateInsertion == dateinsertion,null,x=> x.PossederCaracteristiques).FirstOrDefault();
            return dto;
        }

        #endregion
    }
}
