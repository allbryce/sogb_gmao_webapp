/***** ACTEUR ******/
var msgInvalidForm = "";
var typePersonne = { LibelleTypePersonne: "", PersonnePhysique: true };
var selectedContact = { ContactID: "", Nom: "", Prenom: "" };
var currentDirigeantRowIndex;
var MasterGridView;

function TypeContactIDSelectedIndexChanged(s, e) {
    LoadMasterGrid();
}

function initAddContactModal() {
    $("#CodeToEdit").val("");
    ContactModal_Nom.SetValue(null);
    ContactModal_LocaliteID.SetValue(null);
    ContactModal_DateNaissance.SetValue(null);
    ContactModal_LieuNaissance.SetValue(null);
    ContactModal_Adresse.SetValue(null);
    ContactModal_NumeroTelephone1.SetValue(null);
    ContactModal_NumeroTelephone2.SetValue(null);
    ContactModal_NumeroTelephone3.SetValue(null);
    ContactModal_AdresseMail1.SetValue(null);
    ContactModal_AdresseMail2.SetValue(null);
    if (typeof ContactModal_Sexe !== 'undefined')ContactModal_Sexe.SetValue(null);
    if (typeof ContactModal_Prenom !== 'undefined')ContactModal_Prenom.SetValue(null);
    if (typeof ContactModal_PieceIdentiteID !== 'undefined')ContactModal_PieceIdentiteID.SetValue(null);
    if (typeof ContactModal_NumeroPiece !== 'undefined')ContactModal_NumeroPiece.SetValue(null);
    if (typeof ContactModal_DateDelivrancePiece !== 'undefined')ContactModal_DateDelivrancePiece.SetValue(null);
    if (typeof ContactModal_LieuDelivrancePiece !== 'undefined')ContactModal_LieuDelivrancePiece.SetValue(null);
    if (typeof ContactModal_CompteContribuable !== 'undefined')ContactModal_CompteContribuable.SetValue(null);
    if (typeof ContactModal_NumeroTicketIdentification !== 'undefined')ContactModal_NumeroTicketIdentification.SetValue(null);
    if (typeof ContactModal_NumeroMatriculeNational !== 'undefined')ContactModal_NumeroMatriculeNational.SetValue(null);
    if (typeof ActiviteArray !== 'undefined') ActiviteArray.SetValue(null);
    if (typeof CultureArray !== 'undefined') CultureArray.SetValue(null);
}

function GetContact() {
    var model = {
        ContactID: $("#CodeToEdit").val(),
        Nom: ContactModal_Nom.GetValue(),
        LocaliteID: ContactModal_LocaliteID.GetValue(),
        DateNaissance: ContactModal_DateNaissance.GetValue().toUTCString(),
        LieuNaissance: ContactModal_LieuNaissance.GetValue(),
        Adresse: ContactModal_Adresse.GetValue(),
        NumeroTelephone1: ContactModal_NumeroTelephone1.GetValue(),
        NumeroTelephone2: ContactModal_NumeroTelephone2.GetValue(),
        NumeroTelephone3: ContactModal_NumeroTelephone3.GetValue(),
        AdresseMail1: ContactModal_AdresseMail1.GetValue(),
        AdresseMail2: ContactModal_AdresseMail2.GetValue(),
        //Actif : Actif.GetValue(),
        PersonnePhysique: typePersonne.PersonnePhysique
    };

    if (typeof ContactModal_Prenom !== 'undefined') model.Prenom = ContactModal_Prenom.GetValue();
    if (typeof ContactModal_NumeroTicketIdentification !== 'undefined') model.NumeroTicketIdentification = ContactModal_NumeroTicketIdentification.GetValue();
    if (typeof ContactModal_NumeroMatriculeNational !== 'undefined') model.NumeroMatriculeNational = ContactModal_NumeroMatriculeNational.GetValue();
    if (typeof ContactModal_Sexe !== 'undefined') model.Sexe = ContactModal_Sexe.GetValue();
    if (typeof ContactModal_CompteContribuable !== 'undefined') model.CompteContribuable = ContactModal_CompteContribuable.GetValue();
    if (typeof ContactModal_ActiviteID !== 'undefined') model.ActiviteID = ContactModal_ActiviteID.GetValue();
    if (typeof ContactModal_PieceIdentiteID !== 'undefined') model.PieceIdentiteID = ContactModal_PieceIdentiteID.GetValue();
    if (typeof ContactModal_NumeroPiece !== 'undefined') model.NumeroPiece = ContactModal_NumeroPiece.GetValue();
    if (typeof ContactModal_DateDelivrancePiece !== 'undefined') model.DateDelivrancePiece = ContactModal_DateDelivrancePiece.GetValue().toUTCString();
    if (typeof ContactModal_LieuDelivrancePiece !== 'undefined') model.LieuDelivrancePiece = ContactModal_LieuDelivrancePiece.GetValue();
    if (typeof ActiviteArray !== 'undefined') model.ActiviteArray = ActiviteArray.GetValue();
    if (typeof CultureArray !== 'undefined') model.CultureArray = CultureArray.GetValue();
    if (typeof gridViewFonctionContact !== 'undefined') model.FonctionContactString = GetDirigeants();
    if (typeof gridViewReferenceBancaire !== 'undefined') model.ReferenceBancaireString = GetReferenceBancaire();

    return model;
}

