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
var idAnciennePlantation = '';
var idAnneeCulture = 0;
var idDateEvolution = '';




function Rafraichir(s, e) { 
    critereValidated = true;
    ValiderDateEvolution();
    ValiderMotifEvolution()
    ValiderMatriculeOperateur();
    ValiderContacts(); 
    if (critereValidated) {
        gridViewDonneesGeographique.PerformCallback();
    }
}


function gridViewDonneesGeographiqueOnBeginCallback(s, e) {
    
    e.customArgs['PlantationID'] = PlantationID.GetValue() || "";
    e.customArgs['NouvellePlantationID'] = PlantationID.GetValue() || "";
    e.customArgs['AnciennePlantationID'] = idAnciennePlantation;
    e.customArgs['Contacts'] = Contacts.GetValue() || "";
    e.customArgs['AnneeCulture'] = addAnneeCulture.GetValue() || "";
    e.customArgs['CloneID'] = addCloneID.GetValue() || "";
    if (DateEvolution.GetValue() !== null) {
        e.customArgs['DateEvolution']= DateEvolution.GetValue().toUTCString();
    }
}


function gridPlantationOnBeginCallback(s, e) {
    e.customArgs['PlantationID'] = PlantationID.GetValue() || ""; 
   
}


function OpenAddCessionModal(item) {
    AddMode = true;
    
    $("#IdToAnciennePlantation").val(item.AnciennePlantationID);
    idAnciennePlantation = item.AnciennePlantationID;
    MotifEvolutionID.SetValue(item.MotifEvolutionID);
    AddCessionModal.PerformCallback({
        CloneID: item.CloneID,
        AnneeCulture: item.AnneeCulture,
        Superficie: item.Superficie,
        AnciennePlantationID: item.AnciennePlantationID,
        NouvellePlantationID: item.NouvellePlantationID,
        DateEvolution: item.DateEvolution,
        MotifEvolutionID: item.MotifEvolutionID,
        AddMode:true
    });
    AddCessionModal.Show();
}

function OpenEditCessionModal(item) {
    AddMode = false;
    idAnciennePlantation = item.AnciennePlantationID;
   // MotifEvolutionID.SetValue(item.MotifEvolutionID);
    AddCessionModal.PerformCallback({
        CloneID: item.CloneID,
        AnneeCulture: item.AnneeCulture,
        Superficie: item.Superficie,
        AnciennePlantationID: item.AnciennePlantationID,
        NouvellePlantationID: item.NouvellePlantationID,
        DateEvolution: item.DateEvolution,
        MotifEvolutionID: item.MotifEvolutionID,
        AddMode: false
    });
    AddCessionModal.Show();
}

function OpenDeleteCessionModal(item) {
    $("#IdNouvellePlantationToDelete").val(item.NouvellePlantationID);
   // $("IdDateEvolutionToDelete").val(item.DateEvolution);
    $("#IdCloneToDelete").val(item.CloneID);
   // $("IdAnneeCultureToDelete").val(item.AnneeCulture);
    $("#IdAnciennePlantationToDelete").val(item.AnciennePlantationID);
    idAnneeCulture = item.AnneeCulture;
    idDateEvolution = item.DateEvolution;
    DeleteSuperficieModal.PerformCallback({
        CloneID: item.CloneID,
        AnneeCulture: item.AnneeCulture,
        AnciennePlantationID: item.AnciennePlantationID,
        NouvellePlantationID: item.NouvellePlantationID,
        DateEvolution: item.DateEvolution
    });
    DeleteSuperficieModal.Show();
}

function AddCession(s, e) {
    var ajoutreussi = false;
    var dateEvolution
    dateEvolution = DateEvolution.GetValue();

    if (DateEvolution.GetValue() !== null) {
        dateEvolution = DateEvolution.GetValue().toUTCString();
    }
    
    var model = {
        CloneID: addCloneID.GetValue(),
        AnneeCulture: addAnneeCulture.GetValue(),
        MotifEvolutionID: MotifEvolutionID.GetValue(),
        DateEvolution: dateEvolution,
        CultureID: CultureID.GetValue(),  
        PlantationID: PlantationID.GetValue(),
        AnciennePlantationID: PlantationID.GetValue(),
        Contacts: Contacts.GetValue(), 
        MatriculeOperateur: MatriculeOperateur.GetValue(),
        SuperficieGeolocalisee: addSuperficie.GetValue(),
        DonneesGeographiqueString: DonneesGeographiqueString   
       };

    $.ajax({
        url: $("#AddCession").val(),
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
        AddCessionModal.Hide();
        gridPlantation.PerformCallback({ PlantationID: PlantationID.GetValue() });
        ajoutreussi = false;
    }

}



function EditCession(s, e) {
    var editreussi = false;
    var dateEvolution
    dateEvolution = DateEvolution.GetValue();
    if (DateEvolution.GetValue() !== null) {
        dateEvolution = DateEvolution.GetValue().toUTCString();
    }
    var model = {
        CloneID: addCloneID.GetValue(),
        AnneeCulture: addAnneeCulture.GetValue(),
        MotifEvolutionID: MotifEvolutionID.GetValue(),
        DateEvolution: dateEvolution,
        MatriculeOperateur: MatriculeOperateur.GetValue(),
        Superficie: addSuperficie.GetValue(),
        DonneesGeographiqueString: DonneesGeographiqueString
    };

    $.ajax({
        url: $("#EditCession").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                editreussi = true;
            }
        }, error: function (e) {
            editreussi = false;
        }
    });

    if (editreussi) {
        AddCessionModal.Hide();
        gridPlantation.PerformCallback({ PlantationID: PlantationID.GetValue() });
        editreussi = false;
    }

}



