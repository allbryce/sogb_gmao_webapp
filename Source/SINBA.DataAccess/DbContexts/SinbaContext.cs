using Microsoft.AspNet.Identity.EntityFramework;
using Sinba.BusinessModel.Entity;
using Sinba.DataAccess.Mapping;
using System.Data.Entity;

namespace Sinba.DataAccess
{
    /// <summary>
    /// Contexte d'accès à la base de données du projet
    /// </summary>
    public class SinbaContext : IdentityDbContext<SinbaUser>
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="SinbaContext" /> class.
        /// </summary>
        static SinbaContext()
        {
            //Database.SetInitializer<SinbaContext>(new MigrateDatabaseToLatestVersion<SinbaContext, Migrations.SinbaConfiguration>());
            Database.SetInitializer<SinbaContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinbaContext" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SinbaContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinbaContext" /> class.
        /// </summary>
        public SinbaContext()
            : base("Name=SinbaContext", throwIfV1Schema: false)
        {
        }

        public static SinbaContext Create()
        {
            return new SinbaContext();
        }
        #endregion

        #region DbSets
        public DbSet<Action> Actions { get; set; }
        public DbSet<Fonction> Fonctions { get; set; }
        public DbSet<FonctionAction> FonctionActions { get; set; }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<ProfilRight> ProfilRights { get; set; }
        public DbSet<UserRight> UserRights { get; set; }

        //**************************************************************

        //***************************** Tables Atlas DPV *********************************

        public virtual DbSet<Absence> Absence { get; set; }
        public virtual DbSet<Affectation> Affectation { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<AssocierMateriel> AssocierMateriel { get; set; }
        public virtual DbSet<AttribuerFiche> AttribuerFiche { get; set; }
        public virtual DbSet<BonMagasin> BonMagasin { get; set; }
        public virtual DbSet<CaracteristiqueComposant> CaracteristiqueComposant { get; set; }
        public virtual DbSet<Classemateriel> Classemateriel { get; set; }
        public virtual DbSet<Composant> Composant { get; set; }
        public virtual DbSet<ComposerMateriel> ComposerMateriel { get; set; }
        public virtual DbSet<Confidentialite> Confidentialite { get; set; }
        public virtual DbSet<ConstaterPanne> ConstaterPanne { get; set; }
        public virtual DbSet<Decision> Decision { get; set; }
        public virtual DbSet<DecisionAffectation> DecisionAffectation { get; set; }
        public virtual DbSet<DecisionBon> DecisionBon { get; set; }
        public virtual DbSet<DecisionFiche> DecisionFiche { get; set; }
        public virtual DbSet<Delai> Delai { get; set; }
        public virtual DbSet<Departement> Departement { get; set; }
        public virtual DbSet<Direction> Direction { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Domaine> Domaine { get; set; }
        public virtual DbSet<Employe> Employe { get; set; }
        public virtual DbSet<Etape> Etape { get; set; }
        public virtual DbSet<EtatFiche> EtatFiche { get; set; }
        public virtual DbSet<Famille> Famille { get; set; }
        public virtual DbSet<FicheDeTravail> FicheDeTravail { get; set; }
        public virtual DbSet<Fournisseur> Fournisseur { get; set; }
        public virtual DbSet<GroupeInventaire> GroupeInventaire { get; set; }
        public virtual DbSet<GroupeUtilisateur> GroupeUtilisateur { get; set; }
        public virtual DbSet<ImputationAnalytique> ImputationAnalytique { get; set; }
        public virtual DbSet<Inventaire> Inventaire { get; set; }
        public virtual DbSet<InventorierMaterield> InventorierMaterield { get; set; }
        public virtual DbSet<Localisation> Localisation { get; set; }
        public virtual DbSet<LocaliserMateriel> LocaliserMateriel { get; set; }
        public virtual DbSet<Marque> Marque { get; set; }
        public virtual DbSet<Materiel> Materiel { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<NiveauSignataire> NiveauSignataire { get; set; }
        public virtual DbSet<Panne> Panne { get; set; }
        public virtual DbSet<PossederCaracteristiques> PossederCaracteristiques { get; set; }
        public virtual DbSet<PouvoirSigner> PouvoirSigner { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Signaler> Signaler { get; set; }
        public virtual DbSet<Sortir> Sortir { get; set; }
        public virtual DbSet<SousFamille> SousFamille { get; set; }
        public virtual DbSet<Suppleer> Suppleer { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<SystemePrivilege> SystemePrivilege { get; set; }
        public virtual DbSet<SystemeTypeDocument> SystemeTypeDocument { get; set; }
        public virtual DbSet<Tache> Tache { get; set; }
        public virtual DbSet<TacheEffectuee> TacheEffectuee { get; set; }
        public virtual DbSet<TypeAffectation> TypeAffectation { get; set; }
        public virtual DbSet<TypeFiche> TypeFiche { get; set; }
        public virtual DbSet<TypeMateriel> TypeMateriel { get; set; }
        public virtual DbSet<TypePanne> TypePanne { get; set; }
        public virtual DbSet<TypeTache> TypeTache { get; set; }
        public virtual DbSet<Unite> Unite { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
        public virtual DbSet<SignataireParNiveau> SignataireParNiveau { get; set; }

        //***************************** VUES *********************************

        #endregion

        #region Methods
        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region Parametres

            modelBuilder.Entity<Langue>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Langue>()
                .Property(e => e.Libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Langue>()
                .Property(e => e.LibelleEn)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.IdSociete)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.SiteUtilisateur)
                .WithRequired(e => e.Site)
                .HasForeignKey(e => e.IdSite);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.SiteCulture)
                .WithRequired(e => e.Site)
                .HasForeignKey(e => e.IdSite);

            modelBuilder.Entity<SiteCulture>()
                .Property(e => e.IdSite)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCulture>()
                .Property(e => e.IdSysCulture)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SiteUtilisateur>()
                .Property(e => e.IdSite)
                .IsUnicode(false);

            modelBuilder.Entity<Societe>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Societe>()
                .Property(e => e.Libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Societe>()
                .Property(e => e.LibelleEn)
                .IsUnicode(false);

            modelBuilder.Entity<Societe>()
                .HasMany(e => e.Site)
                .WithRequired(e => e.Societe)
                .HasForeignKey(e => e.IdSociete)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Societe>()
              .Property(e => e.MonnaieDeBase)
              .IsUnicode(false);

            #endregion




            #region Atlas DPV


            modelBuilder.Entity<BonMagasin>()
                .HasMany(e => e.DecisionBon)
                .WithRequired(e => e.BonMagasin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BonMagasin>()
                .HasMany(e => e.Sortir)
                .WithRequired(e => e.BonMagasin)
                .WillCascadeOnDelete(false);

           
            modelBuilder.Entity<CaracteristiqueComposant>()
                .HasMany(e => e.PossederCaracteristiques)
                .WithRequired(e => e.CaracteristiqueComposant)
                .HasForeignKey(e => e.CaracteristiqueComposantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaracteristiqueComposant>()
                .HasMany(e => e.Composant)
                .WithMany(e => e.CaracteristiqueComposant)
                .Map(m => m.ToTable("CaracteriserComposant").MapLeftKey("CaracteristiqueComposantId").MapRightKey("ComposantId"));

          

            modelBuilder.Entity<Composant>()
                .Property(e => e.LibelleComposant)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Composant>()
                .HasMany(e => e.ComposerMateriel)
                .WithRequired(e => e.Composant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Composant>()
                .HasMany(e => e.PossederCaracteristiques)
                .WithRequired(e => e.Composant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Confidentialite>()
                .HasMany(e => e.Confidentialite1)
                .WithMany(e => e.Confidentialite2)
                .Map(m => m.ToTable("AccederConfidentialite").MapLeftKey("ConfidentialiteID").MapRightKey("ConfidentialiteAccederID"));

            modelBuilder.Entity<Confidentialite>()
                .HasMany(e => e.Utilisateur)
                .WithMany(e => e.Confidentialite)
                .Map(m => m.ToTable("AvoirConfidentialite").MapLeftKey("ConfidentialiteId").MapRightKey("UtilisateurId"));


            modelBuilder.Entity<Decision>()
                .HasMany(e => e.DecisionAffectation)
                .WithRequired(e => e.Decision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Decision>()
                .HasMany(e => e.DecisionBon)
                .WithRequired(e => e.Decision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Decision>()
                .HasMany(e => e.DecisionFiche)
                .WithRequired(e => e.Decision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DecisionBon>()
                .Property(e => e.Commentaire)
                .IsUnicode(false);


            modelBuilder.Entity<Delai>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.Delai)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Departement>()
                .HasMany(e => e.Service)
                .WithRequired(e => e.Departement)
                .HasForeignKey(e => e.DepartementId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Direction>()
                .Property(e => e.LibelleDirection)
                .IsUnicode(false);

            modelBuilder.Entity<Direction>()
                .HasMany(e => e.Departement)
                .WithRequired(e => e.Direction)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Document>()
                .HasMany(e => e.Affectation)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.BonMagasin)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasOptional(e => e.Etape)
                .WithRequired(e => e.Document);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Domaine>()
                .HasMany(e => e.Composant)
                .WithRequired(e => e.Domaine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Domaine>()
                .HasMany(e => e.Utilisateur)
                .WithMany(e => e.Domaine)
                .Map(m => m.ToTable("FairePartie").MapLeftKey("DomaineId").MapRightKey("UtilisateurId"));

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.Affectation)
                .WithRequired(e => e.Employe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.Employe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.Tache)
                .WithRequired(e => e.Employe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.TacheEffectuee)
                .WithRequired(e => e.Employe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etape>()
                .HasMany(e => e.DecisionAffectation)
                .WithRequired(e => e.Etape)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etape>()
                .HasMany(e => e.DecisionBon)
                .WithRequired(e => e.Etape)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etape>()
                .HasMany(e => e.DecisionFiche)
                .WithRequired(e => e.Etape)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etape>()
                .HasMany(e => e.PouvoirSigner)
                .WithRequired(e => e.Etape)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etape>()
                .HasMany(e => e.Suppleer)
                .WithRequired(e => e.Etape)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EtatFiche>()
                .Property(e => e.LibelleEtatFiche)
                .IsUnicode(false);

            modelBuilder.Entity<EtatFiche>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.EtatFiche)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Famille>()
                .HasMany(e => e.SousFamille)
                .WithRequired(e => e.Famille)
                .WillCascadeOnDelete(false);
           

            modelBuilder.Entity<FicheDeTravail>()
                .Property(e => e.FicheDeTravailParentId)
                .IsUnicode(false);

           
            modelBuilder.Entity<FicheDeTravail>()
                .Property(e => e.Observation)
                .IsUnicode(false);

            modelBuilder.Entity<FicheDeTravail>()
                .HasMany(e => e.AttribuerFiche)
                .WithRequired(e => e.FicheDeTravail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FicheDeTravail>()
                .HasMany(e => e.ConstaterPanne)
                .WithRequired(e => e.FicheDeTravail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FicheDeTravail>()
                .HasMany(e => e.DecisionFiche)
                .WithRequired(e => e.FicheDeTravail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FicheDeTravail>()
                .HasMany(e => e.Signaler)
                .WithRequired(e => e.FicheDeTravail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FicheDeTravail>()
                .HasMany(e => e.Sortir)
                .WithRequired(e => e.FicheDeTravail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FicheDeTravail>()
                .HasMany(e => e.TacheEffectuee)
                .WithRequired(e => e.FicheDeTravail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.NomFournisseur)
                .IsUnicode(false);

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Fournisseur>()
                .HasMany(e => e.Materiel)
                .WithRequired(e => e.Fournisseur)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupeInventaire>()
                .Property(e => e.LibelleGroupeInventaire)
                .IsUnicode(false);

            modelBuilder.Entity<GroupeUtilisateur>()
                .Property(e => e.LibelleGroupeUtilisateur)
                .IsUnicode(false);

            modelBuilder.Entity<GroupeUtilisateur>()
                .HasMany(e => e.Utilisateur)
                .WithMany(e => e.GroupeUtilisateur)
                .Map(m => m.ToTable("AppartenirGroupe").MapLeftKey("GroupeUtilisateurId").MapRightKey("UtilisateurId"));

            modelBuilder.Entity<ImputationAnalytique>()
                .Property(e => e.LibelleImputationAnalytique)
                .IsUnicode(false);

            modelBuilder.Entity<ImputationAnalytique>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.ImputationAnalytique)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Inventaire>()
                .Property(e => e.Commentaire)
                .IsUnicode(false);

            modelBuilder.Entity<Inventaire>()
                .HasMany(e => e.InventorierMaterield)
                .WithRequired(e => e.Inventaire)
                .HasForeignKey(e => e.NumeroInventaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InventorierMaterield>()
                .Property(e => e.LocalisationActuelle)
                .IsUnicode(false);

          
            modelBuilder.Entity<Localisation>()
                .Property(e => e.LibelleLocalisaton)
                .IsUnicode(false);

            modelBuilder.Entity<Localisation>()
                .HasMany(e => e.Affectation)
                .WithRequired(e => e.Localisation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Localisation>()
                .HasMany(e => e.InventorierMaterield)
                .WithRequired(e => e.Localisation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Localisation>()
                .HasMany(e => e.LocaliserMateriel)
                .WithRequired(e => e.Localisation)
                .WillCascadeOnDelete(false);

           
            modelBuilder.Entity<Marque>()
                .Property(e => e.LibelleMarque)
                .IsUnicode(false);

            modelBuilder.Entity<Materiel>()
                .Property(e => e.LibelleMateriel)
                .IsUnicode(false);

           

            modelBuilder.Entity<Materiel>()
                .Property(e => e.PrixAchat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Materiel>()
                .Property(e => e.NumeroBonCommande)
                .IsUnicode(false);

            modelBuilder.Entity<Materiel>()
                .Property(e => e.NumeroSerie)
                .IsUnicode(false);

            modelBuilder.Entity<Materiel>()
                .Property(e => e.NumeroImmobilisation)
                .IsUnicode(false);

            modelBuilder.Entity<Materiel>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.Affectation)
                .WithRequired(e => e.Materiel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.AssocierMateriel)
                .WithRequired(e => e.Materiel)
                .HasForeignKey(e => e.MaterielId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.AssocierMateriel1)
                .WithRequired(e => e.Materiel1)
                .HasForeignKey(e => e.MaterielAssocieId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.ComposerMateriel)
                .WithRequired(e => e.Materiel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.Materiel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.InventorierMaterield)
                .WithRequired(e => e.Materiel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.LocaliserMateriel)
                .WithRequired(e => e.Materiel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materiel>()
                .HasMany(e => e.PossederCaracteristiques)
                .WithRequired(e => e.Materiel)
                .WillCascadeOnDelete(false);

           
            modelBuilder.Entity<Model>()
                .Property(e => e.LibelleModel)
                .IsUnicode(false);

            modelBuilder.Entity<NiveauSignataire>()
                .Property(e => e.LibelleNiveauSignataire)
                .IsUnicode(false);

            modelBuilder.Entity<NiveauSignataire>()
                .HasMany(e => e.PouvoirSigner)
                .WithRequired(e => e.NiveauSignataire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NiveauSignataire>()
                .HasMany(e => e.SignataireParNiveau)
                .WithRequired(e => e.NiveauSignataire)
                .WillCascadeOnDelete(false);

           

            modelBuilder.Entity<Panne>()
                .Property(e => e.LibellePanne)
                .IsUnicode(false);

            modelBuilder.Entity<Panne>()
                .HasMany(e => e.ConstaterPanne)
                .WithRequired(e => e.Panne)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Panne>()
                .HasMany(e => e.Signaler)
                .WithRequired(e => e.Panne)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sections>()
                .Property(e => e.LibelleSections)
                .IsUnicode(false);

            modelBuilder.Entity<Sections>()
                .HasMany(e => e.AttribuerFiche)
                .WithRequired(e => e.Sections)
                .WillCascadeOnDelete(false);
            

            modelBuilder.Entity<Service>()
                .Property(e => e.LibelleService)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Affectation)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.ServiceId);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Affectation1)
                .WithOptional(e => e.Service1)
                .HasForeignKey(e => e.SerciceResponsableId);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Employe)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.FicheDeTravail1)
                .WithOptional(e => e.Service1)
                .HasForeignKey(e => e.ServiceEmmetteurId);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Sections)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

           
            modelBuilder.Entity<Signaler>()
                .Property(e => e.Commentaire)
                .IsUnicode(false);

           
            modelBuilder.Entity<Sortir>()
                .Property(e => e.PrixUnitaire)
                .HasPrecision(19, 4);

           
            modelBuilder.Entity<SousFamille>()
                .Property(e => e.LibelleSousFamille)
                .IsUnicode(false);

           
            modelBuilder.Entity<Suppleer>()
                .Property(e => e.NumeroOrdre)
                .IsUnicode(false);

            modelBuilder.Entity<SystemePrivilege>()
                .Property(e => e.LibelleSystemPrivilege)
                .IsUnicode(false);

            modelBuilder.Entity<SystemePrivilege>()
                .HasMany(e => e.PouvoirSigner)
                .WithRequired(e => e.SystemePrivilege)
                .HasForeignKey(e => e.SystemePrivilegeId)
                .WillCascadeOnDelete(false);

          
            modelBuilder.Entity<SystemeTypeDocument>()
                .Property(e => e.LibelleTypeDocument)
                .IsUnicode(false);

            modelBuilder.Entity<SystemeTypeDocument>()
                .HasMany(e => e.Document)
                .WithRequired(e => e.SystemeTypeDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tache>()
                .Property(e => e.LibelleTache)
                .IsUnicode(false);

            modelBuilder.Entity<Tache>()
                .HasMany(e => e.TacheEffectuee)
                .WithRequired(e => e.Tache)
                .WillCascadeOnDelete(false);
         
            modelBuilder.Entity<TacheEffectuee>()
                .Property(e => e.Commentaire)
                .IsUnicode(false);
          
            modelBuilder.Entity<TypeAffectation>()
                .Property(e => e.LibelleTypeAffectation)
                .IsUnicode(false);

            modelBuilder.Entity<TypeAffectation>()
                .HasMany(e => e.Affectation)
                .WithRequired(e => e.TypeAffectation)
                .WillCascadeOnDelete(false);
          
            modelBuilder.Entity<TypeFiche>()
                .Property(e => e.LibelleTypeFiche)
                .IsUnicode(false);

            modelBuilder.Entity<TypeFiche>()
                .HasMany(e => e.FicheDeTravail)
                .WithRequired(e => e.TypeFiche)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeMateriel>()
                .Property(e => e.LibelleTypeMateriel)
                .IsUnicode(false);

            modelBuilder.Entity<TypePanne>()
                .Property(e => e.LibelleTypePanne)
                .IsUnicode(false);

            modelBuilder.Entity<TypeTache>()
                .Property(e => e.LibelleTypeTache)
                .IsUnicode(false);

            modelBuilder.Entity<TypeTache>()
                .HasMany(e => e.Tache)
                .WithRequired(e => e.TypeTache)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unite>()
                .Property(e => e.LibelleUnite)
                .IsUnicode(false);

            modelBuilder.Entity<Unite>()
                .HasMany(e => e.PossederCaracteristiques)
                .WithRequired(e => e.Unite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.NomComplet)
                .IsUnicode(false);

           
            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.Absence)
                .WithRequired(e => e.Utilisateur)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.Suppleer)
                .WithRequired(e => e.Utilisateur)
                .HasForeignKey(e => e.UtilisateurId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.Suppleer1)
                .WithRequired(e => e.Utilisateur1)
                .HasForeignKey(e => e.SuppleantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.Tache)
                .WithRequired(e => e.Utilisateur)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.SignataireParNiveau)
                .WithRequired(e => e.Utilisateur)
                .WillCascadeOnDelete(false);


            #endregion

            // Gestion des droits

            //****************** Ajouter pour gérer les sites par utilisateurs

            modelBuilder.Entity<SinbaUser>()
                .HasMany(e => e.SiteUtilisateurs)
                .WithRequired(e => e.SinbaUser)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);


            //****************** Fin Ajouter pour gérer les sites par utilisateurs

            modelBuilder.Configurations.Add(new ListeActionMap());
            modelBuilder.Configurations.Add(new ListeFonctionMap());
            modelBuilder.Configurations.Add(new FonctionActionMap());
            modelBuilder.Configurations.Add(new ProfilMap());
            modelBuilder.Configurations.Add(new ProfilRightMap());
            modelBuilder.Configurations.Add(new UserRightMap());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SinbaUser>().ToTable("User", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole", "dbo");


        }

        #endregion
    }
}
