var critereValidated = true;
var SuperficieString = "";
var IntervenantString = "";
var DonneesGeographiqueString = "";
var gridViewValid = true;
var globalValidation = false, editInvoked = false;
var AddMode = true;
var msgUnique = "";
var idTypeActeur = '';
var idCulture = '';
var totalSuperficie = 0.00;
var personnePhysique = true;


function Afficher(s, e) {
    critereValidated = true;
  //  ValiderCulture();
    //var datNais = DateNaissance.GetValue();
    //if (datNais === null || datNais === "") {
    //    datNais = DateNaissance.GetValue();
    //}
    //else {
    //    datNais = DateNaissance.GetValue().toUTCString();
    //}

    if (critereValidated) {
        gridPlantation.PerformCallback();
    }

        
        //gridPlantation.PerformCallback({
        //    ContactID: ContactID.GetValue(),
        //    Noms: Noms.GetValue(),
        //    NumeroTelephones: NumeroTelephones.GetValue(),
        //    DateNaissance: datNais,
        //    NumeroPiece: NumeroPiece.GetValue(),
        //    AdresseMails: AdresseMails.GetValue(),
        //    NumeroTicketIdentification: NumeroTicketIdentification.GetValue(),
        //    NumeroMatriculeNational: NumeroMatriculeNational.GetValue(),
        //    PersonnePhysique: PersonnePhysique.GetValue()
        //});
}

function ValiderMatriculeOperateur() {
    if (addMatriculeOperateur.GetValue() === null || addMatriculeOperateur.GetValue() === "") {
        critereValidated = false;
        $("#addMatriculeOperateur").addClass('requiredField');
    }
    else {
        $("#addMatriculeOperateur").removeClass('requiredField');
    }
}

function ValiderCulture() {
    if (CultureID.GetValue() === null || CultureID.GetValue() === "") {
        critereValidated = false;
        $("#CultureID").addClass('requiredField');
    }
    else {
        $("#CultureID").removeClass('requiredField');
    }
}

function gridPlantationOnBeginCallback(s, e) {
    //var dateNaissance
    //dateNaissance = DateNaissance.GetValue();
    if (DateNaissance.GetValue() !== null) {
       // dateNaissance = DateNaissance.GetValue().toUTCString();
        e.customArgs['DateNaissance'] = DateNaissance.GetValue().toUTCString();
    }
    e.customArgs['ContactID'] = ContactID.GetValue() || "";
    e.customArgs['Noms'] = Noms.GetValue() || "";
    e.customArgs['NumeroTelephones'] = NumeroTelephones.GetValue() || "";
    //e.customArgs['DateNaissance'] = dateNaissance;
    e.customArgs['NumeroPiece'] = NumeroPiece.GetValue() || "";
    e.customArgs['AdresseMails'] = AdresseMails.GetValue() || "";
    e.customArgs['NumeroTicketIdentification'] = NumeroTicketIdentification.GetValue() || "";
    e.customArgs['NumeroMatriculeNational'] = NumeroMatriculeNational.GetValue() || "";
    e.customArgs['PersonnePhysique'] = PersonnePhysique.GetValue();
}

