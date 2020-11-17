using Sinba.Resources.Resources;
using Sinba.Resources.Resources.Entity;
using System;
using System.Collections.Generic;

namespace Sinba.Resources
{
    public static class SinbaConstants
    {
        #region Actions
        /// <summary>
        /// Class listing Actions (constants)
        /// </summary>
        public static class Actions
        {
            //--------------------------------------------A--------------------------------
            public const string AugmenterCredit = "AugmenterCredit";
            public const string AddAlternances = "AddAlternances";
            public const string AddOTOperations = "AddOTOperations";
            public const string AugmenterCreditPartial = "AugmenterCreditPartial";
            public const string AugmenterCreditEdit = "AugmenterCreditEdit";
            public const string AddCodeAlternanceFrequenceSaignee = "AddCodeAlternanceFrequenceSaignee";
            public const string AddTravailleur = "AddTravailleur";
            public const string AddPrixUnitaire = "AddPrixUnitaire";
            public const string ArticleParEmploiPartial = "ArticleParEmploiPartial";
            //--------------------------------------------B--------------------------------
            public const string BlocParcellePartial = "BlocParcellePartial";
            //--------------------------------------------C--------------------------------
            public const string Cloturer = "Cloturer";
            public const string Close = "Close";
            public const string CboRubriqueGroupePartial = "CboRubriqueGroupePartial";
            public const string CboCentreDeCoutPartial = "CboCentreDeCoutPartial";
            public const string CboNaturePartial = "CboNaturePartial";
            public const string CallBackPanelPartial = "CallBackPanelPartial"; 
            public const string ContactDirigeantPartial = "ContactDirigeantPartial";
            public const string ComposantPartial = "ComposantPartial";
            public const string AssocieMaterielPartial = "AssocieMaterielPartial";

            //--------------------------------------------D--------------------------------
            public const string DivisionCallBack = "DivisionCallBack";
            public const string DestinationPartial = "DestinationPartial";
            public const string DotationsPartial = "DotationsPartial";
            public const string VentilationsPartial = "VentilationsPartial";
            //--------------------------------------------E--------------------------------
            public const string EditOTOTOperations = "EditOTOTOperations";
            public const string Extend = "Extend";
            public const string EquipeCallBack = "EquipeCallBack";
            //--------------------------------------------F--------------------------------
            public const string FonctionContactPartial = "FonctionContactPartial";
            //--------------------------------------------G--------------------------------
            public const string GetPlanteur = "GetPlanteur";
            public const string GetUserCultures = "GetUserCultures";
            public const string GetUserSociete = "GetUserSociete";
            public const string GetTravailleurLibreList = "GetTravailleurLibreList";
            public const string GetTravailleurDateSortie = "GetTravailleurDateSortie";
            public const string GetTravailleurList = "GetTravailleurList";
            public const string GetListClonePartial = "GetListClonePartial";
            public const string GetTableParametre = "GetTableParametre";
            public const string GetActivite = "GetActivite";
            public const string GetSiteList = "GetSiteList";
            public const string GetActeurList = "GetActeurList";
            public const string GetContactList = "GetContactList";
            public const string GetInterventContactList = "GetInterventContactList";
            
            public const string GetGroupementList = "GetGroupementList";
            public const string GetLocaliteList = "GetLocaliteList";
            public const string GetTypeContactList = "GetTypeContactList";
            public const string GetCultureList = "GetCultureList";

            public const string GetPlantationList = "GetPlantationList";
            public const string GetMateriel = "GetMateriel";
            public const string GetCaracteristique = "GetCaracteristique";
            public const string AssocierMaterielPartial = "AssocierMaterielPartial";
            public const string GetCulture = "GetCulture";


            //--------------------------------------------H--------------------------------
            public const string HistoriqueTravailleurPartial = "HistoriqueTravailleurPartial";
            //--------------------------------------------I--------------------------------
            public const string Index = "Index";
            public const string ImportModalPartial = "ImportModalPartial";
            public const string ExportTo = "ExportTo";
            public const string Export = "Export";
            public const string CentreDeCoutSitePartial = "CentreDeCoutSitePartial";
            public const string CentreDeCoutPartial = "CentreDeCoutPartial";
            public const string ActeurPartial = "ActeurPartial";
            public const string ClonePartial = "ClonePartial";
            public const string ContactPartial = "ContactPartial";
            public const string ProjetPartial = "ProjetPartial";
            public const string OrganismePartial = "OrganismePartial";
            public const string EditContactPartial = "EditContactPartial";
            
            public const string CentreDeCoutPartial1 = "CentreDeCoutPartial1";
            public const string AnneePlantingPartial = "AnneePlantingPartial";

            public const string AnneePlantingPartial1 = "AnneePlantingPartial1";
            public const string AnneeCulturePartial = "AnneeCulturePartial";
            
            public const string VersionBudgetPartial = "VersionBudgetPartial";
            public const string NormePartial = "NormePartial";
            public const string CentreDeCoutSitePartial1 = "CentreDeCoutSitePartial1";
            public const string CentreDeCoutSitePartial2 = "CentreDeCoutSitePartial2";

            public const string BlocPartial1 = "BlocPartial1";
            public const string BlocPartial2 = "BlocPartial2";

            public const string RubriqueGroupePartial = "RubriqueGroupePartial";
            public const string NaturePartial = "NaturePartial";
            public const string NaturePartial1 = "NaturePartial1";
            public const string NaturePartial2 = "NaturePartial2";

            public const string IsOperationNeedCredit = "IsOperationNeedCredit";
            public const string IsBudgetProductionAnnuelleBlocUnique = "IsBudgetProductionAnnuelleBlocUnique";
            public const string IsDateForTypeUtilisateurAvailable = "IsDateForTypeUtilisateurAvailable";
            public const string Increase = "Increase";
            public const string CboSysEspacementPlantationPartial = "CboSysEspacementPlantationPartial";
            public const string CboSysTypePropagationPartial = "CboSysTypePropagationPartial";
            public const string CboSysTypePlantingPartial = "CboSysTypePlantingPartial";
            public const string CboSysMaterielVegetalPartial = "CboSysMaterielVegetalPartial";
            public const string CboCentreDeCoutSitePartial = "CboCentreDeCoutSitePartial";
            public const string GetSysRapport = "GetSysRapport";

