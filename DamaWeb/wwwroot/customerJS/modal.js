

$(function () {
   
    var placeholder = $('#placeholder');
    $('button[data-toggle="ajax-game-request"]').click(function (event) {
        var url = $(this).data('url');
        console.log(url);
        $.get(url).done(function (data) {
            placeholder.html(data);
            placeholder.find('.modal').modal('show') ;
        })
    })

    
    

    placeholder.on('click', '[data-send="modal"]', function (event) {
        //var form = $(this).parents('.modal').find('form');
        //var url = form.attr('action');
        //var senddata = form.serialize();

        var id = $(this).data('value');
        $.get("/GameRoom/RequestGame/" + id).done(function (event) {
            toastr.success('istek ugurla gonderildi');
            placeholder. find('.modal').modal('hide');
        })
        
    })
})