
$(function () {
    var placeholder = $('#notifshow');

    $('#NotifDropDown').click(function (event) {
        var url = $(this).data('url'); placeholder.html("");
        $.ajax({
            url: url,
            method: "post",

            success: function (data) {
                placeholder.html(data);
            }
        });       
    });
    var placeholderChat = $('#chatarea');

    $('#chatDropDown').click(function (event) {
        console.log("sds");
        var url = $(this).data('url'); placeholderChat.html("");
        $.ajax({
            url: url,
            method: "post",

            success: function (data) {
                placeholderChat.html(data);
            }
        });
    });
})