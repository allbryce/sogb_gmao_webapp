var critereValidated = true;
var SuperficieString = "";
var CulturePratiqueeString = "";
var DonneesGeographiqueString = "";
var gridViewValid = true;
var globalValidation = false, editInvoked = false;
var AddMode = true;
var msgUnique = "";
var idTypeContact = '';


function Afficher(s, e) {
    critereValidated = true;
    if (critereValidated)  
        gridPlanteur.PerformCallback({
            PlanteurID: PlanteurID.GetValue(),
            NumIdentification: NumIdentification.GetValue(),
            NomPlanteur: NomPlanteur.GetValue(),
            PrenomPlanteur: PrenomPlanteur.GetValue()
        });
}

function gridPlanteurOnBeginCallback(s, e) {
    e.customArgs['NumIdentification'] = NumIdentification.GetValue() || 0;
    e.customArgs['PlanteurID'] = PlanteurID.GetValue() || "";
    e.customArgs['NomPlanteur'] = NomPlanteur.GetValue() || "";
    e.customArgs['PrenomPlanteur'] = PrenomPlanteur.GetValue() || "";
}

function OpenAddPlanteurModal() {
    AddPlanteurModal.PerformCallback({AddMode: true });
    AddPlanteurModal.Show();
}

function OpenEditPlanteurModal(item) {
    EditPlanteurModal.PerformCallback({PlanteurID: item.PlanteurID, AddMode: false });
    EditPlanteurModal.Show();
}

function OpenDeletePlanteurModal(item) {
    $("#IdToDelete").val(item.PlanteurID);
    DeletePlanteurModal.PerformCallback({PlanteurID: item.PlanteurID});
    DeletePlanteurModal.Show();
}