function AddContact() {

    var ajoutreussi = false;
    var model = GetContact();
    var requestResponse = null;

    $.ajax({
        url: $("#AddContact").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            if (typeof result.Value !== 'undefined') {
                requestResponse = result;
            }
        },
        error: function (e) {
            window.alert(e);
        }
    });

    if (requestResponse !== null) {
        if (requestResponse.Value) {
            initAddContactModal();
            $("#msgModalError").addClass("hidden");
            AddContactModal.Hide();
            MasterGridView.PerformCallback();
        } else {
            $('#msgModalError').removeClass('hidden');
            $('#msgModalError_Message').html(requestResponse.Errors.join('<br>'));
        }
    }

}

function EditContact() {
    var ajoutreussi = false;
    var model = GetContact();
    var requestResponse = null;

    $.ajax({
        url: $("#EditContact").val(),
        type: 'POST',
        async: false,
        data: model,
        success: function (result) {
            if (typeof result.Value !== 'undefined') {
                requestResponse = result;
            }
        },
        error: function (e) {
            window.alert(e);
        }
    });

    if (requestResponse !== null) {
        if (requestResponse.Value) {
            initAddContactModal();
            $("#msgModalError").addClass("hidden");
            AddContactModal.Hide();
            MasterGridView.PerformCallback();
        } else {
            $('#msgModalError').removeClass('hidden');
            $('#msgModalError_Message').html(requestResponse.Errors.join('<br>'));
        }
    }
}

function OpenAddContactModal(item) {
        AddMode = true;
        item.AddMode = true;
        if (typeof item.PersonnePhysique === 'undefined') item.PersonnePhysique = typePersonne.PersonnePhysique;
        if (typeof item.LibelleTypePersonne === 'undefined') item.LibelleTypePersonne = typePersonne.LibelleTypePersonne;
        MasterGridView = ASPxClientControl.GetControlCollection().GetByName(item.GridName);
        AddContactModal.PerformCallback(item);
        AddContactModal.Show();
        $("#AddContactModal_PWH-1T").html(item.LibelleTypePersonne);
}

function OpenEditModal(item) {
        AddMode = false;
        item.AddMode = false;
        $("#CodeToEdit").val(item.ContactID);
        $("#ContactID").val(item.ContactID);
        MasterGridView = ASPxClientControl.GetControlCollection().GetByName(item.GridName);
        if (typeof item.PersonnePhysique === 'undefined') item.PersonnePhysique = typePersonne.PersonnePhysique;
        if (typeof item.LibelleTypePersonne === 'undefined') item.LibelleTypePersonne = typePersonne.LibelleTypePersonne;
        AddContactModal.PerformCallback(item);
        AddContactModal.Show();
        $("#AddContactModal_PWH-1T").html(item.LibelleTypePersonne);
}

function SelectionChanged(s, e) {
    s.GetSelectedFieldValues("ProjetPlanteurID", GetSelectedFieldValuesCallback);
}

function GetSelectedFieldValuesCallback(values) {
    //SelectedRows.BeginUpdate();
    //try {
    //    SelectedRows.ClearItems();
    //    for (var i = 0; i < values.length; i++) {
    //        SelectedRows.AddItem(values[i]);
    //    }
    //} finally {
    //    SelectedRows.EndUpdate();
    //}
    //$("#count").html(gvRowSelection.GetSelectedRowCount());

}

function closeMessage() {
    $("#msgModalError").addClass("hidden");
}

function OnBatchEditStartEditing(s, e) {
    currentColumnName = e.focusedColumn.fieldName;
    currentDirigeantRowIndex = e.visibleIndex;
}

