$(function () {
    $('#memberID').on('keyup', function () {
        $.ajax({
            url: '/Reservation/GetMemberReservationsForAdmin',
            type: 'GET',
            data: { memberID: this.value },
            beforeSend: function () {
                NProgress.start();
            },
            complete: function () {
                NProgress.done();
            },

            success: function (response) {
                var s = '<option value="-1">Please Select a Reservation</option>';
                for (var i = 0; i < response.length; i++) {
                    s += '<option value="' + response[i].ID + '">' + response[i].Date + '</option>';
                }
                $("#reservationsList").html(s);
            }
        });
    });
});