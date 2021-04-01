using DamaWeb.Repostory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class ChatController : Controller
    {
        public JsonResult GetLast50(int id)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var rep = new ChatRepository();
            var msg=rep.getLastTop(50, userid, id);
            return new JsonResult(msg);
        }

        public void SendMesage(int recveid, string message)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var rep = new ChatRepository();
            rep.Insert(new Model.Models.Chat { Date = DateTime.Now, Message = message, ReciveId = recveid, SenderId = userid });
        }

        public IActionResult GetMessages()
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            ViewBag.userid = userid;
            var rep = new ChatRepository();
            var msgs = rep.GetMesageGrup(userid);
            return PartialView("GetMessages",msgs);
        }
    }
}
