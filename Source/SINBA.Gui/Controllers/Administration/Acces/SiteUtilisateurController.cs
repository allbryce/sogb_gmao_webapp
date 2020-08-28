using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.Resources;
using Sinba.Gui.Security;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sinba.Gui.Controllers
{
    #region Attributes
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("Administration/Acces/SiteUtilisateur")]
    [Route("{action=Edit}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin, SinbaRoles.AdminSite)]
    #endregion
    public class SiteUtilisateurController : AdministrationAccesController
    {
        #region Variables
        // Services
        private IRightManagementService rightManagementService;
        #endregion

        #region Overrides
        // Set the Controller Name here (used for Navigation).
        public override string ControllerName { get { return SinbaConstants.Controllers.Profil; } }
        #endregion

        #region Constructors
        public SiteUtilisateurController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        

        #region Edit
       
        [HttpGet, Route(SinbaConstants.Routes.EditIdUser)]
        public ActionResult Edit(string idUser)
        {
            SinbaUser utilisateur = null;
            SiteUtilisateurViewModel siteUtilisateurViewModel = null;

            if (!string.IsNullOrWhiteSpace(idUser))
            {
                utilisateur = GetUtilisateur(idUser);

                if (utilisateur == null || string.IsNullOrWhiteSpace(idUser))
                {
                    return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Utilisateur);
                }

                FillViewBag(idUser);
                siteUtilisateurViewModel = new SiteUtilisateurViewModel()
                {
                    IdUser = utilisateur.Id,
                    NomPrenom = string.Format("{0} {1}", utilisateur.Prenom, utilisateur.Nom)
                };

                return SinbaView(ViewNames.EditPartial, siteUtilisateurViewModel, PageTitle(string.Format("{0} {1}", utilisateur.Prenom, utilisateur.Nom)), true);
            }

            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Utilisateur);
        }

        [HttpPost, ValidateInput(false), Route(SinbaConstants.Routes.EditIdUser)]
        public ActionResult Edit(SiteUtilisateurViewModel siteUtilisateurViewModel, FormCollection formCollection)
        {
            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                //FillViewBag(siteUtilisateurViewModel.IdUser);
                return SinbaView(ViewNames.EditPartial, siteUtilisateurViewModel);
            }

            if (!string.IsNullOrWhiteSpace(siteUtilisateurViewModel.IdUser))
            {
                siteUtilisateurViewModel.SitesToken= GetTokens(formCollection);
                siteUtilisateurViewModel.IdUserModif =GetUserId();

                // Edit mode (Update Entity)
                var dto = rightManagementService.UpdateSiteUtilisateur(siteUtilisateurViewModel);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Utilisateur);
        }
        #endregion        

        #region ViewBag
        /// <summary>
        /// Fills the view bag lists.
        /// </summary>
        private void FillViewBag(string idUser = null)
        {
            // Liste des Sites
            List<Site> lst = new List<Site>();
            var dto = this.rightManagementService.GetSiteList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }

            
            List<SiteUtilisateur> lstSiteAutorise = new List<SiteUtilisateur>();
            if (!string.IsNullOrWhiteSpace(idUser))
            {
                var dto2 = this.rightManagementService.GetSiteUtilisateurList(idUser);
                if (!TreatDto(dto2) && dto2.Value != null)
                {                    
                    lstSiteAutorise = dto2.Value.ToList();
                    string[] tokenSite=new string[lstSiteAutorise.Count];
                    for (int i = 0; i < lstSiteAutorise.Count; i++)
                    {
                        tokenSite[i] = lstSiteAutorise[i].IdSite;
                    }
                    ViewBag.Tokens = tokenSite;
                }                                             

            }
            
            ViewBag.SiteList = lst;
        }

        /// <summary>
        /// Fills the authorized actions view bag.
        /// </summary>
        /// 24-May-16 - Rene: creation
        /// Change history:
        private void FillAuthorizedActionsViewBag()
        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.ProfilRight);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
        }
        #endregion

        #region Other Methods
        private string PageTitle(string nomPrenom)
        {
            return string.Format(RightManagementResource.SiteUtilisateurTitle, nomPrenom);
        }

        /// <summary>
        /// Gets the profil.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private SinbaUser GetUtilisateur(string idUser)
        {
            SinbaUser utilisateur = null;
            if (!string.IsNullOrWhiteSpace(idUser))
            {
                var dto = this.rightManagementService.GetSinbaUser(idUser);
                if (!TreatDto(dto) && dto.Value != null)
                {
                    utilisateur = dto.Value;
                }
            }
            return utilisateur;
        }

        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns></returns>
        private string GetTokens(FormCollection formCollection)
        {
            string tokenValues;            
            if ((tokenValues = formCollection["Sites_TKV"]) != null)
            {
                tokenValues = tokenValues.Replace("[", string.Empty).Replace("]", string.Empty).Replace("\"", string.Empty);
            }
            return tokenValues;
        }
        #endregion
    }
}