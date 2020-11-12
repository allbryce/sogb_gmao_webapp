using Dev.Tools.Architecture.DataAccess;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.RepositoryInterface;

namespace Sinba.BusinessModel.Data
{
    /// <summary>
    /// Définit l'interface pour accèder à la base de données de l'application Sinba
    /// Ajouter dans cette interface les  déclarations de repository
    /// </summary>
    public interface ISinbaUnitOfWork : IUnitOfWork
    {
        IRepository<Action> ActionRepository { get; }
        IRepository<Fonction> FonctionRepository { get; }
        IRepository<FonctionAction> FonctionActionRepository { get; }
        IRepository<Profil> ProfilRepository { get; }
        IRepository<ProfilRight> ProfilRightRepository { get; }
        IRepository<UserRight> UserRightRepository { get; }
        IRepository<SinbaUser> SinbaUserRepository { get; }
        //******************************************************//
        IRepository<Langue> LangueRepository { get; }
        IRepository<Site> SiteRepository { get; }
        IRepository<SiteUtilisateur> SiteUtilisateurRepository { get; }
        IRepository<Societe> SocieteRepository { get; }
        IRepository<SiteCulture> SiteCultureRepository { get; }

        //*********************Atlas DPV********************//

        IRepository<Absence> AbsenceRepository { get; }
        IRepository<AccederConfidentialite> AccederConfidentialiteRepository { get; }
        IRepository<Affectation> AffectationRepository { get; }
        IRepository<AppartenirGroupe> AppartenirGroupeRepository { get; }
        IRepository<Article> ArticleRepository { get; }
        IRepository<AssocierMateriel> AssocierMaterielRepository { get; }
        IRepository<AttribuerFiche> AttribuerFicheRepository { get; }
        IRepository<AvoirConfidentialite> AvoirConfidentialiteRepository { get; }
        IRepository<BonMagasin> BonMagasinRepository { get; }
        IRepository<CaracteriserComposant> CaracteriserComposantRepository { get; }
        IRepository<CaracteristiqueComposant> CaracteristiqueComposantRepository { get; }
        IRepository<Classemateriel> ClassematerielRepository { get; }
        IRepository<Composant> ComposantRepository { get; }
        IRepository<ComposerMateriel> ComposerMaterielRepository { get; }
        IRepository<Confidentialite> ConfidentialiteRepository { get; }
        IRepository<ConstaterPanne> ConstaterPanneRepository { get; }
        IRepository<Decision> DecisionRepository { get; }
        IRepository<DecisionAffectation> DecisionAffectationRepository { get; }
        IRepository<DecisionBon> DecisionBonRepository { get; }
        IRepository<DecisionFiche> DecisionFicheRepository { get; }
        IRepository<Delai> DelaiRepository { get; }
        IRepository<Departement> DepartementRepository { get; }
        IRepository<Direction> DirectionRepository { get; }
        IRepository<Document> DocumentRepository { get; }
        IRepository<Domaine> DomaineRepository { get; }
        IRepository<Employe> EmployeRepository { get; }
        IRepository<Etape> EtapeRepository { get; }
        IRepository<EtatFiche> EtatFicheRepository { get; }
        IRepository<FairePartie> FairePartieRepository { get; }
        IRepository<Famille> FamilleRepository { get; }
        IRepository<FicheDeTravail> FicheDeTravailRepository { get; }
        IRepository<Fournisseur> FournisseurRepository { get; }
        IRepository<GroupeInventaire> GroupeInventaireRepository { get; }
        IRepository<GroupeUtilisateur> GroupeUtilisateurRepository { get; }
        IRepository<ImputationAnalytique> ImputationAnalytiqueRepository { get; }
        IRepository<Inventaire> InventaireRepository { get; }
        IRepository<InventorierMaterield> InventorierMaterieldRepository { get; }
        IRepository<Localisation> LocalisationRepository { get; }
        IRepository<LocaliserMateriel> LocaliserMaterielRepository { get; }
        IRepository<Marque> MarqueRepository { get; }
        IRepository<Materiel> MaterielRepository { get; }
        IRepository<Model> ModelRepository { get; }
        IRepository<NiveauSignataire> NiveauSignataireRepository { get; }
        IRepository<Panne> PanneRepository { get; }
        IRepository<PossederCaracteristiques> PossederCaracteristiquesRepository { get; }
        IRepository<PouvoirSigner> PouvoirSignerRepository { get; }
        IRepository<Sections> SectionsRepository { get; }
        IRepository<Service> ServiceRepository { get; }
        IRepository<Signaler> SignalerRepository { get; }
        IRepository<SignataireParNiveau> SignataireParNiveauRepository { get; }
        IRepository<Sortir> SortirRepository { get; }
        IRepository<SousFamille> SousFamilleRepository { get; }
        IRepository<Suppleer> SuppleerRepository { get; }
        IRepository<SystemePrivilege> SystemePrivilegeRepository { get; }
        IRepository<SystemeTypeDocument> SystemeTypeDocumentRepository { get; }
        IRepository<Tache> TacheRepository { get; }
        IRepository<TacheEffectuee> TacheEffectueeRepository { get; }
        IRepository<TypeAffectation> TypeAffectationRepository { get; }
        IRepository<TypeFiche> TypeFicheRepository { get; }
        IRepository<TypeMateriel> TypeMaterielRepository { get; }
        IRepository<TypePanne> TypePanneRepository { get; }
        IRepository<TypeTache> TypeTacheRepository { get; }
        IRepository<Unite> UniteRepository { get; }
        IRepository<Utilisateur> UtilisateurRepository { get; }

        #region Procedure repositories

        IProcedureRepository ProcedureRepository { get; }
        IRequestsRepository RequestsRepository { get; }

        #endregion

    }

}
