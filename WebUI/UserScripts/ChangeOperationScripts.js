function funcChangeOperation() {
    /// <summary>Change existing operation in repository</summary>
    $('.OperationName').focusin(SetDefaultText);
    $('.OperationValue').focusin(SetDefaultText);
    $('.OperationName').focusout({ type: "name" }, funcChangeFocusout);
    $('.OperationValue').focusout({ type: "value" }, funcChangeFocusout);
    $('.OperationName').keypress(function (e) {
        if (e.which == 13) {
            funcChangeOperationWork("name", $(this));
        }
    });
    $('.OperationValue').keypress(function (e) {
        if (e.which == 13) {
            funcChangeOperationWork("value", $(this));
        }
    });
}

function funcChangeFocusout(eventObject) {
    funcChangeOperationWork(eventObject.data.type, $(this));
}

var temp = "";
function funcChangeOperationWork(type, object) {
    var newName;
    var newValue;
    var current = object.prop('value');
    var currentRow = object.parent().parent();
    if (current != temp) {
        var id = object.parent().parent().prop('id');
        if (type == "name") {
            newName = current;
            newValue = object.parent().next().children('.OperationValue').prop('value');
        } else {
            newValue = current;
            newName = object.parent().prev().children('.OperationName').prop('value');
        }
        $.post("/Purse/ChangeOperation", { id: id, newName: newName, newValue: newValue }, function () {
            var dayRow = currentRow.prevAll('.Day').first();
            if (type != "name") {
                $('#MonthSumSpan').text(parseInt($('#MonthSumSpan').text()) - parseInt(temp) + parseInt(newValue));
                dayRow.children('.Sum').text(dayRow.children('.Sum').text() - parseInt(temp) + parseInt(newValue));
                temp = current;
            }
            funcSetDiagram();
        });
    }
}

function SetDefaultText() {
    temp = $(this).prop('value');
}
