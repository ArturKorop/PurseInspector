var thisYear;
var thisMonth;

function SetScripts() {
    funcAddNewOperation();
    funcDeleteOperation();
    funcChangeOperation();
    funcSetCurrentDate();
    funcLiveSHOperation();
}

function funcSetCurrentDate() {
    thisYear = $('#Year').text();
    thisMonth = $('#MonthNumber').text();
}
// Add operation
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
        $.post("/Purse/AddOperation", { year: thisYear, month: thisMonth, day: day, operationName: operationName, operationValue: operationValue, operationType: 'span' }, function (data) {
            funcClear(item, type);
            var dayRow = item.parent().parent().prevAll('.Day').first();
            var newRowAddSpan = dayRow.children().first().prop('rowspan') + 1;
            dayRow.children().each(function () {
                if ($(this).prop('id') != 'SpanDaysSingleOperations') {
                    $(this).prop('rowspan', newRowAddSpan);
                    $(this).prop('rowspan2', $(this).prop('rowspan2') + 1);
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
            $('#MonthSumSpan').text(parseInt($('#MonthSumSpan').text()) + parseInt(operationValue));
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
// /Add operation

// Delete operation
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
            $('#MonthSumSpan').text(parseInt($('#MonthSumSpan').text()) - parseInt(operationValue));
        });
    });
}
// /Delete operation

// Change operation
function funcChangeOperation() {

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
        });
    }
}

function SetDefaultText() {
    temp = $(this).prop('value');
}
// /Change operation

// NextMonth
function funcNext() {
    $('#Next').click(function () {
        thisYear = $('#Year').text();
        thisMonth = $('#MonthNumber').text();
        $.post("/Purse/NextMonth", { currentMonth: thisMonth, currentYear: thisYear }, function (data) {
            $('#tablePurse').remove();
            $('#Year').text(data.ThisYear);
            $('#MonthNumber').text(data.ThisMonth);
            $('#MonthName').text(data.Name);
            $('#MainDir').after(function () {
                var table = '';
                var tr = '';
                $.each(data.Days, function (k, val) {
                    var rowspan = val.SpanDaysSingleOperations.length + 2;
                    tr = '<tr class="Day">' +
                            '<td rowspan="' + rowspan + '">' + val.Number + '</td>' +
                            '<td rowspan="' + rowspan + '">' + val.Name + '</td>' +
                            '<td colspan="3" id="SpanDaysSingleOperations"></td>' +
                            '<td rowspan="' + rowspan + '" class="Sum">' + val.SumSpan + '</td>' +
                            '</tr>';
                    var trNewOperation = '<tr class="NewOperation">' +
                        '<td>' +
                        '<input id="NewOperationName" class="NewOperationName" type="text" value="" name="OperationName">' +
                        '</td>' +
                        '<td>' +
                        '<input id="NewOperationValue" class="NewOperationValue" type="text" value="" name="OperationValue">' +
                        '</td>' +
                        '</tr>';
                    tr = tr + trNewOperation;
                    var trOperation = '';
                    $.each(val.SpanDaysSingleOperations, function (l, item) {
                        trOperation = trOperation +
                                '<tr class="Operation" id="' + item.Id + '">' +
                                '<td>' +
                                '<input id="OperationName" class="OperationName" type="text" value="' + item.OperationName + '" name="OperationName">' +
                                '</td>' +
                                '<td>' +
                                '<input id="OperationValue" class="OperationValue" type="text" value="' + item.Value + '" name="OperationValue">' +
                                '</td>' +
                                '<td class="ButtonDelete">X</td>' +
                                '</tr>';
                    });
                    tr = tr + trOperation;
                    table = table + tr;
                });
                var trAllSumSpan = '<tr>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td id="MonthSumSpan">' + data.MonthSumSpan + '</td></tr>';
                table = table + trAllSumSpan;
                return '<table id="tablePurse">' +
                        table +
                        '</table>';
            });
            SetScripts();
        });
    });
}
// \NextMonth