function Remove(s, e) {
    var deletereussi = false;
    var model = {
        CloneID: $("#IdCloneToDelete").val(),
        AnneeCulture: idAnneeCulture,//$("#IdAnneeCultureToDelete").val(),
        DateEvolution: idDateEvolution,
        NouvellePlantationID: $("#IdNouvellePlantationToDelete").val(),
        AnciennePlantationID: $("#IdAnciennePlantationToDelete").val(),
       
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
        DeleteSuperficieModal.Hide();
        deletereussi = false;
        gridPlantation.PerformCallback({ PlantationID: PlantationID.GetValue() });
    }
}


function closeMessage() {
    $("#msgAddRequiredFields").addClass("hidden");
}


function SubmitForm() {
    var validator = $("#frmCession").validate();
    if (validator.form()) {
            if (gridViewDonneesGeographique.batchEditApi.ValidateRows()) {
                GenerateDonneesGeographiqueFromGrid();

                if (AddMode) {
                    AddCession();
                }
                else {
                    EditCession();
                }
            }
       
    }
  }




function GenerateDonneesGeographiqueFromGrid() {
    var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
    var latitude, numeroGeolocalisation, longitude, altitude, superficie, idPlantation, idContact, idAnciennePlantation, dateMiseExploitation
    var DonneesGeographique = [];
    if (indices !== null) {
        if (indices.length === 0) {
            DonneesGeographiqueString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                latitude = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'Latitude', false);
                numeroGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'NumeroGeolocalisation', false);
                longitude = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'Longitude', false);
                altitude = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'Altitude', false);
                superficie = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                idPlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'NouvellePlantationID', false);
                idAnciennePlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'AnciennePlantationID', false);
                idContact = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'ContactID', false);
                dateMiseExploitation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'DateMiseExploitation', false);  
                if (idPlantation !== null && superficie !== null) {
                DonneesGeographique.push({
                        Latitude: latitude,
                        NumeroGeolocalisation: numeroGeolocalisation,
                        Longitude: longitude,
                        Altitude: altitude,
                        NouvellePlantationID: idPlantation,
                        AnciennePlantationID: idAnciennePlantation,
                        DateMiseExploitation: dateMiseExploitation,
                        Superficie: superficie,
                        ContactID: idContact
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
    gridViewValid = true;
    var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {      
        var super1 = addSuperficie.GetValue();
        if (totalSuperficie === super1) {
            gridViewValid = true
        }
        else {
            gridViewValid = false
        }
    }
    return gridViewValid;
}


function gridViewDonneesGeographiqueBatchEditEndEditing(s, e) {
    var idPlantation = e.rowValues[s.GetColumnByField('NouvellePlantationID').index].value;
    //var numGeolocalisation = e.rowValues[s.GetColumnByField('NumGeolocalisation').index].value;
    //var coordonneesGPS = e.rowValues[s.GetColumnByField('CoordonneesGPS').index].value;
    //var donneesGeolocalisation = e.rowValues[s.GetColumnByField('DonneesGeolocalisation').index].value;
    var superficie = e.rowValues[s.GetColumnByField('Superficie').index].value;
    if (!editInvoked && idPlantation !== null   && superficie !== null) {
        if (superficie > 0)
            setTimeout(function () { gridViewDonneesGeographiqueCustomValidate(); }, 0);
    }
}


function gridViewDonneesGeographiqueBatchEditRowValidating(s, e) {
    var validateidPlantation = e.validationInfo[s.GetColumnByField('NouvellePlantationID').index];
    //var validateidAnneeCulture = e.validationInfo[s.GetColumnByField('AnneeCulture').index];
    // var validateNouvellePlantationID = e.validationInfo[s.GetColumnByField('NouvellePlantationID').index];
    var validateSuperficie = e.validationInfo[s.GetColumnByField('Superficie').index];
    totalSuperficie = 0.00;
    var total = 0.00;
    if (validateidPlantation && validateSuperficie) {
        var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstSuperficie = [];
            for (var i = 0; i < indices.length; i++) {
                var currentPlantationID = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'NouvellePlantationID', false);
                //var currenteAnneeCultureID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                var currentSuperficie = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                if (currentPlantationID && currentSuperficie && indices[i] !== e.visibleIndex) {
                    lstSuperficie.push(currentPlantationID);
                    total += currentSuperficie;
                }
            }
            total += validateSuperficie.value;
            totalSuperficie = total;
            
            ////ce nom a été affecté sur le grid batch edit
            $("#Total1_Superficie").html(total);
            var tot = total;
            var super1 = addSuperficie.GetValue();
            if (super1 === null || super1 === '') {
                super1 = 0;
            }
            if (AddMode) {
                if (tot > 0 && tot !== super1) {
                    $("#msgAddRequiredFields_message").html(msgValidationSuperficie.replace('{0}', super1.toString()));
                    $("#msgAddRequiredFields").removeClass('hidden');
                }
                else {
                    $("#msgAddRequiredFields").addClass("hidden");
                }
            }
          

            for (var j = 0; j < lstSuperficie.length; j++) {
                if (lstSuperficie[j] === validateidPlantation.value) {
                   validateidPlantation.isValid = false;
                  //  validateidPlantation.errorText = msgUnique;
                }
            }
        }
    }
}