            //--------------------------------------------J--------------------------------
            //--------------------------------------------K--------------------------------
            //--------------------------------------------L--------------------------------

            //--------------------------------------------M--------------------------------
            //--------------------------------------------N--------------------------------
            public const string NormeDetailsListPartial = "NormeDetailsListPartial";

            //--------------------------------------------O--------------------------------
            public const string OperationPartial = "OperationPartial";
            public const string OperationDetailPartial = "OperationDetailPartial";
            public const string OpenReport = "OpenReport";
            //--------------------------------------------P--------------------------------
            public const string Prolonger = "Prolonger";
            public const string PrevisionPartial = "PrevisionPartial";
            public const string PlanteurPartial = "PlanteurPartial";
            public const string PlantationPartial = "PlantationPartial";
            public const string SuperficieListPartial = "SuperficieListPartial";
            public const string IntervenantPartial = "IntervenantPartial";
            public const string IntervenantListPartial = "IntervenantListPartial";
            public const string ProjetPlanteurActeurPartial = "ProjetPlanteurActeurPartial";
            //--------------------------------------------Q--------------------------------
            //--------------------------------------------R--------------------------------
            public const string ReferenceBancairePartial = "ReferenceBancairePartial";
            public const string Rapport0Partial = "Rapport0Partial";
            public const string Rapport1Partial = "Rapport1Partial";
            public const string Rapport2Partial = "Rapport2Partial";
            public const string Rapport3Partial = "Rapport3Partial";
            public const string RapportPartial = "RapportPartial";
            public const string Receptionner = "Receptionner";
            public const string ResendValidation = "ResendValidation";
            public const string Receive = "Receive";
            public const string Resend = "Resend";
            public const string RemoveTravailleur = "RemoveTravailleur";
            public const string Rejeter = "Rejeter";
            public const string RepartirUniteOeuvre = "RepartirUniteOeuvre";

            //--------------------------------------------S--------------------------------
            //--------------------------------------------T--------------------------------

            //--------------------------------------------O--------------------------------
            public const string OTOperationsPartial = "OTOperationsPartial";
            public const string OTOperationsListPartial = "OTOperationsListPartial";
            public const string OTBlocsPartial = "OTBlocsPartial";
            //--------------------------------------------U--------------------------------
            //--------------------------------------------V--------------------------------
            public const string Validation = "Validation";
            public const string Valider = "Valider";
            public const string ValiderCodeSociete = "ValiderCodeSociete";
            //--------------------------------------------W--------------------------------
            //--------------------------------------------X--------------------------------
            //--------------------------------------------Y--------------------------------
            //--------------------------------------------Z--------------------------------
            /// <summary>ListPartial</summary>
            public const string ListPartial = "ListPartial";
            public const string AddPartial = "AddPartial";
            public const string MoveUdDownOTValidateurFamille = "MoveUdDownOTValidateurFamille";
            public const string ModalPartial = "ModalPartial";
            public const string ModalPlanteurPartial = "ModalPlanteurPartial";
            public const string ModalRecherchePlantationPartial = "ModalRecherchePlantationPartial";

            public const string ModalPlantationPartial = "ModalPlantationPartial";
            public const string ModalCessionPartial = "ModalCessionPartial";
            public const string ModalSuperficieCessionPartial = "ModalSuperficieCessionPartial";
            public const string ModalEditSuperficieCessionPartial = "ModalEditSuperficieCessionPartial";
            

            public const string ModalPartial1 = "ModalPartial1";
            public const string ModalEvolYieldProfilePartial = "ModalEvolYieldProfilePartial";

            /// <summary>Edit</summary>
            public const string Edit = "Edit";
            public const string EditUnite = "EditUnite";
            public const string EditCaracteristique = "EditCaracteristique";
            public const string EditComposant = "EditComposant";
            public const string GetValeurParEmploye = "GetValeurParEmploye";
            /// <summary>MultiEdit</summary>
            public const string MultiEdit = "MultiEdit";
            /// <summary>Edit</summary>
            public const string Edit1 = "Edit1";

            /// <summary>EditDetail</summary>
            public const string EditDetail = "EditDetail";

            /// <summary>Add</summary>
            public const string Add = "Add";
            public const string MaterielAddModalPartial = "MaterielAddModalPartial";

            public const string AddContact = "AddContact";
            public const string EditContact = "EditContact";

            /// <summary>AddPlantation</summary>
            public const string AddPlantation = "AddPlantation";

            /// <summary>AddCession</summary>
            public const string AddCession = "AddCession";
            /// <summary>AddCessionDonneesGeographique</summary>
            public const string AddCessionDonneesGeographique = "AddCessionDonneesGeographique";
            /// <summary>AddCessionSurface</summary>
            public const string AddCessionSurface = "AddCessionSurface";
            /// <summary>ValiderSaisie</summary>
            public const string ValiderSaisie = "ValiderSaisie";
            

            /// <summary>UpdatePlantationDonneesGeoSurface</summary>
            public const string UpdatePlantationDonneesGeoSurface = "UpdatePlantationDonneesGeoSurface";
            


            /// <summary>EditCession</summary>
            public const string EditCession = "EditCession";
            

            /// <summary>AddIntervenant</summary>
            public const string AddIntervenant = "AddIntervenant";

            /// <summary>EditProjetPlanteur</summary>
            public const string EditProjetPlanteur = "EditProjetPlanteur";

            /// <summary>DeleteProjetPlanteur</summary>
            public const string DeleteProjetPlanteur = "DeleteProjetPlanteur";


            /// <summary>Upload</summary>
            public const string Upload = "Upload";

            /// <summary>Importer</summary>
            public const string Importer = "Importer";

            /// <summary>Add1</summary>
            public const string Add1 = "Add1";

            /// <summary>AddDetail</summary>
            public const string AddOTOperation = "AddOTOperation";

            /// <summary>AddDetail</summary>
            public const string AddDetail = "AddDetail";

            /// <summary>Delete</summary>
            public const string Delete = "Delete";
            public const string DeleteComposant = "DeleteComposant";

            /// <summary>Dupliquer</summary>
            public const string Dupliquer = "Dupliquer";

            /// <summary>DeleteDetail</summary>
            public const string DeleteDetail = "DeleteDetail";

            /// <summary>SearchListPartial</summary>
            public const string SearchListPartial = "SearchListPartial";

