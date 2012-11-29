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