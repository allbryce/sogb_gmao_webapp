var isAddMode = true;
var currentMaterielID;
var gridname;
var isError;
var canClose =true;

function openMaterielAddModal() {
    isAddMode = true;
    MaterielModal.Show();
}

function openCaracteristiquesAddModal( item ) {

    currentMaterielID = item.MaterielId;
    isAddMode = true;
    CaracteristiquesModal.Show();
   gridname = "gridComposant_"+currentMaterielID;
}

function openAssocieModal(item) {

    GetMaterielDomaine(item.DomaineId, item.MaterielId);
    currentMaterielID = item.MaterielId;
    AssocieModal.Show();
}

function SaveCaracteristiques(s,e) {
    var test;
    if (isAddMode) {
        test = CarateristiqueAdd();
    }
    else {
        caracteristiqueEdit();
    }    
    if (canClose) {

        CaracteristiquesModal.Hide();

    } else {
        window.alert("Erreur! vérifiez vos entrées");
    }

    if (!test) {
        gridname.PerformCallback;
    } else {
        window.alert("Une erreur s'est produite lors de l'exécution de cette tâche. Veuillez réessayer SVP");
    }
}

function openCaracteristiquesEditModal(item) {

    var caracteristique = GetComposant(item.MaterielId, item.ComposantId, item.DateInsertion);
    //caracteristique.DateInsertion = item.DateInsertion;
    setComposant(caracteristique);
    CaracteristiquesModal.Show();
}

function openMaterielEditModal(materielid) {
    isAddMode = false;
    currentMateriel = GetMateriel(materielid);
    SetMateriel(currentMateriel);
    MaterielModal.Show();
}

function SaveMateriel() {

    if (isAddMode === true) {

        MaterielAdd();

    }
    else {

        MaterielEdit();
    }

    gridMateriel.PerformCallback();
    if (canClose)
    {

        MaterielModal.Hide();

    }
}


function SetMateriel(materiel) {

    NumeroSerie.SetValue(materiel.NumeroSerie);
    LibelleMateriel.SetValue(materiel.LibelleMateriel);
    DomaineId.SetValue(materiel.DomaineId);
    FournisseurId.SetValue(materiel.FournisseurId);
    NumeroModel.SetValue(materiel.NumeroModel);
    SousFamilleId.SetValue(materiel.SousFamilleId);
    NumeroBonCommande.SetValue(materiel.NumeroBonCommande);
    ClasseMaterielId.SetValue(materiel.ClasseMaterielId);
    TypeMaterielId.SetValue(materiel.TypeMaterielId);
    PrixAchat.SetValue(materiel.PrixAchat);
    Note.SetValue(materiel.Note);
}
function setComposant(caracteristique) {

    ComposantId.SetValue(caracteristique.ComposantId);
    Quantite.SetValue(caracteristique.Quantite);
    if (caracteristique.DateInsertion != null) {

        var date = new Date(caracteristique.DateInsertion);
        DateInsertion.SetValue(date);

    }
    Plafond.SetValue(caracteristique.Plafond);
    var items = caracteristique.Caracteristiques;
    var y = 0;
    for (var lib = 0; lib < items.length; lib++) {
        gridCaracteristique.AddNewRow();
    }
    var indices = gridCaracteristique.batchEditApi.GetRowVisibleIndices();

    for (var i = 0; i < indices.length; i++) {
        gridCaracteristique.batchEditApi.SetCellValue(indices[i], 'CaracteristiqueComposantId', items[i].CaracteristiqueComposantId);
        gridCaracteristique.batchEditApi.SetCellValue(indices[i], 'Valeur', items[i].Valeur);
        gridCaracteristique.batchEditApi.SetCellValue(indices[i], 'Unite', items[i].UniteId);
    }
   
}

function MaterielAdd()
{

    if (DateAcquisition.GetValue.toUTCString < DateMiseEnService.GetValue.toUTCString) {

        window.alert("La mise en service ne peut pas se faire avant l'acquisition");
        canClose = false;
    }
   
    var materiel = {};
    materiel.NumeroSerie = NumeroSerie.GetValue();
    materiel.LibelleMateriel = LibelleMateriel.GetValue();
    materiel.DomaineId = DomaineId.GetValue();
    materiel.FournisseurId = FournisseurId.GetValue();
    materiel.NumeroModel = NumeroModel.GetValue();
    materiel.SousFamilleId = SousFamilleId.GetValue();
    materiel.NumeroBonCommande = NumeroBonCommande.GetValue();
    materiel.ClasseMaterielId = ClasseMaterielId.GetValue();
    materiel.TypeMaterielId = TypeMaterielId.GetValue();
    materiel.Garantie = Garantie.GetValue().toUTCString();
    materiel.Actif = Actif.GetValue();
    if (DateAcquisition.GetValue() !== null) {
        materiel.DateAcquisition = DateAcquisition.GetValue().toUTCString();
    }


    if (DateMiseEnService.GetValue() !== null) {
        materiel.DateMiseEnService = DateMiseEnService.GetValue().toUTCString();
    }

   
    materiel.PrixAchat = PrixAchat.GetValue();
    AddMateriel(materiel);

   
}

