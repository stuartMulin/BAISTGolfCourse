function addMemberToReservation() {
    var memberID =  $('#memberID').val();
    debugger;
    $.ajax({
        url: '/Reservation/AddMembers',
        type: 'POST',
        data: { memberID: memberID, teeTimeID: teeTimeID },
        beforeSend: function () {
            NProgress.start();
        },
        complete: function () {
            NProgress.done();
        },
        success: function (response) {
            $('#memberID').val('');
            if (response === null) {
                toastr.error('Member ID or Email is invalid or Member is already in the list!', 'Error');
            }
            else {
                var member = _.where(reservations, { memberID: response.ID })[0];

                if (member !== undefined)
                {
                    toastr.error('Member already in the list', 'Error');
                }
                else
                {
                    if (counter < 3 && counter >= 0) {
                        counter++;
                        var reservation = { memberID: response.ID, status: 2 };
                        reservations.push(reservation);

                        $('#membersToBeAdded').append(
                        '<a id="removeButton' + response.ID + '" class="list-group-item"><span onclick="removeFromReservationList(this.id)" class="badge m-t-10" id="removeTrigger' + response.ID + '">X</span><h5>' + response.FirstName
                        + " " + response.LastName + '</h5></a>');
                    }
                    else {
                        toastr.warning('Maximum of 4 reservations for a tee time', 'Warning');
                    }
                } 
            }
        }
    });
}

function removeFromReservationList(id) {
    var idToRemove = id;
    counter--;
    id = id.replace('removeTrigger', '');
    id = parseInt(id);
    reservations = _.without(reservations, _.findWhere(reservations, {memberID: id}));

    $('#removeButton' + id).remove();
}