            /// <summary>IsNameUsed</summary>
            public const string IsNameUsed = "IsNameUsed";

            /// <summary> </summary>
            public const string GetOperationsByFamille = "GetOperationsByFamille";

            /// <summary> GetBlocsByUnite </summary>
            public const string GetBlocsByUnite = "GetBlocsByUnite";

            /// <summary> GetParcellesByUnite </summary>
            public const string GetParcellesByUnite = "GetParcellesByUnite";

            /// <summary> GetEquipesBySite </summary>
            public const string GetEquipesBySite = "GetEquipesBySite";

            /// <summary> OperationEditModal_BeginCallback </summary>
            public const string OperationEditModal_BeginCallback = "OperationEditModal_BeginCallback";

            /// <summary>IsIdUsed</summary>
            public const string IsIdUsed = "IsIdUsed";

            /// <summary>IsCodeUsed</summary>
            public const string IsCodeUsed = "IsCodeUsed";

            /// <summary>IsLabelUsed</summary>
            public const string IsLabelUsed = "IsLabelUsed";

            /// <summary>IsTypeUsed</summary>
            public const string IsTypeUsed = "IsTypeUsed";

            /// <summary>NomPartial</summary>
            public const string NomPartial = "NomPartial";

            public const string QuantiteTacheOperationPartial = "QuantiteTacheOperationPartial";

            public const string OTValidateurFamilleOperationPartial = "OTValidateurFamilleOperationPartial";

            /// <summary>PaysPartial</summary>
            public const string PaysPartial = "PaysPartial";

            /// <summary>VillesPaysPartial</summary>
            public const string VillesPaysPartial = "VillesPaysPartial";

            /// <summary>Login</summary>
            public const string Login = "Login";

            /// <summary>LogOff</summary>
            public const string LogOff = "LogOff";

            /// <summary>ForgotPassword</summary>
            public const string ForgotPassword = "ForgotPassword";

            /// <summary>Register</summary>
            public const string Register = "Register";

            /// <summary>ResetPassword</summary>
            public const string ResetPassword = "ResetPassword";

            /// <summary>ResetPasswordConfirmation</summary>
            public const string ResetPasswordConfirmation = "ResetPasswordConfirmation";

            /// <summary>ForgotPasswordConfirmation</summary>
            public const string ForgotPasswordConfirmation = "ForgotPasswordConfirmation";

            /// <summary>ForgotPasswordConfirmation</summary>
            public const string AddCloneBudget = "AddCloneBudget";

            /// <summary>SendCode</summary>
            public const string SendCode = "SendCode";

            /// <summary>ConfirmEmail</summary>
            public const string ConfirmEmail = "ConfirmEmail";

            /// <summary>ChangePassword</summary>
            public const string ChangePassword = "ChangePassword";

            /// <summary>SetPassword</summary>
            public const string SetPassword = "SetPassword";

            /// <summary>SetPasswordUser</summary>
            public const string SetPasswordUser = "SetPasswordUser";

            /// <summary>EditAccountInfo</summary>
            public const string EditAccountInfo = "EditAccountInfo";

            /// <summary>NotAuthorized</summary>
            public const string NotAuthorized = "NotAuthorized";

            /// <summary>GetUserSites</summary>
            public const string GetUserSites = "GetUserSites";

            /// <summary>SetUserSiteAndCulture</summary>
            public const string SetUserSiteAndCulture = "SetUserSiteAndCulture";

            /// <summary>TypePropagationPartial</summary>
            public const string TypePropagationPartial = "TypePropagationPartial";

            /// <summary>EspacementPlantationPartial</summary>
            public const string EspacementPlantationPartial = "EspacementPlantationPartial";

            /// <summary>TypePlantingPartial</summary>
            public const string TypePlantingPartial = "TypePlantingPartial";

            /// <summary>MaterielVegetalPartial</summary>
            public const string MaterielVegetalPartial = "MaterielVegetalPartial";

            /// <summary>CulturePartial</summary>
            public const string CulturePartial = "CulturePartial";

            /// <summary>ValidateRecensement</summary>
            public const string ValidateRecensement = "ValidateRecensement";

            /// <summary>RecensementEtatArbrePartial</summary>
            public const string RecensementEtatArbrePartial = "RecensementEtatArbrePartial";

            /// <summary>ActionsFonctionPartial</summary>
            public const string ActionsFonctionPartial = "ActionsFonctionPartial";
            /// <summary>ActionsMaterielPartial</summary>
            public const string ActionsMaterielPartial = "ActionsMaterielPartial";

            /// <summary>TypeContactActivitePartial</summary>
            public const string TypeContactActivitePartial = "TypeContactActivitePartial";

            /// <summary>ConditionnementPartial</summary>
            public const string ConditionnementPartial = "ConditionnementPartial";

            /// <summary>GetPrevision</summary>
            public const string GetPrevision = "GetPrevision";

            /// <summary>BatchUpdatePartial</summary>
            public const string BatchUpdatePartial = "BatchUpdatePartial";

            /// <summary>ConditionnementPartial</summary>
            public const string ListFacteurTerroirPartial = "ListFacteurTerroirPartial";

            /// <summary>ConditionnementPartial</summary>
            public const string NormeListePartial = "NormeListePartial";

            /// <summary>ProfilRightActionsPartial</summary>
            public const string ProfilRightActionsPartial = "ProfilRightActionsPartial";

            /// <summary>SiteUtilisateurPartial</summary>
            public const string SiteUtilisateurPartial = "SiteUtilisateurPartial";

            /// <summary>UtilisateurRightActionsPartial</summary>
            public const string UtilisateurRightActionsPartial = "UtilisateurRightActionsPartial";

            /// <summary>ProfilRightEditPartial</summary>
            public const string ProfilRightEditPartial = "ProfilRightEditPartial";

            /// <summary>CheckDates</summary>
            public const string CheckDates = "CheckDates";

            /// <summary>GetBlocPartial</summary>
            public const string GetBlocPartial = "GetBlocPartial";

            /// <summary>GetDatesPartial</summary>
            public const string GetDatesPartial = "GetDatesPartial";

            /// <summary>IsDrcUnique</summary>
            public const string IsDrcUnique = "IsDrcUnique";

            public const string IsOTOperationUnique = "IsOTOperationUnique";

            /// <summary>IsTauxExtractionUnique</summary>
            public const string IsTauxExtractionUnique = "IsTauxExtractionUnique";

