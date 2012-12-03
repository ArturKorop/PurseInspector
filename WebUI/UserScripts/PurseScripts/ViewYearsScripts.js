function funcViewYear(year) {
    /// <summary>Return a new view, were we can see a years data for month</summary>
    window.location.href = '/' +
        'Purse/ViewYear?currentYear=' + year;
}