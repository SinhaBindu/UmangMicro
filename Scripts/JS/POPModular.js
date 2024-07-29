
function BindPOPData(PlanDate) {
    $.ajax({
        url: document.baseURI + "/Report/GetCalendarClickPopModularData",
        type: "Post",
        data: { 'PlanDate': PlanDate },//'MonthId': MonthId, 'YearId': YearId, 
        success: function (resp) {
            debugger;
            if (resp.IsSuccess) {
                $('#POPModel').html(resp.res);
                if ($.fn.dataTable.isDataTable('#poptbl')) {
                    $("#poptbl").dataTable().fnDestroy();
                }
                $('#poptbl').DataTable({
                    //paging: false,
                    fixedHeader: true,
                    //responsive: false,
                    // scrollCollapse: false,
                    "dom": '<"pull-left"f><"pull-right"l>tip',
                    "pageLength": 100,
                    "lengthChange": false,
                    "buttons": [
                        {
                            extend: 'excel',
                            text: 'Export excel',
                            className: 'exportExcel',
                            filename: 'Export excel',
                            exportOptions: {
                                modifier: {
                                    page: 'all'
                                }
                            }
                        }]
                });

                $('.popup').addClass('is-visible');
            }
            else {
                $('#POPModel').html("<span class='text-danger'>Record Not Found !!</span>");//TO DO
                // jQuery.event.trigger("ajaxStop");
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            $('#POPModel').html(errormsg);
            //jQuery.event.trigger("ajaxStop");
            return false;
        }
    });
}

jQuery(document).ready(function ($) {
    $(document).keyup(function (event) {
        if (event.which == '27') {
            $('.popup').removeClass('is-visible');
        }
    });
});
function make() {
    $('#popup1').css("dispaly", "none");
    $('#POPModel').html('');
}
