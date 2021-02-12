var isAddMode = true;
var currentMaterielID;
var gridname;
var isError;
var canClose = true;
var currentDomaineID

 //Open Add Modal 
function openMaterielAddModal() {
    isAddMode = true;
    InitMaterielModal();
    //MaterielId.Hide();
    MaterielModal.Show();
}
function openCaracteristiquesAddModal( item ) {

    currentMaterielID = item.MaterielId;
    isAddMode = true;
    gridname = "gridComposant_" + currentMaterielID;
    InitComposantModal();
    CaracteristiquesModal.Show();
  
}
function openAffectationModal(item) {
    currentMaterielID = item;
    //ChargerModal();
    AffectationModal.show();
}
function openAssocieModal(item) {

    currentMaterielID = item.MaterielId;
    currentDomaineID = item.DomaineId;
    AssocieModal.Show();
    gridAssociation.PerformCallback();
}

function AssocierMaterielBeginCallback(s, e) {
    e.customArgs['MaterielId'] = currentMaterielID;
    e.customArgs['DomaineId'] = currentDomaineID;
}
//Open Edit Modal
function openCaracteristiquesEditModal(item) {

    isAddMode = false;
    var caracteristique = GetComposant(item.MaterielId, item.ComposantId, item.DateInsertion);
    setComposant(caracteristique);
    CaracteristiquesModal.Show();
    currentMaterielID = item.MaterielId;
    gridname = "gridComposant_" + currentMaterielID;
}
function openMaterielEditModal(materielid) {
    isAddMode = false;
    currentMaterielID = materielid;
    currentMateriel = GetMateriel(materielid);
    SetMateriel(currentMateriel);
    MaterielModal.Show();
}
function RecupererMateriel() {
    var materiel = {}
    if (DateAcquisition.GetValue.toUTCString < DateMiseEnService.GetValue.toUTCString) {
        window.alert("La mise en service ne peut pas se faire avant l'acquisition");
    }
    else {
        materiel.MaterielId = currentMaterielID;
        materiel.NumeroSerie = NumeroSerie.GetValue();
        materiel.LibelleMateriel = LibelleMateriel.GetValue();
        materiel.DomaineId = DomaineId.GetValue();
        materiel.FournisseurId = FournisseurId.GetValue();
        materiel.NumeroModel = NumeroModel.GetValue();
        materiel.SousFamilleId = SousFamilleId.GetValue();
        materiel.NumeroBonCommande = NumeroBonCommande.GetValue();
        materiel.ClasseMaterielId = ClasseMaterielId.GetValue();
        materiel.TypeMaterielId = TypeMaterielId.GetValue();
        materiel.Note = Note.GetValue();
        materiel.Immobilise = Immobilise.GetValue();
        materiel.NumeroImmobilisation = NumeroImmobilisation.GetValue();
        if (Garantie.GetValue() !== null) {
            materiel.Garantie = Garantie.GetValue().toUTCString();
        }
        if (DateAcquisition.GetValue() !== null) {
            materiel.DateAcquisition = DateAcquisition.GetValue().toUTCString();
        }
        if (DateMiseEnService.GetValue() !== null) {
            materiel.DateMiseEnService = DateMiseEnService.GetValue().toUTCString();
        }
        if (DateSortie.GetValue() !== null) {
            materiel.DateSortie = DateSortie.GetValue().toUTCString();
        }     
        materiel.GroupeInventaireId = GroupeInventaireId.GetValue();       
        materiel.PrixAchat = PrixAchat.GetValue();
        materiel.Actif = Actif.GetValue();
    }
   
    return materiel;
}
//Save
function SaveCaracteristiques(s,e) {
    var test = CarateristiqueAdd();  
    //else {
    //    caracteristiqueEdit();
    //}    
    if (canClose) {
        CaracteristiquesModal.Hide();
    } 
    if (!test) {
        gridname.PerformCallback;
    }
}

function SaveMateriel() {
    
    if (isAddMode === true) {
        AddMateriel(RecupererMateriel()); 
    }
    else {
        EditMateriel(RecupererMateriel());
    }
    gridMateriel.PerformCallback();
    if (canClose)
    {
        MaterielModal.Hide();
    }
}
function SaveAssocierMateriel() {

    ComitAssocieMateriel(GetAssocieMateriel());
    AssocieModal.Hide();
}
function GetAssocieMateriel() {
    
    var indices = gridAssociation.batchEditApi.GetRowVisibleIndices();
    var Association = [];
    if (indices !== null) {
        if (indices.length === 0) {
            return JSON.stringify(caracteristique);
        } else {
            for (var i = 0; i < indices.length; i++) {
                var currentAssociation = {
                    MaterielId: currentMaterielID,
                    MaterielAssocieId: gridAssociation.batchEditApi.GetCellValue(indices[i], 'MaterielAssocieId', false),
                    DateInstallation: gridAssociation.batchEditApi.GetCellValue(indices[i], 'DateInstallation', false),
                    DateRetrait: gridAssociation.batchEditApi.GetCellValue(indices[i], 'DateRetrait', false),
                };
                if (currentAssociation.MaterielId !== null && currentAssociation.MaterielAssocieId !== null && currentAssociation.DateInstallation != null) {
                    Association.push(currentAssociation);
                }
            }
            return JSON.stringify(Association);
        }
    }
    return JSON.stringify(Association);
    
}