function OpenAddContactModal() {
    //AddContactModal.PerformCallback();
    //AddContactModal.Show();
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
        ActeurID: addActeurID.GetValue()
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




function OpenAddPlantationModal(item) {
  //  $("IdToCulture").val(item.CultureID);
    AddMode = true;
    personnePhysique = item.PersonnePhysique;
    AddPlantationModal.PerformCallback({
        ContactID: item.ContactID,
        PersonnePhysique: item.PersonnePhysique,
        AddMode: true
    });
    AddPlantationModal.Show();
}

function OpenEditPlantationModal(item) {
  // $("IdToNumeroPlanteur").val(item.NumeroPlanteur);
    AddMode = false;
    personnePhysique = item.PersonnePhysique;
    AddPlantationModal.PerformCallback({
        ContactID: item.ContactID, PlantationID: item.PlantationID,
        PersonnePhysique: item.PersonnePhysique, AddMode: false
    });
    AddPlantationModal.Show();
}

function OpenDeletePlantationModal(item) {
    $("#IdToDelete1").val(item.ContactID);
    $("IdToContactID").val(item.ContactID);
    DeletePlantationModal.PerformCallback({ PlantationID: item.PlantationID });
    DeletePlantationModal.Show();
}

function AddPlantation(s, e) {
    var ajoutreussi = false;
    var dateAttestationPropriete, dateGeolocalisation, dateAdhesion
    critereValidated = true;
    ValiderMatriculeOperateur();
    if (critereValidated) {
        dateAttestationPropriete = addDateAttestationPropriete.GetValue();
        dateGeolocalisation = addDateGeolocalisation.GetValue();

        if (addDateAttestationPropriete.GetValue() !== null) {
            dateAttestationPropriete = addDateAttestationPropriete.GetValue().toUTCString();
        }
        if (addDateGeolocalisation.GetValue() !== null) {
            dateGeolocalisation = addDateGeolocalisation.GetValue().toUTCString();
        }

        if (addDateAdhesion.GetValue() !== null) {
            dateAdhesion = addDateAdhesion.GetValue().toUTCString();
        }

        var model = {
            PlantationID: addPlantationID.GetValue(),
            ContactID: addContactID.GetValue(),
            LocaliteID: addLocaliteID.GetValue(),
            NumeroApromac: addNumeroApromac.GetValue(),
            GroupementID: addGroupementID.GetValue(),
            CultureID: addCultureID.GetValue(),
            DistanceUsine: addDistanceUsine.GetValue(),
            DateAttestationPropriete: dateAttestationPropriete,
            AddMode: AddMode,
            DateGeolocalisation: dateGeolocalisation,
            NumeroGeolocalisation: addNumeroGeolocalisation.GetValue(),
            Altitude: addAltitude.GetValue(),
            Latitude: addLatitude.GetValue(),
            Longitude: addLongitude.GetValue(),
            // DonneesGeolocalisation:  addDonneesGeolocalisation.GetValue(),
            MatriculeOperateur: addMatriculeOperateur.GetValue(),
            SuperficieGeolocalisee: addSuperficie.GetValue(),
            IntervenantString: IntervenantString,
            SuperficieString: SuperficieString,
            PersonnePhysique: personnePhysique,     
            DateAdhesion: dateAdhesion
        };

        $.ajax({
            url: $("#AddPlantation").val(),
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

            AddPlantationModal.Hide();
            // gridPlantation.PerformCallback({ PlantationID: PlantationID.GetValue(), NumIdentification: NumIdentification.GetValue(), NomPlanteur: NomPlanteur.GetValue(), PrenomPlanteur: PrenomPlanteur.GetValue() });
            gridPlantation.PerformCallback({
                ContactID: ContactID.GetValue(),
                Noms: Noms.GetValue(),
                NumeroTelephones: NumeroTelephones.GetValue(),
                DateNaissance: DateNaissance.GetValue(),
                NumeroPiece: NumeroPiece.GetValue(),
                AdresseMails: AdresseMails.GetValue(),
                NumeroTicketIdentification: NumeroTicketIdentification.GetValue(),
                NumeroMatriculeNational: NumeroMatriculeNational.GetValue(),
                PersonnePhysique: PersonnePhysique.GetValue()
            });
            ajoutreussi = false;
        }
    }
}



function GenerateSurperficieFromGrid() {
    var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
    var idClone, idAnneeCulture, dateMiseExploitation, superficie1
    var superficie = [];
    if (indices !== null) {
        if (indices.length === 0) {
            SuperficieString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                //idNouvellePlantation = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'NouvellePlantationID', false);
                idClone = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                idAnneeCulture = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                dateMiseExploitation = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'DateMiseExploitation', false);
                superficie1 = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'Superficie1', false);
                if (idClone !== null && idAnneeCulture !== null  && superficie1 !== null) {
                    superficie.push({
                        AnneeCulture: idAnneeCulture,
                       // NouvellePlantationID:idNouvellePlantation,
                        CloneID: idClone,
                        DateMiseExploitation: dateMiseExploitation,
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
   // globalValidation = true;
    gridViewValid = true;
   // var total = 0.00;
    var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {
        //for (var i = 0; i < indices.length; i++) {
        //    editInvoked = true;
        //     gridViewSuperficie.batchEditApi.StartEdit(indices[i], 1);
           
        //    gridViewSuperficie.batchEditApi.StartEdit(indices[i], 1);
        //    gridViewSuperficie.batchEditApi.EndEdit();
        //    editInvoked = false;

        //}
       // totalSuperficie
       // total += validateSuperficie.value;
        ////ce nom a été affecté sur le grid batch edit
       // $("#Total1_Superficie1").html(total);
       // var tot = totalSuperficie;
        var super1 = addSuperficie.GetValue();

        if (totalSuperficie === super1) {
            gridViewValid=true
        }
        else {
            gridViewValid=false
        }
    }
   // globalValidation = false;
    return gridViewValid;
}

function gridViewSuperficieBatchEditEndEditing(s, e) {
    var idClone = e.rowValues[s.GetColumnByField('CloneID').index].value;
    var idAnneeCulture = e.rowValues[s.GetColumnByField('AnneeCulture').index].value;
    var superficie1 = e.rowValues[s.GetColumnByField('Superficie1').index].value;
    if (!editInvoked && idClone !== null && idAnneeCulture !== null  && superficie1 !== null) {
        if (superficie1 > 0)
            setTimeout(function () { gridViewSuperficieCustomValidate(); }, 0);
    }
}

function gridViewSuperficieBatchEditRowValidating(s, e) {
    var validateidClone = e.validationInfo[s.GetColumnByField('CloneID').index];
    var validateidAnneeCulture = e.validationInfo[s.GetColumnByField('AnneeCulture').index];
   // var validateNouvellePlantationID = e.validationInfo[s.GetColumnByField('NouvellePlantationID').index];
    var validateSuperficie = e.validationInfo[s.GetColumnByField('Superficie1').index];
    totalSuperficie = 0.00;
    var total = 0.00;
    if (validateidClone && validateidAnneeCulture ) {
        var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstSuperficie = [];
            for (var i = 0; i < indices.length; i++) {
                var currentCloneID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                var currenteAnneeCultureID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                var currentSuperficie = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'Superficie1', false);
              if (currentCloneID && currenteAnneeCultureID  && indices[i] !== e.visibleIndex) {
                
                lstSuperficie.push(currenteAnneeCultureID);

                    total += currentSuperficie;
                }
            }
            total += validateSuperficie.value;
            totalSuperficie = total;
            ////ce nom a été affecté sur le grid batch edit
            $("#Total1_Superficie1").html(total);
            var tot = total;
            var super1 = addSuperficie.GetValue();
            if (super1 === null || super1 === '') {
                super1 = 0;
            }
            
            if (tot > 0 && tot !== super1) {
                $("#msgAddRequiredFields_message").html(msgValidationSuperficie.replace('{0}',super1.toString()));
                $("#msgAddRequiredFields").removeClass('hidden');
            }
            else {
                $("#msgAddRequiredFields").addClass("hidden");
            }

            for (var j = 0; j < lstSuperficie.length; j++) {
                if (lstSuperficie[j] === validateidAnneeCulture.value) {
                    validateidAnneeCulture.isValid = false;
                    validateidAnneeCulture.errorText = msgUnique;          
                }
            }
        }
    }
}

function closeMessage() {
    $("#msgAddRequiredFields").addClass("hidden");
}


function SubmitForm() {
    var validator = $("#frmPlantation").validate();

    if (validator.form()) {

        if (gridViewSuperficie.batchEditApi.ValidateRows()
            && gridViewSuperficieCustomValidate()
            && gridViewIntervenant.batchEditApi.ValidateRows()
            && gridViewIntervenantCustomValidate()) {
            GenerateSurperficieFromGrid();
            GenerateIntervenantFromGrid();
            AddPlantation();
        }         
       // if (AddMode) {
           // if (gridViewSuperficie.batchEditApi.ValidateRows()
           //     && gridViewSuperficieCustomValidate()) {
           //     GenerateSurperficieFromGrid();
           //    // GenerateIntervenantFromGrid();
           //    AddPlantation();
           //}         
        //}
        //else {
        //        AddPlantation();     
        //}
    }
}

function GenerateIntervenantFromGrid() {
    var indices = gridViewIntervenant.batchEditApi.GetRowVisibleIndices();
    var idTypeContact, dateAdhesion, idContact
    var Intervenant = [];
    if (indices !== null) {
        if (indices.length === 0) {
            IntervenantString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                idContact = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'ContactID', false);
                idTypeContact = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'TypeContactID', false);
                dateAdhesion = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'DateAdhesion', false);
                if (idTypeContact !== null && dateAdhesion !== null) {
                    Intervenant.push({
                        TypeContactID: idTypeContact,
                        ContactID: idContact,
                        DateAdhesion: dateAdhesion
                    });
                } else {
                    return false;
                }
            }
            IntervenantString = JSON.stringify(Intervenant);
            return true;
        }
    }
    return false;
}

