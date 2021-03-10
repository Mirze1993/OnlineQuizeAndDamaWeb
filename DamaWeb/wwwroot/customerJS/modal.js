$(function () {
    var placeholder = $('#placeholder');
    $('button[data-toggle="ajax-game-request]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholder.html(data);
            placeholder.find('.modal').modal('show');
        })
    })

    placeholder.on('click', '[data-send="modal"]', function (event) {
        //var form = $(this).parents('.modal').find('form');
        //var url = form.attr('action');
        //var senddata = form.serialize();

        var id = $(this).data('value');
        $.get("/")
        laceholder.find('.modal').modal('hide');
    })
})