function InitMaterielModal() {
    NumeroSerie.SetValue(null);
    LibelleMateriel.SetValue(null);
    DomaineId.SetValue(null);
    FournisseurId.SetValue(null);
    NumeroModel.SetValue(null);
    SousFamilleId.SetValue(null);
    NumeroBonCommande.SetValue(null);
    ClasseMaterielId.SetValue(null);
    TypeMaterielId.SetValue(null);
    Garantie.SetValue(null);
    Actif.SetValue(null);
    Note.SetValue(null);
    DateAcquisition.SetValue(null);
    DateMiseEnService.SetValue(null);
    PrixAchat.SetValue(null);
    Immobilise.SetValue(null);
    DateSortie.SetValue(null);
    NumeroImmobilisation.SetValue(null);
    GroupeInventaireId.SetValue(null);
}

//
//Setting
function SetMateriel(materiel) {
    InitMaterielModal();
    //MaterielId.SetValue(currentMaterielID);
    NumeroSerie.SetValue(materiel.NumeroSerie);
    LibelleMateriel.SetValue(materiel.LibelleMateriel);
    DomaineId.SetValue(materiel.DomaineId);
    FournisseurId.SetValue(materiel.FournisseurId);
    NumeroModel.SetValue(materiel.NumeroModel);
    SousFamilleId.SetValue(materiel.SousFamilleId);
    NumeroBonCommande.SetValue(materiel.NumeroBonCommande);
    ClasseMaterielId.SetValue(materiel.ClasseMaterielId);
    TypeMaterielId.SetValue(materiel.TypeMaterielId);
    NumeroImmobilisation.SetValue(materiel.NumeroImmobilisation);
    Immobilise.SetValue(materiel.Immobilise);
    PrixAchat.SetValue(materiel.PrixAchat);
    Note.SetValue(materiel.Note);
    Actif.SetValue(materiel.Actif);
    GroupeInventaireId.SetValue(materiel.GroupeInventaireId);
    if (materiel.Garantie != null) {
    Garantie.SetValue(new Date(parseInt(materiel.Garantie.replace('/Date(', ''))));
    }
    if (materiel.DateMiseEnService != null) {
        DateMiseEnService.SetValue(new Date(parseInt(materiel.DateMiseEnService.replace('/Date(', ''))));
    }
    if (materiel.DateAcquisition != null) {
       DateAcquisition.SetValue(new Date(parseInt(materiel.DateAcquisition.replace('/Date(', ''))));
    }
    if (materiel.DateSortie != null) {
        DateSortie.SetValue(new Date(parseInt(materiel.DateSortie.replace('/Date(', ''))));
    }
}

function setComposant(caracteristique) {

    InitComposantModal();
    ComposantId.SetValue(caracteristique.ComposantId);
    Quantite.SetValue(caracteristique.Quantite);
    if (caracteristique.DateInsertion != null) {
        //let date = new Date(caracteristique.DateInsertion);
        var date = new Date(parseInt(caracteristique.DateInsertion.replace('/Date(', '')));
        DateInsertion.SetValue(date);
    }
    //Plafond.SetValue(caracteristique.Plafond);
    var items = caracteristique.Caracteristiques;
    for (var lib = 0; lib < items.length; lib++) {
        gridCaracteristique.AddNewRow();
    }
    var indices = gridCaracteristique.batchEditApi.GetRowVisibleIndices();
    for (var i = 0; i < indices.length; i++) {
        gridCaracteristique.batchEditApi.SetCellValue(indices[i], 'CaracteristiqueComposantId', items[i].CaracteristiqueComposantId);
        gridCaracteristique.batchEditApi.SetCellValue(indices[i], 'Valeur', items[i].Valeur);
        gridCaracteristique.batchEditApi.SetCellValue(indices[i], 'UniteId', items[i].UniteId);
    }
}

