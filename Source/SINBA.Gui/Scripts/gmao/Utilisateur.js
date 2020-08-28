
function DateDebut_Changed() {
    SetDates();
}

function SetDates() {
    var dateDebut = DateDebut.GetValue();
    DateFin.SetEnabled(false);
    if (dateDebut !== null) {
        DateFin.SetMinDate(dateDebut);
        DateFin.SetEnabled(true);
        var dateFin = DateFin.GetValue();
        if (dateFin !== null && dateFin < dateDebut)
            DateFin.SetValue(null);
    }
    else {
        DateFin.SetValue(null);
        var minDate = new Date();
        var today = new Date(minDate.getFullYear(), minDate.getMonth(), minDate.getDate());
        DateDebut.SetMinDate(today);
        DateFin.SetMinDate(today);
    }
}

function OnParameterChanged()
{
    var idTypeUtilisateurListe = TypeUtilisateur.GetValue();
    var dateDebut = DateDebut.GetValue();
    var dateFin = DateFin.GetValue();
    var idUser = $("#IdUser").val();
    var idTypeUtilisateur = $("#Id").val();

    SetDates();

    if (idTypeUtilisateurListe != null && idTypeUtilisateurListe != 0 && dateDebut != null)
    {
        var month = dateDebut.getMonth() + 1;
        var dateDebutS = dateDebut.getDate() + "/" + month + "/" + dateDebut.getFullYear();
        var DateFinS;
        if (dateFin != null)
        {
            var month = dateFin.getMonth() + 1;
            DateFinS = dateFin.getDate() + "/" + month + "/" + dateFin.getFullYear();
        }

        $.ajax({
            url: $("#IsDateForTypeUtilisateurAvailable").val(),
            type: 'GET',
            data: {
                'idTypeUtilisateur': idTypeUtilisateur, 'idUser': idUser, 'idTypeUtilisateurListe': idTypeUtilisateurListe, 'dateDebutS': dateDebutS, 'DateFinS': DateFinS
            },
            success: function (result) {
                msg = JSON.stringify(result);
                if (msg != '""') {
                    alert(msg);
                    DateDebut.SetValue(null);
                    DateFin.SetValue(null);
                }
            },
            error: function (response) {alert('Erreur');}
        });
    }
}


