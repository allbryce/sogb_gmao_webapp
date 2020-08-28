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
    $("#Contacts").removeClass('requiredField');
    $("#CultureID").removeClass('requiredField');
    $("#TypeContactID").removeClass('requiredField');
    $("#PlantationID").removeClass('requiredField');
    $("#ContactID").removeClass('requiredField');
    $("#Contacts").removeClass('requiredField');
    $("#Plantations").removeClass('requiredField');
    $("#DateAdhesion").removeClass('requiredField');
    //var IdPlantationVal = '';
    //var IdContact = '';

    //var plantations = '';
    //var contacts = '';

  ////  ValiderCulture();
  //  var dateAdhesion = DateAdhesion.GetValue();
  //  if (dateAdhesion === null || dateAdhesion === "") {
  //      dateAdhesion = DateAdhesion.GetValue();
  //  }
  //  else {
  //      dateAdhesion = DateAdhesion.GetValue().toUTCString();
  //  }

  //  if (ModeSaisie.GetValue())
  //  {
  //      if (typeof (Plantations) !== 'undefined') {
  //          plantations = Plantations.GetValue();
  //      }     
  //      if (typeof (ContactID) !== 'undefined') {
  //          IdContact = ContactID.GetValue();
  //      }
  //  }
  //  else
  //  {
  //      if (typeof (PlantationID) !== 'undefined') {
  //          IdPlantationVal = PlantationID.GetValue();
  //      }
  //      if (typeof (Contacts) !== 'undefined') {
  //          contacts = Contacts.GetValue();
  //      }
  //  }

   // ModeSaisie.Caption = "test";
    gridPlantation.PerformCallback();

     
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

function ValiderDateAdhesion() {
    if (DateAdhesion.GetValue() === null || DateAdhesion.GetValue() === "") {
        critereValidated = false;
        $("#DateAdhesion").addClass('requiredField');
    }
    else {
        $("#DateAdhesion").removeClass('requiredField');
    }
}

function ValiderTypeContact() {
    if (TypeContactID.GetValue() === null || TypeContactID.GetValue() === "") {
        critereValidated = false;
        $("#TypeContactID").addClass('requiredField');
    }
    else {
        $("#TypeContactID").removeClass('requiredField');
    }
}

function ValiderContact() {
    if (typeof (ContactID) !== 'undefined') {   
        if (ContactID.GetValue() === null || ContactID.GetValue() === "") {
            critereValidated = false;
            $("#ContactID").addClass('requiredField');
        }
        else {
            $("#ContactID").removeClass('requiredField');
        }
    }
}

function ValiderPlantation() {
    if (typeof (PlantationID) !== 'undefined') {
        if (PlantationID.GetValue() === null || PlantationID.GetValue() === "") {
            critereValidated = false;
            $("#PlantationID").addClass('requiredField');
        }
        else {
            $("#PlantationID").removeClass('requiredField');
        }
    }
}

function ValiderPlantations() {
    if (typeof (Plantations) !== 'undefined') {
        if (Plantations.GetValue() === null || Plantations.GetValue() === "") {
            critereValidated = false;
            $("#Plantations").addClass('requiredField');
        }
        else {
            $("#Plantations").removeClass('requiredField');
        }
    }
}

function ValiderContacts() {
    if (typeof (Contacts) !== 'undefined') {
        if (Contacts.GetValue() === null || Contacts.GetValue() === "") {
            critereValidated = false;
            $("#Contacts").addClass('requiredField');
        }
        else {
            $("#Contacts").removeClass('requiredField');
        }
    }
}

