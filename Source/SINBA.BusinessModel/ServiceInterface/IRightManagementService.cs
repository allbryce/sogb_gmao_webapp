using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using System.Collections.Generic;

namespace Sinba.BusinessModel.ServiceInterface
{
    /// <summary>
    /// Interface du service RightManagement
    /// </summary>
    /// <remarks>
    /// Add methods signatures here.
    /// </remarks>
    public interface IRightManagementService
    {
        #region Action
        ListDto<Action> GetActionList();

        SimpleDto<Action> GetAction(string code);

        BoolDto InsertAction(Action action);

        BoolDto DeleteAction(string code);

        BoolDto UpdateAction(Action action);

        BoolDto IsActionNomUnique(string nom, string code = null);

        BoolDto IsActionCodeUnique(string code);

        BoolDto IsActionUsed(string codeAction);
        #endregion

        #region Fonction
        ListDto<Fonction> GetFonctionList(bool includeSuperAdmin = false);

        ListDto<Fonction> GetFonctionListWithDependencies(bool includeSuperAdmin);

        ListDto<FonctionAction> GetFonctionActionList(string codeFonction = null);

        SimpleDto<Fonction> GetFonction(string code);

        BoolDto InsertFonction(Fonction fonction);

        BoolDto DeleteFonction(string code);

        BoolDto UpdateFonction(Fonction fonction);

        BoolDto IsFonctionCodeUnique(string code);

        BoolDto IsFonctionUsed(string codeFonction);
        #endregion

        #region Profil
        ListDto<Profil> GetProfilList();

        ListDto<Profil> GetProfilListWithDependencies();

        SimpleDto<Profil> GetProfil(long id);

        BoolDto InsertProfil(Profil profil);

        BoolDto UpdateProfil(Profil profil, bool updateProfilRights);

        BoolDto DeleteProfil(long id);

        BoolDto IsProfilUsed(long id);

        SimpleDto<Profil> GetProfil(string name);
        #endregion

        #region User rights
        ListDto<UserRight> GetUserRightList(string idUser);

        SimpleDto<UserRight> GetUserRight(string idUser, string codeFonction, string codeAction);

        BoolDto InsertUserRight(UserRight userRight);

        BoolDto DeleteUserRight(UserRight userRight);

        BoolDto UpdateUserRight(UserRight userRight);
        #endregion

        #region User

        ListDto<SinbaUser> GetSinbaUserList();

        SimpleDto<SinbaUser> GetSinbaUser(string id);

        ListDto<SinbaUser> GetSinbaUserForSelectedIds(List<string> ids);
        #endregion

        #region Societe

        ListDto<Societe> GetSocieteList();

        SimpleDto<Societe> GetSociete(string id);

        BoolDto InsertSociete(Societe societe);

        BoolDto UpdateSociete(Societe societe);

        BoolDto DeleteSociete(string id);

        BoolDto IsSocieteIdUnique(string id);

        BoolDto IsSocieteIdUsed(string id, string idHidden);

        BoolDto IsSocieteUsed(string id);
        #endregion

        #region Site
        SimpleDto<Site> GetSite(string id);

        ListDto<Site> GetSiteList();

        ListDto<Site> GetSiteListWithDependencies();

        BoolDto InsertSite(Site site);

        BoolDto UpdateSite(Site site);

        BoolDto DeleteSite(string id);

        BoolDto IsSiteIdUsed(string id, string idHidden);

        BoolDto IsSiteUsed(string id);

        #endregion

        #region SiteUtilisateur
        ListDto<SiteUtilisateur> GetSiteUtilisateurList(string idUser = null);

        BoolDto UpdateSiteUtilisateur(SiteUtilisateurViewModel siteUtilisateurViewModel);

        BoolDto InsertSiteUtilisateur(SiteUtilisateur siteUtilisateur);

         void DeleteSiteUtilisateurByIdUser(string idUser);

        BoolDto DeleteSiteUtilisateur(string idUser, string idSite);

        #endregion

    }
}
