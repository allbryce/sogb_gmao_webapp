using Dev.Tools.Architecture.DataAccess;
using Dev.Tools.EntityFramework;
using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.RepositoryInterface;
using Sinba.DataAccess.Repositories;

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





            this.ProcedureRepository = new ProcedureRepository(DbContext);
            this.RequestsRepository = new RequestsRepository(DbContext);


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

        #region Data Atlas DPV


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
