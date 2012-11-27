function funcSetDiagram() {
    $.post("/Purse/SpanStatistics", { currentMonth: thisMonth, currentYear: thisYear }, function (data) {
        funcDiagram_jqPlot(data);
    });
}
function funcDiagram_jqPlot(dataStat) {
    var dataTemp = [];
    var val = 0;
    $.each(dataStat, function () {
        dataTemp[val] = [this.OperationName, this.Value];
        val++;
    });
    if (dataTemp.length == 0)
        dataTemp[0] = ["Empty",0];
    $.jqplot('DiagramMonth', [dataTemp],
                {
                    seriesDefaults: {
                        renderer: jQuery.jqplot.PieRenderer,
                        rendererOptions: {
                            showDataLabels: true,
                            dataLabels: 'percent' | 'value'
                            //   dataLabelFormatString: '%d%%'
                        }
                    },
                    legend: { show: true, location: 'e' }
                }
            );
}
function funcDiagram_Sparkline() {
    var myvalues = [10, 8, 5, 7, 4, 4, 1];
    $('.inlinesparkline').sparkline(myvalues, {
        type: 'pie',
        width: '200px',
        height: '200px',
        sliceColors: ['#5d3092', '#4dc9ec', '#9de49d', '#9074b1', '#66aa00', '#dd4477', '#0099c6', '#990099'],
        borderWidth: 7,
        borderColor: '#f5f5f5',
        tooltipFormat: '<span style="color: {{color}}">&#9679;</span> {{offset:names}} ({{percent.1}}%)',
        tooltipValueLookups: {
            names: {
                0: 'Automotive',
                1: 'Locomotive',
                2: 'Unmotivated',
                3: 'Three',
                4: 'Four',
                5: 'Five'
                // Add more here
            }
        }
    });
}