function left(name, msg, time) {
    var maindiv = $("<div></div>");
    maindiv.addClass('direct-chat-msg');

    var infodiv = $("<div></div>");
    infodiv.addClass('direct-chat-infos clearfix');

    var spanname = $("<span></span>");
    spanname.addClass('direct-chat-name float-left');
    spanname.text(name);

    var timee = $("<span></span>");
    timee.addClass('direct-chat-timestamp float-right');
    timee.text(time);

    var img = $('<img />', {
        src: '/dist/img/avatar5.png'
    });
    img.addClass("direct-chat-img");

    var txt = $("<div></div>");
    txt.addClass('direct-chat-text');
    txt.text(msg);

    infodiv.append(spanname);
    infodiv.append(timee);
    maindiv.append(infodiv);
    maindiv.append(img);
    maindiv.append(txt);
    return maindiv;;
}
function right(name, msg, time) {
    var maindiv = $("<div></div>");
    maindiv.addClass('direct-chat-msg right');

    var infodiv = $("<div></div>");
    infodiv.addClass('direct-chat-infos clearfix');

    var spanname = $("<span></span>");
    spanname.addClass('direct-chat-name float-right');
    spanname.text(name);

    var timee = $("<span></span>");
    timee.addClass('direct-chat-timestamp float-left');
    timee.text(time);

    infodiv.append(spanname);
    infodiv.append(timee);

    var txt = $("<div></div>");
    txt.addClass('direct-chat-text');
    txt.text(msg);

    var img = $('<img />', {
        src: '/dist/img/avatar5.png'
    });
    img.addClass("direct-chat-img");

    maindiv.append(infodiv);
    maindiv.append(img);
    maindiv.append(txt);
    return maindiv;
}