function Add(s, e) {
    var dateNais, dateDelivrancePiece
    dateNais = addDateNaissance.GetValue();
    dateDelivrancePiece = addDateDelivrancePiece.GetValue();
    if (addDateNaissance.GetValue() !== null) {
        dateNais = addDateNaissance.GetValue().toUTCString()
    }
    if (addDateDelivrancePiece.GetValue() !== null) {
        dateDelivrancePiece = addDateDelivrancePiece.GetValue().toUTCString()
    }
    var ajoutreussi = false;
    var model = {
        PlanteurID: addPlanteurID.GetValue(),
        NumIdentification: addNumIdentification.GetValue(),
        NomPlanteur: addNomPlanteur.GetValue(),
        PrenomPlanteur: addPrenomPlanteur.GetValue(),
        DateNaissance: dateNais,
        LieuNaissance: addLieuNaissance.GetValue(),
        SexePlanteur: addSexePlanteur.GetValue(),
        VillagePlanteur: addVillagePlanteur.GetValue(),
        PieceIdentiteID: addPieceIdentiteID.GetValue(),
        NumeroPiecePlanteur: addNumeroPiecePlanteur.GetValue(),
        DateDelivrancePiece: dateDelivrancePiece,
        LieuDelivrancePiece: addLieuDelivrancePiece.GetValue(),
        Adresse: addAdresse.GetValue(),
        NumeroTelephone: addNumeroTelephone.GetValue(),
        NumeroTelephone1: addNumeroTelephone1.GetValue(),
        NumeroTelephone2: addNumeroTelephone2.GetValue(),
        AdresseMail1: addAdresseMail1.GetValue(),
        AdresseMail2: addAdresseMail2.GetValue(),
        IDTbPlanteursID: addIDTbPlanteursID.GetValue(),
        ContactID: addContactID.GetValue()
    };
    $.ajax({
        url: $("#Add").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                ajoutreussi = true;
            }
        }, error: function (e) {
            ajoutreussi = false;
        }
    });

    if (ajoutreussi) {
        AddPlanteurModal.Hide();
        gridPlanteur.PerformCallback({ PlanteurID: PlanteurID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
        ajoutreussi = false;
    }
}

function Edit(s, e) {
    var dateNais, dateDelivrancePiece
    dateNais = editDateNaissance.GetValue();
    dateDelivrancePiece = editDateDelivrancePiece.GetValue();
    if (editDateNaissance.GetValue() !== null) {
        dateNais = editDateNaissance.GetValue().toUTCString()
    }
    if (editDateDelivrancePiece.GetValue() !== null) {
        dateDelivrancePiece = editDateDelivrancePiece.GetValue().toUTCString()
    }
    var editreussi = false;
    var model = {
        PlanteurID: editPlanteurID.GetValue(),
        NumIdentification: editNumIdentification.GetValue(),
        NomPlanteur: editNomPlanteur.GetValue(),
        PrenomPlanteur: editPrenomPlanteur.GetValue(),
        DateNaissance: dateNais, //editDateNaissance.GetValue().toUTCString(),
        LieuNaissance: editLieuNaissance.GetValue(),
        SexePlanteur: editSexePlanteur.GetValue(),
        VillagePlanteur: editVillagePlanteur.GetValue(),
        PieceIdentiteID: editPieceIdentiteID.GetValue(),
        NumeroPiecePlanteur: editNumeroPiecePlanteur.GetValue(),
        DateDelivrancePiece: dateDelivrancePiece,//editDateDelivrancePiece.GetValue().toUTCString(),
        LieuDelivrancePiece: editLieuDelivrancePiece.GetValue(),
        Adresse: editAdresse.GetValue(),
        NumeroTelephone: editNumeroTelephone.GetValue(),
        NumeroTelephone1: editNumeroTelephone1.GetValue(),
        NumeroTelephone2: editNumeroTelephone2.GetValue(),
        AdresseMail1: editAdresseMail1.GetValue(),
        AdresseMail2: editAdresseMail2.GetValue(),
        IDTbPlanteursID: editIDTbPlanteursID.GetValue()
    };
    $.ajax({
        url: $("#Edit").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                //if (result === true) {
                editreussi = true;
            }
        }, error: function (e) {
            editreussi = false;
        }
    });

    if (editreussi) {
        EditPlanteurModal.Hide();
        gridPlanteur.PerformCallback({ PlanteurID: PlanteurID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
        editreussi = false;
    }
}

function Remove(s, e) {
    var deletereussi = false;
    var model = {
        PlanteurID: $("#IdToDelete").val()
    };
    $.ajax({
        url: $("#Delete").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                deletereussi = true;
            }
        }, error: function (e) {
            deletereussi = false;
        }
    });
    if (deletereussi) {
        DeletePlanteurModal.Hide();
        deletereussi = false;
        gridPlanteur.PerformCallback({ PlanteurID: PlanteurID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
    }
}


function gridProjetPlanteurOnBeginCallback(s, e) {
    e.customArgs['NumeroPlanteur'] = $("IdToNumeroPlanteur").val || "";
}

function OpenAddProjetPlanteurModal(item) {
  //  $("IdToNumeroPlanteur").val(item.NumeroPlanteur);
    AddMode = true;
    AddProjetPlanteurModal.PerformCallback({ NumeroPlanteur: item.NumeroPlanteur, AddMode: true });
    AddProjetPlanteurModal.Show();
}

function OpenEditProjetPlanteurModal(item) {
  // $("IdToNumeroPlanteur").val(item.NumeroPlanteur);
     AddMode = false;
    AddProjetPlanteurModal.PerformCallback({ NumeroPlanteur: item.NumeroPlanteur, ProjetPlanteurID: item.ProjetPlanteurID, AddMode: false });
    AddProjetPlanteurModal.Show();
}

function OpenDeleteProjetPlanteurModal(item) {
    $("#IdToDelete1").val(item.ProjetPlanteurID);
    $("IdToNumeroPlanteur").val(item.NumeroPlanteur);
    DeleteProjetPlanteurModal.PerformCallback({ProjetPlanteurID: item.ProjetPlanteurID });
    DeleteProjetPlanteurModal.Show();
}

function AddProjetPlanteur(s, e) {
    var ajoutreussi = false;
    var model = {
        ProjetPlanteurID : addProjetPlanteurID.GetValue(),
        NumeroPlanteur : addNumeroPlanteur.GetValue(),
        GroupementID : addGroupementID.GetValue(),
        CompteGeneral : addCompteGeneral.GetValue(),
        //NumeroCompte : addNumeroCompte.GetValue(),
        //ModePaiementID : addModePaiementID.GetValue(),
        //BanqueID : addBanqueID.GetValue(),
       // ClefPlanteur : addClefPlanteur.GetValue(),
        //RibPlanteur: addRibPlanteur.GetValue(),
        KilometrePlanteur : addKilometrePlanteur.GetValue(),
        CodeSecuriteID : addCodeSecuriteID.GetValue(),
        CompteContribuable : addCompteContribuable.GetValue(),
        LocaliteID : addLocaliteID.GetValue(),  
       // NumeroDelegue : addNumeroDelegue.GetValue(),
        PrixTpKg : addPrixTpKgTpt.GetValue(),
        DistanceEstimee : addDistanceEstimee.GetValue(),
        KmFacture : addKmFacture.GetValue(),
        PrimeExceptionnelle : addPrimeExceptionnelle.GetValue(),
        Analytique : addAnalytique.GetValue(),
        PrixTpKgTpt : addPrixTpKgTpt.GetValue(),
        ReversPreCollecte : addReversPreCollecte.GetValue(),
        ReversActionnaire : addReversActionnaire.GetValue(),
        PlanfondPoids : addPlanfondPoids.GetValue(),
        PrimePlanfondPoids: addPrimePlanfondPoids.GetValue(),
        RetSubCollecte: addRetSubCollecte.GetValue(),
        RetenuePreCollecte: addRetenuePreCollecte.GetValue(),
        RetSubPreCollecte : addRetSubPreCollecte.GetValue(),
        RevSubCollecte : addRevSubCollecte.GetValue(),
        RevSubPreCollecte : addRevSubPreCollecte.GetValue(),
        PlanfondPoids2 : addPlanfondPoids2.GetValue(),
        PrimePlanfondPoids2 : addPrimePlanfondPoids2.GetValue(),
        PlanfondPoids3 : addPlanfondPoids3.GetValue(),
        PrimePlanfondPoids3 : addPrimePlanfondPoids3.GetValue(),
        PlanfondPoids4 : addPlanfondPoids4.GetValue(),
        PrimePlanfondPoids4 : addPrimePlanfondPoids4.GetValue(),
        Bloque : addBloque.GetValue(),
        ZonePlanteurID : addZonePlanteurID.GetValue(),
        NoApromac : addNoApromac.GetValue(),
        P2ASaisir : addP2ASaisir.GetValue(),
        SuperficieString: SuperficieString,
        CulturePratiqueeString: CulturePratiqueeString,
        DonneesGeographiqueString: DonneesGeographiqueString,
        AddMode: AddMode
        // PayeBordChamps : addPayeBordChamps.GetValue(),
       // EstDelegue : addKmFacture.GetValue()
    };

    $.ajax({
        url: $("#AddProjetPlanteur").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                ajoutreussi = true;
            }
        }, error: function (e) {
            ajoutreussi = false;
        }
    });

    if (ajoutreussi) {
        
        AddProjetPlanteurModal.Hide();
        gridPlanteur.PerformCallback({ PlanteurID: PlanteurID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
        ajoutreussi = false;
    }
}

function EditProjetPlanteur(s, e) {
    
    var editreussi = false;
    var model = {
        ProjetPlanteurID : editProjetPlanteurID.GetValue(),
        NumeroPlanteur : editNumeroPlanteur.GetValue(),
        GroupementID : editGroupementID.GetValue(),
        //CompteGeneral : editCompteGeneral.GetValue(),
        //NumeroCompte : editNumeroCompte.GetValue(),
        //ModePaiementID : editModePaiementID.GetValue(),
        //BanqueID : editBanqueID.GetValue(),
        //ClefPlanteur : editClefPlanteur.GetValue(),
        //RibPlanteur: editRibPlanteur.GetValue(),
        KilometrePlanteur : editKilometrePlanteur.GetValue(),
        CodeSecuriteID : editCodeSecuriteID.GetValue(),
        CompteContribuable : editCompteContribuable.GetValue(),
        LocaliteID : editLocaliteID.GetValue(),
        //NumeroDelegue : editNumeroDelegue.GetValue(),
        PrixTpKg : editPrixTpKgTpt.GetValue(),
        DistanceEstimee : editDistanceEstimee.GetValue(),
        KmFacture : editKmFacture.GetValue(),
        PrimeExceptionnelle : editPrimeExceptionnelle.GetValue(),
        Analytique : editAnalytique.GetValue(),
        PrixTpKgTpt : editPrixTpKgTpt.GetValue(),
        ReversPreCollecte : editReversPreCollecte.GetValue(),
        ReversActionnaire : editReversActionnaire.GetValue(),
        PlanfondPoids : editPlanfondPoids.GetValue(),
        PrimePlanfondPoids : editPrimePlanfondPoids.GetValue(),
        RetSubCollecte : editRetSubCollecte.GetValue(),
        RetSubPreCollecte : editRetSubPreCollecte.GetValue(),
        RevSubCollecte : editRevSubCollecte.GetValue(),
        RevSubPreCollecte : editRevSubPreCollecte.GetValue(),
        PlanfondPoids2 : editPlanfondPoids2.GetValue(),
        PrimePlanfondPoids2 : editPrimePlanfondPoids2.GetValue(),
        PlanfondPoids3 : editPlanfondPoids3.GetValue(),
        PrimePlanfondPoids3 : editPrimePlanfondPoids3.GetValue(),
        PlanfondPoids4 : editPlanfondPoids4.GetValue(),
        PrimePlanfondPoids4 : editPrimePlanfondPoids4.GetValue(),
        Bloque : editBloque.GetValue(),
        ZonePlanteurID : editZonePlanteurID.GetValue(),
        NoApromac : editNoApromac.GetValue(),
        P2ASaisir: editP2ASaisir.GetValue(),
        SuperficieString: SuperficieString,
        CulturePratiqueeString: CulturePratiqueeString,
        DonneesGeographiqueString: DonneesGeographiqueString
        //PayeBordChamps : editPayeBordChamps.GetValue(),
        //EstDelegue : editKmFacture.GetValue()
    };

    $.ajax({
        url: $("#EditProjetPlanteur").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                //if (result === true) {
                editreussi = true;
            }
        }, error: function (e) {
            editreussi = false;
        }
    });

    if (editreussi) {
        EditProjetPlanteurModal.Hide();
        gridPlanteur.PerformCallback({ PlanteurID: PlanteurID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
        editreussi = false;
    }
}

function RemoveProjetPlanteur(s, e) {
    var deletereussi = false;
    var model = {
        ProjetPlanteurID: $("#IdToDelete1").val()
       // NumeroPlanteur: $("#IdToNumeroPlanteur").val()
    };
   
    $.ajax({
        url: $("#DeleteProjetPlanteur").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                deletereussi = true;
            }
        }, error: function (e) {
            deletereussi = false;
        }
    });
    if (deletereussi) {
        DeleteProjetPlanteurModal.Hide();
        deletereussi = false;
        gridPlanteur.PerformCallback({ PlanteurID: PlanteurID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
    }

}

function GenerateSurperficieFromGrid() {
    var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
    var idClone,  idAnneeCulture, debutExploitation, superficie1
    var superficie = [];
    if (indices !== null) {
        if (indices.length === 0) {
            SuperficieString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                idClone = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                idAnneeCulture = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCultureID', false);
                debutExploitation = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'DebutExploitation', false);
                superficie1 = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'Superficie1', false);
                if (idClone !== null && idAnneeCulture !== null && debutExploitation !== null && superficie1 !== null) {
                    superficie.push({
                        AnneeCultureID: idAnneeCulture,
                        CloneID: idClone,
                        DebutExploitation: debutExploitation,
                        Superficie1: superficie1
                    });
                } else {
                    return false;
                }
            }
            SuperficieString = JSON.stringify(superficie);
            return true;
        }
    }
    return false;
}

