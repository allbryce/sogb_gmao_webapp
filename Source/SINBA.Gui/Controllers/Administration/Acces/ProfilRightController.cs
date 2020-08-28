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
    [RoutePrefix("Administration/Acces/ProfilRight")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    [Roles(SinbaRoles.SuperAdmin, SinbaRoles.AdminSite)]
    #endregion
    public class ProfilRightController : AdministrationAccesController
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
        public ProfilRightController(IRightManagementService rightManagementService)
        {
            this.rightManagementService = rightManagementService;
        }
        #endregion

        #region List
        // GET: Profil rights
        [Route(SinbaConstants.Routes.IndexIdProfil)]
        public ActionResult Index(long? idProfil)
        {
            Profil profil = GetProfil(idProfil);

            if (profil != null)
            {
                FillViewBag(profil);
                FillAuthorizedActionsViewBag();
                return SinbaView(ViewNames.Index, profil.GroupedRights, PageTitle(profil.Nom), true);
            }
            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Profil);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.ProfilRight, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial(long? idProfil)
        {
            Profil profil = GetProfil(idProfil);
            if (profil != null)
            {
                FillViewBag(profil);
                FillAuthorizedActionsViewBag();
                return PartialView(ViewNames.ListPartial, profil.GroupedRights);
            }
            return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Profil);
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.ProfilRight, SinbaConstants.Actions.Index)]
        public ActionResult ProfilRightActionsPartial(string codeFonction)
        {
            ViewData[DbColumns.CodeFonction] = codeFonction;
            ViewBag.ActionList = GetProfilRightActionList(codeFonction);
            return PartialView(ViewNames.ActionsFonctionPartial, new ProfilRight());
        }

        private List<FonctionAction> GetProfilRightActionList(string codeFonction)
        {
            ListDto<FonctionAction> dtoLst = new ListDto<FonctionAction>(new List<FonctionAction>());
            if (!string.IsNullOrWhiteSpace(codeFonction))
            {
                dtoLst = this.rightManagementService.GetFonctionActionList(codeFonction);
                TreatDto(dtoLst);
            }

            return dtoLst.Value != null ? dtoLst.Value.ToList() : new List<FonctionAction>();
        }
        #endregion

        #region Add / Edit
        [HttpGet, Route(SinbaConstants.Routes.AddIdProfil)]
        public ActionResult Add(long? idProfil)
        {
            Profil profil = GetProfil(idProfil);

            if (profil == null)
            {
                return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Profil);
            }

            ProfilRight profilRight = new ProfilRight()
            {
                IdHidden = -1,
                IdProfil = profil.Id
            };

            FillViewBag(profil);

            return SinbaView(ViewNames.EditPartial, profilRight, PageTitle(profil.Nom), true);
        }

        [HttpPost, ValidateInput(false), Route(SinbaConstants.Routes.AddIdProfil)]
        public ActionResult Add(ProfilRight profilRight, FormCollection formCollection)
        {
            Profil profil = GetProfil(profilRight.IdProfil);

            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                FillViewBag(profil);
                return SinbaView(ViewNames.EditPartial, profilRight, PageTitle(profil.Nom), true);
            }

            if (profilRight.IdHidden < 0 && profil != null)
            {
                List<string> actions = GetTokens(formCollection);
                foreach (string codeAction in actions)
                {
                    profil.ProfilRights.Add(new ProfilRight()
                    {
                        IdProfil = profil.Id,
                        CodeFonction = profilRight.CodeFonction,
                        CodeAction = codeAction
                    });
                }

                // Add mode (Insert Entity)
                var dto = rightManagementService.UpdateProfil(profil, true);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpGet, Route(SinbaConstants.Routes.EditIdProfilCodeFonction)]
        public ActionResult Edit(int? idProfil, string codeFonction)
        {
            Profil profil = null;
            ProfilRight profilRight = null;

            if (idProfil.HasValue && !string.IsNullOrWhiteSpace(codeFonction))
            {
                profil = GetProfil(idProfil.Value);

                if (profil == null || string.IsNullOrWhiteSpace(codeFonction))
                {
                    return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Profil);
                }

                FillViewBag(profil, codeFonction);
                profilRight = new ProfilRight()
                {
                    IdProfil = profil.Id,
                    CodeFonction = codeFonction
                };

                return SinbaView(ViewNames.EditPartial, profilRight);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false), Route(SinbaConstants.Routes.EditIdProfilCodeFonction)]
        public ActionResult Edit(ProfilRight profilRight, FormCollection formCollection)
        {
            Profil profil = GetProfil(profilRight.IdProfil);

            // Checking if the returned model is valid
            if (!ModelState.IsValid)
            {
                FillViewBag(profil, profilRight.CodeFonction);
                return SinbaView(ViewNames.EditPartial, profilRight);
            }

            if (profil != null && profilRight.IdHidden >= 0)
            {
                List<string> actions = GetTokens(formCollection);

                List<ProfilRight> profilRightList = profil.ProfilRights.ToList();

                profilRightList.RemoveAll(pr => pr.CodeFonction.Equals(profilRight.CodeFonction, System.StringComparison.OrdinalIgnoreCase));

                profil.ProfilRights = profilRightList.Select(pr => new ProfilRight() { IdProfil = pr.IdProfil, CodeFonction = pr.CodeFonction, CodeAction = pr.CodeAction }).ToList();

                foreach (string codeAction in actions)
                {
                    profil.ProfilRights.Add(new ProfilRight()
                    {
                        IdProfil = profil.Id,
                        CodeFonction = profilRight.CodeFonction,
                        CodeAction = codeAction
                    });
                }

                // Edit mode (Update Entity)
                var dto = rightManagementService.UpdateProfil(profil, true);
                TreatDto(dto);
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Delete
        [Route(SinbaConstants.Routes.DeleteIdProfilCodeFonction)]
        public ActionResult Delete(int? idProfil, string codeFonction)
        {
            if (idProfil.HasValue)
            {
                Profil profil = GetProfil(idProfil.Value);

                if (profil == null || string.IsNullOrWhiteSpace(codeFonction))
                {
                    return RedirectToAction(SinbaConstants.Actions.Index, SinbaConstants.Controllers.Profil);
                }

                List<ProfilRight> profilRightList = profil.ProfilRights.ToList();
                profilRightList.RemoveAll(pr => pr.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase));
                profil.ProfilRights = profilRightList.Select(pr => new ProfilRight() { IdProfil = pr.IdProfil, CodeFonction = pr.CodeFonction, CodeAction = pr.CodeAction }).ToList();

                this.rightManagementService.UpdateProfil(profil, true);

                return RedirectToAction(SinbaConstants.Actions.Index, new { idProfil = idProfil.Value });
            }

            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region ViewBag
        /// <summary>
        /// Fills the view bag lists.
        /// </summary>
        private void FillViewBag(Profil profil = null, string codeFonction = null)
        {
            // Liste des fonction
            List<Fonction> lst = new List<Fonction>();

            if (profil != null)
            {
                if (codeFonction == null)
                {
                    var dto = this.rightManagementService.GetFonctionList(IsSuperAdmin());
                    TreatDto(dto);
                    if (dto.Value != null)
                    {
                        lst = dto.Value.ToList();
                    }

                    for (int i = lst.Count - 1; i >= 0; i--)
                    {
                        if (profil.ProfilRights.Any(pr => pr.CodeFonction.Equals(lst[i].Code, System.StringComparison.OrdinalIgnoreCase)))
                        {
                            lst.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    lst.Add(new Fonction() { Code = codeFonction });
                    ViewBag.ActionList = GetProfilRightActionList(codeFonction);
                    ProfilRightViewModel prvm = profil.GroupedRights.Where(g => g.CodeFonction.Equals(codeFonction, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (prvm != null)
                    {
                        ViewBag.Tokens = prvm.Actions.Split(new string[] { ", " }, System.StringSplitOptions.None);
                    }

                }

                ViewBag.IdProfil = profil.Id;
                ViewBag.FonctionList = lst;
            }
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
        private string PageTitle(string profilName)
        {
            return string.Format(RightManagementResource.ProfilRightsTitle, profilName);
        }

        /// <summary>
        /// Gets the profil.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private Profil GetProfil(long? id)
        {
            Profil profil = null;
            if (id.HasValue)
            {
                var dto = this.rightManagementService.GetProfil(id.Value);
                if (!TreatDto(dto) && dto.Value != null)
                {
                    profil = dto.Value;
                }
            }
            return profil;
        }

        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns></returns>
        private List<string> GetTokens(FormCollection formCollection)
        {
            string tokenValues;
            List<string> ret = new List<string>();
            if ((tokenValues = formCollection["Actions_TKV"]) != null)
            {
                tokenValues = tokenValues.Replace("[", string.Empty).Replace("]", string.Empty).Replace("\"", string.Empty);
                var tokens = tokenValues.Split(Strings.TokenValueSeparator.ToCharArray());
                ret = tokens.ToList();
            }
            return ret;
        }
        #endregion
    }
}