//Custom Jquery extension
jQuery.fn.exists = function () { return this.length > 0; }


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
}

$(function () {

    $("#wizard").steps({
        enableCancelButton: true,
        onStepChanging: function (event, currentIndex, newIndex) {
            // Allways allow previous action even if the current form is not valid!
            if (currentIndex === 0)
            {
                if ($('#startDate').val() !== '' || $('#endDate').val() !== '')
                {
                    if (new Date($('#startDate').val()) < new Date())
                    {
                        toastr.error('Start Date cannot be earlier than today!', 'Error');
                        return false;
                    }
                    if (new Date($('#startDate').val()) > new Date($('#endDate').val())) {
                        toastr.error('Start Date cannot be later than End Date!', 'Error');
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    return true;
                }
                else
                {
                    toastr.error('Start Date and End Date Fields are required!', 'Error');
                    return false;
                }
                
            }
            return true;
        },
        onStepChanged: function (event, currentIndex, priorIndex) {
            // Used to skip the "Warning" step if the user is old enough.
            return true;
        },
        onFinishing: function (event, currentIndex) {

            return true;

        },
        onFinished: function (event, currentIndex) {

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

    $('#startDate').datetimepicker();

    $('#endDate').datetimepicker();
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
    })
}

function createReservation() {
    window.location.href = '/reservations/create';
}

function reservationList()
{
    window.location.href = 'reservations/list'
}