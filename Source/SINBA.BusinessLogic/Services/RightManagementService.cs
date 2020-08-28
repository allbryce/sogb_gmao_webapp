using Dev.Tools.Logger;
using Sinba.BusinessModel.Data;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Resources;
using System.Collections.Generic;
using System.Linq;

namespace Sinba.BusinessLogic.Services
{
    /// <summary>
    /// Implémentation du service Right management
    /// </summary>
    [ServiceLog]
    [SinbaServiceExceptionHandler]
    public class RightManagementService : SinbaServiceBase, IRightManagementService
    {
        #region Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RightManagementService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RightManagementService(ISinbaUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
        #endregion

        #region Methods
        #region Action
        public ListDto<Action> GetActionList()
        {
            ListDto<Action> lst = new ListDto<Action>();
            this.UnitOfWork.ActionRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ActionRepository.Get();
            return lst;
        }

        public SimpleDto<Action> GetAction(string code)
        {
            this.UnitOfWork.ActionRepository.EnableTracking = false;
            SimpleDto<Action> dto = new SimpleDto<Action>();
            dto.Value = this.UnitOfWork.ActionRepository.Get(a => a.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return dto;
        }

        public BoolDto InsertAction(Action action)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ActionRepository.Insert(action);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto DeleteAction(string code)
        {
            BoolDto dto = new BoolDto();
            if (this.UnitOfWork.ActionRepository.Get(a => a.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase)).Any())
            {
                this.UnitOfWork.ActionRepository.Delete(code);
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto UpdateAction(Action action)
        {
            BoolDto dto = new BoolDto();
            Action dbAction = this.UnitOfWork.ActionRepository.Get(a => a.Code.Equals(action.Code, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (dbAction != null)
            {
                dbAction.Nom = action.Nom;
                dbAction.Log = action.Log;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto IsActionNomUnique(string nom, string code = null)
        {
            BoolDto dto = new BoolDto();
            if (!string.IsNullOrWhiteSpace(code))
            {
                dto.Value = !this.UnitOfWork.ActionRepository.Get(a => a.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase) && a.Nom.Equals(nom, System.StringComparison.OrdinalIgnoreCase)).Any();
            }
            else
            {
                dto.Value = !this.UnitOfWork.ActionRepository.Get(a => a.Nom.Equals(nom, System.StringComparison.OrdinalIgnoreCase)).Any();
            }
            return dto;
        }

        public BoolDto IsActionCodeUnique(string code)
        {
            BoolDto dto = new BoolDto();
            dto.Value = !this.UnitOfWork.ActionRepository.Get(a => a.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase)).Any();
            return dto;
        }

        public BoolDto IsActionUsed(string codeAction)
        {
            BoolDto dto = new BoolDto();
            bool userRight = this.UnitOfWork.UserRightRepository.Get(ur => ur.CodeAction.Equals(codeAction, System.StringComparison.OrdinalIgnoreCase)).Any();
            bool profilRight = this.UnitOfWork.ProfilRightRepository.Get(pr => pr.CodeAction.Equals(codeAction, System.StringComparison.OrdinalIgnoreCase)).Any();

            dto.Value = userRight || profilRight;

            return dto;
        }
        #endregion

        #region Fonction
        public ListDto<Fonction> GetFonctionList(bool includeSuperAdmin = false)
        {
            ListDto<Fonction> lst = new ListDto<Fonction>();
            this.UnitOfWork.FonctionRepository.EnableTracking = false;
            if (includeSuperAdmin)
            {
                lst.Value = this.UnitOfWork.FonctionRepository.Get();
            }
            else
            {
                lst.Value = this.UnitOfWork.FonctionRepository.Get(f => (!f.SuperAdmin.HasValue || !f.SuperAdmin.Value));
            }

            return lst;
        }

        public ListDto<Fonction> GetFonctionListWithDependencies(bool includeSuperAdmin)
        {
            ListDto<Fonction> lst = new ListDto<Fonction>();
            List<Fonction> fonctions;
            this.UnitOfWork.FonctionRepository.EnableTracking = false;
            // RTO: nouvelle technique à utiliser
            //var query = this.UnitOfWork.FonctionRepository.Get(null,
            //    f => f.Include(fa => fa.FonctionActions.Select(ur => ur.UserRights))
            //    .Include(fa => fa.FonctionActions.Select(pr => pr.ProfilRights)).OrderBy(x => x.Code), null)
            //    .Select(x => new Fonction()
            //    {
            //        Code = x.Code,
            //        MenuPath = x.MenuPath,
            //        Log = x.Log,
            //        SuperAdmin = x.SuperAdmin,
            //        IsUsed = x.FonctionActions.Any(fa => fa.ProfilRights.Any()) || x.FonctionActions.Any(fa => fa.UserRights.Any())
            //    });
            //if (!includeSuperAdmin)
            //{
            //    query = query.Where(f => (!f.SuperAdmin.HasValue || !f.SuperAdmin.Value));
            //}

            if (includeSuperAdmin)
            {
                fonctions = this.UnitOfWork.FonctionRepository.Get().ToList();
            }
            else
            {
                fonctions = this.UnitOfWork.FonctionRepository.Get(f => (!f.SuperAdmin.HasValue || !f.SuperAdmin.Value)).ToList();
            }

            // Contrôle de dépendances
            if (fonctions != null && fonctions.Any())
            {
                var profilRight = this.UnitOfWork.ProfilRightRepository.Get().ToList();
                var userRight = this.UnitOfWork.UserRightRepository.Get().ToList();
                foreach (var fonction in fonctions)
                {
                    fonction.IsUsed = profilRight.Where(x => x.CodeFonction.Equals(fonction.Code)).Any() || userRight.Where(x => x.CodeFonction.Equals(fonction.Code)).Any();
                }
            }
            lst.Value = fonctions;

            return lst;
        }

        public ListDto<FonctionAction> GetFonctionActionList(string codeFonction = null)
        {
            this.UnitOfWork.FonctionActionRepository.EnableTracking = false;
            ListDto<FonctionAction> lst = new ListDto<FonctionAction>();
            if (string.IsNullOrWhiteSpace(codeFonction))
            {
                lst.Value = this.UnitOfWork.FonctionActionRepository.Get(null, null, fa => fa.Fonction, fa => fa.Action);
            }
            else
            {
                lst.Value = this.UnitOfWork.FonctionActionRepository.Get(fa => fa.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase), null, fa => fa.Fonction, fa => fa.Action);
            }
            return lst;
        }

        public SimpleDto<Fonction> GetFonction(string code)
        {
            this.UnitOfWork.FonctionRepository.EnableTracking = false;
            SimpleDto<Fonction> dto = new SimpleDto<Fonction>();
            dto.Value = this.UnitOfWork.FonctionRepository.Get(f => f.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase), null, f => f.FonctionActions).FirstOrDefault();
            return dto;
        }

        public BoolDto InsertFonction(Fonction fonction)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.FonctionRepository.Insert(fonction);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto DeleteFonction(string code)
        {
            BoolDto dto = new BoolDto();
            if (this.UnitOfWork.FonctionRepository.Get(f => f.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase)).Any())
            {
                this.UnitOfWork.FonctionRepository.Delete(code);
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto UpdateFonction(Fonction fonction)
        {
            BoolDto dto = new BoolDto();
            Fonction dbFonction = this.UnitOfWork.FonctionRepository.Get(f => f.Code.Equals(fonction.Code, System.StringComparison.OrdinalIgnoreCase), null, f => f.FonctionActions).FirstOrDefault();
            if (dbFonction != null)
            {
                dbFonction.MenuPath = fonction.MenuPath;
                dbFonction.SuperAdmin = fonction.SuperAdmin;
                dbFonction.Log = fonction.Log;
                dbFonction.FonctionActions.Clear();
                dbFonction.FonctionActions = fonction.FonctionActions.Select(fa => new FonctionAction { CodeFonction = fonction.Code, CodeAction = fa.CodeAction }).ToList();
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto IsFonctionCodeUnique(string code)
        {
            BoolDto dto = new BoolDto();
            dto.Value = !this.UnitOfWork.FonctionRepository.Get(f => f.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase)).Any();
            return dto;
        }

        public BoolDto IsFonctionUsed(string codeFonction)
        {
            BoolDto dto = new BoolDto();
            bool userRight = this.UnitOfWork.UserRightRepository.Get(ur => ur.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase)).Any();
            bool profilRight = this.UnitOfWork.ProfilRightRepository.Get(pr => pr.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase)).Any();

            dto.Value = userRight || profilRight;
            return dto;
        }
        #endregion

        #region Profil
        public ListDto<Profil> GetProfilList()
        {
            ListDto<Profil> lst = new ListDto<Profil>();
            this.UnitOfWork.ProfilRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ProfilRepository.Get();
            return lst;
        }

        public ListDto<Profil> GetProfilListWithDependencies()
        {
            ListDto<Profil> lst = new ListDto<Profil>();
            this.UnitOfWork.ProfilRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.ProfilRepository.Get(null, null,
                x => x.SinbaUsers,
                x => x.ProfilRights);
            return lst;
        }

        public SimpleDto<Profil> GetProfil(long id)
        {
            SimpleDto<Profil> dto = new SimpleDto<Profil>();
            this.UnitOfWork.ProfilRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.ProfilRepository.Get(p => p.Id == id, null, p => p.SinbaUsers, p => p.ProfilRights).FirstOrDefault();
            return dto;
        }

        public BoolDto InsertProfil(Profil profil)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ProfilRepository.Insert(profil);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto UpdateProfil(Profil profil, bool updateProfilRights)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ProfilRepository.EnableTracking = true;
            Profil dbProfil = this.UnitOfWork.ProfilRepository.Get(p => p.Id == profil.Id, null, p => p.ProfilRights).FirstOrDefault();

            if (dbProfil != null)
            {
                dbProfil.Nom = profil.Nom;
                if (updateProfilRights)
                {
                    dbProfil.ProfilRights.Clear();
                    dbProfil.ProfilRights = profil.ProfilRights.Select(pr => new ProfilRight()
                    {
                        IdProfil = profil.Id,
                        CodeFonction = pr.CodeFonction,
                        CodeAction = pr.CodeAction
                    }).ToList();
                }
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto DeleteProfil(long id)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.ProfilRepository.Delete(id);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto IsProfilUsed(long id)
        {
            BoolDto dto = new BoolDto();
            var profil = this.UnitOfWork.ProfilRepository.Get(p => p.Id == id, null,
                x => x.SinbaUsers,
                x => x.ProfilRights).FirstOrDefault();
            if (profil == null || !profil.IsUsed)
                dto.Value = false;
            else
                dto.Value = true;

            return dto;
        }

        public SimpleDto<Profil> GetProfil(string name)
        {
            this.UnitOfWork.ProfilRepository.EnableTracking = false;
            SimpleDto<Profil> dto = new SimpleDto<Profil>();
            dto.Value = this.UnitOfWork.ProfilRepository.Get(p => p.Nom.Equals(name, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return dto;
        }

        #endregion

        #region User rights
        public ListDto<UserRight> GetUserRightList(string idUser)
        {
            ListDto<UserRight> lst = new ListDto<UserRight>();
            this.UnitOfWork.UserRightRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.UserRightRepository.Get(ur => ur.IdUser.Equals(idUser, System.StringComparison.OrdinalIgnoreCase));
            return lst;
        }

        public SimpleDto<UserRight> GetUserRight(string idUser, string codeFonction, string codeAction)
        {
            SimpleDto<UserRight> dto = new SimpleDto<UserRight>();
            this.UnitOfWork.UserRightRepository.EnableTracking = false;

            dto.Value = this.UnitOfWork.UserRightRepository.Get(ur => ur.IdUser.Equals(idUser) &&
                ur.CodeFonction.Equals(codeFonction) &&
                ur.CodeAction.Equals(codeAction)).FirstOrDefault();

            return dto;
        }

        public BoolDto InsertUserRight(UserRight userRight)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.UserRightRepository.Insert(userRight);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto DeleteUserRight(UserRight userRight)
        {
            BoolDto dto = new BoolDto();

            UserRight dbUserRight = this.UnitOfWork.UserRightRepository.Get(ur => ur.IdUser.Equals(userRight.IdUser) &&
                ur.CodeFonction.Equals(userRight.CodeFonction) &&
                ur.CodeAction.Equals(userRight.CodeAction)).FirstOrDefault();

            if (dbUserRight != null)
            {
                this.UnitOfWork.UserRightRepository.Delete(dbUserRight);
                this.UnitOfWork.Commit();
                dto.Value = true;
            }

            return dto;
        }

        public BoolDto UpdateUserRight(UserRight userRight)
        {
            BoolDto dto = new BoolDto();

            UserRight dbUserRight = this.UnitOfWork.UserRightRepository.Get(ur => ur.IdUser.Equals(userRight.IdUser) &&
                ur.CodeFonction.Equals(userRight.CodeFonction) &&
                ur.CodeAction.Equals(userRight.CodeAction)).FirstOrDefault();

            if (dbUserRight != null)
            {
                dbUserRight.DenyAccess = userRight.DenyAccess;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }

            return dto;
        }
        #endregion

        #region User
        public ListDto<SinbaUser> GetSinbaUserList()
        {
            System.DateTime date = System.DateTime.Now.Date;
            ListDto<SinbaUser> lst = new ListDto<SinbaUser>();
            this.UnitOfWork.SinbaUserRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.SinbaUserRepository.Get(null, null, u => u.Roles,u=>u.SiteUtilisateurs);

            return lst;
        }
        public SimpleDto<SinbaUser> GetSinbaUser(string id)
        {
            SimpleDto<SinbaUser> dto = new SimpleDto<SinbaUser>();
            this.UnitOfWork.SinbaUserRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.SinbaUserRepository.Get(u => u.Id == id, null, null).FirstOrDefault();
            return dto;
        }

        public ListDto<SinbaUser> GetSinbaUserForSelectedIds(List<string> ids)
        {
            ListDto<SinbaUser> lst = new ListDto<SinbaUser>();
            if (ids != null && ids.Any())
            {
                this.UnitOfWork.SinbaUserRepository.EnableTracking = false;
                lst.Value = this.UnitOfWork.SinbaUserRepository.Get(u => ids.Contains(u.Id));
            }
            return lst;
        }
        #endregion

        #endregion

        #region Societe
        public ListDto<Societe> GetSocieteList()
        {
            ListDto<Societe> lst = new ListDto<Societe>();
            this.UnitOfWork.SocieteRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.SocieteRepository.Get();
            return lst;
        }

        public SimpleDto<Societe> GetSociete(string id)
        {
            SimpleDto<Societe> dto = new SimpleDto<Societe>();
            this.UnitOfWork.SocieteRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.SocieteRepository.Get(p => p.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return dto;
        }

        public BoolDto InsertSociete(Societe societe)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SocieteRepository.Insert(societe);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto UpdateSociete(Societe societe)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SocieteRepository.EnableTracking = true;
            Societe dbSociete = this.UnitOfWork.SocieteRepository.Get(p => p.Id == societe.Id).FirstOrDefault();

            if (dbSociete != null)
            {
                dbSociete.Id = societe.Id;
                dbSociete.Libelle = societe.Libelle;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto DeleteSociete(string id)
        {
            BoolDto dto = new BoolDto();
            if (UnitOfWork.SocieteRepository.Get(n => n.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase)).Any())
            {
                UnitOfWork.SocieteRepository.Delete(id);
                UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto IsSocieteIdUnique(string id)
        {
            BoolDto dto = new BoolDto();
            dto.Value = !this.UnitOfWork.SocieteRepository.Get(p => p.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase)).Any();
            return dto;
        }

        public BoolDto IsSocieteIdUsed(string id, string idHidden)
        {
            BoolDto dto = new BoolDto();
            UnitOfWork.SocieteRepository.EnableTracking = false;
            var query = UnitOfWork.SocieteRepository.Get(p => p.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(idHidden))
            {
                query = query.Where(p => p.Id != idHidden);
            }

            dto.Value = query.Any();
            return dto;
        }

        public BoolDto IsSocieteUsed(string id)
        {
            BoolDto dto = new BoolDto();
            dto.Value = this.UnitOfWork.SiteRepository.Get(p => p.IdSociete.Equals(id, System.StringComparison.OrdinalIgnoreCase)).Any();
            return dto;
        }
        #endregion

        #region Site
        public SimpleDto<Site> GetSite(string id)
        {
            SimpleDto<Site> dto = new SimpleDto<Site>();
            this.UnitOfWork.SiteRepository.EnableTracking = false;
            dto.Value = this.UnitOfWork.SiteRepository.Get(p => p.Id.Equals(id,System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return dto;
        }

        public ListDto<Site> GetSiteList()
        {
            ListDto<Site> lst = new ListDto<Site>();
            this.UnitOfWork.SiteRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.SiteRepository.Get();
            return lst;
        }
        
        public ListDto<Site> GetSiteListWithDependencies()
        {
            ListDto<Site> lst = new ListDto<Site>();
            this.UnitOfWork.SiteRepository.EnableTracking = false;
            lst.Value = this.UnitOfWork.SiteRepository.Get(null, null,
              p => p.SiteUtilisateur);
            return lst;
        }
       public BoolDto InsertSite(Site site)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SiteRepository.Insert(site);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public BoolDto UpdateSite(Site site)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SiteRepository.EnableTracking = true;
            Site dbSite = this.UnitOfWork.SiteRepository.Get(p => p.Id.Equals(site.Id,System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (dbSite != null)
            {
                dbSite.Id = site.Id;
                dbSite.Libelle = site.Libelle;
                dbSite.IdSociete = site.IdSociete;
                this.UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto DeleteSite(string id)
        {
            BoolDto dto = new BoolDto();
            Site SiteToDelete = UnitOfWork.SiteRepository.Get(p => p.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase), null, p => p.SiteUtilisateur).FirstOrDefault();
            if (SiteToDelete != null && !SiteToDelete.IsUsed)
            {
                UnitOfWork.SiteRepository.Delete(id);
                UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        public BoolDto IsSiteIdUsed(string id, string idHidden)
        {
            BoolDto dto = new BoolDto();
            UnitOfWork.SiteRepository.EnableTracking = false;
            var query = UnitOfWork.SiteRepository.Get(p => p.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(idHidden))
            {
                query = query.Where(p => p.Id != idHidden);
            }

            dto.Value = query.Any();
            return dto;
        }

        public BoolDto IsSiteUsed(string id)
        {
            BoolDto dto = new BoolDto();
            dto.Value = this.UnitOfWork.SiteUtilisateurRepository.Get(p => p.IdSite.Equals(id, System.StringComparison.OrdinalIgnoreCase)).Any();
            return dto;
        }

        #endregion

        #region SiteUtilisateur
        public ListDto<SiteUtilisateur> GetSiteUtilisateurList(string idUser = null)
        {
            this.UnitOfWork.SiteUtilisateurRepository.EnableTracking = false;
            ListDto<SiteUtilisateur> lst = new ListDto<SiteUtilisateur>();
            if (string.IsNullOrWhiteSpace(idUser))
            {
                lst.Value = this.UnitOfWork.SiteUtilisateurRepository.Get(null, null, p => p.Site, p => p.SinbaUser);
            }
            else
            {
                lst.Value = this.UnitOfWork.SiteUtilisateurRepository.Get(p => p.IdUser.Equals(idUser, System.StringComparison.OrdinalIgnoreCase), null, p => p.Site, p => p.SinbaUser);
            }
            return lst;
        }

        public BoolDto UpdateSiteUtilisateur(SiteUtilisateurViewModel siteUtilisateurViewModel)
        {
            BoolDto dto = new BoolDto();
            
            DeleteSiteUtilisateurByIdUser(siteUtilisateurViewModel.IdUser);
            if (!string.IsNullOrWhiteSpace(siteUtilisateurViewModel.SitesToken))
            {
                List<string> lstSiteAutorises = new List<string>();
                lstSiteAutorises = siteUtilisateurViewModel.SitesToken.Split(Strings.TokenValueSeparator.ToCharArray()).ToList();

                for (int i = 0; i < lstSiteAutorises.Count(); i++)
                {
                    var dto2 = InsertSiteUtilisateur(new SiteUtilisateur { IdUser = siteUtilisateurViewModel.IdUser, IdSite = lstSiteAutorises[i], DateModif = System.DateTime.Now, IdUserModif = siteUtilisateurViewModel.IdUserModif });                    
                }
            }

            dto.Value = true;
            return dto;            
        }

        public BoolDto InsertSiteUtilisateur(SiteUtilisateur siteUtilisateur)
        {
            BoolDto dto = new BoolDto();
            this.UnitOfWork.SiteUtilisateurRepository.Insert(siteUtilisateur);
            this.UnitOfWork.Commit();
            dto.Value = true;
            return dto;
        }

        public void DeleteSiteUtilisateurByIdUser(string idUser)
        {            
            ListDto<SiteUtilisateur> lstSiteUtilisateurToDelete = new ListDto<SiteUtilisateur>();
            lstSiteUtilisateurToDelete.Value = UnitOfWork.SiteUtilisateurRepository.Get(p => p.IdUser.Equals(idUser, System.StringComparison.OrdinalIgnoreCase)).ToList();
            if (lstSiteUtilisateurToDelete.Value.Count() > 0)
            {
                foreach (var item in lstSiteUtilisateurToDelete.Value.ToList())
                {
                    if (item != null)
                    {
                        DeleteSiteUtilisateur(item.IdUser,item.IdSite);
                    }
                }
            }          

        }

        public BoolDto DeleteSiteUtilisateur(string idUser, string idSite)
        {
            BoolDto dto = new BoolDto();
            SiteUtilisateur SiteUtilisateurToDelete = UnitOfWork.SiteUtilisateurRepository.Get(p => p.IdUser.Equals(idUser, System.StringComparison.OrdinalIgnoreCase) && p.IdSite.Equals(idSite, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (SiteUtilisateurToDelete != null)
            {
                UnitOfWork.SiteUtilisateurRepository.Delete(SiteUtilisateurToDelete);
                UnitOfWork.Commit();
                dto.Value = true;
            }
            return dto;
        }

        #endregion


    }
}
