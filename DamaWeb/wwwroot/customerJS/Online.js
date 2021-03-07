var timer; var timer2;
function ShowOnline() {
    console.log($("#online").is(":checked"));
    if ($("#online").prop('checked') == true) { 
        clearInterval(timer2);
        clearInterval(timer);
        timer = setInterval(SetOnline, 2000);
        timer2=setInterval(GetOnlineUsers, 2000);
    }
    if ($("#online").prop('checked') == false) {
        clearInterval(timer);
    }
}

function SetOnline() {
    console.log("SetOnline");
    $.ajax({
        method: "post",
        url: "/Home/Online",
        success: function () {
        }
    });
}

function GetOnlineUsers() {
    console.log("GetOnlineUsers");
    $.ajax({
        method: "post",
        url: "/Home/GetOnlineUsers",
        success: function (msg) {
            $(msg).each(function (i,e) {
                console.log(e.name);
            });
            
        }
    });
}