function gridViewSuperficieCustomValidate() {
    globalValidation = true;
    gridViewValid = true;
    var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {
        for (var i = 0; i < indices.length; i++) {
            editInvoked = true;
            gridViewSuperficie.batchEditApi.StartEdit(indices[i], 1);
            gridViewSuperficie.batchEditApi.EndEdit();
            editInvoked = false;
        }
    }
    globalValidation = false;
    return gridViewValid;
}

function gridViewSuperficieBatchEditEndEditing(s, e) {
    var idClone = e.rowValues[s.GetColumnByField('CloneID').index].value;
    var idAnneeCulture = e.rowValues[s.GetColumnByField('AnneeCultureID').index].value;
    var debutExploitation = e.rowValues[s.GetColumnByField('DebutExploitation').index].value;
    var superficie1 = e.rowValues[s.GetColumnByField('Superficie1').index].value;
    if (!editInvoked && idClone !== null && idAnneeCulture !== null && debutExploitation !== null && superficie1 !== null) {
        if (superficie1 > 0)
            setTimeout(function () { gridViewSuperficieCustomValidate(); }, 0);
    }
}

function gridViewSuperficieBatchEditRowValidating(s, e) {
    var validateidClone = e.validationInfo[s.GetColumnByField('CloneID').index];
    var validateidAnneeCulture = e.validationInfo[s.GetColumnByField('AnneeCultureID').index];
   // var validateDebutExploitation = e.validationInfo[s.GetColumnByField('DebutExploitation').index];
    //var validateSuperficie1 = e.validationInfo[s.GetColumnByField('Superficie1').index];

    if (validateidClone && validateidAnneeCulture) {
        var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstSuperficie = [];
            for (var i = 0; i < indices.length; i++) {
                var currentCloneID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                var currenteAnneeCultureID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCultureID', false);
                if (currentCloneID && currenteAnneeCultureID && indices[i] !== e.visibleIndex) {
                    lstSuperficie.push(currentCloneID);

                    //lstSuperficie.push({
                    //    currentCloneID, currenteAnneeCultureID
                    //});
                }
            }
            for (var j = 0; j < lstSuperficie.length; j++) {
                if (lstSuperficie[j] === validateidClone.value) {
                    validateidClone.isValid = false;
                    validateidClone.errorText = msgUnique;
                }
            }
        }
    }
}

