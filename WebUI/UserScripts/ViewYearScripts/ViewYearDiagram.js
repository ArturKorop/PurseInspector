function funcSetViewYearDiagram(year) {
    /// <summary>Create post request to PurseController and, if sucessfull, call function for diagram paint</summary>
    $.post("/Purse/YearSpanStatistics", { currentYear: year }, function (data) {
        funcDiagram_Flot(data, 'DiagramYear', 'DiagramYearLabel');
    });
}