            /// <summary>IsBudgetRepartitionProductionMensuelleUnique</summary>
            public const string IsBudgetRepartitionProductionMensuelleUnique = "IsBudgetRepartitionProductionMensuelleUnique";

            /// <summary>IsBudgetNombreJoursUnique</summary>
            public const string IsBudgetNombreJoursUnique = "IsBudgetNombreJoursUnique";

            /// <summary>DocumentViewerPartial</summary>
            public const string DocumentViewerPartial = "DocumentViewerPartial";

            public const string ReportParametersPartial = "ReportParametersPartial";

            /// <summary>ExportReport</summary>
            public const string ExportReport = "ExportReport";

            /// <summary>RelevePontBasculeDetailPartial</summary>
            public const string RelevePontBasculeDetailPartial = "RelevePontBasculeDetailPartial";

            /// <summary>OrderRows</summary>
            public const string OrderRows = "OrderRows";

            public const string Validate = "Validate";

            /// <summary>ChampsOperationPartial</summary>
            public const string ChampsOperationPartial = "ChampsOperationPartial";

            /// <summary>EquipeTravailleurPartial</summary>
            public const string EquipeTravailleurPartial = "EquipeTravailleurPartial";

            /// <summary>GetUtilisateursBySection</summary>
            public const string GetUtilisateursBySection = "GetUtilisateursBySection";

            /// <summary>ArticlePartial</summary>
            public const string ArticlePartial = "ArticlePartial";
            /// <summary>ArticlePartial</summary>
            public const string ArticleEmploiPartial = "ArticleEmploiPartial";

            /// <summary>ArticleMatierePremierePartial</summary>
            public const string ArticleMatierePremierePartial = "ArticleMatierePremierePartial";

            /// <summary>ArticleProduitPartial</summary>
            public const string ArticleProduitPartial = "ArticleProduitPartial";

            
            /// <summary>EmploiPartial</summary>
            public const string EmploiPartial = "EmploiPartial";

        }
        #endregion

        #region Controllers
        /// <summary>
        /// Class listing controllers (constants)
        /// </summary>
        public static class Controllers
        {
            public const string Home = "Home";
            public const string Contact = "Contact";
            public const string DonneesDeBase = "DonneesDeBase";
            public const string Account = "Account";
            public const string Manage = "Manage";
            public const string Fonction = "Fonction";
            public const string Profil = "Profil";
            public const string Departement = "Departement";
            public const string Direction = "Direction";
            public const string Service= "Service";
            public const string Materiel = "Materiel";
            public const string Composant = "Composant";
            public const string Sections = "Sections";
            public const string ProfilRight = "ProfilRight";
            public const string Utilisateur = "Utilisateur";
            public const string UtilisateurRight = "UtilisateurRight";
            public const string SiteUtilisateur = "SiteUtilisateur";
            public const string SectionEMS = "SectionEMS";

            public const string Intervenant = "Intervenant";
            public const string Plantation = "Plantation";
            public const string PlantationAutreModel = "PlantationAutreModel";
            public const string PlantationAutre = "PlantationAutre";



            public const string Cession = "Cession";
            public const string CessionPlantation = "CessionPlantation";
            public const string Activite = "Activite";
            public const string Localite = "Localite";
            public const string PieceIdentite = "PieceIdentite";
            
            public const string Groupement = "Groupement";
            public const string TypeContact = "TypeContact";

            public const string Conseil0 = "Conseil0";
            public const string Conseil1 = "Conseil1";
            public const string Conseil2 = "Conseil2";
            public const string Conseil3 = "Conseil3";
            public const string Conseil4 = "Conseil4";
            public const string Societe = "Societe";
            public const string Site = "Site";
            public const string Rubrique = "Rubrique";
            public const string AnneePlanting = "AnneePlanting";
     

        }
        #endregion

        #region Regular Expressions
        public static class RegularExpressions
        {
            /// <summary>\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*</summary>
            public const string Email = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        }
        #endregion

        #region Numeric Values
        public static class NumericValues
        {
            public static class Length
            {
                public const int nVarChar100 = 100;
                public const int nVarChar50 = 50;
                public const int nVarChar15 = 15;
                public const int nVarChar25 = 25;
                public const int nVarChar20 = 20;
                public const int nVarChar10 = 10;
                public const int nVarChar6 = 6;
                public const int nVarChar5 = 5;
                public const int int10 = 10;
                public const int nChar1 = 1;
                public const int nChar2 = 2;
                public const int nChar3 = 3;

                /// <summary>Nom: 100</summary>
                public const int NomMax = 100;

                /// <summary>Libellé: 100</summary>
                public const int LibelleMax = 100;
                /// <summary>Libellé: 30</summary>
                public const int Libelle30 = 30;

                /// <summary>CodeRubriqueNiveauMax: 5</summary>
                public const int CodeRubriqueNiveauMax = 5;

                /// <summary>CodeDirectionMax: 10</summary>
                public const int CodeDirectionMax = 10;

                /// <summary>CodeDirectionMax: 10</summary>
                public const int CodeSctionsMax = 10;

                /// <summary>CodeServiceMax: 10</summary>
                public const int CodeServiceMax = 10;


                /// <summary>IdSocieteMax: 10</summary>
                public const int IdSocieteMax = 10;

                /// <summary>Username: 256</summary>
                public const int UsernameMax = 256;

                /// <summary>Password MaxLength: 20</summary>
                public const int PasswordMax = 20;

                /// <summary>Password MinLength: 6</summary>
                public const int PasswordMin = 6;

                /// <summary>Profil nom: 200</summary>
                public const int ProfilNomMax = 200;

                /// <summary>Code Fonction/Action: 100</summary>
                public const int CodeFonctionActionMax = 100;
            }

            /// <summary>FormLayoutElementWidth: 200</summary>
            public const int FormLayoutElementWidth = 200;

            /// <summary>Label width: 110</summary>
            public const int LabelWidth = 110;
        }
        #endregion

        #region Configuration Keys
        public static class ConfigKeys
        {
            /// <summary>
            /// The session timeout (in minutes)
            /// </summary>
            public const string SessionTimeout = "SessionTimeout";

            /// <summary>
            /// The view bag link (used for TempData)
            /// </summary>
            public const string ViewBagLink = "ViewBagLink";
        }
        #endregion

