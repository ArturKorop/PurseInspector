function funcGoToMonth(year,month) {
    /// <summary>Return a new view, were we can see a month data</summary>
    window.location.href = '/Purse/GetMonth?&currentMonth=' + month + '&currentYear=' + year;
}

function funcSetButtonClickMonthTransport() {
    var dataMonth = [['monthJanuary', 1], ['monthFebruary', 2], ['monthMarch', 3], ['monthApril', 4], ['monthMay', 5], ['monthJune', 6],
        ['monthJuly', 7], ['monthAugust', 8], ['monthSeptember', 9], ['monthOctober', 10], ['monthNovember', 11], ['monthDecember', 12]];
    $('.ViewMonth').click(function () {
        var monthName = $(this).prop('id');
        var monthNumber;
        $(dataMonth).each(function() {
            if (this[0] == monthName)
                monthNumber = this[1];
        });
        funcGoToMonth($('#YearName').text(), monthNumber);
    });
}