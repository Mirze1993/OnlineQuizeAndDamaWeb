var timer;
function ShowOnline() {
    localStorage.setItem("online", $("#online").prop('checked') )
    if ($("#online").prop('checked') == true) { 
        console.log("rtdhdtrhd");
        clearInterval(timer);
        timer = setInterval(SetOnline, 10000);
    }
    if ($("#online").prop('checked') == false) {
        clearInterval(timer);
    }
}



function SetOnline() {
    console.log("asdas");
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
           
        }
    });
}