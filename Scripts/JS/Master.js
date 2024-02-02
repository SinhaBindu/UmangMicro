var baseurl = document.baseURI;
var AddPicListed = [];
//const urlParams = new URLSearchParams(window.location.search);
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
$(function () {

    $('.datepicker').datepicker({
        format: 'dd-M-yyyy',
        autoclose: true
        //startDate: '-3d'
    });

    // Basic ddl Binds
    //debugger;
    //BindState("IDState");
    //BindDistrict("IDDistrict", $('.state').val());
    //BindDistrict("IDRegDistrict", $('.state').val());
    //BindReferToList("ReferedTo", "");

    //DatePicker();

     //ComponentID Filter
    //$('#ComponentID').multiselect({
    //    includeSelectAllOption: true,
    //});

    //$('#ComponentID').val(['e54e6352-b38b-43aa-8569-0dd897d66cf2',
    //    '0e73c2eb-3aca-41a1-9c6b-3352780db37c', '1e7e8b7d-4323-4977-a714-6477ad911575', 'ab299025-f2d5-4b85-a69e-7175388343f5',
    //    '06fc8a68-e904-489c-a464-72eb7d065796']).multiselect('refresh');
});

function GetAutoSeletedComponent(Elemt) {
    //return $('#' + Elemt).val(['e54e6352-b38b-43aa-8569-0dd897d66cf2',
    //    '0e73c2eb-3aca-41a1-9c6b-3352780db37c', '1e7e8b7d-4323-4977-a714-6477ad911575', 'ab299025-f2d5-4b85-a69e-7175388343f5',
    //    '06fc8a68-e904-489c-a464-72eb7d065796']).multiselect('refresh');
}
function GetAutoSeletedLocationOrganization(Elemt) {
    //return $('#' + Elemt).multiselect('selectAll').multiselect('refresh');
}

/* Only Digit Allowed */
function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

/* -------------------------------------------- Date Range -------------------------------*/

function getAge(dateString) {

    const dates = new Date(dateString); // {object Date}            

    const covdate = new Intl.DateTimeFormat("en-US", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit"
    }).format(dates);
    dateString = covdate;

    var now = new Date();

    var yearNow = now.getFullYear();
    var monthNow = now.getMonth();
    var dateNow = now.getDate();
    //date must be mm/dd/yyyy
    var dob = new Date(dateString.substring(6, 10),
        dateString.substring(0, 2) - 1,
        dateString.substring(3, 5)
    );

    var yearDob = dob.getFullYear();
    var monthDob = dob.getMonth();
    var dateDob = dob.getDate();
    var age = {};
    var ageString = "";
    var yearString = "";
    var monthString = "";
    var dayString = "";


    yearAge = yearNow - yearDob;

    if (monthNow >= monthDob)
        var monthAge = monthNow - monthDob;
    else {
        yearAge--;
        var monthAge = 12 + monthNow - monthDob;
    }

    if (dateNow >= dateDob)
        var dateAge = dateNow - dateDob;
    else {
        monthAge--;
        var dateAge = 31 + dateNow - dateDob;

        if (monthAge < 0) {
            monthAge = 11;
            yearAge--;
        }
    }

    age = {
        years: yearAge,
        months: monthAge,
        days: dateAge
    };

    if (age.years > 1) yearString = " years";
    else yearString = " year";
    if (age.months > 1) monthString = " months";
    else monthString = " month";
    if (age.days > 1) dayString = " days";
    else dayString = " day";

    if ((age.years > 15) && (age.months > 0) && (age.days > 0))
        ageString = age.years + yearString + ", " + age.months + monthString + ", and " + age.days + dayString + " old.";
   else if ((age.years > 0) && (age.months > 0) && (age.days > 0))
        ageString = age.years + yearString + ", " + age.months + monthString + ", and " + age.days + dayString + " old.";
    else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
        ageString = "Only " + age.days + dayString + " old!";
    else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
        ageString = age.years + yearString + " old. Happy Birthday!!";
    else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
        ageString = age.years + yearString + " and " + age.months + monthString + " old.";
    else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
        ageString = age.months + monthString + " and " + age.days + dayString + " old.";
    else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
        ageString = age.years + yearString + " and " + age.days + dayString + " old.";
    else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
        ageString = age.months + monthString + " old.";
   // else ageString = "Oops! Could not calculate age!";
    else ageString = ""; //toastr.error("Error", "Can't be valid date.");

    return ageString;
}