function MaterielEdit()
{

    if (DateAcquisition.GetValue.toUTCString < DateMiseEnService.GetValue.toUTCString) {
        window.alert("La mise en service ne peut pas se faire avant l'acquisition");        
    }
    var materiel = {};
  
    materiel.NumeroSerie = NumeroSerie.GetValue();
    materiel.LibelleMateriel = LibelleMateriel.GetValue();
    materiel.DomaineId = DomaineId.GetValue();
    materiel.FournisseurId = FournisseurId.GetValue();
    materiel.NumeroModel = NumeroModel.GetValue();
    materiel.SousFamilleId = SousFamilleId.GetValue();
    materiel.NumeroBonCommande = NumeroBonCommande.GetValue();
    materiel.ClasseMaterielId = ClasseMaterielId.GetValue();
    materiel.TypeMaterielId = TypeMaterielId.GetValue();
    materiel.Garantie = Garantie.GetValue.toUTCString();
    if (DateAcquisition.GetValue() !== null) {
        materiel.DateAcquisition = DateAcquisition.GetValue().toUTCString();
    }
    if (DateMiseEnService.GetValue() !== null) {
        materiel.DateMiseEnService = DateMiseEnService.GetValue().toUTCString();

    } 
    materiel.PrixAchat = PrixAchat.GetValue();
    EditMateriel(materiel);
}


function CarateristiqueAdd() {

    var Composantcaracteristique =
        {
            MaterielId: currentMaterielID,
            ComposantId: ComposantId.GetValue(),
            Quantite: Quantite.GetValue(),
            Plafond: Plafond.GetValue()     
        };
     if(DateInsertion.GetValue() !== null) {
         Composantcaracteristique.DateInsertion= DateInsertion.GetValue().toUTCString()
            }
    Composantcaracteristique.CaracteristiqueComposantString = GetCaracteristiques();
    isError = AddCaracteristique(Composantcaracteristique);
    if (isError) {

        window.alert("Une erreur s'est produite, veuillez vérifier votre saisie");
        canClose = false;
    } else {
        
    }   
}

function caracteristiqueEdit() {

    var Composermateriel = {};
    var possedercaracteristique = {};

    Composermateriel.composantId = composantId;
    Composermateriel.DateIsertion = DateInsertion;
    Composermateriel.Quantite = Quantite;
    Composermateriel.Plafond = Plafond;
    possedercaracteristique.CaracteristiqueComposantId = CaracteristiqueComposantId;
    possedercaracteristique.Valeur = Valeur;
    possedercaracteristique.UniteId = UniteId;
    EditCaracteristique(Composermateriel, possedercaracteristique);
}

// Json

function AddMateriel(materiel) {
    var lien = $("#linkmateriel").val();
    $.ajax({
        url: "Materiel/Add",
        type: 'POST',
        async: false,
        data: { 'materiel': materiel },
        success: function (result) {
            if (result !== null) {
                materiel = result;
            }
        },
        complete: function () {
        }
    });
}

function AddCaracteristique(composantcaracteristique) {
    var lien = $("#linkcaracteristique").val();
    $.ajax({
        url: "Materiel/AddCaracteristique",
        type: 'POST',
        async: false,
        data: { 'composantcaracteristique': composantcaracteristique },
        success: function (result) {
            if (result !== null) {
                composantcaracteristique = result; 
            }
        },
        complete: function () {
        }
    });
    return composantcaracteristique;
}

function EditMateriel(materiel) {
    var lien = $("#linkmateriel").val();
    $.ajax({
        url: "Materiel/Edit",
        type: 'POST',
        async: false,
        data: { 'materiel': materiel },
        success: function (result) {
            if (result !== null) {

                materiel = result;
            }
        },
        complete: function () {

        }
    });
}

 function GetMateriel(materielid) {
        var materiel = {};
        var lien = $("#linkmateriel").val();
        $.ajax({
            url: "Materiel/GetMateriel",
            type: 'POST',
            async: false,
            data: { 'materielid': materielid },
            success: function (result) {
                if (result !== null) {

                    materiel = result;
                }
            },
            complete: function () {

            }
        });

        return materiel;

}

function GetMaterielDomaine(domaineid,materielid) {
        var lien = $("#linkmateriel").val();
        $.ajax({
            url: "Materiel/GetMaterielDomaine",
            type: 'POST',
            async: false,
            data: { 'domaineid': domaineid, 'materielid': materielid },
            success: function (result) {
                if (result !== null) {}
            },
            complete: function () {}
        });
}

function GetComposant(materielid, composantid, Dateinsertion){
    var composant = {};
    var lien = $("#linkcaracteristique").val();
        $.ajax({
            url: "Materiel/GetComposeMateriel",
            type: 'POST',
            async: false,
            data: { 'materielid': materielid,'composantid': composantid,'Dateinsertion': Dateinsertion },
            success: function (result) {
                //result = JSON.parse(result);
                if (result !== null) {
                    composant = result;
                }
            },
            complete: function () { }
        });
    return composant;
 }
    


/***Recupération de la grille caracteristique **/

function GetCaracteristiques() {

    var indices = gridCaracteristique.batchEditApi.GetRowVisibleIndices();
    var caracteristique = [];
    if (indices !== null) {
        if (indices.length === 0) {
            return JSON.stringify(caracteristique);
        } else {
            for (var i = 0; i < indices.length; i++) {
                var currentcaracteristique = {
                    CaracteristiqueComposantId: gridCaracteristique.batchEditApi.GetCellValue(indices[i], 'CaracteristiqueComposantId', false),
                    Valeur: gridCaracteristique.batchEditApi.GetCellValue(indices[i], 'Valeur', false),
                    UniteId: gridCaracteristique.batchEditApi.GetCellValue(indices[i], 'UniteId', false),
                };
                if (currentcaracteristique.CaracteristiqueComposantId !== null && currentcaracteristique.UniteId !== null ) {
                    caracteristique.push(currentcaracteristique);
                }
            }
            return JSON.stringify(caracteristique);
        }
    }
    return JSON.stringify(caracteristique);
}


       

    
    