function SubmitForm() {
    var validator = $("#frmPlanteur").validate();
    //if (gridViewSuperficie.batchEditApi.ValidateRows()) {
   // if (validator.form()) {
    if (gridViewSuperficie.batchEditApi.ValidateRows() && gridViewSuperficieCustomValidate()) { GenerateSurperficieFromGrid(); }
    if (gridViewCulturePratiquee.batchEditApi.ValidateRows() && gridViewCulturePratiqueeCustomValidate()) { GenerateCulturePratiqueeFromGrid(); }
    if (gridViewDonneesGeographique.batchEditApi.ValidateRows() && gridViewDonneesGeographiqueCustomValidate()) { GenerateDonneesGeographiqueFromGrid(); }

    if (validator.form()) {
        AddProjetPlanteur();
       // if (AddMode === true) { AddProjetPlanteur(); } else { EditProjetPlanteur(); }
    }
    //}
}

function GenerateCulturePratiqueeFromGrid() {
    var indices = gridViewCulturePratiquee.batchEditApi.GetRowVisibleIndices();
    var idCulture, annee
    var CulturePratiquee = [];
    if (indices !== null) {
        if (indices.length === 0) {
            CulturePratiqueeString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                idCulture= gridViewCulturePratiquee.batchEditApi.GetCellValue(indices[i], 'CultureID', false);
                annee = gridViewCulturePratiquee.batchEditApi.GetCellValue(indices[i], 'Annee', false);
                if (idCulture !== null && annee !== null ) {
                    CulturePratiquee.push({
                        CultureID: idCulture,
                        Annee: annee
                    });
                } else {
                    return false;
                }
            }
            CulturePratiqueeString = JSON.stringify(CulturePratiquee);
            return true;
        }
    }
    return false;
}