// A bit of jQuery to call the getAge() function and update the page...
$(document).ready(function () {
    $("#submitDate").click(function (e) {
        e.preventDefault();

        $("#age").html(getAge($("input#date").val()));

    });
});
function GetTotalYearsFromTwoDate(Date1, Date2) {
    var dt1 = moment(Date1);
    var dt2 = moment(Date2);
    if (dt1.isValid() && dt2.isValid()) {
        //moment('2018-04-21').diff(moment('2017-012-21'), 'years', true);
        var totYears = moment(dt1).diff(dt2, 'years', true);
        return parseInt(totYears);
    }
}

function GetTotalMonthsFromTwoDate(Date1, Date2) {
    var dt1 = moment(Date1);
    var dt2 = moment(Date2);
    if (dt1.isValid() && dt2.isValid()) {
        //moment('2018-04-21').diff(moment('2017-012-21'), 'years', true);
        var totMonths = moment(dt1).diff(dt2, 'months', true);
        return parseInt(totMonths);
    }
}

function GetDOBByAgeYearAndMonth(Year, Month) {
    console.log(Year, ', ' + Month);
    var totalMonth = parseInt(Year != null ? (Year * 12) : 0) + parseInt(Month ?? 0);
    var dob = moment().add(-totalMonth, 'months');
    return dob;
}

function GetQuarterFromDateRange(FromDate, ToDate) {
    //var fd = moment(FromDate, 'YYYY/MM/DD');
    var fd = moment(FromDate).month(); //, 'YYYY/MM/DD');
    var quarterNo = 0;
    var fQuarterNo = 0, tQuarterNo = 0, monthNo = 0;
    monthNo = moment(FromDate).month() + 1; //fd.format('M');
    console.log(FromDate + ', MonthNo from Date is: ' + monthNo);
    if (parseInt(monthNo) >= 10 && parseInt(monthNo) <= 12) {
        fQuarterNo = 1;
    }
    else if (monthNo >= 1 && monthNo <= 3) {
        fQuarterNo = 2;
    }
    else if (monthNo >= 4 && monthNo <= 6) {
        fQuarterNo = 3;
    }
    else if (monthNo >= 7 && monthNo <= 9) {
        fQuarterNo = 4;
    }
    quarterNo = fQuarterNo;
    console.log('Quater No. :' + quarterNo);
    return quarterNo;
    //if (monthNo > = 10 && monthNo <= 12)
    //{
    //    tQuarterNo = 1;
    //}
    //else if (monthNo > = 1 && monthNo <= 3)
    //{
    //    tQuarterNo = 2;
    //}
    //else if (monthNo > = 4 && monthNo <= 6)
    //{
    //    tQuarterNo = 3;
    //}
    //else if (monthNo > = 7 && monthNo <= 9)
    //{
    //    tQuarterNo = 4;
    //}

    //var month = fd.format('M');
    //var day = check.format('D');
    //var year = check.format('YYYY');
}

function processAjaxData(response, urlPath) {
    document.getElementById("content").innerHTML = response.html;
    document.title = response.pageTitle;
    window.history.pushState({ "html": response.html, "pageTitle": response.pageTitle }, "", urlPath);
}

//function DatePicker() {
//    $('.datepicker').datepicker({
//        singleDatePicker: true,
//        showDropdowns: true,
//        minYear: 2021,
//       // maxYear: 2021,//parseInt(moment().format('YYYY'), 10)
//        endDate: new Date(),
//    //},
//       // function (start, end, label) {
//        //var years = moment().diff(start, 'years');
//        //alert("You are " + years + " years old!");
//    });
//}
/* -------------------------------------------- Closed Date Range -------------------------------*/