        #region Routes
        public static class Routes
        {
            public const string Add = "Add";
            public const string AddCode = "Add/{code}";
            public const string AddIdUser = "Add/{idUser}";
            public const string AddId = "Add/{id}";
            public const string DeleteId = "Delete/{id}";
            public const string DeleteIdArticle = "Delete/{idArticle}";
            public const string DeleteIdCompteAnalytiqueIdSociete = "Delete/{IdCompteAnalytique}/{IdSociete}";
            public const string EditIdCompteAnalytiqueIdSociete = "Edit/{IdCompteAnalytique}/{IdSociete}";
            public const string DeleteIdIndicateurDeReference = "Delete/{IdIndicateur}/{exercice}/{IdRapport1}";
            public const string EditIdIdIndicateurDeReference = "Edit/{IdIndicateur}/{exercice}/{IdRapport1}";
            public const string DeleteIdNature = "Delete/{IdNature}";
            public const string EditIdNature = "Edit/{IdNature}";

            public const string DeleteIdBudgetRapport1 = "Delete/{CodePeriode}/{IdRapport1}/{IdSite}";
            public const string EditIdIdBudgetRapport1 = "Edit/{CodePeriode}/{IdRapport1}/{IdSite}";

            public const string DeleteIdBudgetRapport3 = "Delete/{CodePeriode}/{IdRapport3}/{IdSite}";
            public const string EditIdIdBudgetRapport3 = "Edit/{CodePeriode}/{IdRapport3}/{IdSite}";

            public const string EditPeriodeId = "Edit/{PeriodeID}";
            public const string DeletePeriodeId = "Delete/{PeriodeID}";

            public const string EditRecensement = "Edit/{Id}/{CodeBloc}/{CodeVersionBudget}";
            public const string EditId = "Edit/{id}";
            public const string EditIdArticle = "Edit/{idArticle}";
            public const string IndexId = "Index/{id}";
            public const string IndexBloc = "Index/{Exercice}/{CodeVersionBudget}/{AfficherBlocAbattu}/{AfficherBlocExistant}";
            public const string IndexIdUser = "Index/{idUser}";
            public const string IndexActeur = "Index/{typeActeurID}";

            /// <summary>Delete</summary>
            public const string Delete = "Delete";

            /// <summary>Edit</summary>
            public const string Edit = "Edit";

            /// <summary>Edit/{code}</summary>
            public const string EditCode = "Edit/{code}";

            /// <summary>Edit/{LocaliteID}</summary>
            public const string EditLocaliteID = "Edit/{LocaliteID}";
            /// <summary>Delete/{LocaliteID}</summary>
            public const string DeleteLocaliteID = "Delete/{LocaliteID}";

            /// <summary>Edit/{PieceIdentiteID}</summary>
            public const string EditPieceIdentiteID = "Edit/{PieceIdentiteID}";
            /// <summary>Delete/{PieceIdentiteID}</summary>
            public const string DeletePieceIdentiteID = "Delete/{PieceIdentiteID}";

            /// <summary>Edit/{GroupementID}</summary>
            public const string EditGroupementID = "Edit/{GroupementID}";
            /// <summary>Delete/{GroupementID}</summary>
            public const string DeleteGroupementID = "Delete/{GroupementID}";

            /// <summary>Edit/{ActiviteID}</summary>
            public const string EditActiviteID = "Edit/{ActiviteID}";
            /// <summary>Delete/{ActiviteID}</summary>
            public const string DeleteActiviteID = "Delete/{ActiviteID}";



            /// <summary>Edit/{TypeContactID}</summary>
            public const string EditTypeContactID = "Edit/{TypeContactID}";
            /// <summary>Delete/{TypeContactID}</summary>
            public const string DeleteTypeContactID = "Delete/{TypeContactID}";

            /// <summary>Edit/{codeNumeroBudget}</summary>
            public const string EditCodeNumeroBudget = "Edit/{codeNumeroBudget}";

            /// <summary>Delete/{codeNumeroBudget}</summary>
            public const string DeleteCodeNumeroBudget = "Delete/{codeNumeroBudget}";

            /// <summary>Edit/{CodeEmploi}</summary>
            public const string EditCodeEmploi = "Edit/{codeEmploi}";

            /// <summary>Delete/{CodeEmploi}</summary>
            public const string DeleteCodeEmploi = "Delete/{codeEmploi}";

            /// <summary>Edit/{IdAnneeCulture}</summary>
            public const string EditAnneeCulture = "Edit/{IdAnneeCulture}";

            /// <summary>Delete/{IdAnneeCulture}</summary>
            public const string DeleteAnneeCulture = "Delete/{IdAnneeCulture}";

            /// <summary>Edit/{ExerciceDebut}/{CodeBloc}/{CodeBlocBudget}</summary>
            public const string EditEvolutionBlocBudget = "Edit/{ExerciceDebut}/{CodeBloc}/{CodeBlocBudget}";

            /// <summary>Delete/{ExerciceDebut}/{CodeBloc}/{CodeBlocBudget}</summary>
            public const string DeleteEvolutionBlocBudget = "Delete/{ExerciceDebut}/{CodeBloc}/{CodeBlocBudget}";

            /// <summary>Edit/{IdNature}/{CodeNumeroBudget}</summary>
            public const string EditPrixUnitaire = "Edit/{idRubriqueGroupe}/{codeNumeroBudget}";

            /// <summary>Delete/{IdNature}/{CodeNumeroBudget}</summary>
            public const string DeletePrixUnitaire = "Delete/{IdNature}/{CodeNumeroBudget}";

            /// <summary>Edit/{ExerciceDebut}/{CodeBloc}/{CodeBlocBudget}</summary>
            public const string EditEvolutionBloc = "Edit/{PeriodeDebut}/{CodeBloc}/{CodeNumeroBudget}";

            /// <summary>Delete/{ExerciceDebut}/{CodeBloc}/{CodeBlocBudget}</summary>
            public const string DeleteEvolutionBloc = "Delete/{PeriodeDebut}/{CodeBloc}/{CodeNumeroBudget}";

