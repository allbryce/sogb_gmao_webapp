using Dev.Tools.Architecture.DataAccess;
using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using SINBA.BusinessModel.Data;
using SINBA.BusinessModel.Entity;

namespace SINBA.DataAccessMock
{
    /// <summary>
    /// Définit la classe mockée d'accès aux données pour les tests unitaires.
    /// </summary>
    [DataLog]
    [DataExceptionHandler]
    public class EMSAgroUnitOfWorkMock : ISINBAUnitOfWork
    {
        // Use the class  Dev.Tools.Architecture.DataAccess.GenericTestRepository<TEntity> instead of Repository

        #region Methods
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion

        #region Repositories
        /// <summary>
        /// Gets or sets the bloc repository.
        /// </summary>
        /// <value>
        /// The bloc repository.
        /// </value>
        public IRepository<Bloc> BlocRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the culture utilisateur repository.
        /// </summary>
        /// <value>
        /// The culture utilisateur repository.
        /// </value>
        public IRepository<CultureUtilisateur> CultureUtilisateurRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the division repository.
        /// </summary>
        /// <value>
        /// The division repository.
        /// </value>
        public IRepository<Division> DivisionRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the categorie unite mesure repository.
        /// </summary>
        /// <value>
        /// The categorie unite mesure repository.
        /// </value>
        public IRepository<CategorieUniteMesure> CategorieUniteMesureRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the culture repository.
        /// </summary>
        /// <value>
        /// The culture repository.
        /// </value>
        public IRepository<Culture> CultureRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the entite repository.
        /// </summary>
        /// <value>
        /// The entite repository.
        /// </value>
        public IRepository<Entite> EntiteRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the espacement plantation repository.
        /// </summary>
        /// <value>
        /// The espacement plantation repository.
        /// </value>
        public IRepository<EspacementPlantation> EspacementPlantationRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the etat arbre repository.
        /// </summary>
        /// <value>
        /// The etat arbre repository.
        /// </value>
        public IRepository<EtatArbre> EtatArbreRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the langue repository.
        /// </summary>
        /// <value>
        /// The langue repository.
        /// </value>
        public IRepository<Langue> LangueRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the pays repository.
        /// </summary>
        /// <value>
        /// The pays repository.
        /// </value>
        public IRepository<Pays> PaysRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the unite mesure repository.
        /// </summary>
        /// <value>
        /// The unite mesure repository.
        /// </value>
        public IRepository<UniteMesure> UniteMesureRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the ville repository.
        /// </summary>
        /// <value>
        /// The ville repository.
        /// </value>
        public IRepository<Ville> VilleRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the log repository.
        /// </summary>
        /// <value>
        /// The log repository.
        /// </value>
        public IRepository<Log> LogRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the log connexion repository.
        /// </summary>
        /// <value>
        /// The log connexion repository.
        /// </value>
        public IRepository<LogConnexion> LogConnexionRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the nombre emplacement repository.
        /// </summary>
        /// <value>
        /// The nombre emplacement repository.
        /// </value>
        public IRepository<NombreEmplacement> NombreEmplacementRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the pourcentage exploitation bloc repository.
        /// </summary>
        /// <value>
        /// The pourcentage exploitation bloc repository.
        /// </value>
        public IRepository<PourcentageExploitationBloc> PourcentageExploitationBlocRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the recensement repository.
        /// </summary>
        /// <value>
        /// The recensement repository.
        /// </value>
        public IRepository<Recensement> RecensementRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the recensement etat arbre repository.
        /// </summary>
        /// <value>
        /// The recensement etat arbre repository.
        /// </value>
        public IRepository<RecensementEtatArbre> RecensementEtatArbreRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the releve pont bascule repository.
        /// </summary>
        /// <value>
        /// The releve pont bascule repository.
        /// </value>
        public IRepository<RelevePontBascule> RelevePontBasculeRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the releve pont bascule detail repository.
        /// </summary>
        /// <value>
        /// The releve pont bascule detail repository.
        /// </value>
        public IRepository<RelevePontBasculeDetail> RelevePontBasculeDetailRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the site repository.
        /// </summary>
        /// <value>
        /// The site repository.
        /// </value>
        public IRepository<Site> SiteRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the site culture repository.
        /// </summary>
        /// <value>
        /// The site culture repository.
        /// </value>
        public IRepository<SiteCulture> SiteCultureRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the site utilisateur repository.
        /// </summary>
        /// <value>
        /// The site utilisateur repository.
        /// </value>
        public IRepository<SiteUtilisateur> SiteUtilisateurRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the societe repository.
        /// </summary>
        /// <value>
        /// The societe repository.
        /// </value>
        public IRepository<Societe> SocieteRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the temporary budget nombre jours recolte repository.
        /// </summary>
        /// <value>
        /// The temporary budget nombre jours recolte repository.
        /// </value>
        public IRepository<TempBudgetNombreJours> TempBudgetNombreJoursRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the temporary budget product annuelle bloc repository.
        /// </summary>
        /// <value>
        /// The temporary budget product annuelle bloc repository.
        /// </value>
        public IRepository<TempBudgetProdAnnuelleBloc> TempBudgetProdAnnuelleBlocRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the temporary budget repartition product mois repository.
        /// </summary>
        /// <value>
        /// The temporary budget repartition product mois repository.
        /// </value>
        public IRepository<TempBudgetRepartitionProdMois> TempBudgetRepartitionProdMoisRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the temporary donnees industrielle repository.
        /// </summary>
        /// <value>
        /// The temporary donnees industrielle repository.
        /// </value>
        public IRepository<TempDonneeIndustrielle> TempDonneesIndustrielleRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the traduction repository.
        /// </summary>
        /// <value>
        /// The traduction repository.
        /// </value>
        public IRepository<Traduction> TraductionRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the unite repository.
        /// </summary>
        /// <value>
        /// The unite repository.
        /// </value>
        public IRepository<Unite> UniteRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        public IRepository<SINBAUser> UserRepository { get; protected set; }

