
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
})