function SelectContact() {
    SearchContactModal.Show();
}

function GetDirigeants() {
    var indices = gridViewFonctionContact.batchEditApi.GetRowVisibleIndices();
    var dirigeants = [];
    if (indices !== null) {
        if (indices.length === 0) {
            return JSON.stringify(dirigeants);
        } else {
            for (var i = 0; i < indices.length; i++) {

                var currentdirigent = {
                    ContactID: gridViewFonctionContact.batchEditApi.GetCellValue(indices[i], 'ContactID', false),
                    FonctionID: gridViewFonctionContact.batchEditApi.GetCellValue(indices[i], 'FonctionID', false),
                    AutreContactID: gridViewFonctionContact.batchEditApi.GetCellValue(indices[i], 'AutreContactID', false)
                };
                if (currentdirigent.AutreContactID !== null && currentdirigent.FonctionID !== null) {
                    dirigeants.push(currentdirigent);
                } else {
                    return JSON.stringify(dirigeants);
                }

            }
            return JSON.stringify(dirigeants);
        }
    }
    return JSON.stringify(dirigeants);
}

function GetReferenceBancaire() {
    var indices = gridViewReferenceBancaire.batchEditApi.GetRowVisibleIndices();
    var references = [];
    if (indices !== null) {
        if (indices.length === 0) {
            return JSON.stringify(references);
        } else {
            for (var i = 0; i < indices.length; i++) {

                var currentreference = {
                    FournisseurID: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'FournisseurID', false),
                    CodeBanque: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'CodeBanque', false),
                    NumeroCompte: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'NumeroCompte', false),
                    CleRib: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'CleRib', false),
                    DateDebutSituation: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'DateDebutSituation', false),
                    DateFinSituation: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'DateFinSituation', false),
                    ModePaiementID: gridViewReferenceBancaire.batchEditApi.GetCellValue(indices[i], 'ModePaiementID', false)
                };

                //traitement des dates
                //traitement des dates
                if (currentreference.DateDebutSituation !== null)
                    currentreference.DateDebutSituation = currentreference.DateDebutSituation.toUTCString();
                if (currentreference.DateFinSituation !== null)
                    currentreference.DateFinSituation = currentreference.DateFinSituation.toUTCString();

                if (currentreference.AutreContactID !== null && currentreference.FonctionID !== null) {
                    references.push(currentreference);
                } else {
                    return JSON.stringify(references);
                }

            }
            return JSON.stringify(references);
        }
    }
    return JSON.stringify(references);
}

function SearchGridOnBeginCallback(s, e) { 
    e.customArgs['RechercheLocaliteID'] = Contact_LocaliteID.GetValue();
    e.customArgs['RechercheDateNaissance'] =  Contact_DateNaissance.GetValue();
    e.customArgs['RechercheNomEtPrenom'] = Contact_NomEtPrenom.GetValue();
    e.customArgs['RechercheSexe'] = Contact_Sexe.GetValue();
    e.customArgs['RechercheNumeroPiece'] = Contact_NumeroPiece.GetValue();
    e.customArgs['RechercheNumeroTicketIdentification'] = Contact_NumeroTicketIdentification.GetValue();
    e.customArgs['RechercheNumeroMatriculeNational'] = Contact_NumeroMatriculeNational.GetValue();
    e.customArgs['RechercheTelephone'] = Contact_NumeroTelephone.GetValue();
    e.customArgs['RechercheEmail'] = Contact_Email.GetValue();
}

function SearchGridOnFocusedRowChanged(s, e) {

}

function SearchGridOnSelectionChanged(s, e) {
    if (e.visibleIndex !== -1) {
        s.GetSelectedFieldValues('ContactID;Nom;Prenom', OnGetRowValues);
        console.log(selectedContact);
    }
}

function OnGetRowValues(values) {
    selectedContact.ContactID = values[0][0];
    selectedContact.Nom = values[0][1];
    selectedContact.Prenom = values[0][2];
}

function LoadSearchGrid() {
    SearchGrid.PerformCallback();
}

function AjouterDirigeant() {

    gridViewFonctionContact.batchEditApi.SetCellValue(currentDirigeantRowIndex, "AutreContactID", selectedContact.ContactID, null, true);
    gridViewFonctionContact.batchEditApi.SetCellValue(currentDirigeantRowIndex, "DescriptionContact", selectedContact.Nom + " " + selectedContact.Prenom, null, true);
    SearchContactModal.Hide();

}