function gridViewCulturePratiqueeCustomValidate() {
    globalValidation = true;
    gridViewValid = true;
    var indices = gridViewCulturePratiquee.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {
        for (var i = 0; i < indices.length; i++) {
            editInvoked = true;
            gridViewCulturePratiquee.batchEditApi.StartEdit(indices[i], 1);
            gridViewCulturePratiquee.batchEditApi.EndEdit();
            editInvoked = false;
        }
    }
    globalValidation = false;
    return gridViewValid;
}

function gridViewCulturePratiqueeBatchEditEndEditing(s, e) {
    var idCulture = e.rowValues[s.GetColumnByField('CultureID').index].value;
    var idAnnee = e.rowValues[s.GetColumnByField('Annee').index].value;
    if (!editInvoked && idCulture !== null && idAnnee !== null ) {
        if (idAnnee > 0)
            setTimeout(function () { gridViewCulturePratiqueeCustomValidate(); }, 0);
    }
}

function gridViewCulturePratiqueeBatchEditRowValidating(s, e) {
    var validateidCulture = e.validationInfo[s.GetColumnByField('CultureID').index];
    var validateidAnnee = e.validationInfo[s.GetColumnByField('Annee').index];
    if (validateidCulture && validateidAnnee) {
        var indices = gridViewCulturePratiquee.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstCulturePratiquee = [];
            for (var i = 0; i < indices.length; i++) {
                var currentCultureID = gridViewCulturePratiquee.batchEditApi.GetCellValue(indices[i], 'CultureID', false);
                var currentAnnee = gridViewCulturePratiquee.batchEditApi.GetCellValue(indices[i], 'Annee', false);
                if (currentCultureID && currentAnnee && indices[i] !== e.visibleIndex) {
                    lstCulturePratiquee.push(currentCultureID);            
                }
            }
            for (var j = 0; j < lstCulturePratiquee.length; j++) {
                if (lstCulturePratiquee[j] === validateidCulture.value) {
                    validateidCulture.isValid = false;
                    validateidCulture.errorText = msgUnique;
                }
            }
        }
    }
}

