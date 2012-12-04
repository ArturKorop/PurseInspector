var thisYear;
var thisMonth;

function funcSetScripts() {
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

function funcSetDiagram() {
    /// <summary>Create post request to PurseController and, if sucessfull, call function for diagram paint</summary>
    $.post("/Purse/SpanStatistics", { currentMonth: thisMonth, currentYear: thisYear }, function (data) {
        funcDiagram_Flot(data, "DiagramMonth","DiagramMonthLabel");
    });
}