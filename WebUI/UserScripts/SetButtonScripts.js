function funcSHOperation() {
    /// <summary>Set event for hide and show operation in table</summary>
    $('#ButtonSet').buttonset();
    $('#ButtonDir').buttonset();
    $('#ButtonCheckSHOperation').mousedown(funcAddSHOperation);
    $('#ButtonDiagram').mousedown(funcAddSHDiagram);
    $('#ButtonYear').mousedown(funcAddViewYear);
}
function funcAddViewYear() {
    funcViewYear(thisYear);
}
function funcAddSHOperation() {
    if (!$(this).prop("checked")) {
        funcHideOperation();
        $(this).prop("checked", true);
        $(this).prop('value', 'Платежи');
        $(this).css('color', 'green');
    } else {
        funcShowOperation();
        $(this).prop("checked", false);
        $(this).prop('value', 'Платежи');
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

function funcAddSHDiagram() {
    /// <summary>Set event for hide and show diagram</summary>
    if (!$(this).prop("checked")) {
        funcHideDiagram();
        $(this).prop("checked", true);
        $(this).prop('value', 'График');
        $(this).css('color', 'green');
    } else {
        funcShowDiagram();
        $(this).prop("checked", false);
        $(this).prop('value', 'График');
        $(this).css('color', 'red');
    }
}

function funcHideDiagram() {
    $("#DiagramMonth").hide();
}

function funcShowDiagram() {
    funcSetDiagram();
    $("#DiagramMonth").show();
}