function GenerateDonneesGeographiqueFromGrid() {
    var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
    var dateGeolocalisation, numGeolocalisation, coordonneesGPS, donneesGeolocalisation, superficie
    var DonneesGeographique = [];
    if (indices !== null) {
        if (indices.length === 0) {
            DonneesGeographiqueString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                dateGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'DateGeolocalisation', false);
                numGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'NumGeolocalisation', false);
                coordonneesGPS = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'CoordonneesGPS', false);
                donneesGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'DonneesGeolocalisation', false);
                superficie = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                //donneesGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'DonneesGeolocalisation', false);
               // if (dateGeolocalisation !== null && numGeolocalisation !== null && coordonneesGPS !== null && donneesGeolocalisation !== null && superficie !== null) {
                if (dateGeolocalisation !== null && numGeolocalisation !== null && donneesGeolocalisation !== null && superficie !== null) {
                DonneesGeographique.push({
                        DateGeolocalisation: dateGeolocalisation,
                        NumGeolocalisation: numGeolocalisation,
                        CoordonneesGPS: coordonneesGPS,
                        DonneesGeolocalisation: donneesGeolocalisation,
                        Superficie: superficie
                    });
                } else {
                    return false;
                }
            }
            DonneesGeographiqueString = JSON.stringify(DonneesGeographique);
            return true;
        }
    }
    return false;
}

function gridViewDonneesGeographiqueCustomValidate() {
    globalValidation = true;
    gridViewValid = true;
    var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {
        for (var i = 0; i < indices.length; i++) {
            editInvoked = true;
            gridViewDonneesGeographique.batchEditApi.StartEdit(indices[i], 1);
            gridViewDonneesGeographique.batchEditApi.EndEdit();
            editInvoked = false;
        }
    }
    globalValidation = false;
    return gridViewValid;
}

function gridViewDonneesGeographiqueBatchEditEndEditing(s, e) {
    var dateGeolocalisation = e.rowValues[s.GetColumnByField('DateGeolocalisation').index].value;
    var numGeolocalisation = e.rowValues[s.GetColumnByField('NumGeolocalisation').index].value;
    var coordonneesGPS = e.rowValues[s.GetColumnByField('CoordonneesGPS').index].value;
    var donneesGeolocalisation = e.rowValues[s.GetColumnByField('DonneesGeolocalisation').index].value;
    var superficie = e.rowValues[s.GetColumnByField('Superficie').index].value;
    if (!editInvoked && dateGeolocalisation !== null && numGeolocalisation !== null && coordonneesGPS !== null && donneesGeolocalisation !== null && superficie !== null) {
        if (superficie > 0)
            setTimeout(function () { gridViewDonneesGeographiqueCustomValidate(); }, 0);
    }
}

