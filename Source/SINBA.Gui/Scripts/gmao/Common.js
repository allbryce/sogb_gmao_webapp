var GET = "GET", POST = "POST";
var loadingPanelText;

function GetVisibleIndex(event) {
    var target = event.target;
    while (target && (target.nodeName !== 'TR' || target.attributes.length <= 0 || target.attributes['id'] === null || !target.attributes['id'].value.includes('DXDataRow'))) {
        target = target.parentElement;
    }
    var id = target.attributes['id'].value;
    var index = id.split("DataRow")[1];
    if (index !== null && index.length > 0 && !isNaN(index)) {
        index = parseInt(index);
    }
    return index;
}
function SetMaxLength(memo, maxLength) {
    if (!memo)
        return;
    if (typeof (maxLength) !== "undefined" && maxLength >= 0) {
        memo.maxLength = maxLength;
        memo.maxLengthTimerToken = window.setInterval(function () {
            var text = memo.GetText();
            if (text && text.length > memo.maxLength)
                memo.SetText(text.substr(0, memo.maxLength));
        }, 10);
    } else if (memo.maxLengthTimerToken) {
        window.clearInterval(memo.maxLengthTimerToken);
        delete memo.maxLengthTimerToken;
        delete memo.maxLength;
    }
}
function OnMaxLengthChanged(s, e) {
    var maxLength = 300;
    SetMaxLength(Remarque, maxLength);
}
function FnValiderChampsObligatoires(champs) {
    var issue = true;
    try {
        for (var champ in champs) {
            var control = window[champ];
            if (control instanceof ASPxClientComboBox) {

                if (control.GetValue() === null) {
                    issue = false;
                    $("#" + champ).addClass('requiredField');
                }
                else {
                    $("#" + champ).removeClass('requiredField');
                }

            }

        }
    }
    catch(ex){
        issue = false;
    }
    return issue;
}
function AjaxRequest(url, type, data, success) {
    $.ajax({
        url: url,
        type: type,
        data: data,
        success: success
    });
}
function onFormSubmit() {
    if ($("#frm").valid()) {
        saveBtn.SetEnabled(false);
        setTimeout(function () { saveBtn.SetEnabled(true); }, 3000);
    }
}
function escapeSpecialCharacters(text) {
    var element = document.createElement('span');
    element.innerHTML = text;
    return element.innerHTML;
}
function isNullOrWhiteSpace(str) {
    return str === null || str.match(/^ *$/) !== null;
}
function showLoadingPanel(text) {
    loadingPanelText = LoadingPanel.GetText();
    if (text !== null && text.length > 0) {
        LoadingPanel.SetText(text);
    }
    LoadingPanel.Show();
}
function hideLoadingPanel() {
    LoadingPanel.Hide();
    LoadingPanel.SetText(loadingPanelText);
}
function deleteGridRow(key, gridName, deleteUrl) {
    if (confirm(escapeSpecialCharacters(deleteConfirmMsg))) {
        AjaxRequest(deleteUrl, POST, { id: key },
            function () {
                var grid = ASPxClientControl.GetControlCollection().GetByName(gridName);
                grid.Refresh();
            });
    }
}
function InitGridViewByName(gridname) {
    let grid = ASPxClientControl.GetControlCollection().GetByName(gridname)
    let indices = grid.batchEditApi.GetRowVisibleIndices();
    for (let index = 0; index < indices.length; index++) {
        grid.DeleteRow(index);
    }
}
function InitGridView(grid) {
    let indices = grid.batchEditApi.GetRowVisibleIndices();
    for (let index = 0; index < indices.length; index++) {
        grid.DeleteRow(index);
    }
}


