﻿var data = [];
var chartData = [];
const weekdays = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
$(document).ready(function () {
    BindData();
    //$('#MonthId').change(function () {
    //    BindData();
    //});
    //$('#YearId').change(function () {
    //    BindData();
    //});
});
function generateChartData(data) {
    // Calculate the starting weekday index (0-6 of the first date in the given
    // array)
    debugger;
    const firstWeekday = new Date(data[0].date).getDay(),
        monthLength = data.length,
        lastElement = data[monthLength - 1].date,
        lastWeekday = new Date(lastElement).getDay(),
        lengthOfWeek = 6,
        emptyTilesFirst = firstWeekday,
        chartData = [];

    // Add the empty tiles before the first day of the month with null values to
    // take up space in the chart
    for (let emptyDay = 0; emptyDay < emptyTilesFirst; emptyDay++) {
        chartData.push({
            x: emptyDay,
            y: 5,
            value: null,
            color: null,
            name: null,
            date: null,
            custom: {
                empty: true
            }
        });
    }

    // Loop through and populate with temperature and dates from the dataset
    for (let day = 1; day <= monthLength; day++) {
        // Get date from the given data array
        const date = data[day - 1].date;
        // Offset by thenumber of empty tiles
        const xCoordinate = (emptyTilesFirst + day - 1) % 7;
        const yCoordinate = Math.floor((firstWeekday + day - 1) / 7);
        const id = day;

        // Get the corresponding temperature for the current day from the given
        // array
        // debugger;
        const PlanDate = data[day - 1].date;
        const NoofPlan = data[day - 1].NoOfPlan;
        const NoofAchieved = data[day - 1].NoofAchieved;
        const NoOfAchievement = data[day - 1].NoOfAchievement;
        var color = "";// NoOfAchievement != null ? "#3FAD18" ? temperature - NoofAchieved != 0 : "#AD3118" :"#F0940F";
        if (NoofPlan == 0 && NoofAchieved == 0) {
            color = "";//red #E4080A NoofPlan & NoofAchieved zero
        }
        if (NoOfAchievement == null && NoofPlan != 0 && NoofAchieved == 0) {
            color = "#E4080A";//bule #267EEA NoofPlan data & NoofAchieved data is zero
        }
        if (NoOfAchievement != null && NoofAchieved != NoofPlan) {
            color = "#FE9900";//org NoofPlan & NoofAchieved not will be equal
        }
        if (NoOfAchievement != null && NoofAchieved == NoofPlan) {
            color = "#3FAD18";//green NoofPlan & NoofAchieved will be equal
        }



        chartData.push({
            x: xCoordinate,
            y: 5 - yCoordinate,
            value: NoofPlan + '/' + NoofAchieved,
            color: color,
            name: NoofPlan + '/' + NoofAchieved,
            PlanDate: PlanDate,
            date: new Date(date).getTime(),
            custom: {
                monthDay: id,
            }
        });
    }

    // Fill in the missing values when dataset is looped through.
    const emptyTilesLast = lengthOfWeek - lastWeekday;
    for (let emptyDay = 1; emptyDay <= emptyTilesLast; emptyDay++) {
        chartData.push({
            x: (lastWeekday + emptyDay) % 7,
            y: 0,
            value: null,
            color: null,
            name: null,
            PlanDate: null,
            date: null,
            custom: {
                empty: true
            }
        });
    }
    return chartData;
}
function Treamap(DisMonthYear) {
    Highcharts.chart('containercalendar', {
        chart: {
            type: 'heatmap'
        },

        title: {
            // text: 'Modular Session - No Of Plan/Achievement Data',
            text: '',
            align: 'left'
        },

        subtitle: {
            text: 'Plan/Achievement variation at day through <b>' + DisMonthYear + '</b>',
            align: 'left'
        },

        accessibility: {
            landmarkVerbosity: 'one'
        },

        tooltip: {
            enabled: true,
            outside: true,
            zIndex: 20,
            headerFormat: '',
            //pointFormat: '{#unless point.custom.empty}{point.date:%A, %b %e, ' +
            //    '%Y}{/unless}',
            pointFormat: '{#unless point.custom.empty}Modular Session - No of (Plan/Achieved) : {point.name}{/unless}',
            //nullFormat: 'No data'
        },
        credits: {
            enabled: false
        },
        xAxis: {
            categories: weekdays,
            opposite: true,
            lineWidth: 26,
            offset: 13,
            lineColor: 'rgba(27, 26, 37, 0.2)',
            labels: {
                rotation: 0,
                y: 20,
                style: {
                    textTransform: 'uppercase',
                    fontWeight: 'bold'
                }
            },
            accessibility: {
                description: 'weekdays',
                rangeDescription: 'X Axis is showing all 7 days of the week, ' +
                    'starting with Sunday.'
            }
        },
        plotOptions: {
            series: {
                cursor: 'pointer',
                point: {
                    events: {
                        click: function () {
                            debugger;
                            BindPOPData(this.PlanDate);
                        }
                    }
                }
            }
        },

        yAxis: {
            min: 0,
            max: 5,
            accessibility: {
                description: 'weeks'
            },
            visible: false
        },

        legend: {
            align: 'right',
            layout: 'vertical',
            verticalAlign: 'middle'
        },

        colorAxis: {
            min: 0,
            stops: [
                [0.2, 'lightblue'],
                [0.4, '#CBDFC8'],
                [0.6, '#F3E99E'],
                [0.9, '#F9A05C']
            ],
            labels: {
                format: '{value}'
            }
        },

        series: [{
            keys: ['x', 'y', 'value', 'color', 'name', 'PlanDate', 'date', 'id'],
            data: chartData,
            nullColor: 'rgba(196, 196, 196, 0.2)',
            borderWidth: 2,
            borderColor: 'rgba(196, 196, 196, 0.2)',
            dataLabels: [{
                enabled: true,
                format: '{#unless point.custom.empty}{point.value:.1f}{/unless}',
                style: {
                    textOutline: 'none',
                    fontWeight: 'normal',
                    fontSize: '1rem'
                },
                y: 4
            }, {
                enabled: true,
                align: 'left',
                verticalAlign: 'top',
                format: '{#unless ' +
                    'point.custom.empty}{point.custom.monthDay}{/unless}',
                backgroundColor: 'whitesmoke',
                padding: 2,
                style: {
                    textOutline: 'none',
                    color: 'rgba(70, 70, 92, 1)',
                    fontSize: '0.8rem',
                    fontWeight: 'bold',
                    opacity: 0.5
                },
                x: 1,
                y: 1
            }]
        }]
    });
}
function BindData() {
    $('#msg').html('');
    //jQuery.event.trigger("ajaxStart");
    $('#msg').removeClass("text-danger");
    //var MonthId = $('#MonthId').val() == "" ? "0" : $('#MonthId').val()
    //var YearId = $('#YearId').val() == "" ? "0" : $('#YearId').val()
    var formattedDate = new Date();
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth()+1;
    var y = formattedDate.getFullYear();
    $.ajax({
        url: document.baseURI + "/Report/GetCalendarReportData",
        type: "Post",
        data: { 'MonthId': m, 'YearId': y },
        success: function (resp) {
            debugger;
            if (resp.IsSuccess) {
                var resData = JSON.parse(resp.res);
                var tbl1 = resData.Table.length != 0 ? resData.Table : null;
                if (tbl1) {
                    $('#popupmain').addClass('is-visible');
                   // $('#popupmain').show();
                    data = []; //chartData = [];
                    var DisMonthYear = "";
                    for (var i = 0; i < tbl1.length; i++) {
                        DisMonthYear = tbl1[i].ColumnDisplayMonth + "-" + tbl1[i].ColumnDisplayYear;
                        data.push({ date: tbl1[i].Date, NoOfPlan: tbl1[i].Noofplan, NoofAchieved: tbl1[i].Noofachived, NoOfAchievement: tbl1[i].NoofAchievement });
                    }
                    debugger;
                    // generateChartData(SRData);
                    // chartData = generateChartData(data);
                    chartData = generateChartData(data);
                    debugger;
                    Treamap(DisMonthYear);


                } else {
                    $('#msg').html("Record Not Found !!");//TO DO
                    $('#msg').addClass("text-danger");//TO DO
                }
            }
            else {
                $('#msg').html("Record Not Found !!");//TO DO
                $('#msg').addClass("text-danger");//TO DO
                // jQuery.event.trigger("ajaxStop");
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            $('#msg').html(errormsg);
            //jQuery.event.trigger("ajaxStop");
            return false;
        }
    });
}

function popmainclose() {
    $('#popupmain').removeClass('is-visible');
    $('#popupmain').css("dispaly", "none");
    $('#containercalendar').html('');
}