function OnCloneIDBeginCallback(s, e) {
    if (addCultureID.GetValue() !== null || addCultureID.GetValue() === "")
    {
        e.customArgs['idCulture'] = idCulture;
    }
}

function addCultureIDSelectedIndexChanged(s, e) {
    if (s.GetValue() != null || s.GetValue() != "") {
        idCulture = s.GetValue();      
    }
}


function MatriculeOperateurGotFocus(s, e) {
    MatriculeOperateur.PerformCallback();
}
function MatriculeOperateurInit(s, e) {
    if (s.GetItemCount() === 1) {
        s.SetSelectedIndex(0);
    }
}

function MatriculeOperateurBeginCallback(s, e) {
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


function PlantationIDBeginCallback(s, e) {
    e.customArgs['idCulture'] = CultureID.GetValue() || "";
}

function PlantationIDSelectedIndexChanged(s, e) {
  
    if (s.GetValue() != null || s.GetValue() != "") {
        if (s.GetSelectedItem() != null) {
            LocaliteID.SetValue(s.GetSelectedItem().GetColumnText("LocaliteID"));
            DistanceUsine.SetValue(s.GetSelectedItem().GetColumnText("DistanceUsine"));
        }
        else {
            LocaliteID.SetValue(null);
            DistanceUsine.SetValue(null);
        }
               
            gridPlantation.PerformCallback();
        }
}

function ContactsBeginCallback(s, e) {
    e.customArgs["idActivite"] = "PI";
}

function ContactsEndCallback(s, e) {
    $("#Contacts").removeClass('requiredField');
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

function ValiderDateEvolution() {
    if (DateEvolution.GetValue() === null || DateEvolution.GetValue() === "") {
        critereValidated = false;
        $("#DateEvolution").addClass('requiredField');
    }
    else {
        $("#DateEvolution").removeClass('requiredField');
    }
}

function ValiderMotifEvolution() {
    if (MotifEvolutionID.GetValue() === null || MotifEvolutionID.GetValue() === "") {
        critereValidated = false;
        $("#MotifEvolutionID").addClass('requiredField');
    }
    else {
        $("#MotifEvolutionID").removeClass('requiredField');
    }
}

function ValiderMatriculeOperateur() {
        if (MatriculeOperateur.GetValue() === null || MatriculeOperateur.GetValue() === "") {
            critereValidated = false;
            $("#MatriculeOperateur").addClass('requiredField');
        }
        else {
            $("#MatriculeOperateur").removeClass('requiredField');
        }
}

function ValiderPlantation() {
   
        if (PlantationID.GetValue() === null || PlantationID.GetValue() === "") {
            critereValidated = false;
            $("#PlantationID").addClass('requiredField');
        }
        else {
            $("#PlantationID").removeClass('requiredField');
        }
   
}


function ValiderContacts() {
        if (Contacts.GetValue() === null || Contacts.GetValue() === "") {
            critereValidated = false;
            $("#Contacts").addClass('requiredField');
        }
        else {
            $("#Contacts").removeClass('requiredField');
        }
}

