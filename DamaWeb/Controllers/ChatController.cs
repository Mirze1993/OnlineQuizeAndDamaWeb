using DamaWeb.Hubs;
using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IHubContext<MainHub> hub;

        public ChatController(IHubContext<MainHub> _hub)
        {
            hub = _hub;
        }
      
        [HttpPost]
        public JsonResult GetLast50(int id)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var rep = new ChatRepository();
            var msg=rep.getLastTop(30, userid, id);
            msg.Reverse();
            return new JsonResult(msg);
        }
        [HttpPost]
        public void SendMesage(int recveid, string message)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var rep = new ChatRepository();
            rep.Insert(new Model.Models.Chat { Date = DateTime.Now, Message = message, ReciveId = recveid, SenderId = userid });
            hub.Clients.User(recveid.ToString()).SendAsync("ChatNotif", User.Identity.Name, userid);
        }
        
        [HttpPost]
        public IActionResult GetMessages()
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            ViewBag.userid = userid;
            var rep = new ChatRepository();
            var msgs = rep.GetMesageGrup(userid);
            return PartialView("GetMessages",msgs);
        }

        [HttpPost]
        public JsonResult GetLastIsNoReadMsg(int senderId)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            
            var rep = new ChatRepository();
            var msgs = rep.GetLastIsNoReadMsg(userid, senderId);
            return new JsonResult(msgs);
        }
    }
}
