using Dev.Tools.Architecture.DataAccess;
using Dev.Tools.EntityFramework;
using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.RepositoryInterface;
using Sinba.DataAccess;

namespace Sinba.DataAccess
{
    /// <summary>
    /// Classe d'accès à la base de données de l'application Sinba utilisant EF pour faire le lien sur les tables.
    /// </summary>
    [DataLog]
    [DataExceptionHandler]
    public class SinbaUnitOfWork : ISinbaUnitOfWork
    {
        #region Properties
        /// <summary>
        /// The database context
        /// </summary>
        private SinbaContext DbContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SinbaUnitOfWork" /> class.
        /// </summary>
        public SinbaUnitOfWork()
        {
            this.DbContext = new SinbaContext();
            this.DbContext.Configuration.LazyLoadingEnabled = false;

            // Initialise repositories here
            this.ActionRepository = new GenericRepository<Action>(DbContext);
            this.FonctionRepository = new GenericRepository<Fonction>(DbContext);
            this.FonctionActionRepository = new GenericRepository<FonctionAction>(DbContext);
            this.ProfilRepository = new GenericRepository<Profil>(DbContext);
            this.ProfilRightRepository = new GenericRepository<ProfilRight>(DbContext);
            this.UserRightRepository = new GenericRepository<UserRight>(DbContext);
            this.SinbaUserRepository = new GenericRepository<SinbaUser>(DbContext);
            //***********************************************************//
            this.LangueRepository = new GenericRepository<Langue>(DbContext);
            this.SiteRepository = new GenericRepository<Site>(DbContext);
            this.SiteCultureRepository = new GenericRepository<SiteCulture>(DbContext);
            this.SiteUtilisateurRepository = new GenericRepository<SiteUtilisateur>(DbContext);
            this.SocieteRepository = new GenericRepository<Societe>(DbContext);

            //***********************************************************//

            this.AbsenceRepository = new GenericRepository<Absence>(DbContext);
            this.AccederConfidentialiteRepository = new GenericRepository<AccederConfidentialite>(DbContext);
            this.AffectationRepository = new GenericRepository<Affectation>(DbContext);
            this.AppartenirGroupeRepository = new GenericRepository<AppartenirGroupe>(DbContext);
            this.ArticleRepository = new GenericRepository<Article>(DbContext);
            this.AssocierMaterielRepository = new GenericRepository<AssocierMateriel>(DbContext);
            this.AttribuerFicheRepository = new GenericRepository<AttribuerFiche>(DbContext);
            this.AvoirConfidentialiteRepository = new GenericRepository<AvoirConfidentialite>(DbContext);
            this.BonMagasinRepository = new GenericRepository<BonMagasin>(DbContext);
            this.CaracteriserComposantRepository = new GenericRepository<CaracteriserComposant>(DbContext);
            this.CaracteristiqueComposantRepository = new GenericRepository<CaracteristiqueComposant>(DbContext);
            this.ClassematerielRepository = new GenericRepository<Classemateriel>(DbContext);
            this.ComposantRepository = new GenericRepository<Composant>(DbContext);
            this.ComposerMaterielRepository = new GenericRepository<ComposerMateriel>(DbContext);
            this.ConfidentialiteRepository = new GenericRepository<Confidentialite>(DbContext);
            this.ConstaterPanneRepository = new GenericRepository<ConstaterPanne>(DbContext);
            this.DecisionRepository = new GenericRepository<Decision>(DbContext);
            this.DecisionAffectationRepository = new GenericRepository<DecisionAffectation>(DbContext);
            this.DecisionBonRepository = new GenericRepository<DecisionBon>(DbContext);
            this.DecisionFicheRepository = new GenericRepository<DecisionFiche>(DbContext);
            this.DelaiRepository = new GenericRepository<Delai>(DbContext);
            this.DepartementRepository = new GenericRepository<Departement>(DbContext);
            this.DirectionRepository = new GenericRepository<Direction>(DbContext);
            this.DocumentRepository = new GenericRepository<Document>(DbContext);
            this.DomaineRepository = new GenericRepository<Domaine>(DbContext);
            this.EmployeRepository = new GenericRepository<Employe>(DbContext);
            this.EtapeRepository = new GenericRepository<Etape>(DbContext);
            this.EtatFicheRepository = new GenericRepository<EtatFiche>(DbContext);
            this.FairePartieRepository = new GenericRepository<FairePartie>(DbContext);
            this.FamilleRepository = new GenericRepository<Famille>(DbContext);
            this.FicheDeTravailRepository = new GenericRepository<FicheDeTravail>(DbContext);
            this.FournisseurRepository = new GenericRepository<Fournisseur>(DbContext);
            this.GroupeInventaireRepository = new GenericRepository<GroupeInventaire>(DbContext);
            this.GroupeUtilisateurRepository = new GenericRepository<GroupeUtilisateur>(DbContext);
            this.ImputationAnalytiqueRepository = new GenericRepository<ImputationAnalytique>(DbContext);
            this.InventaireRepository = new GenericRepository<Inventaire>(DbContext);
            this.InventorierMaterieldRepository = new GenericRepository<InventorierMaterield>(DbContext);
            this.LocalisationRepository = new GenericRepository<Localisation>(DbContext);
            this.LocaliserMaterielRepository = new GenericRepository<LocaliserMateriel>(DbContext);
            this.MarqueRepository = new GenericRepository<Marque>(DbContext);
            this.MaterielRepository = new GenericRepository<Materiel>(DbContext);
            this.ModelRepository = new GenericRepository<Model>(DbContext);
            this.NiveauSignataireRepository = new GenericRepository<NiveauSignataire>(DbContext);
            this.PanneRepository = new GenericRepository<Panne>(DbContext);
            this.PossederCaracteristiquesRepository = new GenericRepository<PossederCaracteristiques>(DbContext);
            this.PouvoirSignerRepository = new GenericRepository<PouvoirSigner>(DbContext);
            this.SectionsRepository = new GenericRepository<Sections>(DbContext);
            this.ServiceRepository = new GenericRepository<Service>(DbContext);
            this.SignalerRepository = new GenericRepository<Signaler>(DbContext);
            this.SignataireParNiveauRepository = new GenericRepository<SignataireParNiveau>(DbContext);
            this.SortirRepository = new GenericRepository<Sortir>(DbContext);
            this.SousFamilleRepository = new GenericRepository<SousFamille>(DbContext);
            this.SuppleerRepository = new GenericRepository<Suppleer>(DbContext);
            this.SystemePrivilegeRepository = new GenericRepository<SystemePrivilege>(DbContext);
            this.SystemeTypeDocumentRepository = new GenericRepository<SystemeTypeDocument>(DbContext);
            this.TacheRepository = new GenericRepository<Tache>(DbContext);
            this.TacheEffectueeRepository = new GenericRepository<TacheEffectuee>(DbContext);
            this.TypeAffectationRepository = new GenericRepository<TypeAffectation>(DbContext);
            this.TypeFicheRepository = new GenericRepository<TypeFiche>(DbContext);
            this.TypeMaterielRepository = new GenericRepository<TypeMateriel>(DbContext);
            this.TypePanneRepository = new GenericRepository<TypePanne>(DbContext);
            this.TypeTacheRepository = new GenericRepository<TypeTache>(DbContext);
            this.UniteRepository = new GenericRepository<Unite>(DbContext);
            this.UtilisateurRepository = new GenericRepository<Utilisateur>(DbContext);




        }
        #endregion

