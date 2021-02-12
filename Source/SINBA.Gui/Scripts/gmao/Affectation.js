var currentId;

function openAffectationModal(item) {
    currentId = item.LocalisationId;
    AffectationModal.Show();
    gridAffecterMateriel.PerformCallback();
}

function AffecterMaterielBeginCallback(s, e) {
    e.customArgs['LocalisationId'] = currentId;
}