// Dé
function SetGridView(item) {
    let index = gridAssociation.batchEditApi.GetRowVisibleIndices();
    for (let i = 0; i < index.length+1; i++) {
        gridAssociation.DeleteRow(-i);
    }    
    //
    var indice = gridAssociation.batchEditApi.GetRowVisibleIndices();
    if (item.length != indice.length) {
    for (let lib = 0; lib < item.length; lib++) {
        gridAssociation.AddNewRow();
        }
    }
    var indices = gridAssociation.batchEditApi.GetRowVisibleIndices();
    // let date = new Date(item[i].DateInstallation);
    for (let i = 0; i < item.length; i++){
        
            if (item[i].DateInstallation != null) {
                let date = new Date(parseInt(item[i].DateInstallation.replace('/Date(', '')));          
                //let date = datestring.getDate() + "/" + datestring.getMonth() + "/" + datestring.getFullYear();                
                gridAssociation.batchEditApi.SetCellValue(indices[i],'DateInstallation',date);
            }
            gridAssociation.batchEditApi.SetCellValue(indices[i], 'MaterielId', currentMaterielID);
            gridAssociation.batchEditApi.SetCellValue(indices[i], 'MaterielAssocieId', item[i].MaterielAssocieId);
            gridAssociation.batchEditApi.SetCellValue(indices[i], 'LibelleMaterielAssocie', item[i].LibelleMaterielAssocie);
        if (item[i].DateRetrait != null) {
            let datestring = new Date(parseInt(item[i].DateRetrait.replace('/Date(', '')));
            let date = datestring.getDate() + "/" + datestring.getMonth() + "/" + datestring.getFullYear();
            gridAssociation.batchEditApi.SetCellValue(indices[i], 'DateRetrait', date);
        }
            //gridAssociation.batchEditApi.SetCellValue(indices[i], 'DateRetrait', date);
    }
}
function InitComposantModal() {
    ComposantId.SetValue(null);
    Quantite.SetValue(null);
    DateInsertion.SetValue(null);
    var index = gridCaracteristique.batchEditApi.GetRowVisibleIndices();
    for (let i = 0; i < index.length + 1; i++) {
        gridCaracteristique.DeleteRow(-i);
    }
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
                if (currentcaracteristique.CaracteristiqueComposantId !== null && currentcaracteristique.UniteId !== null && ComposantId != null && DateInsertion != null) {
                    caracteristique.push(currentcaracteristique);
                }
            }
            return JSON.stringify(caracteristique);
        }
    }
    return JSON.stringify(caracteristique);
}
//Edit 
function CarateristiqueAdd() {

    var Composantcaracteristique =
        {
            MaterielId: currentMaterielID,
            ComposantId: ComposantId.GetValue(),
            Quantite: Quantite.GetValue(),
        };
     if(DateInsertion.GetValue() !== null) {
         Composantcaracteristique.DateInsertion= DateInsertion.GetValue().toUTCString()
            }
    Composantcaracteristique.CaracteristiqueComposantString = GetCaracteristiques();
    if (isAddMode) {
        isError = AddCaracteristique(Composantcaracteristique);
    }
    else {
        isError = CaracteristiqueEdit(Composantcaracteristique);
    }
    InitComposantModal();
    if (isError) {

        window.alert("Une erreur s'est produite, veuillez vérifier votre saisie");
        canClose = false;
    }
    else {
        
    }   
}

// Json
function AddMateriel(materiel){
    var lien = $("#linkmateriel").val();
    $.ajax({
        url: "Materiel/Add",
        type: 'POST',
        async: false,
        data: {'materiel': materiel},
        success: function (result) {
            if (result !== null) {
                materiel = result;
            }
        },
        complete: function () {
        }
    });
}
function ChargerModal(){
    var lien = $("#linkaffectation").val();
    $.ajax({
        url: "Materiel/LoadAffectation",
        type: 'POST',
        async: false,
        data: {},
        success: function (result) {           
        },
        complete: function () {
        }
    });
}

function ComitAssocieMateriel(associemateriel) {
    var lien = $("#linkmateriel").val();
    $.ajax({
        url: "Materiel/AddAssocieMateriel",
        type: 'POST',
        async: false,
        data: { 'associemateriel': associemateriel},
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

function CaracteristiqueEdit(composantcaracteristique) {
    var lien = $("#linkcaracteristique").val();
    $.ajax({
        url: "Materiel/EditCaracteristique",
        type: 'POST',
        async: false,
        data:  composantcaracteristique ,
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

function GetComposant(materielid, composantid, Dateinsertion){
    var composant = {};
    var lien = $("#linkcaracteristique").val();
        $.ajax({
            url: "Materiel/GetComposeMateriel",
            type: 'POST',
            async: false,
            data: { 'materielid': materielid,'composantid': composantid,'Dateinsertion': Dateinsertion },
            success: function (result) {
                //result = JSON.parse(result)
                if (result !== null) {
                    composant = result;
                }
            },
            complete: function () { }
        });
    return composant;
 }
    




       

    
    