function gridViewIntervenantCustomValidate() {
    //globalValidation = true;
    gridViewValid = true;
    var indices = gridViewIntervenant.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {
        //for (var i = 0; i < indices.length; i++) {
        //    editInvoked = true;
        //    gridViewIntervenant.batchEditApi.StartEdit(indices[i], 1);
        //    gridViewIntervenant.batchEditApi.EndEdit();
        //    editInvoked = false;
        //}
        gridViewValid = true;
    }
    //globalValidation = false;
    return gridViewValid;
}

function gridViewIntervenantBatchEditEndEditing(s, e) {
    var idTypeContact = e.rowValues[s.GetColumnByField('TypeContactID').index].value;
    var idContact = e.rowValues[s.GetColumnByField('ContactID').index].value;
    var idDateAdhesion = e.rowValues[s.GetColumnByField('DateAdhesion').index].value;
    if (!editInvoked && idTypeContact !== null && idDateAdhesion !== null && idContact !== null) {
        if (idDateAdhesion > 0)
            setTimeout(function () { gridViewIntervenantCustomValidate(); }, 0);
    }
}

function gridViewIntervenantBatchEditRowValidating(s, e) {
    var validateidContact = e.validationInfo[s.GetColumnByField('ContactID').index];
    var validateidTypeContact = e.validationInfo[s.GetColumnByField('TypeContactID').index];
    var validateidDateAdhesion = e.validationInfo[s.GetColumnByField('DateAdhesion').index];
    if (validateidTypeContact && validateidDateAdhesion && validateidContact) {
        var indices = gridViewIntervenant.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstIntervenant = [];
            for (var i = 0; i < indices.length; i++) {
                var currentContactID = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'ContactID', false);
                var currentTypeContactID = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'TypeContactID', false);
                var currentDateAdhesion = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'DateAdhesion', false);
                if (currentTypeContactID && currentDateAdhesion && currentContactID && indices[i] !== e.visibleIndex) {
                    lstIntervenant.push(currentTypeContactID);
                }
            }
            for (var j = 0; j < lstIntervenant.length; j++) {
                if (lstIntervenant[j] === validateidTypeContact.value) {
                    validateidTypeContact.isValid = false;
                    validateidTypeContact.errorText = msgUnique;
                }
            }
        }
    }
}

