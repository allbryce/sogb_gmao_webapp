//using Dev.Tools.Exceptions;
//using Dev.Tools.Logger;
//using Sinba.BusinessModel.ServiceInterface;
//using Sinba.Gui.Security;
//using Sinba.Gui.UIModels;
//using Sinba.Resources;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Sinba.BusinessModel.Entity;
//using Sinba.Resources.Resources.Entity;

//namespace Sinba.Gui.Controllers
//{

//    #region Attributes
//    [IHMLog()]
//    [IhmExceptionHandler()]
//    [RoutePrefix("DonneesDeBase/DonneesDeBase/Activite")]
//    [Route("{action=Index}")]
//    [ClaimsAuthorize]
//    #endregion
//    public class ActiviteController : DonneesDeBaseDonneesDeBaseController
//    {
//        // GET: Activite
//        #region Variables
//        // Services
//        private IDonneesDeBaseService donnesDeBaseService;
//        // private string userIdSociete;
//        private string userId;
//        #endregion

//        #region Overrides
//        // Set the Controller Name here (used for Navigation).
//        public override string ControllerName { get { return SinbaConstants.Controllers.Activite; } }
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes a new instance of the <see cref="ActiviteController" /> class.
//        /// </summary>
//        /// <param name="listeService">The liste service.</param>
//        public ActiviteController(IDonneesDeBaseService donnesDeBaseService)
//        {
//            this.donnesDeBaseService = donnesDeBaseService;
//          //  userIdSociete = GetUserSocieteId();
//            userId = GetUserId();
//        }
//        #endregion

//        #region List
//        // GET: Activite
//        public ActionResult Index()
//        {
//            FillViewBag();
//            FillAuthorizedActionsViewBag();
//            return SinbaView(ViewNames.Index, GetActiviteList());
//        }

//        [ClaimsAuthorize(SinbaConstants.Controllers.Activite, SinbaConstants.Actions.Index)]
//        public ActionResult ListPartial()
//        {
//            FillViewBag();
//            FillAuthorizedActionsViewBag();
//            return PartialView(ViewNames.ListPartial, GetActiviteList());
//        }

        

//        /// <summary>
//        /// Gets the profil list.
//        /// </summary>
//        /// <returns></returns>
//        private List<Activite> GetActiviteList()
//        {
//            List<Activite> lst = new List<Activite>();
//            var dto = donnesDeBaseService.GetActiviteListWithDependencies();
//            if (!TreatDto(dto) && dto.Value != null)
//            {
//                lst = dto.Value.ToList();
//            }
//            return lst;
//        }

//        #endregion

//        #region Add / Edit
//        [HttpGet]
//        public ActionResult Add()
//        {
//            FillViewBag(true);
//            return SinbaView(ViewNames.EditPartial, new Activite());
//        }

//        [HttpPost, ValidateInput(false)]
//        public ActionResult Add(Activite activite)
//        {
//            if (!ModelState.IsValid)
//            {
//                FillViewBag(true);
//                return SinbaView(ViewNames.EditPartial, activite);
//            }
//            // Add mode (Insert Entity)
//            activite.DateCreation = System.DateTime.Now;
//            activite.UserCreation = userId;
//            var dto = donnesDeBaseService.InsertActivite(activite);
//            TreatDto(dto);

//            return RedirectToAction(SinbaConstants.Actions.Index);
//        }

//        [Route(SinbaConstants.Routes.EditActiviteID)]
//        public ActionResult Edit(string ActiviteID)
//        {
//            if (!String.IsNullOrEmpty(ActiviteID))
//            {
//                var dto = donnesDeBaseService.GetActivite(ActiviteID);
//                TreatDto(dto);
//                var activite = dto.Value;
//                if (activite != null)
//                {
//                    FillViewBag();
//                    return SinbaView(ViewNames.EditPartial, activite);
//                }
//            }

//            return RedirectToAction(SinbaConstants.Actions.Index);
//        }

//        [HttpPost, ValidateInput(false)]
//        [Route(SinbaConstants.Routes.EditActiviteID)]
//        public ActionResult Edit(Activite activite)
//        {
//            if (!ModelState.IsValid)
//            {
//                FillViewBag();
//                return SinbaView(ViewNames.EditPartial, activite);
//            }
//            // Edit mode (Update Entity)
//            activite.DateModification = System.DateTime.Now;
//            activite.UserModification = userId;
//            var dto = donnesDeBaseService.UpdateActivite(activite);
//            TreatDto(dto);

//            return RedirectToAction(SinbaConstants.Actions.Index);
//        }
//        #endregion

//        #region Delete
//        [Route(SinbaConstants.Routes.DeleteActiviteID)]
//        public ActionResult Delete(string ActiviteID)
//        {
//            if (!String.IsNullOrEmpty(ActiviteID))
//            {
//                var dtoDelete = donnesDeBaseService.DeleteActivite(ActiviteID);
//                TreatDto(dtoDelete);
//            }

//            return RedirectToAction(SinbaConstants.Actions.Index);
//        }
//        #endregion

//        #region Other Methods
//        ///// <summary>
//        ///// Determines whether [is identifier used] [the specified identifier].
//        ///// </summary>
//        ///// <param name="code">The identifier.</param>
//        ///// <returns></returns>
//        //[AllowAnonymous]
//        //public ActionResult IsCodeUsed(string code, int id)
//        //{
//        //    bool ret = true;
//        //    var dto = donnesDeBaseService.IsActiviteCodeUsed(code, id, userIdSociete);

//        //    if (!TreatDto(dto))
//        //    {
//        //        ret = !dto.Value;
//        //    }

//        //    return Json(ret, JsonRequestBehavior.AllowGet);
//        //}

//        #endregion

//        #region ViewBag
//        /// <summary>
//        /// Fills the authorized actions view bag.
//        /// </summary>
//        /// 24-May-16 - Rene: creation
//        /// Change history:
//        private void FillAuthorizedActionsViewBag()
//        {
//            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Activite);
//            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
//            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
//            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
//        }

//        /// <summary>
//        /// Charger Ville view bag.
//        /// </summary>
//        private void FillViewBag(bool addMode = false)
//        {
//            //// Direction
//            //List<Ville> lst = new List<Ville>();
//            //var dto = this.donnesDeBaseService.GetVilleList();
//            //if (!TreatDto(dto) && dto.Value != null)
//            //{
//            //    lst = dto.Value.ToList();
//            //}
//            //ViewBag.Ville = lst;
//            ViewBag.AddMode = addMode;
//        }

//        #endregion



//    }
//}