/* -------------------------------------------- Bind DDL -------------------------------*/

function BindState(Ele) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    //$('#IDState').append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI+ "/Master/GetStateList",
        type: "Post",
        data: '',
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                })
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
function BindDistrict(Ele, Sel) {
    
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetDistrictList",
        type: "Post",
        data: JSON.stringify({ 'StateId': Sel }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,   
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
function BindBlock(Ele, Sel) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetBlockList",
        type: "Post",
        data: JSON.stringify({ 'DistrictId': Sel }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
function BindCluster(Ele, Sel) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetClusterList",
        type: "Post",
        data: JSON.stringify({ 'BlockId': Sel }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
//function OnChangeState(District, Sel) {
//    if (Sel != 'undefined') {
//        BindDistrict(District, Sel.value);
//    }
//}
//Block
function OnChangeDistrict(Ele, Sel) {
    if (Sel != 'undefined') {
        var d = Ele;
        BindBlock(Ele, Sel);
    }
}
function OnChangeBlock(Ele, Sel) {
    if (Sel != 'undefined') {
        var d = Ele;
        BindCluster(Ele, Sel);
    }
}

//function BindReferToList(ElementId, SelectedValue) {
//    $('#' + ElementId).empty();
//    //$('#' + ElementId).prop("disabled", false);
//    //$('#' + ElementId).append($("<option>").val('').text('--Select--'));
//    $.ajax({
//        url: document.baseURI + "/Master/GetReferToList",
//        type: "Get",
//        data: {},
//        contentType: "application/json; charset=utf-8",
//        global: false,
//        async: false,
//        dataType: "json",
//        success: function (resp) {
//            if (resp.IsSuccess) {
//                var data = resp.res;
//                $.each(data, function (i, exp) {
//                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
//                });
//            }
//            else {
//                alert(resp.IsSuccess);
//            }
//        },
//        error: function (req, error) {
//            if (error === 'error') { error = req.statusText; }
//            var errormsg = 'There was a communication error: ' + error;
//            //Do To Message display
//        }
//    });
//    $('#' + ElementId).trigger("chosen:updated");
//}


////////////////////////////////////////////////////////////////// Bind Master new 

function BindddlOutListPop(ElementId, SelectedValue) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetddlOutput",
        type: "Get",
        data: { 'OutcomeID': SelectedValue },
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //alert(errormsg);
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}

function BindOrganization(ElementId, SelectedValue) {
    //console.log('select value-'+SelectedValue);
    //$('#' + ElementId).empty();
    //$('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('--Select--'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "/Master/GetDDLOrganization",
        type: "Get",
        data: {},
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    console.log(exp);
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                    console.log(ElementId);
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    //console.log('select value-'+SelectedValue);
    $('#IDOrganization').multiselect();
    $('#' + ElementId).trigger("chosen:updated");
}

function BindLocation(ElementId, SelectedValue) {
    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "/Master/GetDDLLocation",
        type: "Get",
        data: {},
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindComponent(ElementId, SelectedValue) {
    console.log('Component select value-'+SelectedValue);
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "/Master/GetDDLComponent",
        type: "Get",
        data: {},
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindOutcome(ElementId, SelectedValue, IDComponent) {
    console.log('binding outcome select value-'+SelectedValue);
    console.log(ElementId + ', ' + SelectedValue + ', ' + IDComponent);
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('All'));
    if (IDComponent != null && IDComponent != '' && IDComponent != undefined) {
        $.ajax({
            //url: document.baseURI + "/Master/GetHSCBlock",
            url: document.baseURI + "/Master/GetDDLOutcome",
            type: "GET",
            data: { 'ComponentID': IDComponent },
            contentType: "application/json; charset=utf-8",
            global: false,
            async: false,
            dataType: "json",
            success: function (resp) {
                if (resp.IsSuccess) {
                    
                    var data = resp.res;
                    $.each(data, function (i, exp) {
                        $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                        $('#' + ElementId).val(SelectedValue);
                    });
                    console.log('bo - ' + SelectedValue);
                    $('#' + ElementId).val(SelectedValue);
                }
                else {
                    alert(resp.IsSuccess);
                }
            },
            error: function (req, error) {
                if (error === 'error') { error = req.statusText; }
                var errormsg = 'There was a communication error: ' + error;
                //Do To Message display
            }
        });

        $('#' + ElementId).val(SelectedValue);

    }
    else {
        $.ajax({
            //url: document.baseURI + "/Master/GetHSCDistrict",
            url: document.baseURI + "/Master/GetDDLOutcome",
            type: "Get",
            data: {},
            contentType: "application/json; charset=utf-8",
            //global: false,
            //async: false,
            dataType: "json",
            success: function (resp) {
                if (resp.IsSuccess) {
                    var data = resp.res;
                    $.each(data, function (i, exp) {
                        $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                    });
                    $('#' + ElementId).val(SelectedValue);
                }
                else {
                    alert(resp.IsSuccess);
                }
            },
            error: function (req, error) {
                if (error === 'error') { error = req.statusText; }
                var errormsg = 'There was a communication error: ' + error;
                //Do To Message display
            }
        });
    }
    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindOutput(ElementId, SelectedValue, IDOutcome) {
    console.log('Binding output dddl');
    console.log(ElementId + ', ' + SelectedValue + ', ' + IDOutcome);
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('All'));
    //var vl = $('#' + IDOutcome).val();
    var vl = IDOutcome == "" ? $('#IDOutcome').val() : IDOutcome;
    console.log(vl);
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCBlock",
        url: document.baseURI +"/Master/GetddlOutput",
        type: "Get",
        data: { 'OutcomeID': IDOutcome },
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                    $('#' + ElementId).val(SelectedValue);
                });
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}

function BindActivity(ElementId, SelectedValue, IDOutput) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('All'));
    //var vl = $('#' + IDOutput).val();
    var vl = IDOutput == "" ? $('#IDOutput').val() : IDOutput;
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCBlock",
        url: document.baseURI + "/Master/GetDDLActivity",
        type: "Get",
        data: { 'OutputID': vl },
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                    $('#' + ElementId).val(SelectedValue);
                });
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}

