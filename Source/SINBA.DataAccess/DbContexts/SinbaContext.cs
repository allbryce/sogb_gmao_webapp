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

        public virtual DbSet<Langue> Langue { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<SiteUtilisateur> SiteUtilisateur { get; set; }
        public virtual DbSet<Societe> Societe { get; set; }
        public virtual DbSet<SiteCulture> SiteCulture { get; set; }

        //***************************** Tables Atlas DPV *********************************


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
