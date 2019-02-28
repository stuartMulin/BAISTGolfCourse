//Custom Jquery extension
jQuery.fn.exists = function () { return this.length > 0; };

var teeTimeList = [];
var teeTimeID = 0;
var teeTimeSelected = { id: 0, startDate: "", startTime: "", endDate: "", endTime: "", count: 0 };
var reservations = [];
var counter = 0;

//Toastr Options
toastr.options = {
    "closeButton": true,
    "debug": false,
    "progressBar": true,
    "preventDuplicates": false,
    "positionClass": "toast-top-right",
    "onclick": null,
    "showDuration": "400",
    "hideDuration": "1000",
    "timeOut": "7000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

$(function () {

    $(".nav a").on("click", function () {
        $(".nav").find(".active").removeClass("active");
        $(this).parent().addClass("active");
    });
   

    $('#createDOB').datepicker({
        orientation: 'top',
        startDate: '01-01-1940',
        todayHighlight: true,
        toggleActive: true,
        todayBtn: true,
        endDate: new Date()
    });

    //$('#pageTable').DataTable();

    $("#wizard").steps({
        enableCancelButton: true,
        onStepChanging: function (event, currentIndex, newIndex) {
            // Allways allow previous action even if the current form is not valid!
            if (currentIndex === 0) {
                if ($('#startDate').val() !== '' || $('#endDate').val() !== '') {
                    if (new Date($('#startDate').val()) < new Date()) {
                        toastr.error('Start Date cannot be earlier than today or same day!', 'Error');
                        return false;
                    }
                    if (new Date($('#startDate').val()) > new Date($('#endDate').val())) {
                        toastr.error('Start Date cannot be later than End Date!', 'Error');
                        return false;
                    }
                    else {

                        var startDate = $('#startDate').val();
                        var endDate = $('#endDate').val();
                        var endTime = $('#endTime').val();
                        var startTime = $('#startTime').val();

                        var teeTimeFinder = {
                            startDate: startDate, endDate: endDate,
                            startTime: startTime, endTime: endTime
                        };

                        $.ajax({
                            url: '/Reservations/FindTeeTimes',
                            type: 'POST',
                            data: { teeTimeFinder: teeTimeFinder },
                            beforeSend: function () {
                                NProgress.start();
                            },
                            complete: function () {
                                NProgress.done();
                            },

                            success: function (response) {
                                $('#teeTimeList').html(response);
                            }
                        });
                        return true;
                    }
                }
                else {
                    toastr.error('Start Date and End Date Fields are required!', 'Error');
                    return false;
                }

            }
            else if (currentIndex === 1 && newIndex === 2) {
                if (teeTimeSelected.id === 0) {
                    toastr.error('You have to select a tee time...', 'Error');
                    return false;
                }
                else {
                    $('#teeTimeDetail').load('/Reservations/GetWithMembers', function () {
                        $('#startDateText').html('<i class="fa fa-clock-o m-r-10"></i>' +
                        teeTimeSelected.startDate + ' ' + teeTimeSelected.startTime);

                        $('#endDateText').html('<i class="fa fa-clock-o m-r-10"></i>' +
                            teeTimeSelected.endDate + ' ' + teeTimeSelected.endTime);

                        $('#reservationCount').html('<i class="fa fa-users m-r-10"></i>' +
                            teeTimeSelected.count + ' reservations');
                    });

                    
                    return true;
                }

            }
            else {
                return true;
            }

        },
        onStepChanged: function (event, currentIndex, priorIndex) {
            return true;
        },
        onFinishing: function (event, currentIndex) {
            var inputModel = { teeTimeID: teeTimeID, reservations: reservations };
            $.ajax({
                url: "/reservations/create",
                data: { inputModel: inputModel },
                type: 'POST',
                beforeSend: function () {
                    NProgress.start();
                },
                complete: function () {
                    NProgress.done();
                },
                error: function (response)
                {
                    toastr.error('An Error Occurred. You are already in this tee time', 'Error');
                    return false;
                },
                success: function (response) {
                    toastr.success('Reservation Created!', 'Success');
                   
                    window.location.href = '/reservations';
                }
            });

        },
        onFinished: function (event, currentIndex) {
            window.location.href = '/reservations';
        },
        onCanceled: function (event) {
            var confirmation = confirm('Are you sure you want to cancel the reservation?');

            if (confirmation === true) {
                window.location.href = "/reservations";
            }
            else
                return false;
        }
    });

    $('#startDate').datepicker({
        orientation: 'top',
        startDate: new Date(),
        todayHighlight: true,
        toggleActive: true,
        todayBtn: true
    });

    $('#endDate').datepicker({
        orientation: 'top',
        startDate: new Date(),
        todayHighlight: true,
        toggleActive: true,
        todayBtn: true
    });

    $('#datePlayed').datepicker({
        orientation: 'bottom',
        endDate: new Date(),
        todayHighlight: true,
        toggleActive: true,
        todayBtn: true
    });

    $('#startTime').timepicker();
    $('#endTime').timepicker();

    $('#calendar').fullCalendar({
        theme: false,
        header: {
            right: 'month',
            center: 'title',
            left: 'today prev,next'
        },
        footer: {
            right: 'month',
            center: 'title',
            left: 'today prev,next'
        },
        themeButtonIcons: {
            prev: 'circle-triangle-w',
            next: 'circle-triangle-e',
            prevYear: 'seek-prev',
            nextYear: 'seek-next'
        },
        defaultView: 'month',
        eventClick: function (event, jsEvent, view) {
            window.location.href = '/reservations/details/' + event.id;
        },
        events: "/Reservations/GetList"
       
    });
});

function makeDecision(id, action) {
    $.ajax({
        url: "/AdminTasks/MakeDecision",
        data: { id: parseInt(id), option: action },
        beforeSend: function () {
            NProgress.start();
        },
        complete: function () {
            NProgress.done();
        },
        type: "GET",
        success: function (response) {
            toastr.success(response, 'Response');
        }
    });
}

function createReservation() {
    window.location.href = '/reservations/create';
}

function reservationList()
{
    window.location.href = '/reservations/list';
}

function bookReservation(id, startDate, startTime, endDate, endTime, count) {
    teeTimeSelected.id = id;
    teeTimeSelected.startDate = startDate;
    teeTimeSelected.startTime = startTime;
    teeTimeSelected.endDate = endDate;
    teeTimeSelected.endTime = endTime;
    teeTimeSelected.count = count;
    teeTimeID = id;
    counter = count;

    console.log(teeTimeSelected);
}

function showToastrError()
{
    toastr.error('An Error Occurred', 'Error');
}

function showToastrSuccess() {
    toastr.success('Score successfully saved!', 'Success');
}