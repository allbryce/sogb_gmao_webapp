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
var idPlantation = '';
var idAncienneDateGeolocalisation = '';
var idDateGeolocalisation = '';
var idPrecedentPlantation = '';
var currentDonneGeographiqueRowIndex;
var currentColumnName;


function Afficher(s, e) {
    critereValidated = true;
    if (critereValidated) {
        gridPlantation.PerformCallback();
    }
}

function Rafraichir(s, e) { 
    critereValidated = true;
    ValiderDateGeolocalisation();
   // ValiderMotifEvolution()
    ValiderMatriculeOperateur();
    ValiderContacts(); 
        if (critereValidated) {
            gridViewDonneesGeographique.PerformCallback();
        }
  }


function gridViewDonneesGeographiqueOnBeginCallback(s, e) {
    
    e.customArgs['PlantationID'] = PlantationID.GetValue() || "";
    e.customArgs['Contacts'] = Contacts.GetValue() || "";
    if (DateGeolocalisation.GetValue() !== null) {
        e.customArgs['DateGeolocalisation'] = DateGeolocalisation.GetValue().toUTCString();
    }
    e.customArgs['AncienneDateGeolocalisation'] = idAncienneDateGeolocalisation;
    e.customArgs['AnciennePlantationID'] = idAnciennePlantation;
    e.customArgs['PrecedentPlantationID'] = idPrecedentPlantation;
}


function gridViewSuperficieOnBeginCallback(s, e) {

   
    e.customArgs['PlantationID'] = idPlantation;
    //  e.customArgs['NouvellePlantationID'] = PlantationID.GetValue() || "";
    // e.customArgs['AnciennePlantationID'] = idAnciennePlantation;
   /// e.customArgs['Contacts'] = Contacts.GetValue() || "";
    // e.customArgs['AnneeCulture'] = addAnneeCulture.GetValue() || "";
    // e.customArgs['CloneID'] = addCloneID.GetValue() || "";
    //if (DateGeolocalisation.GetValue() !== null) {
    //    e.customArgs['DateGeolocalisation'] = DateGeolocalisation.GetValue().toUTCString();
    //}
   
        e.customArgs['AncienneDateGeolocalisation'] = idAncienneDateGeolocalisation;
    
}

function gridPlantationOnBeginCallback(s, e) {
    e.customArgs['PlantationID'] = PlantationID.GetValue() || "";  
    e.customArgs['CultureID'] = CultureID.GetValue() || "";
    //var dateGeolo;
    //dateGeolo = dtmDateGeolocalisation.GetValue();
    if (dtmDateGeolocalisation.GetValue() !== null) {
        e.customArgs['DateGeolocalisation'] = dtmDateGeolocalisation.GetValue().toUTCString();
    }
    //else {
    //    e.customArgs['DateGeolocalisation'] = null;
    //}
}

function gridViewDonneesGeographiqueBatchEditStartEditing(s, e) {
    //currentColumnName = e.focusedColumn.fieldName;
    currentDonneGeographiqueRowIndex = e.visibleIndex;
}


function OpenAddCessionModal(item) {
    AddMode = true;
    critereValidated = true;
    ValiderPlantation();
    if (critereValidated) {
        idPlantation = item.PlantationID;
        idAnciennePlantation = item.PlantationID;
        idAncienneDateGeolocalisation = item.AncienneDateGeolocalisation;
        idDateGeolocalisation = item.DateGeolocalisation;

        AddCessionPlantationModal.PerformCallback({
            PlantationID: item.PlantationID,
            DateGeolocalisation: idDateGeolocalisation,//item.DateGeolocalisation,
            AncienneDateGeolocalisation: idAncienneDateGeolocalisation,///item.AncienneDateGeolocalisation,
            Superficie: item.Superficie,
            MatriculeOperateur: item.MatriculeOperateur,
            AnciennePlantationID: item.PlantationID,
            AddMode: true
        });
        AddCessionPlantationModal.Show();

      ///  ValiderSaisie();

        }
}