function gridViewDonneesGeographiqueBatchEditRowValidating(s, e) {
    var validatedateGeolocalisation = e.validationInfo[s.GetColumnByField('DateGeolocalisation').index];
    var validateNumGeolocalisation = e.validationInfo[s.GetColumnByField('NumGeolocalisation').index];
    var validateCoordonneesGPS = e.validationInfo[s.GetColumnByField('CoordonneesGPS').index];
    var validateDonneesGeolocalisation = e.validationInfo[s.GetColumnByField('DonneesGeolocalisation').index];
    var validateSuperficie = e.validationInfo[s.GetColumnByField('Superficie').index];
    if (validatedateGeolocalisation && validateNumGeolocalisation && validateCoordonneesGPS && validateDonneesGeolocalisation && validateSuperficie) {
        var indices = gridViewCulturePratiquee.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstDonneesGeographique = [];
            for (var i = 0; i < indices.length; i++) {
                var currentDateGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'DateGeolocalisation', false);
                var currentNumGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'NumGeolocalisation', false);
                if (currentDateGeolocalisation && currentNumGeolocalisation && indices[i] !== e.visibleIndex) {
                    lstDonneesGeographique.push(currentDateGeolocalisation);
                }
            }
            for (var j = 0; j < lstDonneesGeographique.length; j++) {
                if (lstDonneesGeographique[j] === validatedateGeolocalisation.value) {
                    validatedateGeolocalisation.isValid = false;
                    validatedateGeolocalisation.errorText = msgUnique;
                }
            }
        }
    }
}


function idTypeContactSelectedIndexChanged(s, e) {
   // var idContact = s.GetValue() || 0;
    addContactID.SetValue(null);
    addNomPlanteur.SetValue(null);
    addPrenomPlanteur.SetValue(null);
    addDateNaissance.SetValue(null);
    addLieuNaissance.SetValue(null);
    addSexePlanteur.SetValue(null);
    addVillagePlanteur.SetValue(null);
    addPieceIdentiteID.SetValue(null);
    addNumeroPiecePlanteur.SetValue(null);
    addDateDelivrancePiece.SetValue(null);
    addLieuDelivrancePiece.SetValue(null);
    addAdresse.SetValue(null);
    addNumeroTelephone.SetValue(null);
    addNumeroTelephone1.SetValue(null);
    addNumeroTelephone2.SetValue(null);
    addAdresseMail1.SetValue(null);
    addAdresseMail2.SetValue(null);
    //LoadContact(idContact);
}

//function LoadContact(idTypeContact) {
//    addContactID.ClearItems();
//    idTypeContact = addTypeContactID.GetValue();
//    if (idTypeContact !== null) {

//        $.ajax({
//            url: $("#GetContactList").val(),
//            type: 'POST',
//            async: true,
//            data: { 'idTypeContact': idTypeContact },
//            success: function (result) {
//                if (result !== null) {
//                    var arr = $.map(result, function (element) { return element });
//                    // Load ContactID
//                    for (var i = 0; i < arr.length; i++) {
//                        addContactID.AddItem([arr[i].ContactID]);
                      
//                    }
//                }
//            },
//            error: function (e) {
//                window.alert(e);
//            }
//        });
//    }
//}

function addContactIDSelectedIndexChanged(s, e) {
    if (addContactID.GetValue() != null || addContactID.GetValue() != "" ) {
        RenseignerPlanteur();
    }  
}

//function addContactIDLostFocus(s, e) {
//    var idContact = addContactID.GetValue() || "";
//    if (idContact == null ) {
//        addNomPlanteur.ClearItems();
//        addPrenomPlanteur.ClearItems();
//        addDateNaissance.ClearItems();
//        addLieuNaissance.ClearItems();
//        addSexePlanteur.ClearItems();
//        addVillagePlanteur.ClearItems();
//        addPieceIdentiteID.ClearItems();
//        NumeroPieceContact.ClearItems();
//        addDateDelivrancePiece.ClearItems();
//        addLieuDelivrancePiece.ClearItems();
//        addAdresse.ClearItems();
//        addNumeroTelephone.ClearItems();
//        addNumeroTelephone1.ClearItems();
//        addNumeroTelephone2.ClearItems();
//        addAdresseMail1.ClearItems();
//        addAdresseMail2.ClearItems();
//    }
   
//}

