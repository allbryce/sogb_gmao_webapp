var load = true;

function IdSiteGotFocus() { FillUserSites(); }
function IdCultureGotFocus() { FillUserSites(); }
function UserNameValueChanged() { ClearLists(); }
function UserNameLostFocus() { FillUserSites(); }
function PasswordValueChanged() { ClearLists(); }
function PasswordLostFocus() { FillUserSites(); }
function closeMessage() {  }

function FillUserSites() {
    if (load) {
        var username = UserName.GetValue();
        var password = Password.GetValue();

        //if (username === null || username.length === 0 || password === null || password.length < 6 || IdSite.GetItemCount() > 0) return;
        if (username === null || username.length === 0 || password === null || password.length < 6 ) return;

        //IdSite.ClearItems();
        //IdCulture.ClearItems();
        load = false;

        LoadingPanel.Show();

        $.ajax({
            url: $("#GetUserSites").val(),
            type: 'POST',
            async: true,
            data: { 'username': username, 'password': password },
            success: function (result) {
                if (result !== null) {

                    if (result.HasError !== "undefined" && result.Data !== "undefined") {
                        if (result.HasError === false) {

                            var arr = $.map(result.Data, function (element) { return element; });

                            // Load IdSite
                            for (var i = 0; i < arr.length; i++) {
                                //IdSite.AddItem(arr[i].Libelle || arr[i].Id, arr[i].Id);
                            }
                            // if (arr.length === 1) { IdSite.SetSelectedIndex(0); }

                        } else {
                            //
                            //$('#msgAddRequiredFields_message').html(result.Message);
                            //$("#msgAddRequiredFields").removeClass("hidden").fadeTo(3000, 2000).slideUp(2000, function () { $("#msgAddRequiredFields").slideUp(2000); });
                        }
                    }
                }
            },
            complete: function () {
                LoadingPanel.Hide();
            } 
        });
    }
}

function ClearLists() {
    //IdSite.ClearItems();
    //IdSite.SetValue("");
    load = true;
}

function SelectAndClosePopup(returnValue) {
    pcModalMode.Hide();

    var url = '@ViewBag.ReturnUrl';
    var form = $('#loginForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    var m = { Username: UserName.GetValue(), Password: Password.GetValue(), RememberMe: null, SiteSelected: null, SiteId: returnValue };

    $.ajax({
        type: 'POST',
        url: '@Url.Action(SinbaConstants.Actions.Login, SinbaConstants.Controllers.Account)',
        data: { __RequestVerificationToken: token, model: m, returnUrl: url },
        success: function () {
            if (url.length === 0) {
                url = "/";
            }
            window.open(url, "_self");
        }
    });
}

