function funcNext() {
    /// <summary>View next month</summary>
    $('#Next').click(function () {
        window.thisYear = $('#Year').text();
        window.thisMonth = $('#MonthNumber').text();
        $.post("/Purse/NextMonth", { currentMonth: window.thisMonth, currentYear: window.thisYear }, function (data) {
            funcCreateMontTable(data);
        });
    });
}

function funcPrev() {
    /// <summary>View prev month</summary>
    $('#Prev').click(function () {
        window.thisYear = $('#Year').text();
        window.thisMonth = $('#MonthNumber').text();
        $.post("/Purse/PrevMonth", { currentMonth: window.thisMonth, currentYear: window.thisYear }, function (data) {
            funcCreateMontTable(data);
        });
    });
}
 
function funcCreateMontTable(data) {
    /// <summary>Create html code for month table</summary>
    /// <param name="data" type="Object">Table data</param>
    $('#tablePurse').remove();
    $('#Year').text(data.ThisYear);
    $('#MonthNumber').text(data.ThisMonth);
    $('#MonthName').text(data.Name);
    $('#main').prepend(function () {
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
    funcSetScripts();
    funcSetDiagram();
    funcSetAutocomplete();
}
