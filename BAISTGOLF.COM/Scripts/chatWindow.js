//Custom Jquery extension
jQuery.fn.exists = function () { return this.length > 0; }

$(function () {
    var memberChatHubProxy = $.connection.memberChatHub;
    memberChatHubProxy.client.addMessageToDiv = function (dateTime, currentUserName, message) {
        $('#convoWindow').append("<div class='user-chat-div'><i class='fa fa-comments chat-pic'></i><div class='chat-message-div'><p class='f-s-15'>" + currentUserName + " <i class='bold' style='font-size: 10px'>at " + dateTime + "</i></p><p>" + message + "</p></div></div>");
        scrollDivToBottom();
    }
    $.connection.hub.start();

    $('#chatTextBox').keypress(function () {
        if (event.which == 13) {
            if ($('#chatTextBox').val() == "") {
                $('#chatTextBox').css('border', '1px solid red');
            }
            else {
                startChat();
            }
        }

    })
})

function scrollDivToBottom() {
    if ($('#convoWindow').exists()) {
        $('#convoWindow').animate({
            scrollTop: $('#convoWindow').get(0).scrollHeight
        }, 0);
    }
}

function startChat() {
    var memberChatHubProxy = $.connection.memberChatHub;
    $.connection.hub.start().done(function () {

        $('#chatTextBox').css('border', '1px solid lightgray');
        $('#startText').remove();
        var currentUserName = $('#currentUserName').text();
        var message = $('#chatTextBox').val();
        memberChatHubProxy.server.send(currentUserName, message);
        console.log(memberChatHubProxy.server);
        $('#chatTextBox').val('');

    })
}