// PrevMonth
function funcPrev() {
    $('#Prev').click(function () {
        thisYear = $('#Year').text();
        thisMonth = $('#MonthNumber').text();
        $.post("/Purse/PrevMonth", { currentMonth: thisMonth, currentYear: thisYear }, function (data) {
            $('#tablePurse').remove();
            $('#Year').text(data.ThisYear);
            $('#MonthNumber').text(data.ThisMonth);
            $('#MonthName').text(data.Name);
            $('#MainDir').after(function () {
                var table = '';
                var tr = '';
                $.each(data.Days, function (k, val) {
                    var rowspan = val.SpanDaysSingleOperations.length + 2;
                    tr = '<tr class="Day">' +
                            '<td rowspan="' + rowspan + '">' + val.Number + '</td>' +
                            '<td rowspan="' + rowspan + '">' + val.Name + '</td>' +
                            '<td colspan="3" id="SpanDaysSingleOperations"></td>' +
                            '<td rowspan="' + rowspan + '" class="Sum">' + val.SumSpan + '</td>' +
                            '</tr>';
                    var trNewOperation = '<tr class="NewOperation">' +
                        '<td>' +
                        '<input id="NewOperationName" class="NewOperationName" type="text" value="" name="OperationName">' +
                        '</td>' +
                        '<td>' +
                        '<input id="NewOperationValue" class="NewOperationValue" type="text" value="" name="OperationValue">' +
                        '</td>' +
                        '</tr>';
                    tr = tr + trNewOperation;
                    var trOperation = '';
                    $.each(val.SpanDaysSingleOperations, function (l, item) {
                        trOperation = trOperation +
                                '<tr class="Operation" id="' + item.Id + '">' +
                                '<td>' +
                                '<input id="OperationName" class="OperationName" type="text" value="' + item.OperationName + '" name="OperationName">' +
                                '</td>' +
                                '<td>' +
                                '<input id="OperationValue" class="OperationValue" type="text" value="' + item.Value + '" name="OperationValue">' +
                                '</td>' +
                                '<td class="ButtonDelete">X</td>' +
                                '</tr>';
                    });
                    tr = tr + trOperation;
                    table = table + tr;
                });
                var trAllSumSpan = '<tr>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td id="MonthSumSpan">' + data.MonthSumSpan + '</td></tr>';
                table = table + trAllSumSpan;
                return '<table id="tablePurse">' +
                        table +
                        '</table>';
            });
            SetScripts();
        });
    });
}
// \PrevMonth

// Hide and show operation
function funcSHOperation() {
    $('#ButtonSet').buttonset();
    $('#ButtonCheckSHOperation').mousedown(funcAddSHOperation);
}

function funcAddSHOperation() {
    if (!$(this).prop("checked")) {
        funcHideOperation();
        $(this).prop("checked", true);
        $(this).prop('value', 'Show');
        $(this).css('color', 'green');
    } else {
        funcShowOperation();
        $(this).prop("checked", false);
        $(this).prop('value', 'Hide');
        $(this).css('color', 'red');
    }
}
function funcHideOperation() {
    $('.Operation').hide();
    $('.Day').children().each(function () {
        if ($(this).prop('id') != 'SpanDaysSingleOperations') {
            $(this).prop('rowspan2', $(this).prop('rowspan'));
            $(this).prop('rowspan', '2');
        }
    });
}
function funcShowOperation() {
    $('.Operation').show();
    $('.Day').children().each(function () {
        if ($(this).prop('id') != 'SpanDaysSingleOperations') {
            $(this).prop('rowspan', $(this).prop('rowspan2'));
        }
    });
}

function funcLiveSHOperation() {
    if ($('#ButtonCheckSHOperation').prop('checked')) {
        funcHideOperation();
    } 
}
// \\Hide and show operation