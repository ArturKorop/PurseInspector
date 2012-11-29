function funcSetDiagram() {
    /// <summary>Create post request to PurseController and, if sucessfull, call function for diagram paint</summary>
    $.post("/Purse/SpanStatistics", { currentMonth: thisMonth, currentYear: thisYear }, function (data) {
        funcDiagram_Flot(data);
    });
}

function funcDiagram_Flot(dataStat) {
    /// <summary>Create diagram for month operation. I think it is the best plugin for plooting , sparkline and jqPlot is worse</summary>
    /// <param name="dataStat" type="">Collection of SingleOperation</param>
    var dataTemp = [];
    var val = 0;
    $.each(dataStat, function() {
        dataTemp[val] = { label: this.OperationName, data: this.Value };
        val++;
    });
    if (dataTemp.length == 0)
        dataTemp[0] = ["Empty", 0];
    $.plot($("#DiagramMonth"), dataTemp,
        {
            series: {
                pie: {
                    show: true,
                    radius: 3 / 4,
                    label: {
                        show: true,
                        radius: 3 / 4,
                        formatter: function(label, series) {
                            return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%(' + series.data[0][1] + 'грн.)</div>';
                        },
                        threshold: 0.02,
                        background: {
                            opacity: 0.5,
                            color: '#000'
                        }
                    }
                }
            },
            legend: {
                show: false
            },
            grid: {
                hoverable: true
            }
        });
        $("#DiagramMonth").bind("plothover", pieHover);
}
function funcDiagram_jqPlot(dataStat) {
    /// <summary>Create diagram for month operation.</summary>
    /// <param name="dataStat" type="">Collection of SingleOperation</param>
    var dataTemp = [];
    var val = 0;
    $.each(dataStat, function() {
        dataTemp[val] = [this.OperationName, this.Value];
        val++;
    });
    if (dataTemp.length == 0)
        dataTemp[0] = ["Empty", 0];

    var total = 0;
    $(dataTemp).map(function () { total += this[1]; });
    var myLabels = $.makeArray($(dataTemp).map(function () { return this[0] + ": " + Math.round(this[1] / total * 100) + "% (" + this[1] + " грн.)"; }));

    $.jqplot('DiagramMonth', [dataTemp],
        {
            seriesDefaults: {
                renderer: jQuery.jqplot.PieRenderer,
                rendererOptions: {
                    showDataLabels: true,
                    dataLabels:myLabels,
                    dataLabelPositionFactor: 0.8,
                    dataLabelThreshold: 0
                }
            },
            legend: { show: true, location: 'ne' }
        }
    );
}

function funcDiagram_Sparkline() {
    /// <summary>Create diagram for month operation.</summary>
    /// <param name="dataStat" type="">Collection of SingleOperation</param>
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

function pieHover(event, pos, obj) {
    /// <summary>Function, that  call when mouse cursor was on part of diagram pie</summary>
    /// <param name="event" type="Object"></param>
    /// <param name="pos" type="Object"></param>
    /// <param name="obj" type="Object">Data from part of pie diagram</param>
    /// <returns type="Object"></returns>
    if (!obj)
        return;
    var percent = parseFloat(obj.series.percent).toFixed(2);
    $("#DiagramMonthLabel").html('<span style="font-weight: bold; color: ' + obj.series.color + '">' + obj.series.label + ' (' + percent + '%: ' + obj.series.data[0][1] +'грн.)</span>');
}