function OpenAddSuperficieModal(item) {
    AddMode = true;
    currentDonneGeographiqueRowIndex = item;
    var superfici, dateGeolocalisation, idanciennePlantation, idContact, noms 

    dateGeolocalisation = DateGeolocalisation.GetValue();
    idAncienneDateGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'AncienneDateGeolocalisation').toUTCString()
    idPlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'PlantationID');
    superfici = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'SuperficieGeolocalisee');
    idanciennePlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'AnciennePlantationID');
    idContact = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'ContactID');
    noms = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'Noms');
    idPrecedentPlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(currentDonneGeographiqueRowIndex, 'PrecedentPlantationID');

    if (DateGeolocalisation.GetValue() !== null) {
        dateGeolocalisation = DateGeolocalisation.GetValue().toUTCString();
    }
    AddCessionSuperficieModal.PerformCallback({
        PlantationID: idPlantation,
        DateGeolocalisation: dateGeolocalisation,
        AncienneDateGeolocalisation: idAncienneDateGeolocalisation, 
        AnciennePlantationID: idanciennePlantation,
        PrecedentPlantationID: idPrecedentPlantation,
        ContactID: idContact,
        Noms: noms,
        Superficie: superfici 
    });
    AddCessionSuperficieModal.Show();
}

function OpenEditCessionModal(item) {
    critereValidated = true;
    AddMode = false;
    ValiderPlantation();


    if (critereValidated) {
        EditCessionPlantationModal.PerformCallback({
            PlantationID: item.PlantationID,
            DateGeolocalisation: item.DateGeolocalisation,
            MatriculeOperateur: item.MatriculeOperateur
        });
        EditCessionPlantationModal.Show()
    }
    
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

function AddCessionDonneesGeographique(s, e) {
    var ajoutreussi = false;
    var dateGeolocalisation, dateGeo, idAncienneDate
    dateGeolocalisation = DateGeolocalisation.GetValue();
    var dateGeoloc = new Date(DateGeolocalisation.GetValue());
    var ancienneDate = idAncienneDateGeolocalisation.substring(0, 10);
    dateGeo = dateGeoloc.toLocaleDateString();
    idAncienneDate = ancienneDate;
    
    if (dateGeo === idAncienneDate) {
        $("#msgAddRequiredFields_message").html('Veuillez saisir la nouvelle date de géolocalisation.');
        $("#msgAddRequiredFields").removeClass('hidden');
    }
    else {

        if (DateGeolocalisation.GetValue() !== null) {
            dateGeolocalisation = DateGeolocalisation.GetValue().toUTCString();
        }
        var model = {
            PlantationID: addPlantationID.GetValue(),
            AncienneDateGeolocalisation: idAncienneDateGeolocalisation,
            Superficie: addSuperficie.GetValue(),
            DateGeolocalisation: dateGeolocalisation,
            AnciennePlantationID: idAnciennePlantation,
            Contacts: Contacts.GetValue(),
            MatriculeOperateur: MatriculeOperateur.GetValue(),
            SuperficieGeolocalisee: addSuperficie.GetValue(),
            DonneesGeographiqueString: DonneesGeographiqueString
        };

        $.ajax({
            url: $("#AddCessionDonneesGeographique").val(),
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

            btnAddDonneesGeographique.SetEnabled(false);
            btnRafraichir.SetEnabled(false);
            ajoutreussi = false;
        }
        $("#msgAddRequiredFields").addClass("hidden");
    }
}

function AddCessionSurface(s, e) {
    var ajoutreussi = false;
    var blnValide = false
    var dateGeolocalisation
    dateGeolocalisation = DateGeolocalisation.GetValue();
    if (DateGeolocalisation.GetValue() !== null) {
        dateGeolocalisation = DateGeolocalisation.GetValue().toUTCString();
    }
    var model = {
        PlantationID: addPlantationID.GetValue(),
        AncienneDateGeolocalisation: idAncienneDateGeolocalisation,
        AnciennePlantationID: idAnciennePlantation,
        PrecedentPlantationID: idPrecedentPlantation,
        Superficie: addSupSuperficie.GetValue(),
        SuperficieGeolocalisee: addSuperficie.GetValue(),
        DateGeolocalisation: dateGeolocalisation,
        SuperficieString: SuperficieString
    };

    $.ajax({
        url: $("#AddCessionSurface").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            result = result;
            if (result !== null) {
                blnValide = result;
                ajoutreussi = true;
            }
        }, error: function (e) {
            ajoutreussi = false;
        }
    });

    if (ajoutreussi) {
       
        AddCessionSuperficieModal.Hide();
        if (blnValide === true) {
            PlantationID.SetValue(null);
            dtmDateGeolocalisation.SetValue(DateGeolocalisation.GetValue());
            gridPlantation.PerformCallback({
                CultureID: CultureID.GetValue(),
                PlantationID: PlantationID.GetValue(),
                DateGeolocalisation: dateGeolocalisation
            });
            AddCessionPlantationModal.Hide(); 
        }
        ajoutreussi = false;
    }

}


function ValiderSaisie() {
    var dateGeolocalisation
    dateGeolocalisation = DateGeolocalisation.GetValue();
    if (DateGeolocalisation.GetValue() !== null) {
        dateGeolocalisation = DateGeolocalisation.GetValue().toUTCString();
    }
    var model = {
        PlantationID: addPlantationID.GetValue(),
        AncienneDateGeolocalisation: idAncienneDateGeolocalisation,
        DateGeolocalisation: dateGeolocalisation
    };
        //Requête ajax
        $.ajax({
            url: $("#ValiderSaisie").val(),
            type: 'POST',
            async: false,
            data: model,
            success: function (result) {
                //result = result;
                if (result !== null) {
                    var blnValide = JSON.parse(result.blnValid);
                    if (blnValide === true) {
                        gridPlantation.PerformCallback({ CultureID: CultureID.GetValue() });
                        AddCessionPlantationModal.Hide();
                    }
                }
            },
            error: function () {
                window.alert("Error");
            }
        });
    //}
}



function UpdatePlantationDonneesGeoSurface(s, e) {
    var ajoutreussi = false;
    critereValidated=true
    var dateAttestationPropriete, dateGeolocalisation
    ValidereditMatriculeOperateur();

    if (critereValidated) {

        dateAttestationPropriete = editDateAttestationPropriete.GetValue();
        dateGeolocalisation = editDateGeolocalisation.GetValue();

        if (editDateAttestationPropriete.GetValue() !== null) {
            dateAttestationPropriete = editDateAttestationPropriete.GetValue().toUTCString();
        }
        if (editDateGeolocalisation.GetValue() !== null) {
            dateGeolocalisation = editDateGeolocalisation.GetValue().toUTCString();
        }
        var model = {
            PlantationID: editPlantationID.GetValue(),
            ContactID: editContactID.GetValue(),
            LocaliteID: editLocaliteID.GetValue(),
            NumeroApromac: editNumeroApromac.GetValue(),
            GroupementID: editGroupementID.GetValue(),
            DistanceUsine: editDistanceUsine.GetValue(),
            DateAttestationPropriete: dateAttestationPropriete,
            DateGeolocalisation: dateGeolocalisation,
            NumeroGeolocalisation: editNumeroGeolocalisation.GetValue(),
            Altitude: editAltitude.GetValue(),
            LatitudeEdit: editLatitude.GetValue(),
            LongitudeEdit: editLongitude.GetValue(),
            MatriculeOperateur: editMatriculeOperateur.GetValue(),
            SuperficieGeolocalisee: editSuperficie.GetValue(),
            SuperficieString: SuperficieString
        };

        $.ajax({
            url: $("#UpdatePlantationDonneesGeoSurface").val(),
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
            /// AddCessionModal.Hide();
            gridPlantation.PerformCallback({ PlantationID: PlantationID.GetValue() });
            EditCessionPlantationModal.Hide();

            ajoutreussi = false;
        }
    }
}


function closeMessage() {
    $("#msgAddRequiredFields").addClass("hidden");
}

function closeMessage1() {
    $("#msgAddRequiredFields1").addClass("hidden");
}

function closeMessage2() {
    $("#msgAddRequiredFields2").addClass("hidden");
}

function SubmitForm() {
    var validator = $("#frmCessionPlantation").validate();
    if (validator.form()) {
        if (gridViewDonneesGeographique.batchEditApi.ValidateRows() && gridViewDonneesGeographiqueCustomValidate()) {
                GenerateDonneesGeographiqueFromGrid();
                AddCessionDonneesGeographique();  
            }      
    }
  }


function GenerateDonneesGeographiqueFromGrid() {
    var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
    var latitude, numeroGeolocalisation, longitude, altitude, superficieGeolocalisee, idPlantation, idContact, ancienneDateGeolocalisation, idAnciennePlantation
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
                superficieGeolocalisee = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'SuperficieGeolocalisee', false);
                idPlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'PlantationID', false);
                //idAnciennePlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'AnciennePlantationID', false);
                idContact = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'ContactID', false);
                ancienneDateGeolocalisation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'AncienneDateGeolocalisation', false);  
                idAnciennePlantation = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'AnciennePlantationID', false);  

                if (idPlantation !== null && superficieGeolocalisee !== null) {
                DonneesGeographique.push({
                        Latitude: latitude,
                        NumeroGeolocalisation: numeroGeolocalisation,
                        Longitude: longitude,
                        Altitude: altitude,
                        PlantationID: idPlantation,
                        SuperficieGeolocalisee: superficieGeolocalisee,
                        ContactID: idContact,
                        AnciennePlantationID: idAnciennePlantation,
                         AncienneDateGeolocalisation: ancienneDateGeolocalisation
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
    var idPlantation = e.rowValues[s.GetColumnByField('PlantationID').index].value;
    //var numGeolocalisation = e.rowValues[s.GetColumnByField('NumGeolocalisation').index].value;
    //var coordonneesGPS = e.rowValues[s.GetColumnByField('CoordonneesGPS').index].value;
    //var donneesGeolocalisation = e.rowValues[s.GetColumnByField('DonneesGeolocalisation').index].value;
    var superficieGeolocalisee = e.rowValues[s.GetColumnByField('SuperficieGeolocalisee').index].value;
    if (!editInvoked && idPlantation !== null && superficieGeolocalisee !== null) {
        if (superficieGeolocalisee > 0)
            setTimeout(function () { gridViewDonneesGeographiqueCustomValidate(); }, 0);
    }
}


function gridViewDonneesGeographiqueBatchEditRowValidating(s, e) {
    var validateidPlantation = e.validationInfo[s.GetColumnByField('PlantationID').index];
    //var validateidAnneeCulture = e.validationInfo[s.GetColumnByField('AnneeCulture').index];
    // var validateNouvellePlantationID = e.validationInfo[s.GetColumnByField('NouvellePlantationID').index];
    var validateSuperficie = e.validationInfo[s.GetColumnByField('SuperficieGeolocalisee').index];
    totalSuperficie = 0.00;
    var total = 0.00;
    if (validateidPlantation && validateSuperficie) {
        var indices = gridViewDonneesGeographique.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstSuperficie = [];
            for (var i = 0; i < indices.length; i++) {
                var currentPlantationID = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'PlantationID', false);
                //var currenteAnneeCultureID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                var currentSuperficie = gridViewDonneesGeographique.batchEditApi.GetCellValue(indices[i], 'SuperficieGeolocalisee', false);
                if (currentPlantationID && currentSuperficie && indices[i] !== e.visibleIndex) {
                    lstSuperficie.push(currentPlantationID);
                    total += currentSuperficie;
                }
            }
            total += validateSuperficie.value;
            totalSuperficie = total;
            
            ////ce nom a été affecté sur le grid batch edit
            $("#Total1_SuperficieGeolocalisee").html(total);
            var tot = total;
            var super1 = addSuperficie.GetValue();
            if (super1 === null || super1 === '') {
                super1 = 0;
            }
           
                if (tot > 0 && tot !== super1) {
                    $("#msgAddRequiredFields_message").html(msgValidationSuperficie.replace('{0}', super1.toString()));
                    $("#msgAddRequiredFields").removeClass('hidden');
                }
                else {
                    $("#msgAddRequiredFields").addClass("hidden");
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




function editMatriculeOperateurGotFocus(s, e) {
    editMatriculeOperateur.PerformCallback();
}
function editMatriculeOperateurInit(s, e) {
    if (s.GetItemCount() === 1) {
        s.SetSelectedIndex(0);
    }
}

function editMatriculeOperateurBeginCallback(s, e) {
    e.customArgs['idTypeContact'] = "E";
    e.customArgs['idActivite'] = "EM";
}



function PlantationIDBeginCallback(s, e) {
    e.customArgs['idCulture'] = CultureID.GetValue() || "";
}

function PlantationIDSelectedIndexChanged(s, e) {
  
    if (s.GetValue() != null || s.GetValue() != "") {
        //if (s.GetSelectedItem() != null) {
        //    LocaliteID.SetValue(s.GetSelectedItem().GetColumnText("LocaliteID"));
        //    DistanceUsine.SetValue(s.GetSelectedItem().GetColumnText("DistanceUsine"));
        //}
        //else {
        //    LocaliteID.SetValue(null);
        //    DistanceUsine.SetValue(null);
        //}    
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

function ValiderDateGeolocalisation() {
    if (DateGeolocalisation.GetValue() === null || DateGeolocalisation.GetValue() === "") {
        critereValidated = false;
        $("#DateGeolocalisation").addClass('requiredField');
    }
    else {
        $("#DateGeolocalisation").removeClass('requiredField');
    }
}


function ValueChangedDateGeolocalisation() {
    var dateGeo, idAncienneDate
    var dateGeoloc = new Date(DateGeolocalisation.GetValue());
    var ancienneDate = idAncienneDateGeolocalisation.substring(0, 10);
    dateGeo = dateGeoloc.toLocaleDateString();
    idAncienneDate = ancienneDate;
    if (dateGeo === idAncienneDate) {
        $("#msgAddRequiredFields_message").html('Veuillez saisir la nouvelle date de géolocalisation.');
        $("#msgAddRequiredFields").removeClass('hidden');
    }
    else {
        $("#msgAddRequiredFields").addClass("hidden");
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


function ValidereditMatriculeOperateur() {
    if (editMatriculeOperateur.GetValue() === null || editMatriculeOperateur.GetValue() === "") {
        critereValidated = false;
        $("#editMatriculeOperateur").addClass('requiredField');
    }
    else {
        $("#editMatriculeOperateur").removeClass('requiredField');
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


function SubmitSuperficieForm() {
    var validator = $("#frmCessionPlantation").validate();
    if (validator.form()) {
        if (gridViewSuperficie.batchEditApi.ValidateRows() && gridViewSuperficieCustomValidate()) {
            GenerateSurperficieFromGrid();
            AddCessionSurface();
        }
    }
}


function GenerateSurperficieFromGrid() {
    var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
    var idClone, idAnneeCulture, dateMiseExploitation, superficie, idNouvellePlantation, anciennePlantationID, idMotifEvolution
    var superficiePlantation = [];
    if (indices !== null) {
        if (indices.length === 0) {
            SuperficieString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                idNouvellePlantation = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'NouvellePlantationID', false);
                anciennePlantationID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnciennePlantationID', false);
                idMotifEvolution = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'MotifEvolutionID', false);
                idClone = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                idAnneeCulture = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                dateMiseExploitation = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'DateMiseExploitation', false);
                superficie = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                if (idClone !== null && idAnneeCulture !== null && superficie !== null) {
                    superficiePlantation.push({
                        AnneeCulture: idAnneeCulture,
                        NouvellePlantationID: idNouvellePlantation,
                        AnciennePlantationID: anciennePlantationID,
                        MotifEvolutionID: idMotifEvolution,
                        CloneID: idClone,
                        DateMiseExploitation: dateMiseExploitation,
                        Superficie: superficie
                    });
                } else {
                    return false;
                }
            }
            SuperficieString = JSON.stringify(superficiePlantation);
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
       
        var super1 = addSupSuperficie.GetValue();

        if (totalSuperficie === super1) {
            gridViewValid = true
        }
        else {
            gridViewValid = false
        }
    }
    return gridViewValid;
}

function gridViewSuperficieBatchEditEndEditing(s, e) {
    var idClone = e.rowValues[s.GetColumnByField('CloneID').index].value;
    var idAnneeCulture = e.rowValues[s.GetColumnByField('AnneeCulture').index].value;
    var superficie = e.rowValues[s.GetColumnByField('Superficie').index].value;
    if (!editInvoked && idClone !== null && idAnneeCulture !== null && superficie !== null) {
        if (superficie > 0)
            setTimeout(function () { gridViewSuperficieCustomValidate(); }, 0);
    }
}

function gridViewSuperficieBatchEditRowValidating(s, e) {
    var validateidClone = e.validationInfo[s.GetColumnByField('CloneID').index];
    var validateidAnneeCulture = e.validationInfo[s.GetColumnByField('AnneeCulture').index];
    // var validateNouvellePlantationID = e.validationInfo[s.GetColumnByField('NouvellePlantationID').index];
    var validateSuperficie = e.validationInfo[s.GetColumnByField('Superficie').index];
    totalSuperficie = 0.00;
    var total = 0.00;
    if (validateidClone && validateidAnneeCulture) {
        var indices = gridViewSuperficie.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstSuperficie = [];
            for (var i = 0; i < indices.length; i++) {
                var currentCloneID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                var currenteAnneeCultureID = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                var currentSuperficie = gridViewSuperficie.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                if (currentCloneID && currenteAnneeCultureID && indices[i] !== e.visibleIndex) {

                    lstSuperficie.push(currenteAnneeCultureID);

                    total += currentSuperficie;
                }
            }
            total += validateSuperficie.value;
            totalSuperficie = total;
            ////ce nom a été affecté sur le grid batch edit
            $("#Total1_Superficie").html(total);
            var tot = total;
            var super1 = addSupSuperficie.GetValue();
            if (super1 === null || super1 === '') {
                super1 = 0;
            }

            if (tot > 0 && tot !== super1) {
                $("#msgAddRequiredFields_message1").html(msgValidationSuperficie.replace('{0}', super1.toString()));
                $("#msgAddRequiredFields1").removeClass('hidden');
            }
            else {
                $("#msgAddRequiredFields1").addClass("hidden");
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




function GenerateSurperficie1FromGrid() {
    var indices = gridViewSuperficie1.batchEditApi.GetRowVisibleIndices();
    var idClone, idAnneeCulture, dateMiseExploitation, superficie, idNouvellePlantation, anciennePlantationID, idMotifEvolution
    var superficiePlantation = [];
    if (indices !== null) {
        if (indices.length === 0) {
            SuperficieString = "";
            return true;
        } else {
            for (var i = 0; i < indices.length; i++) {
                idNouvellePlantation = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'NouvellePlantationID', false);
                anciennePlantationID = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'AnciennePlantationID', false);
                idMotifEvolution = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'MotifEvolutionID', false);
                idClone = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                idAnneeCulture = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                dateMiseExploitation = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'DateMiseExploitation', false);
                superficie = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                if (idClone !== null && idAnneeCulture !== null && superficie !== null) {
                    superficiePlantation.push({
                        AnneeCulture: idAnneeCulture,
                        NouvellePlantationID: idNouvellePlantation,
                        AnciennePlantationID: anciennePlantationID,
                        MotifEvolutionID: idMotifEvolution,
                        CloneID: idClone,
                        DateMiseExploitation: dateMiseExploitation,
                        Superficie: superficie
                    });
                } else {
                    return false;
                }
            }
            SuperficieString = JSON.stringify(superficiePlantation);
            return true;
        }
    }
    return false;
}

function gridViewSuperficie1CustomValidate() {
    // globalValidation = true;
    gridViewValid = true;
    // var total = 0.00;
    var indices = gridViewSuperficie1.batchEditApi.GetRowVisibleIndices();
    if (indices !== null) {

        var super1 = editSuperficie.GetValue();

        if (totalSuperficie === super1) {
            gridViewValid = true
        }
        else {
            gridViewValid = false
        }
    }
    return gridViewValid;
}

function gridViewSuperficie1BatchEditEndEditing(s, e) {
    var idClone = e.rowValues[s.GetColumnByField('CloneID').index].value;
    var idAnneeCulture = e.rowValues[s.GetColumnByField('AnneeCulture').index].value;
    var superficie = e.rowValues[s.GetColumnByField('Superficie').index].value;
    if (!editInvoked && idClone !== null && idAnneeCulture !== null && superficie !== null) {
        if (superficie > 0)
            setTimeout(function () { gridViewSuperficie1CustomValidate(); }, 0);
    }
}

function gridViewSuperficie1BatchEditRowValidating(s, e) {
    var validateidClone = e.validationInfo[s.GetColumnByField('CloneID').index];
    var validateidAnneeCulture = e.validationInfo[s.GetColumnByField('AnneeCulture').index];
    // var validateNouvellePlantationID = e.validationInfo[s.GetColumnByField('NouvellePlantationID').index];
    var validateSuperficie = e.validationInfo[s.GetColumnByField('Superficie').index];
    totalSuperficie = 0.00;
    var total = 0.00;
    if (validateidClone && validateidAnneeCulture) {
        var indices = gridViewSuperficie1.batchEditApi.GetRowVisibleIndices();
        if (indices !== null) {
            var lstSuperficie = [];
            for (var i = 0; i < indices.length; i++) {
                var currentCloneID = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'CloneID', false);
                var currenteAnneeCultureID = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'AnneeCulture', false);
                var currentSuperficie = gridViewSuperficie1.batchEditApi.GetCellValue(indices[i], 'Superficie', false);
                if (currentCloneID && currenteAnneeCultureID && indices[i] !== e.visibleIndex) {

                    lstSuperficie.push(currenteAnneeCultureID);

                    total += currentSuperficie;
                }
            }
            total += validateSuperficie.value;
            totalSuperficie = total;
            ////ce nom a été affecté sur le grid batch edit
            $("#Total2_Superficie").html(total);
            var tot = total;
            var super1 = editSuperficie.GetValue();
            if (super1 === null || super1 === '') {
                super1 = 0;
            }

            if (tot > 0 && tot !== super1) {
                $("#msgAddRequiredFields_message2").html(msgValidationSuperficie.replace('{0}', super1.toString()));
                $("#msgAddRequiredFields2").removeClass('hidden');
            }
            else {
                $("#msgAddRequiredFields2").addClass("hidden");
            }

            for (var j = 0; j < lstSuperficie.length; j++) {
                if (lstSuperficie[j] === validateidAnneeCulture.value) {
                    validateidAnneeCulture.isValid = false;
                   // validateidAnneeCulture.errorText = msgUnique;
                }
            }
        }
    }
}



function SubmitPlantationDonneesGeoSuperficieForm() {
    var validator = $("#frmCessionPlantation").validate();
    if (validator.form()) {
        if (gridViewSuperficie1.batchEditApi.ValidateRows() && gridViewSuperficie1CustomValidate() ) {
            GenerateSurperficie1FromGrid();
            UpdatePlantationDonneesGeoSurface();
        }
    }
}

function dtmDateGeolocalisationValueChanged(s, e) {

    if (s.GetValue() != null || s.GetValue() != "") {
  gridPlantation.PerformCallback();
    }
}