/*evenements combobox TypeContactID*/
function addContactIDGotFocus(s, e) {
    idTypeContact = addTypeContactID.GetValue() || "";
    if (idTypeContact != null) {
        addContactID.PerformCallback({
            'idTypeContact': addTypeContactID.GetValue()
        });
    }
   
}
function addContactIDBeginCallback(s, e) {
    e.customArgs['idTypeContact'] = addTypeContactID.GetValue();
}
function addContactIDEndCallback(s, e) {
    if (idTypeContact !== "")
        s.SetValue(idTypeContact);
}

function RenseignerPlanteur() {
    var idContact = ''; 
    idContact = addContactID.GetValue();
    if (idContact !== null ) {
        //Requête ajax
        $.ajax({
            url: $("#GetContact").val(),
            type: 'POST',
            async: false,
            data: { idContact: idContact},
            success: function (result) {
                result = JSON.parse(result);
                if (result !== null) {
                    //if (result !== null) {
                    if (result.NomContact !== null) {
                        addNomPlanteur.SetValue(result.NomContact);
                    }
                    else {
                        addNomPlanteur.SetValue(null);
                    }
                    if (result.PrenomContact !== null) {
                        addPrenomPlanteur.SetValue(result.PrenomContact);
                    }
                    else {
                        addPrenomPlanteur.SetValue(null);
                    }
                    var dateNaissance = new Date(result.DateNaissance); 
                    if (dateNaissance !== null) {
                        addDateNaissance.SetValue(dateNaissance);
                    }
                    else {
                        addDateNaissance.SetValue(null);
                    }

                    if (result.LieuNaissance !== null) {
                        addLieuNaissance.SetValue(result.LieuNaissance);
                    }
                    else {
                        addLieuNaissance.SetValue(null);
                    }

                    if (result.SexeContact !== null) {
                        addSexePlanteur.SetValue(result.SexeContact);
                    }
                    else {
                        addSexePlanteur.SetValue(null);
                    }

                    if (result.VillageContact !== null) {
                        addVillagePlanteur.SetValue(result.VillageContact);
                    }
                    else {
                        addVillagePlanteur.SetValue(null);
                    }
                    if (result.PieceIdentiteID !== null) {
                        addPieceIdentiteID.SetValue(result.PieceIdentiteID);
                    }
                    else {
                        addPieceIdentiteID.SetValue(null);
                    }
                    ///Densite----------------------
                    if (result.NumeroPieceContact !== null) {
                        addNumeroPiecePlanteur.SetValue(result.NumeroPieceContact);
                    }
                    else {
                        addNumeroPiecePlanteur.SetValue(null);
                    }

                    var dateDelivrancePiece = new Date(result.DateDelivrancePiece); 
                    if (dateDelivrancePiece !== null) {
                        addDateDelivrancePiece.SetValue(dateDelivrancePiece);
                    }
                    else {
                        addDateDelivrancePiece.SetValue(null);
                    }

                    if (result.LieuDelivrancePiece !== null) {
                        addLieuDelivrancePiece.SetValue(result.LieuDelivrancePiece);
                    }
                    else {
                        addLieuDelivrancePiece.SetValue(null);
                    }

                    if (result.Adresse !== null) {
                        addAdresse.SetValue(result.Adresse);
                    }
                    else {
                        addAdresse.SetValue(null);
                    }

                    if (result.NumeroTelephone1 !== null) {
                        addNumeroTelephone.SetValue(result.NumeroTelephone1);
                    }
                    else {
                        addNumeroTelephone.SetValue(null);
                    }
                    if (result.NumeroTelephone2 !== null) {
                        addNumeroTelephone1.SetValue(result.NumeroTelephone2);
                    }
                    else {
                        addNumeroTelephone1.SetValue(null);
                    }
                    if (result.NumeroTelephone3 !== null) {
                        addNumeroTelephone2.SetValue(result.NumeroTelephone3);
                    }
                    else {
                        addNumeroTelephone2.SetValue(0);
                    }

                    if (result.AdresseMail1 !== null) {
                        addAdresseMail1.SetValue(result.AdresseMail1);
                    }
                    else {
                        addAdresseMail1.SetValue(null);
                    }

                    if (result.AdresseMail2 !== null) {
                        addAdresseMail2.SetValue(result.AdresseMail2);
                    }
                    else {
                        addAdresseMail2.SetValue(null);
                    }
                }
            },
            error: function () {
                //  window.alert("Error");
            }
        });
    }
}