        #region Methods
        /// <summary>
        /// Commence une transaction pour l'instance courante
        /// </summary>
        public void BeginTransaction()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Commit les modification en cours sur la contexte EF.
        /// </summary>
        public void Commit()
        {
#if DEBUG
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                DbContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                throw;
            }
#else

            DbContext.SaveChanges();
#endif
        }

        /// <summary>
        /// Rollbacks la transaction courante.
        /// Recrée un contexte EF
        /// </summary>
        public void Rollback()
        {
            this.DbContext = new SinbaContext(this.DbContext.Database.Connection.ConnectionString);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DbContext.Dispose();
        }
        #endregion

        #region Repositories

        #region Data Gmao
        public IRepository<Absence> AbsenceRepository { get; protected set; }
        public IRepository<AccederConfidentialite> AccederConfidentialiteRepository { get; protected set; }
        public IRepository<Affectation> AffectationRepository { get; protected set; }
        public IRepository<AppartenirGroupe> AppartenirGroupeRepository { get; protected set; }
        public IRepository<Article> ArticleRepository { get; protected set; }
        public IRepository<AssocierMateriel> AssocierMaterielRepository { get; protected set; }
        public IRepository<AttribuerFiche> AttribuerFicheRepository { get; protected set; }
        public IRepository<AvoirConfidentialite> AvoirConfidentialiteRepository { get; protected set; }
        public IRepository<BonMagasin> BonMagasinRepository { get; protected set; }
        public IRepository<CaracteriserComposant> CaracteriserComposantRepository { get; protected set; }
        public IRepository<CaracteristiqueComposant> CaracteristiqueComposantRepository { get; protected set; }
        public IRepository<Classemateriel> ClassematerielRepository { get; protected set; }
        public IRepository<Composant> ComposantRepository { get; protected set; }
        public IRepository<ComposerMateriel> ComposerMaterielRepository { get; protected set; }
        public IRepository<Confidentialite> ConfidentialiteRepository { get; protected set; }
        public IRepository<ConstaterPanne> ConstaterPanneRepository { get; protected set; }
        public IRepository<Decision> DecisionRepository { get; protected set; }
        public IRepository<DecisionAffectation> DecisionAffectationRepository { get; protected set; }
        public IRepository<DecisionBon> DecisionBonRepository { get; protected set; }
        public IRepository<DecisionFiche> DecisionFicheRepository { get; protected set; }
        public IRepository<Delai> DelaiRepository { get; protected set; }
        public IRepository<Departement> DepartementRepository { get; protected set; }
        public IRepository<Direction> DirectionRepository { get; protected set; }
        public IRepository<Document> DocumentRepository { get; protected set; }
        public IRepository<Domaine> DomaineRepository { get; protected set; }
        public IRepository<Employe> EmployeRepository { get; protected set; }
        public IRepository<Etape> EtapeRepository { get; protected set; }
        public IRepository<EtatFiche> EtatFicheRepository { get; protected set; }
        public IRepository<FairePartie> FairePartieRepository { get; protected set; }
        public IRepository<Famille> FamilleRepository { get; protected set; }
        public IRepository<FicheDeTravail> FicheDeTravailRepository { get; protected set; }
        public IRepository<Fournisseur> FournisseurRepository { get; protected set; }
        public IRepository<GroupeInventaire> GroupeInventaireRepository { get; protected set; }
        public IRepository<GroupeUtilisateur> GroupeUtilisateurRepository { get; protected set; }
        public IRepository<ImputationAnalytique> ImputationAnalytiqueRepository { get; protected set; }
        public IRepository<Inventaire> InventaireRepository { get; protected set; }
        public IRepository<InventorierMaterield> InventorierMaterieldRepository { get; protected set; }
        public IRepository<Localisation> LocalisationRepository { get; protected set; }
        public IRepository<LocaliserMateriel> LocaliserMaterielRepository { get; protected set; }
        public IRepository<Marque> MarqueRepository { get; protected set; }
        public IRepository<Materiel> MaterielRepository { get; protected set; }
        public IRepository<Model> ModelRepository { get; protected set; }
        public IRepository<NiveauSignataire> NiveauSignataireRepository { get; protected set; }
        public IRepository<Panne> PanneRepository { get; protected set; }
        public IRepository<PossederCaracteristiques> PossederCaracteristiquesRepository { get; protected set; }
        public IRepository<PouvoirSigner> PouvoirSignerRepository { get; protected set; }
        public IRepository<Sections> SectionsRepository { get; protected set; }
        public IRepository<Service> ServiceRepository { get; protected set; }
        public IRepository<Signaler> SignalerRepository { get; protected set; }
        public IRepository<SignataireParNiveau> SignataireParNiveauRepository { get; protected set; }
        public IRepository<Sortir> SortirRepository { get; protected set; }
        public IRepository<SousFamille> SousFamilleRepository { get; protected set; }
        public IRepository<Suppleer> SuppleerRepository { get; protected set; }
        public IRepository<SystemePrivilege> SystemePrivilegeRepository { get; protected set; }
        public IRepository<SystemeTypeDocument> SystemeTypeDocumentRepository { get; protected set; }
        public IRepository<Tache> TacheRepository { get; protected set; }
        public IRepository<TacheEffectuee> TacheEffectueeRepository { get; protected set; }
        public IRepository<TypeAffectation> TypeAffectationRepository { get; protected set; }
        public IRepository<TypeFiche> TypeFicheRepository { get; protected set; }
        public IRepository<TypeMateriel> TypeMaterielRepository { get; protected set; }
        public IRepository<TypePanne> TypePanneRepository { get; protected set; }
        public IRepository<TypeTache> TypeTacheRepository { get; protected set; }
        public IRepository<Unite> UniteRepository { get; protected set; }
        public IRepository<Utilisateur> UtilisateurRepository { get; protected set; }

        #endregion

        #region Data

        public IRepository<Langue> LangueRepository { get; protected set; }
        public IRepository<Site> SiteRepository { get; protected set; }
        public IRepository<SiteCulture> SiteCultureRepository { get; protected set; }
        public IRepository<SiteUtilisateur> SiteUtilisateurRepository { get; protected set; }
        public IRepository<Societe> SocieteRepository { get; protected set; }

        #endregion

        #region Rights Management
        public IRepository<Action> ActionRepository { get; protected set; }
        public IRepository<Fonction> FonctionRepository { get; protected set; }
        public IRepository<FonctionAction> FonctionActionRepository { get; protected set; }
        public IRepository<Profil> ProfilRepository { get; protected set; }
        public IRepository<ProfilRight> ProfilRightRepository { get; protected set; }
        public IRepository<UserRight> UserRightRepository { get; protected set; }
        public IRepository<SinbaUser> SinbaUserRepository { get; protected set; }

        #endregion

        #region Procedure repositories

        public IProcedureRepository ProcedureRepository { get; private set; }
        public IRequestsRepository RequestsRepository { get; private set; }


        #endregion

        #endregion
    }
}
