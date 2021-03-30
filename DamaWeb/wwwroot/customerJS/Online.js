const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/mainhub")
    .build();

$(function () {
    
    function start() {
        try {
            connection.start().then(function () {               
                var checkboxValue = JSON.parse(localStorage.getItem('online'));
                if (checkboxValue != null) {
                    connection.invoke("SetOnline", checkboxValue).catch(function (e) { console.log(e) });
                }       
            });
            
        } catch (e) {
            console.log("asdsadsadsadsa");
            setTimeout(function () { start(); console.log("asdsadsadsadsa"); }, 1000);
        }
    };
    start();

})
//chat

function ShowOnline() {
    localStorage.setItem("online", $("#online").prop('checked'))
    connection.invoke("SetOnline", $("#online").prop('checked')).catch(function (e) { console.log(e) });
}



//function SetOnline() {
//    $.ajax({
//        method: "post",
//        url: "/Home/Online",
//        success: function () {
//        }
//    });
//}

//function GetOnlineUsers() {
//    $.ajax({
//        method: "post",
//        url: "/Home/GetOnlineUsers",
//        success: function (msg) {
//        }
//    });
//}