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



        #region Procedure repositories

        IProcedureRepository ProcedureRepository { get;  }
        IRequestsRepository RequestsRepository { get;  }

        #endregion

    }

}
