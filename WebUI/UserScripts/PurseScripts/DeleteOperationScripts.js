function funcDeleteOperation() {
    /// <summary>Delete existing operation from repository</summary>
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
            funcSetDiagram();
        });
    });
}