function AddIntervenant(s, e) {
    var ajoutreussi = false;
    critereValidated = true;
    var IdPlantationVal = '';
    var IdContact = ''; 
    var plantations = '';
    var contacts = '';

    ValiderDateAdhesion();
    ValiderTypeContact()
    ValiderCulture();
    ValiderContact();
    ValiderPlantation();
    ValiderPlantations();
    ValiderContacts();

    if (critereValidated) {


        var dateAdhesion = DateAdhesion.GetValue();
        if (dateAdhesion === null || dateAdhesion === "") {
            dateAdhesion = DateAdhesion.GetValue();
        }
        else {
            dateAdhesion = DateAdhesion.GetValue().toUTCString();
        }

        if (ModeSaisie.GetValue()) {
            if (typeof (Plantations) !== 'undefined') {
                plantations = Plantations.GetValue();
            }
            if (typeof (ContactID) !== 'undefined') {
                IdContact = ContactID.GetValue();
            }
        }
        else {
            if (typeof (PlantationID) !== 'undefined') {
                IdPlantationVal = PlantationID.GetValue();
            }
            if (typeof (Contacts) !== 'undefined') {
                contacts = Contacts.GetValue();
            }
        }

        var model = {
            TypeContactID: TypeContactID.GetValue(),
            DateAdhesion: dateAdhesion,
            ContactID: IdContact,
            PlantationID: IdPlantationVal,
            Contacts: contacts,
            Plantations: plantations,
            ModeSaisie: ModeSaisie.GetValue()
        };

        $.ajax({
            url: $("#AddIntervenant").val(),
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
            ValuePanel.PerformCallback(
                {
                    ModeSaisie: ModeSaisie.GetValue()
                }
            );


            gridPlantation.PerformCallback({
                TypeContactID: TypeContactID.GetValue(),
                DateAdhesion: dateAdhesion,
                ContactID: IdContact,
                PlantationID: IdPlantationVal,
                Contacts: contacts,
                Plantations: plantations
                // ModeSaisie: ModeSaisie.GetValue()
            });

            ajoutreussi = false;
        }
    }
}



function closeMessage() {
    $("#msgAddRequiredFields").addClass("hidden");
}






function addCultureIDSelectedIndexChanged(s, e) {
    if (s.GetValue() != null || s.GetValue() != "") {
        idCulture = s.GetValue();   
        if (typeof PlantationID !== 'undefined') {
            PlantationID.SetValue(null);
        }
        //if (typeof Plantations !== 'undefined') {
        //    Plantations.SetValue('');
        //    $("#Plantations").removeClass('requiredField');
        //}  
    }
}

function GetViewModelValue() {
    var viewmodel = {
        ModeSaisie: ModeSaisie.GetValue()
    };
    return viewmodel;
}

function OnPanelBeginCallback(s, e) {
    var model = GetViewModelValue();
    e.customArgs["IntervenantViewModels"] = model;
}

function ModeSaisieValueChanged(s, e) {
    ValuePanel.PerformCallback({    
        ModeSaisie: ModeSaisie.GetValue()
    });

    //var txt_label = document.getElementById("ModeSaisie").nodeValue; 
    //var txt_label = document.getElementById("ModeSaisie").textContent;

 }

function gridPlantationOnBeginCallback(s, e) {
     if (typeof Plantations !== 'undefined') {
         e.customArgs["Plantations"] = Plantations.GetValue();
    }  
    if (typeof Contacts !== 'undefined') {
        e.customArgs["Contacts"] = Contacts.GetValue();
    } 
    if (typeof PlantationID !== 'undefined') {
        e.customArgs["PlantationID"] = PlantationID.GetValue();
    }  
    if (typeof ContactID !== 'undefined') {
        e.customArgs["ContactID"] = ContactID.GetValue();
    }   
    if (DateAdhesion.GetValue() !== null) {
        e.customArgs["DateAdhesion"] = DateAdhesion.GetValue().toUTCString();
    } 
    e.customArgs["CultureID"] = CultureID.GetValue();
    e.customArgs["TypeContactID"] = TypeContactID.GetValue();
    e.customArgs["ModeSaisie"] = ModeSaisie.GetValue();
     
}

function ContactsBeginCallback(s, e) {
    //if (typeof PlantationID !== 'undefined') {
    //    e.customArgs["idPlantation"] = PlantationID.GetValue();
    //}  
    e.customArgs["idTypeContact"] = TypeContactID.GetValue();
}



function ContactIDBeginCallback(s, e) {
        e.customArgs["idTypeContact"] = TypeContactID.GetValue();
}

function PlantationsBeginCallback(s, e) {
    e.customArgs['idCulture'] = CultureID.GetValue();
    //e.customArgs["idTypeContact"] = TypeContactID.GetValue();
    //if (typeof ContactID !== 'undefined') {
    //    e.customArgs["idContact"] = ContactID.GetValue();
    //}   
}


function PlantationIDBeginCallback(s, e) {
    e.customArgs['idCulture'] = CultureID.GetValue();
}

function PlantationIDLostFocus(s, e) {
    ValiderPlantation();
}

function ContactIDLostFocus(s, e) {
    ValiderContact();
}

function TypeContactIDSelectedIndexChanged(s, e) {
    if (typeof PlantationID !== 'undefined') {
        PlantationID.SetValue(null);
    } 
    //if (typeof Plantations !== 'undefined') {
    //    Plantations.SetValue(null);
    //}  

    if (typeof ContactID !== 'undefined') {
        ContactID.SetValue(null);
    }  
    if (typeof Contacts !== 'undefined') {
        Contacts.SetValue(null);
    }  

}

function ContactIDSelectedIndexChanged(s, e) {
   
    if (typeof Plantations !== 'undefined') {
        Plantations.SetValue(null);
    }  
}


function OpenDeleteIntervenantModal(item) {
    $("#IdToDelete").val(item.IntervenantID);
    DeleteIntervenantModal.PerformCallback({ IntervenantID: item.IntervenantID });
    DeleteIntervenantModal.Show();
}

function Remove(s, e) {
    var deletereussi = false;
    var model = {
        idIntervenant: $("#IdToDelete").val()
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
        DeleteIntervenantModal.Hide();
        deletereussi = false;

        gridPlantation.PerformCallback();

    }
}



function OpenEditIntervenantModal(item) {
    $("#IdToDelete").val(item.IntervenantID);

    TypeContactID.SetValue("C");
    // DeleteIntervenantModal.PerformCallback({ IntervenantID: item.IntervenantID });
   // DeleteIntervenantModal.Show();
}