//function GenerateIntervenantFromGrid() {
//    var indices = gridViewIntervenant.batchEditApi.GetRowVisibleIndices();
//    var idTypeContact, dateAdhesion
//    var Intervenant = [];
//    if (indices !== null) {
//        if (indices.length === 0) {
//            IntervenantString = "";
//            return true;
//        } else {
//            for (var i = 0; i < indices.length; i++) {
//                idTypeContact = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'TypeContactID', false);
//                dateAdhesion = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'DateAdhesion', false);
//                if (idTypeContact !== null && dateAdhesion !== null ) {
//                    Intervenant.push({
//                        TypeContactID: idTypeContact,
//                        DateAdhesion: dateAdhesion
//                    });
//                } else {
//                    return false;
//                }
//            }
//            IntervenantString = JSON.stringify(Intervenant);
//            return true;
//        }
//    }
//    return false;
//}

//function gridViewIntervenantCustomValidate() {
//    globalValidation = true;
//    gridViewValid = true;
//    var indices = gridViewIntervenant.batchEditApi.GetRowVisibleIndices();
//    if (indices !== null) {
//        for (var i = 0; i < indices.length; i++) {
//            editInvoked = true;
//            gridViewIntervenant.batchEditApi.StartEdit(indices[i], 1);
//            gridViewIntervenant.batchEditApi.EndEdit();
//            editInvoked = false;
//        }
//    }
//    globalValidation = false;
//    return gridViewValid;
//}

