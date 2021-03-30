

$(function () {
   
    var placeholder = $('#placeholder');

    $('button[data-toggle="ajax-game-request"]').click(function (event) {
        var url = $(this).data('url');       
        $.get(url).done(function (data) {
            placeholder.html(data);
            placeholder.find('.modal').modal('show') ;
        })
    })

    

    placeholder.on('click', '[data-send="modal"]', function (e) {
        //var form = $(this).parents('.modal').find('form');
        //var url = form.attr('action');
        //var senddata = form.serialize();
        
        var id = $(this).data('value');
        var urll = "/GameRoom/RequestGame/" + id
        $.ajax({
            method: "post",
            url: urll,
            success: function (ee) {
                if (ee == 0) toastr.error("xeta bas verdi");
                if (ee == -1) toastr.info("oyuncu ile aktiv oyun ve ya oyun isteyi vardir");
                if (ee > 0) toastr.success("istek ugurla gonderildi");
                placeholder.find('.modal').modal('hide');
            },
        });
    })
})