        /// <summary>
        /// Gets or sets the action repository.
        /// </summary>
        /// <value>
        /// The action repository.
        /// </value>
        public IRepository<Action> ActionRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the fonction repository.
        /// </summary>
        /// <value>
        /// The fonction repository.
        /// </value>
        public IRepository<Fonction> FonctionRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the fonction action repository.
        /// </summary>
        /// <value>
        /// The fonction action repository.
        /// </value>
        public IRepository<FonctionAction> FonctionActionRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the profil repository.
        /// </summary>
        /// <value>
        /// The profil repository.
        /// </value>
        public IRepository<Profil> ProfilRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the profil right repository.
        /// </summary>
        /// <value>
        /// The profil right repository.
        /// </value>
        public IRepository<ProfilRight> ProfilRightRepository { get; protected set; }
        /// <summary>
        /// Gets or sets the user right repository.
        /// </summary>
        /// <value>
        /// The user right repository.
        /// </value>
        public IRepository<UserRight> UserRightRepository { get; protected set; }
        public IRepository<SINBAUser> EmsAgroUserRepository { get; protected set; }
        public IRepository<Section> SectionRepository { get; protected set; }
        public IRepository<SectionUtilisateur> SectionUtilisateurRepository { get; protected set; }
        public IRepository<BlocTechniquePreparationTerrain> BlocTechniquePreparationTerrainRepository { get; protected set; }
        public IRepository<Liste> ListeRepository { get; protected set; }
        public IRepository<ListeType> ListeTypeRepository { get; protected set; }
        public IRepository<Operation> OperationRepository { get; protected set; }
        public IRepository<FamilleOperation> FamilleOperationRepository { get; protected set; }
        public IRepository<QuantiteTache> QuantiteTacheRepository { get; protected set; }
        public IRepository<OTValidateurFamille> OTValidateurFamilleRepository { get; protected set; }
        public IRepository<OTValidateur> OTValidateurRepository { get; protected set; }
        public IRepository<Champ> ChampsRepository { get; protected set; }
        public IRepository<ChampOperation> ChampsOperationRepository { get; protected set; }
        public IRepository<ValeursChampsPrestation> ValeursChampsPrestationRepository { get; protected set; }
        public IRepository<Prestation> PrestationRepository { get; protected set; }
        public IRepository<TypeUtilisateur> TypeUtilisateurRepository { get; protected set; }
        public IRepository<Equipe> EquipeRepository { get; protected set; }
        public IRepository<SousTraitance> SousTraitanceRepository { get; protected set; }
        public IRepository<OT> OTRepository { get; protected set; }
        public IRepository<OTCredit> OTCreditRepository { get; protected set; }
        public IRepository<OTHistoriqueChangement> OTHistoriqueChangementRepository { get; protected set; }
        public IRepository<OTOperation> OTOperationRepository { get; protected set; }
        public IRepository<OTOperationBloc> OTOperationBlocRepository { get; protected set; }
        public IRepository<OTUtilisateurValidation> OTUtilisateurValidationRepository { get; protected set; }
        public IRepository<Travailleur> TravailleurRepository { get; protected set; }
        public IRepository<EquipeTravailleur> EquipeTravailleurRepository { get; protected set; }
        public IRepository<BlocSystemeExploitation> BlocSystemeExploitationRepository { get; protected set; }
        public IRepository<Prime> PrimeRepository { get; protected set; }
        public IRepository<CodePrimeSage> CodePrimeSageRepository { get; protected set; }
        public IRepository<PrimeFonction> PrimeFonctionRepository { get; protected set; }
        public IRepository<PrimeService> PrimeServiceRepository { get; protected set; }
        public IRepository<FonctionTravailleur> FonctionTravailleurRepository { get; protected set; }
        public IRepository<ServiceTravailleur> ServiceTravailleurRepository { get; protected set; }
        public IRepository<Parcelle> ParcelleRepository { get; protected set; }
        public IRepository<OTOperationParcelle> OTOperationParcelleRepository { get; protected set; }
        public IRepository<OTUtilisateurReception> OTUtilisateurReceptionRepository { get; protected set; }
        public IRepository<OTOperationEquipe> OTOperationEquipeRepository { get; protected set; }
        public IRepository<PartSaigneeTravailleur> PartSaigneeTravailleurRepository { get; protected set; }
        public IRepository<PartSaignee> PartSaigneeRepository { get; protected set; }
        public IRepository<Alternance> AlternanceRepository { get; protected set; }
        public IRepository<FrequenceSaigneeAlternance> FrequenceSaigneeAlternanceRepository { get; protected set; }
        public IRepository<FrequenceSaignee> FrequenceSaigneeRepository { get; protected set; }
        #endregion
    }
}