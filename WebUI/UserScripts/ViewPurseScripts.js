function SetScripts() {
    funcAddNewOperation();
    funcDeleteOperation();
}

function funcAddNewOperation() {
    $('.NewOperationName').focusout({ type: "name" }, funcFocusout);
    $('.NewOperationValue').focusout({ type: "value" }, funcFocusout);
    $('.NewOperationName').keypress(function (e) {
        if (e.which == 13) {
            funcOperationAdd("name", $(this));
        }
    });
    $('.NewOperationValue').keypress(function (e) {
        if (e.which == 13) {
            funcOperationAdd("value", $(this));
        }
    });
}

function funcFocusout(eventObject) {
    funcOperationAdd(eventObject.data.type, $(this));
}

function funcOperationAdd(type, object) {
    var item = object;
    var operationName;
    var operationValue;
    if (type == "name") {
        operationName = item.prop('value');
        operationValue = item.parent().next().children('.NewOperationValue').prop('value');
    }
    else {
        operationValue = item.prop('value');
        operationName = item.parent().prev().children('.NewOperationName').prop('value');
    }
    var day = item.parent().parent().prev().children().first().text();
    if (operationName != '' && operationValue != '') {
        $.post("/Purse/AddOperation", { year: 2012, month: 1, day: day, operationName: operationName, operationValue: operationValue, operationType: 'span' }, function (data) {
            funcClear(item, type);
            var dayRow = item.parent().parent().prevAll('.Day').first();
            var newRowAddSpan = dayRow.children().first().prop('rowspan') + 1;
            dayRow.children().each(function () {
                if ($(this).prop('id') != 'SpanDaysSingleOperations') {
                    $(this).prop('rowspan', newRowAddSpan);
                }
            });
            dayRow.nextAll('.Day').first().before('<tr class="Operation" id="' + data + '">' +
                        '<td>' +
                        '<input id="OperationName" class="Operation" type="text" value="' + operationName + '" name="OperationName">' +
                        '</td>' +
                        '<td>' +
                        '<input id="OperationValue" class="Operation" type="text" value="' + operationValue + '" name="OperationValue">' +
                        '</td>' +
                        '<td class="ButtonDelete">X</td>' +
                        '</tr>');
            var newSum = parseInt(dayRow.children('.Sum').text()) + parseInt(operationValue);
            dayRow.children('.Sum').text(newSum);
        });
    }
}

function funcClear(item, type) {
    if (type == "name") {
        item.parent().next().children('.NewOperationValue').prop('value', '');
    }
    else {
        item.parent().prev().children('.NewOperationName').prop('value', '');
    }
    item.prop('value', '');
}

function funcDeleteOperation() {
    $('.ButtonDelete').live('click', function () {
        var operationId = $(this).parent().prop('id');
        var currentRow = $(this).parent();
        var operationValue = currentRow.children().next().children().prop('value');
        $.post("/Purse/DeleteOperation", { id: operationId }, function () {
            var dayRow = currentRow.prevAll('.Day').first();
            var newRowSpan = dayRow.children().first().prop('rowspan') - 1;
            dayRow.children().each(function () {
                if ($(this).prop('id') != 'SpanDaysSingleOperations') {
                    $(this).prop('rowspan', newRowSpan);
                }
            });
            currentRow.remove();
            var newSum = parseInt(dayRow.children('.Sum').text()) - parseInt(operationValue);
            dayRow.children('.Sum').text(newSum);
        });
    });
}