            /// <summary>Edit/{CodePeriode}/{IdSysCulture}/{IdArticleEmploi}</summary>
            public const string EditPrevisionEffectif = "Edit/{CodePeriode}/{CodeVersionBudget}/{IdArticleEmploi}";
            /// <summary>Delete/{CodePeriode}/{IdSysCulture}/{IdArticleEmploi}/{IdCentreDeCoutSite}</summary>
            public const string DeletePrevisionEffectif = "Delete/{CodePeriode}/{CodeVersionBudget}/{IdArticleEmploi}/{IdCentreDeCoutSite}";

            /// <summary>Edit/{CodePeriode}/{CodeBloc}/{CodeNumeroBudget}</summary>
            public const string EditPrevisionBloc = "Edit/{CodePeriode}/{CodeBloc}/{CodeNumeroBudget}";
            /// <summary>Delete/{CodePeriode}/{CodeBloc}/{CodeNumeroBudget}</summary>
            public const string DeletePrevisionBloc = "Delete/{CodePeriode}/{CodeBloc}/{CodeNumeroBudget}";

            /// <summary>Edit/{CodePeriode}/{IdSysCulture}/{CodeEmploi}</summary>
            public const string EditEffectifParService = "Edit/{IdExercice}/{IdArticleEmploi}/{IdService}";
            /// <summary>Delete/{CodePeriode}/{IdSysCulture}/{CodeEmploi}</summary>
            public const string DeleteEffectifParService = "Delete/{IdExercice}/{IdArticleEmploi}/{IdService}";

            /// <summary>"Edit/{IdService}/{IdArticle}/{IdArticleEmploi}/{IdExercice}";</summary>
            public const string EditVentilationOutillage = "Edit/{IdService}/{IdArticle}/{IdArticleEmploi}/{CodeVersionBudget}";
            /// <summary>Delete/{IdCentreDeCoutSite}/{IdArticle}/{IdArticleEmploi}/{IdExercice}</summary>
            public const string DeleteVentilationOutillage = "Delete/{IdCentreDeCoutSite}/{IdArticle}/{IdArticleEmploi}/{CodeVersionBudget}";

            /// <summary>Edit/{IdService}/{IdArticleEmploi}/{CodePeriode}</summary>
            public const string EditArticleParEmploi = "Edit/{IdService}/{IdArticleEmploi}/{CodePeriode}";
            /// <summary>Delete/IdService/{IdArticleEmploi}/{CodePeriode}/{IdArticle}</summary>
            public const string DeleteArticleParEmploi = "Delete/{IdService}/{IdArticleEmploi}/{CodePeriode}/{IdArticle}";

            /// <summary>Edit/{CodePeriode}/{CodeNumeroBudget}/{IdConditionnement}</summary>
            public const string EditPrevisionUsinage = "Edit/{CodePeriode}/{CodeNumeroBudget}/{IdConditionnement}";
            /// <summary>Delete/{CodePeriode}/{CodeNumeroBudget}/{IdConditionnement}</summary>
            public const string DeletePrevisionUsinage = "Delete/{CodePeriode}/{CodeNumeroBudget}/{IdConditionnement}";

            /// <summary>Edit/{IdSysCulture}/{CodeNumeroBudget}/{IdArticle}</summary>
            public const string EditRatioConsommation = "Edit/{IdSysCulture}/{codeVersionBudget}/{IdArticle}";
            /// <summary>Delete/{IdSysCulture}/{CodeNumeroBudget}/{IdArticle}</summary>
            public const string DeleteRatioConsommation = "Delete/{IdSysCulture}/{codeVersionBudget}/{IdArticle}";

            /// <summary>Delete/{IdSysCulture}/{CodeNumeroBudget}/{IdArticle}</summary>
            public const string DeleteNormeTransport = "Delete/{IdArticle}/{codeDivision}";

            /// <summary>Delete/{code}</summary>
            public const string DeleteCode = "Delete/{code}";

            /// <summary>Index/{code}</summary>
            public const string IndexCode = "Index/{code}";

            /// <summary>Add/{idProfil}</summary>
            public const string AddIdProfil = "Add/{idProfil}";

            /// <summary>Index/{idProfil}</summary>
            public const string IndexIdProfil = "Index/{idProfil}";

            /// <summary>Edit/{idProfil}/{codeFonction}</summary>
            public const string EditIdProfilCodeFonction = "Edit/{idProfil}/{codeFonction}";

            /// <summary>Edit/{idUtilisateur}/{codeFonction}/{codeAction}</summary>
            public const string EditIdUtilisateurCodeFonctionCodeAction = "Edit/{idUtilisateur}/{codeFonction}/{codeAction}";

            /// <summary>Delete/{idUtilisateur}/{codeFonction}/{codeAction}</summary>
            public const string DeleteIdUtilisateurCodeFonctionCodeAction = "Delete/{idUtilisateur}/{codeFonction}/{codeAction}";

            /// <summary>Delete/{idProfil}/{codeFonction}</summary>
            public const string DeleteIdProfilCodeFonction = "Delete/{idProfil}/{codeFonction}";

            /// <summary>Index/{idUtilisateur}</summary>
            public const string IndexIdUtilisateur = "Index/{idUtilisateur}";

            /// <summary>Add/{idUtilisateur}</summary>
            public const string AddIdUtilisateur = "Add/{idUtilisateur}";

            /// <summary>Edit/{idUser}</summary>
            public const string EditIdUser = "Edit/{idUser}";

            /// <summary>Edit/{codeVersionBudget}</summary>
            public const string EditCodeVersionBudget = "Edit/{codeVersionBudget}";

            /// <summary>Delete/{codeVersionBudget}</summary>
            public const string DeleteCodeVersionBudget = "Delete/{codeVersionBudget}";

            /// <summary>Edit/{idCloneBudget}/{codeFonction}</summary>
            public const string EditCoefficientProductionHevea = "Edit/{idCloneBudget}/{anneeSaigneeDebut}";

            /// <summary>Delete/{idProfil}/{codeFonction}</summary>
            public const string DeleteCoefficientProductionHevea = "Delete/{idCloneBudget}/{anneeSaigneeDebut}";

            /// <summary>Edit/{idCloneBudget}/{codeFonction}</summary>
            public const string EditCoefficientProductionPalmier = "Edit/{codeVersionBudget}/{idAnneeCulture}";

            /// <summary>Delete/{idProfil}/{codeFonction}</summary>
            public const string DeleteCoefficientProductionPalmier = "Delete/{codeVersionBudget}/{idAnneeCulture}";

