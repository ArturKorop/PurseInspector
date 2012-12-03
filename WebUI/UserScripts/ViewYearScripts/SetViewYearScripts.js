function funcGoToMonth(year,month) {
    /// <summary>Return a new view, were we can see a month data</summary>
    window.location.href = '/Purse/GetMonth?&currentMonth=' + month + '&currentYear=' + year;
}