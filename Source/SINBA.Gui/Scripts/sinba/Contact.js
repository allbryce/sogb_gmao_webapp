
function MasterGridOnBeginCallback(s, e) {
    e.customArgs['PersonnePhysique'] = typePersonne.PersonnePhysique;
    e.customArgs['RechercheNomEtPrenom'] = RechercheNomEtPrenom.GetValue();
    e.customArgs['RechercheLocaliteID'] = RechercheLocaliteID.GetValue();
    e.customArgs['RechercheDateNaissance'] = RechercheDateNaissance.GetValue();
    if (typeof RechercheSexe !== 'undefined') e.customArgs['RechercheSexe'] = RechercheSexe.GetValue();
    if (typeof RechercheNumeroPiece !== 'undefined') e.customArgs['RechercheNumeroPiece'] = RechercheNumeroPiece.GetValue();
    if (typeof RechercheNumeroTicketIdentification !== 'undefined') e.customArgs['RechercheNumeroTicketIdentification'] = RechercheNumeroTicketIdentification.GetValue();
    if (typeof RechercheNumeroMatriculeNational !== 'undefined') e.customArgs['RechercheNumeroMatriculeNational'] = RechercheNumeroMatriculeNational.GetValue();
    e.customArgs['RechercheTelephone'] = RechercheTelephone.GetValue();
    e.customArgs['RechercheEmail'] = RechercheEmail.GetValue();
}

function LoadMasterGrid() {
    MasterGrid.PerformCallback();
}

function Delete() {

    var requestResponse = null;
    $.ajax({
        url: $("#Delete").val(),
        type: 'POST',
        async: false,
        data: { ContactID: $("#CodeToDelete").val() },
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
            //$("#msgModalError").addClass("hidden");
            DeleteModal.Hide();
            MasterGrid.PerformCallback();
        } else {
            //$('#msgModalError').removeClass('hidden');
            //$('#msgModalError_Message').html(requestResponse.Errors.join('<br>'));
        }
    }


}

function OpenDeleteModal(item) {

    $("#CodeToDelete").val(item.ContactID);
    DeleteModal.Show();
}