            /// <summary>Edit/{idRubriqueGroupe}</summary>
            public const string EditIdRubriqueGroupe = "Edit/{idRubriqueGroupe}";

            /// <summary>Delete/{idRubriqueGroupe}</summary>
            public const string DeleteIdRubriqueGroupe = "Delete/{idRubriqueGroupe}";

            /// <summary>Delete/{idSociete}/{articleALier}/{articleDeReference}</summary>
            public const string DeleteArticlesLies = "Delete/{idSociete}/{articleALier}/{articleDeReference}";            

        }
        #endregion

        #region Keys
        public static class Keys
        {
            public const string FieldRequiredKey = "FieldRequiredKey";
        }
        #endregion

        #region DateTime Formats
        public static class DateTimeFormats
        {
            public const string DateOnly = "dd/MM/yyyy";
        }
        #endregion
    }

    public static class SinbaClaims
    {
        public static class Type
        {
            public const string Menu = "Menu";
            public const string FullName = "FullName";
            public const string IpAddress = "IpAddress";
            public const string UserSiteId = "UserSiteId";
            public const string UserSocieteId = "UserSocieteId";
            public const string Exercice = "Exercice";
            public const string CodeVersionBudget = "CodeVersionBudget";
        }

        public const string UserSiteId = "UserSiteId";
        public const string UserCultureId = "UserCultureId";
        public const string Menu = "Menu";
        public const string FullName = "FullName";
        public const string UserCultures = "UserCultures";
        public const string UserSocieteId = "UserSocieteId";
        public const string UserPaysId = "UserPaysId";
        public const string IpAddress = "IpAddress";
        public const string AccesSections = "AccesSections";
        public const string CodeAbsence = "CodeAbsence";
        public const string CodePresence = "CodePresence";
        public const string CodePresenceNuit = "CodePresenceNuit";
        public const string AfficherStatsPrestations = "AfficherStatsPrestations";
        public const string HeuresJournee = "HeuresJournee";
        public const string DossierSageHrm = "DossierSageHrm";
        public const string UserSocieteSite = "UserSocieteSite";
        public const string UserSitesAndCultures = "UserSitesAndCultures";
        public const string DisplayLanguage = "DisplayLanguage";
        public const string UserDomain = "UserDomain";
        public const string Exercice = "Exercice";
        public const string CodeVersionBudget = "CodeVersionBudget";

    }

    public static class SinbaRoles
    {
        /// <summary>Admin</summary>
        public const string Admin = "Admin";

        /// <summary>SuperAdmin</summary>
        public const string SuperAdmin = "SuperAdmin";

        /// <summary>AdminSite</summary>
        public const string AdminSite = "AdminSite";


    }

    public class DatabaseConstants
    {
        public const string DefaultLanguage = "fr";
        public const string UniteMesureHeuresId = "HR";

        public static class Tolerance
        {
            public const int Standard = 3001;
            public const int Fusarium = 3002;
            public const int Ganodema = 3003;
            public const int FusaGano = 3004;
        }

        public static class UnitesMesure
        {
            public static class Poids
            {
                public const string Kg = "KG";
                public const string Tonne = "T";
            }
            public static class Temps
            {
                public const string Secondes = "s";
                public const string Millisecondes = "ms";
            }
        }

        public static class DistributionTarget
        {
            public const decimal Immature = (decimal)(3.0 / 31.0);
            public const decimal Young = (decimal)(5.0 / 31.0);
            public const decimal Prime = (decimal)(13.0 / 31.0);
            public const decimal Old = (decimal)(10.0 / 31.0);
        }

        public class TypeChamps
        {
            public static readonly TypeChamp Bool = new TypeChamp(0, EntityCommonResource.CaseACocher);
            public static readonly TypeChamp Numerique = new TypeChamp(1, EntityCommonResource.Numerique);
            public static readonly TypeChamp ListeDeroulante = new TypeChamp(2, EntityCommonResource.ListeDeroulante);
            public static readonly TypeChamp ListeChoixMultiples = new TypeChamp(3, EntityCommonResource.ListeAChoixMultiples);

            public static readonly List<TypeChamp> TypeChampsListe = new List<TypeChamp> { Bool, Numerique, ListeDeroulante, ListeChoixMultiples };

            public class TypeChamp
            {
                public byte Id { get; }
                public string Libelle { get; }
                public TypeChamp(byte Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }

        public class UniteTerrains
        {
            public static readonly UniteTerrain Bloc = new UniteTerrain("B", EntityColumnResource.Bloc);
            public static readonly UniteTerrain Parcelle = new UniteTerrain("P", EntityColumnResource.Parcelle);

            public static readonly List<UniteTerrain> UniteTerrainListe = new List<UniteTerrain> { Bloc, Parcelle };

            public class UniteTerrain
            {
                public string Id { get; }
                public string Libelle { get; }
                public UniteTerrain(string Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }

            public class UniteTerrainComparer : IEqualityComparer<UniteTerrain>
            {
                public bool Equals(UniteTerrain x, UniteTerrain y)
                {
                    return x.Id == y.Id;
                }
                public int GetHashCode(UniteTerrain obj)
                {
                    return obj.Id.GetHashCode();
                }
            }
        }
        public class TypeLignes
        {
            public static readonly TypeLigne Simple = new TypeLigne(false, StringResource.Simple);
            public static readonly TypeLigne Double = new TypeLigne(true, StringResource.Double);

            public static readonly List<TypeLigne> TypeLigneListe = new List<TypeLigne> { Simple, Double };

            public class TypeLigne
            {
                public bool Id { get; }
                public string Libelle { get; }
                public TypeLigne(bool Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }

        public class Genres
        {
            public static readonly Genre Homme = new Genre("M", StringResource.Masculin);
            public static readonly Genre Femme = new Genre("F", StringResource.Feminin);

            public static readonly List<Genre> GenreListe = new List<Genre> { Homme, Femme };

            public class Genre
            {
                public string Id { get; }
                public string Libelle { get; }
                public Genre(string Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }

        public class TypesFichier
        {
            public static readonly TypeFichier Xls = new TypeFichier("XLS", EntityCommonResource.Xls);
            public static readonly TypeFichier Xlsx = new TypeFichier("XLSX", EntityCommonResource.Xlsx);

            public static readonly List<TypeFichier> TypeFichierListe = new List<TypeFichier> { Xls, Xlsx };

