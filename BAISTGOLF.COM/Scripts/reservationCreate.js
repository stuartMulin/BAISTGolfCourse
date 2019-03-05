var teeTimeList = [];
var teeTimeID = 0;
var teeTimeSelected = { id: 0, startDate: "", startTime: "", endDate: "", endTime: "", count: 0 };
var reservations = [];
var memberID = 0;
var counter = 0;

$(function () {

    memberID = $('#memberID').text();

    $('#wizard').steps({
        enableCancelButton: true,
        onStepChanging: function (event, currentIndex, newIndex) {
            if (currentIndex === 0 && newIndex === 1) {
                if (teeTimeSelected.id === 0) {
                    toastr.error('You have to select a tee time...', 'Error');
                    return false;
                }
                else {
                    var member = { memberID: parseInt(memberID), status: 2 };

                    reservations.push(member);

                    console.log(member)

                    $('#teeTimeDetail').load('/Reservation/GetWithMembers', function () {
                        $('#startDateText').html('<i class="fa fa-clock-o m-r-10"></i>' +
                        teeTimeSelected.startDate + ' ' + teeTimeSelected.startTime);

                        $('#endDateText').html('<i class="fa fa-clock-o m-r-10"></i>' +
                            teeTimeSelected.endDate + ' ' + teeTimeSelected.endTime);

                        $('#reservationCount').html('<i class="fa fa-users m-r-10"></i>' +
                            reservations.length + ' members');
                    });


                    return true;
                }
            }
            return true;
        },
        onStepChanged: function (event, currentIndex, priorIndex) {
            return true;
        },
        onFinishing: function (event, currentIndex) {
            var inputModel = { teeTimeID: teeTimeID, potentialReservations: reservations };
            debugger
            if (inputModel.potentialReservations.length < 4)
            {
                toastr.warning('A reservation must have 4 members', 'Warning');
            }
            else {
                $.ajax({
                    url: "/reservation/create",
                    data: { inputModel: inputModel },
                    type: 'POST',
                    beforeSend: function () {
                        NProgress.start();
                    },
                    complete: function () {
                        NProgress.done();
                    },
                    error: function (response) {
                        toastr.error('An Error Occurred While Creating This Reservation. Please Contact Service Desk', 'Error');
                        return false;
                    },
                    success: function (response) {
                        toastr.success('Reservation Created!', 'Success');
                        window.location.href = '/reservations/list';
                    }
                });
            }
        }

    });

    $('#searchDate').datepicker({
        orientation: 'top',
        startDate: new Date(),
        todayHighlight: true,
        toggleActive: true,
        todayBtn: true
    });
})


function searchForTeeTimes() {
    var searchDate = $('#searchDate').val();

    if (searchDate === '') {
        toastr.error('Enter a value in the field', 'Error');
    } else {
        $.ajax({
            url: "/TeeTime/GetListBySearchDate",
            data: { searchDate: searchDate },
            beforeSend: function () {
                NProgress.start();
            },
            complete: function () {
                NProgress.done();
            },
            type: "GET",
            success: function (response) {
                $('#tee-times').html(response);
            }
        });
    }

}

function selectTeeTime(id, startDate, startTime, endDate, endTime, count) {
    teeTimeSelected.id = id;
    teeTimeSelected.startDate = startDate;
    teeTimeSelected.startTime = startTime;
    teeTimeSelected.endDate = endDate;
    teeTimeSelected.endTime = endTime;
    teeTimeSelected.count = count;
    teeTimeID = id;
    counter = count;

    toastr.info('Tee Time Selected!', 'Selected');
    console.log(teeTimeSelected);
}

function addMemberToTeeTime() {
    var memberEmail = $('#memberEmailSearch').val();

    if (memberEmail === '') {
        toastr.warning('Enter an email first')
    }
    else {
        $.ajax({
            url: "/Members/GetByEmail",
            data: { searchEmail: memberEmail },
            beforeSend: function () {
                NProgress.start();
            },
            complete: function () {
                NProgress.done();
            },
            type: "GET",
            success: function (response) {
                if (response.ID === 0) {
                    toastr.error('This user does not exist')
                } else {
                    var member = _.where(reservations, { memberID: response.ID })[0];

                    if (member !== undefined) {
                        toastr.error('Member already in the list', 'Error');
                    }
                    else if (response.ID == memberID)
                    {
                        toastr.warning('You are already in this reservation!');
                    }
                    else {
                        if (counter < 3 && counter >= 0) {
                            counter++;
                            var reservation = { memberID: response.ID, status: 2 };
                            reservations.push(reservation);

                            $('#reservationCount').html('<i class="fa fa-users m-r-10"></i>' +
                            (counter + 1) + ' members');

                            $('#membersToBeAdded').append(
                            '<a id="removeButton' + response.ID + '" class="list-group-item"><span onclick="removeFromReservationList(this.id)" class="badge m-t-10" id="removeTrigger'
                            + response.ID + '">X</span><h5>' + response.FirstName
                            + " " + response.LastName + '</h5></a>');
                        }
                        else {
                            toastr.warning('Maximum of 4 members for a reservation', 'Warning');
                        }
                    }
                }
            }
        });
    }
}

function removeFromReservationList(id) {
    var idToRemove = id;
    counter--;
    id = id.replace('removeTrigger', '');
    id = parseInt(id);
    reservations = _.without(reservations, _.findWhere(reservations, { memberID: id }));

    $('#removeButton' + id).remove();

    $('#reservationCount').html('<i class="fa fa-users m-r-10"></i>' +
                            (counter + 1) + ' members');
}