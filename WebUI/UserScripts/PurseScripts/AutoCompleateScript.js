var availableTags = [];

function funcSetAutocomplete() {
    $.post("/Purse/GetAutocompleteTags", null, function(data) {
        availableTags = data;
        $(function() {
            $(".NewOperationName").autocomplete({
                source: availableTags
            });
        });
    });
}