            public class TypeFichier
            {
                public string Id { get; }
                public string Nom { get; }
                public TypeFichier(string Id, string Nom)
                {
                    this.Id = Id;
                    this.Nom = Nom;
                }
            }
        }

        public class EquivalentTerrains
        {
            public static readonly EquivalentTerrain NombreEmplacement = new EquivalentTerrain(1, EntityColumnResource.NombreEmplacements);
            public static readonly EquivalentTerrain Surface = new EquivalentTerrain(2, EntityColumnResource.Surface);

            public static List<EquivalentTerrain> GetList() => new List<EquivalentTerrain> { NombreEmplacement, Surface };

            public class EquivalentTerrain
            {
                public int Id { get; }
                public string Libelle { get; }
                public EquivalentTerrain(int Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }

        public class TypeIntervalles
        {
            public static readonly TypeIntervalle DensiteArbresSaignables = new TypeIntervalle(1, EntityCommonResource.DensiteArbresSaignables);
            public static readonly TypeIntervalle NombreAnneesCulture = new TypeIntervalle(2, EntityCommonResource.NombreAnneesCulture);
            public static readonly TypeIntervalle NombreAnneesExploitation = new TypeIntervalle(3, EntityCommonResource.NombreAnneesExploitation);
            public static readonly TypeIntervalle Surface = new TypeIntervalle(4, EntityCommonResource.Surface);

            public static List<TypeIntervalle> GetList() => new List<TypeIntervalle> { DensiteArbresSaignables, NombreAnneesCulture, NombreAnneesExploitation, Surface };

            public class TypeIntervalle
            {
                public byte Id { get; }
                public string Libelle { get; }
                public TypeIntervalle(byte Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }

        public static class ActionValidateur
        {
            public const string Validation = "V";
            public const string Reception = "R";
        }

        public class FeuillePresenceTemplates
        {
            public static readonly FeuillePresenceTemplate FicheChantierEntretienDivers = new FeuillePresenceTemplate("entretiendivers", "Fiche chantier entretien et divers");
            public static readonly FeuillePresenceTemplate FicheChantierEntretienChimique = new FeuillePresenceTemplate("entretienchimique", "Fiche chantier entetien chimique");
            public static readonly FeuillePresenceTemplate FicheChantierRecolte = new FeuillePresenceTemplate("recolte", "Fiche chantier récolte");
            public static readonly FeuillePresenceTemplate FicheChantierSaignee = new FeuillePresenceTemplate("saignee", "Fiche chantier saignée");

            public static readonly List<FeuillePresenceTemplate> FeuillePresenceTemplateListe = new List<FeuillePresenceTemplate> { FicheChantierEntretienDivers, FicheChantierEntretienChimique, FicheChantierRecolte, FicheChantierSaignee };

            public class FeuillePresenceTemplate
            {
                public string Id { get; }
                public string Nom { get; }
                public FeuillePresenceTemplate(string Id, string Nom)
                {
                    this.Id = Id;
                    this.Nom = Nom;
                }
            }
        }

        public class PeriodeStatutTypes
        {
            public static readonly PeriodeStatutType Close = new PeriodeStatutType(0, EntityColumnResource.Closing);
            public static readonly PeriodeStatutType Generate = new PeriodeStatutType(1, EntityColumnResource.Generation);

            public static readonly List<PeriodeStatutType> Liste = new List<PeriodeStatutType> { Close, Generate };

            public class PeriodeStatutType
            {
                public byte Id { get; }
                public string Libelle { get; }
                public PeriodeStatutType(byte Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }

        public static class TypesTravailleurs
        {
            public static readonly TypeTravailleur Tous = new TypeTravailleur(1, EntityColumnResource.Tous);
            public static readonly TypeTravailleur Regie = new TypeTravailleur(2, EntityColumnResource.Regie);
            public static readonly TypeTravailleur Interim = new TypeTravailleur(3, EntityColumnResource.Interim);

            public static readonly List<TypeTravailleur> Liste = new List<TypeTravailleur> { Tous, Regie, Interim };
        }
        public class TypeTravailleur
        {
            public byte Id { get; }
            public string Libelle { get; }
            public TypeTravailleur(byte id, string libelle)
            {
                Id = id;
                Libelle = libelle;
            }
        }

        public static class UnitePrimePenalite
        {
            public const short Montant = 19001;
            public const short Pourcentage = 19002;
        }

        public static class UnitePrimePenaliteRecolte
        {
            public const short Kilogramme = 20001;
            public const short Montant = 20002;
            public const short Pourcentage = 20003;
        }

        public static class StatutsPartSaignee
        {
            public static readonly StatutPartSaignee SansErreur = new StatutPartSaignee(0, EntityColumnResource.PartSaignee);
            public static readonly StatutPartSaignee NonSaignee = new StatutPartSaignee(1, EntityColumnResource.PartNonSaignee);
            public static readonly StatutPartSaignee NonTerminee = new StatutPartSaignee(2, EntityColumnResource.PartNonTerminee);
            public static readonly StatutPartSaignee TacheDepassee = new StatutPartSaignee(3, EntityColumnResource.PartTacheDepassee);

            public static readonly List<StatutPartSaignee> Liste = new List<StatutPartSaignee> { SansErreur, NonSaignee, NonTerminee, TacheDepassee };

            public class StatutPartSaignee
            {
                public byte Id { get; }
                public string Libelle { get; }
                public StatutPartSaignee(byte Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }


        public static class AnneePlanting
        {
            public class Annee
            {
                public short AnneeID { get; set; }
            }

            private const short start = 1980;
            public static List<Annee> ListeAnnee
            {
                get
                {
                    var dates = new List<Annee>();
                    for (short i = start; i <= System.DateTime.Now.Year; i++)
                    {
                        dates.Add(new Annee() { AnneeID =i } );
                    }
                    return dates;

                }
            }
        }

      

        public class Actif
        {
            public static readonly Act Acti = new Act("Actif", "Actif");
            public static readonly Act Inactif = new Act("Inactif", "Inactif");
            public static readonly Act Tout = new Act("*", "Tout");

            public static readonly List<Act> ActListe = new List<Act> { Acti, Inactif, Tout };

            public class Act
            {
                public string Id { get; }
                public string Libelle { get; }
                public Act(string Id, string Libelle)
                {
                    this.Id = Id;
                    this.Libelle = Libelle;
                }
            }
        }
    }

}