function BindTypeOfActivity(ElementId, ActivityID, SelectedValue) {
    $('#' + ElementId).empty();
    //$('#' + ElementId).prop("disabled", false);
   $('#' + ElementId).append($("<option>").val('0').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "/Master/GetDDLTypeOfActivity",
        type: "Get",
        data: { 'ActivityID': ActivityID },
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                    console.log(ElementId);
                });
                if (SelectedValue) {
                    $('#' + ElementId).val(SelectedValue);
                } else {
                    $('#' + ElementId).val('0');
                }                
            }
            else {
                alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindActivityProgressPic(FDid, TDid) {
    var FDv = $('#' + FDid).val();
    var TDv = $('#' + TDid).val();
    $.ajax({
        cache: false,
        //url: document.baseURI + "/Master/GetHSCBlock",
        url: document.baseURI + "/Master/GetActivityProgressPic",
        type: "Get",
        data: { "FD": FDv, "TD": TDv },
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        //processData: true,
        success: function (resp) {
            if (resp.IsSuccess) {
                if (resp.res) {
                    AddPicListed = JSON.parse(resp.res);
                }
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    
}

//function BindOrganization(ElementId, SelectedValue) {
//    $('#' + ElementId).empty();
//    //$('#' + ElementId).prop("disabled", false);
//    //$('#' + ElementId).append($("<option>").val('').text('--Select--'));
//    //var vl = $('#' + IDOutcome).val();
//    $.ajax({
//        //url: document.baseURI + "/Master/GetHSCBlock",
//        url: "../Master/GetOrganizationList",
//        type: "Get",
//        //data: { 'Outcome': vl },
//        contentType: "application/json; charset=utf-8",
//        global: false,
//        async: false,
//        dataType: "json",
//        success: function (resp) {
//            if (resp.IsSuccess) {
//                var data = resp.res;
//                $.each(data, function (i, exp) {
//                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
//                });
//            }
//            else {
//                alert(resp.IsSuccess);
//            }
//        },
//        error: function (req, error) {
//            if (error === 'error') { error = req.statusText; }
//            var errormsg = 'There was a communication error: ' + error;
//            //Do To Message display
//        }
//    });
//    $('#' + ElementId).trigger("chosen:updated");
//}

//function BindLocation(ElementId, SelectedValue) {
//    $('#' + ElementId).empty();
//    //$('#' + ElementId).prop("disabled", false);
//    //$('#' + ElementId).append($("<option>").val('').text('--Select--'));
//    //var vl = $('#' + IDOutcome).val();
//    $.ajax({
//        //url: document.baseURI + "/Master/GetHSCBlock",
//        url: "../Planning/GetLocationList",
//        type: "Get",
//        //data: { 'Outcome': vl },
//        contentType: "application/json; charset=utf-8",
//        global: false,
//        async: false,
//        dataType: "json",
//        success: function (resp) {
//            if (resp.IsSuccess) {
//                var data = resp.res;
//                $.each(data, function (i, exp) {
//                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
//                });
//            }
//            else {
//                alert(resp.IsSuccess);
//            }
//        },
//        error: function (req, error) {
//            if (error === 'error') { error = req.statusText; }
//            var errormsg = 'There was a communication error: ' + error;
//            //Do To Message display
//        }
//    });
//    $('#' + ElementId).trigger("chosen:updated");
//}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function printDiv(elem) {
    var mywindow = window.open();
    var content = document.getElementById(elem).innerHTML;
    var realContent = document.body.innerHTML;
    mywindow.document.write(content);
    mywindow.document.close(); // necessary for IE >= 10


    mywindow.focus(); // necessary for IE >= 10*/


    mywindow.print();
    document.body.innerHTML = realContent;
    mywindow.close();
    return true;
}

/* -------------------------------------------- Bind DDL Code -------------------------------*/
function GetDistrict(Ele, Sel) {

    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetDistList",
        type: "Post",
        data: '',//JSON.stringify({ '': Sel }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
function GetBlock(Ele, Sel,Para1) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetBlckList",
        type: "Post",
        data: JSON.stringify({ 'DistrictId': Para1 }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
function GetPanchayat(Ele, Sel,Para1,Para2) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetPanchayatList",
        type: "Post",
        data: JSON.stringify({ 'DistrictId': Para1, 'BlockId': Para2 }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}
function GetVillage(Ele, Sel, Para1, Para2, Para3) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetVillageList",
        type: "Post",
        data: JSON.stringify({ 'DistrictId': Para1, 'BlockId': Para2, 'PanchayatId': Para3 }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}

function GetSchool(Ele, Sel, Para1, Para2) {
    $('#' + Ele).empty();
    $('#' + Ele).prop("disabled", false);
    $('#' + Ele).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "/Master/GetSchoolList",
        type: "Post",
        data: JSON.stringify({ 'DistrictId': Para1, 'BlockId': Para2 }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + Ele).append($("<option>").val(exp.Value).text(exp.Text));
                });
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + Ele).trigger("chosen:updated");
}

function OnChagDistricts(Ele, Sel) {
    if (Sel != 'undefined') {
        var d = Ele;
        GetBlock(Ele, '', Sel);
    }
}
function OnChagBlocks(Ele, Sel, Para1,Para2) {
    if (Sel != 'undefined') {
        var d = Ele;
        GetPanchayat(Ele, '', Para1, Para2);
        
    }
}
function OnChagPanchayats(Ele, Sel, Para1, Para2,Para3) {
    if (Sel != 'undefined') {
        var d = Ele;
        GetVillage(Ele, '', Para1, Para2,Para3);
    }
}

function BindClassNo(CaseId,sele) {
    $.ajax({
        url: document.baseURI + "/Counsellor/GetClassno",
        type: "Post",
        data: JSON.stringify({ 'CaseId': CaseId }),
        contentType: "application/json; charset=utf-8",
        global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                sele = JSON.parse(resp.res);
                return sele;
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
}