//function gridViewIntervenantBatchEditEndEditing(s, e) {
//    var idTypeContact = e.rowValues[s.GetColumnByField('TypeContactID').index].value;
//    var idDateAdhesion = e.rowValues[s.GetColumnByField('DateAdhesion').index].value;
//    if (!editInvoked && idTypeContact !== null && idDateAdhesion !== null ) {
//        if (idDateAdhesion > 0)
//            setTimeout(function () { gridViewIntervenantCustomValidate(); }, 0);
//    }
//}

//function gridViewIntervenantBatchEditRowValidating(s, e) {
//    var validateidTypeContact = e.validationInfo[s.GetColumnByField('TypeContactID').index];
//    var validateidDateAdhesion = e.validationInfo[s.GetColumnByField('DateAdhesion').index];
//    if (validateidTypeContact && validateidDateAdhesion) {
//        var indices = gridViewIntervenant.batchEditApi.GetRowVisibleIndices();
//        if (indices !== null) {
//            var lstIntervenant = [];
//            for (var i = 0; i < indices.length; i++) {
//                var currentTypeContactID = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'TypeContactID', false);
//                var currentDateAdhesion = gridViewIntervenant.batchEditApi.GetCellValue(indices[i], 'DateAdhesion', false);
//                if (currentTypeContactID && currentDateAdhesion && indices[i] !== e.visibleIndex) {
//                    lstIntervenant.push(currentTypeContactID);            
//                }
//            }
//            for (var j = 0; j < lstIntervenant.length; j++) {
//                if (lstIntervenant[j] === validateidTypeContact.value) {
//                    validateidTypeContact.isValid = false;
//                    validateidTypeContact.errorText = msgUnique;
//                }
//            }
//        }
//    }
//}


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


function OnCloneIDBeginCallback(s, e) {
    if (addCultureID.GetValue() !== null || addCultureID.GetValue() === "")
    {
        e.customArgs['idCulture'] = addCultureID.GetValue();
    }
}

function addCultureIDSelectedIndexChanged(s, e) {
    if (s.GetValue() != null || s.GetValue() != "") {
        idCulture = s.GetValue();      
    }
}


function addMatriculeOperateurGotFocus(s, e) {
    addMatriculeOperateur.PerformCallback();
}
function addMatriculeOperateurInit(s, e) {
    if (s.GetItemCount() === 1) {
        s.SetSelectedIndex(0);
    }
}

function addMatriculeOperateurBeginCallback(s, e) {
    e.customArgs['idTypeContact'] = "E";
    e.customArgs['idActivite'] = "EM";
}


//function GetVisibleIndex(event) {
//    var target = event.target;
//    while (target && (target.nodeName != 'TR' || target.attributes.length <= 0 || target.attributes['id'] == null || !target.attributes['id'].value.includes('DXDataRow'))) {
//        target = target.parentElement;
//    }
//    var id = target.attributes['id'].value;
//    var index = id.split("DataRow")[1];
//    if (index !== null && index.length > 0 && !isNaN(index)) {
//        index = parseInt(index);
//    }
//    return index;
//}

function Rafraichir(s, e) {
    
    gridViewIntervenant.PerformCallback({ AddMode: AddMode, ContactID: addContactID.GetValue(), PlantationID: addPlantationID.GetValue() });

  
}


function gridViewIntervenantOnBeginCallback(s, e) {

    e.customArgs['PlantationID'] = addPlantationID.GetValue() || "";
    e.customArgs['TypeContactID'] = addTypeContactID.GetValue() || "";
    if (addDateAdhesion.GetValue() !== null) {
        e.customArgs['DateAdhesion'] = addDateAdhesion.GetValue().toUTCString();
    }
    e.customArgs['Contacts'] = IntervenantContacts.GetValue() || "";
    //e.customArgs['AnciennePlantationID'] = idAnciennePlantation;
    //e.customArgs['PrecedentPlantationID'] = idPrecedentPlantation;
}

function IntervenantContactsBeginCallback(s, e) {
    
}

