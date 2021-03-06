using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sinba.Resources;
using Sinba.BusinessModel.ServiceInterface;
using Sinba.Gui.UIModels;
using Sinba.BusinessModel.Entity;
using Sinba.Gui.Security;
using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Entity.ViewModels;


namespace Sinba.Gui.Controllers

{
    #region Attributes
    [IHMLog()]
    [IhmExceptionHandler()]
    [RoutePrefix("DonneesDeBase/GestionMateriel/Materiel")]
    [Route("{action=Index}")]
    [ClaimsAuthorize]
    #endregion
    public class MaterielController : DonneesDeBaseGestionMaterielController
    {
        #region Variables
        private IDonneesDeBaseService donnesDeBaseService;
        private string userId;
        //long id;
        //long id;
        #endregion
        public override string ControllerName { get { return SinbaConstants.Controllers.Materiel; } }
        public MaterielController(IDonneesDeBaseService donnesDeBaseService)

        {
            this.donnesDeBaseService = donnesDeBaseService;
            userId = GetUserId();
        }
        #region Materiel
        #region Views
        public ActionResult Index()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            List<Materiel> lst = new List<Materiel>();
            lst = GetMaterielList();
            return SinbaView(ViewNames.Index, GetMaterielList());
        }
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Index)]
        public ActionResult ListPartial()
        {
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ListPartial, GetMaterielList());
        }                 
        #region Add
        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Materiel materiel)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
                //return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.InsertMateriel(materiel);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [Route(SinbaConstants.Routes.EditId)]
        public ActionResult Edit(long id)
        {
            if (id != 0)
            {
                var dto = donnesDeBaseService.GetMateriel(id);
                TreatDto(dto);
                var materiel = dto.Value;
                if (materiel != null)
                {
                    FillViewBag();
                    return SinbaView(ViewNames.MaterielAddModal, materiel);
                }
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Materiel materiel)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag();
                return SinbaView(ViewNames.EditPartial, materiel);
            }
            var dto = donnesDeBaseService.UpdateMateriel(materiel);
            TreatDto(dto);
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion
        #endregion
        #endregion

        #region Autres
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Index)]
        public ActionResult ComposantPartial(Materiel materiel)
        {
            var id = Request.Params;
            ViewData[DbColumns.MaterielId] = materiel.MaterielId;
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.ComposantPartial, GetComposerMateriel(materiel.MaterielId));
        }

        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Add)]
        public ActionResult AssocierPartial(long materielid, long domaineid)
        {           
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.AssocieAddModal);
        }
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Add)]
        public ActionResult AffectationPartial()
        {           
            FillViewBag();
            FillAuthorizedActionsViewBag();
            return PartialView(ViewNames.AffectationModal);
        }


        //[HttpPost, ValidateInput(false)]
        //[ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Add)]
        //public JsonResult LoadAffectation()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        FillViewBag(true);
        //    }
        //    AffectationPartial();
        //    return Json(true); 
        //}

        [HttpPost, ValidateInput(false)]
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Add)]
        public JsonResult AddCaracteristique(ComposantCaracteristiqueViewModel ComposantCaracteristique)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
            }
            var test = new ComposerMateriel();
            var dto = donnesDeBaseService.InsertComposerMateriel(ComposantCaracteristique.ToComposerMateriel());
            TreatDto(dto);
            return Json(dto.HasError, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost, AllowAnonymous]
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Edit)]
        public JsonResult AddAssocieMateriel(string associemateriel)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
            }
            var associe = new AssociematerielViewModel();
            List<AssocierMateriel> associe1 = associe.ToAssocieMateriel(associemateriel).ToList();           
            var dto = donnesDeBaseService.InsertAssocieMateriel(associe1);          
            return Json(true, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost, ValidateInput(false)]
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Edit)]
        public JsonResult EditCaracteristique(ComposantCaracteristiqueViewModel possedercaracteristique)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(true);
            }
            var dto = donnesDeBaseService.UpdateCaracteristique(possedercaracteristique.ToComposerMateriel());
            return Json(true);
        }

        [HttpPost,AllowAnonymous]
        public ActionResult AssocierMaterielPartial(long materielid,long domaineid)
        {
            List<AssocierMateriel> lst = new List<AssocierMateriel>();
            List<AssociematerielViewModel> list = new List<AssociematerielViewModel>();
            FillViewBag(true);
            FillAuthorizedActionsViewBag();
            lst = GetAssocierMateriel(materielid);
            ViewBag.AssocierMateriel = GetAssocierMaterielList(domaineid);
            foreach (AssocierMateriel item in lst)
            {
                list.Add(item.ToViewModel());
            }
            return PartialView(ViewNames.AssocierMaterielPartial, list);
        }
        #endregion

        #region Delete

        [Route(SinbaConstants.Routes.DeleteId)]
        public ActionResult Delete(long id)
        {
            if (id != 0)
            {
                var dtoDelete = donnesDeBaseService.DeleteMateriel(id);
                TreatDto(dtoDelete);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        [ClaimsAuthorize(SinbaConstants.Controllers.Materiel, SinbaConstants.Actions.Delete)]
        public ActionResult DeleteComposant(long materielid, long composantid, DateTime dateinsertion)
        {
            if(materielid!=0 && composantid!= 0)
            {
                var dto =donnesDeBaseService.RemoveComposant(materielid, composantid, dateinsertion);
            }
            return RedirectToAction(SinbaConstants.Actions.Index);
        }
        #endregion

        #region Modal screens

        #region Modal views
        [AllowAnonymous]
        public ActionResult MaterielAddModalPartial()
        {
            Materiel materiel = new Materiel() ;
            FillAuthorizedActionsViewBag();
            FillViewBag(true);
            return SinbaView(ViewNames.MaterielAddModal, materiel);
        }
        public ActionResult CaracteristiquesModal()
        {
            PossederCaracteristiques caracteristique = new PossederCaracteristiques() ;
            FillAuthorizedActionsViewBag();
            FillViewBag(true);
            return SinbaView(ViewNames.CaracteristiquesModal, caracteristique);
        }

        
        #endregion

        #region Json
        [AllowAnonymous]
        public JsonResult GetMateriel(long materielid)
        {
            Materiel materiel = new Materiel() ;
            var dto = donnesDeBaseService.GetMateriel(materielid);
            if (!TreatDto(dto) && dto.Value != null)
            {
                materiel = dto.Value;
            }
            return Json(materiel, JsonRequestBehavior.AllowGet);
        }

        
         [AllowAnonymous]
        public JsonResult GetComposeMateriel(long materielid, long composantid,DateTime dateinsertion)
        {
            ComposerMateriel materiel = new ComposerMateriel();
            var dto = donnesDeBaseService.GetComposerMaterielwithdependencies(composantid,materielid, dateinsertion);
            if (!TreatDto(dto) && dto.Value != null)
            {
                materiel = dto.Value;
            }
            ComposantCaracteristiqueViewModel composer = new ComposantCaracteristiqueViewModel(materiel);
            return Json( composer, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region ViewBag
        private void FillAuthorizedActionsViewBag()

        {
            var actions = User.Identity.GetAuthorizedActions(SinbaConstants.Controllers.Materiel);
            ViewBag.CanAdd = actions.Contains(SinbaConstants.Actions.Add);
            ViewBag.CanEdit = actions.Contains(SinbaConstants.Actions.Edit);
            ViewBag.CanDelete = actions.Contains(SinbaConstants.Actions.Delete);
            
        }

        private void FillViewBag(bool addMode = false)
        {
            ViewBag.AddMode = addMode;
            ViewBag.Materiel = GetMaterielList();
            ViewBag.Domaine = GetDomaineList();
            ViewBag.GroupeInventaire = GetGroupeInventaireList();
            ViewBag.Classemateriel = GetClassematerielList();
            ViewBag.SousFamille = GetSousFamilleList();
            ViewBag.Model = GetModelList();
            ViewBag.TypeMateriel = GetTypeMaterielList();
            ViewBag.Fournisseur = GetFournisseur();
            ViewBag.Unite = GetUnite();
            ViewBag.Composant = Getcomposant();
            ViewBag.CaracteristiqueComposant = GetCaracteristiqueComposant();
            ViewBag.Localisation = GetLocalisationList();

        }
        #endregion

        #region Lists

        private List<Domaine> GetDomaineList()
        {
            var lstDomaine = new List<Domaine>();
            var dto = donnesDeBaseService.GetDomaineList();

            if (!TreatDto(dto) && dto.Value != null)
            {
                lstDomaine = dto.Value.ToList();
            }
            return lstDomaine;
        }
        private List<GroupeInventaire> GetGroupeInventaireList()
        {
            var lstGroupeInventaire = new List<GroupeInventaire>();
            var dto = donnesDeBaseService.GetGroupeInventaireList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstGroupeInventaire = dto.Value.ToList();
            }
            return lstGroupeInventaire;
        }
        private List<Classemateriel> GetClassematerielList()
        {
            var lstClassemateriel = new List<Classemateriel>();
            var dto = donnesDeBaseService.GetClassematerielList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstClassemateriel = dto.Value.ToList();
            }
            return lstClassemateriel;
        }
        private List<SousFamille> GetSousFamilleList()
        {
            var lstSousFamille = new List<SousFamille>();
            var dto = donnesDeBaseService.GetSousFamilleList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstSousFamille = dto.Value.ToList();
            }
            return lstSousFamille;
        }
        private List<Materiel> GetMaterielList()
        {
            List<Materiel> lst = new List<Materiel>();
            var dto = donnesDeBaseService.GetMaterielListWithDependencies();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }       

        private List<Model> GetModelList()
        {
            var lstModel = new List<Model>();
            var dto = donnesDeBaseService.GetModelList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstModel = dto.Value.ToList();
            }
            return lstModel;
        }

        private List<TypeMateriel> GetTypeMaterielList()
        {
            var lstTypeMateriel = new List<TypeMateriel>();
            var dto = donnesDeBaseService.GetTypeMaterielList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstTypeMateriel = dto.Value.ToList();
            }
            return lstTypeMateriel;
        }   

        private List<Fournisseur> GetFournisseur()
        {
            var lstFournisseur = new List<Fournisseur>();
            var dto = donnesDeBaseService.GetFournisseurList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstFournisseur = dto.Value.ToList();
            }          
            return lstFournisseur;
        }
        private List<Composant> GetComposant()
        {
            var lstComposant = new List<Composant>();
            var dto = donnesDeBaseService.GetComposantList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstComposant = dto.Value.ToList();
            }
            return lstComposant;
        }

        private List<ComposerMateriel> GetComposerMateriel(long materielid)
        {
            var lstcomposermateriel = new List<ComposerMateriel>();
            var dto = donnesDeBaseService.GetComposerMaterielListWithDependencies(materielid);
            if (!TreatDto(dto) && dto.Value != null) ;
            {
                lstcomposermateriel = dto.Value.ToList();
            }
            return lstcomposermateriel;
        }
        private List<Materiel> GetAssocierMaterielList(long domaineid)
        {
            var lstmateriel = new List<Materiel>();
            var dto = donnesDeBaseService.GetAssocierMaterielList(domaineid);
            if (!TreatDto(dto) && dto.Value != null) ;
            {
                lstmateriel = dto.Value.ToList();
            }
            return lstmateriel;
        }
        private List<AssocierMateriel> GetAssocierMateriel(long materielid)
        {
            var lstmaterielassocie = new List<AssocierMateriel>();
            var dto = donnesDeBaseService.GetAssocierMaterielListWithDependencies(materielid);
            if (!TreatDto(dto) && dto.Value != null) ;
            {
                lstmaterielassocie = dto.Value.ToList();
            }
            return lstmaterielassocie;
        }
       
        private List<Unite> GetUnite()
        {
            var lstUnite = new List<Unite>();
            var dto = donnesDeBaseService.GetUniteList();
            if (!TreatDto(dto) && dto.Value != null)
            {
                lstUnite = dto.Value.ToList();
            }
            return lstUnite;
        }

        private List<CaracteristiqueComposant> GetCaracteristiqueComposant()
        {
            var lstCaracteristique = new List<CaracteristiqueComposant>();
            var dto = donnesDeBaseService.GetCaracteristiqueComposantList();

            if (!TreatDto(dto) && dto.Value != null)
            {
                lstCaracteristique = dto.Value.ToList();
            }
            return lstCaracteristique;
        }
        private List<Composant> Getcomposant()
        {
            var lstComposant = new List<Composant>();
            var dto = donnesDeBaseService.GetComposantList();

            if (!TreatDto(dto) && dto.Value != null)
            {
                lstComposant = dto.Value.ToList();
            }
            return lstComposant;
        }
        private List<Localisation> GetLocalisationList()
        {
            var lst = new List<Localisation>();
            var dto = donnesDeBaseService.GetLocalisationList();

            if (!TreatDto(dto) && dto.Value != null)
            {
                lst = dto.Value.ToList();
            }
            return lst;
        }       
        #endregion
    }



}