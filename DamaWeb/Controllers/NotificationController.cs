using DamaWeb.Repostory;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class NotificationController : Controller
    {
        
        public IActionResult GetLast5()
        {
            var rep = new NotificationRepostoriy();
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var model=rep.getLastTop(5, userid);
            return PartialView("GetLast5", model);
        }

        
        public IActionResult GetNotif(int id,string type)
        {
            NotificationType t;
            var m=Enum.TryParse(type, out t);
            var rep = new NotificationRepostoriy();
            rep.Delet(id);
            if (t == NotificationType.GameAccept || t == NotificationType.GameReject || t == NotificationType.GameRequest)
                return RedirectToAction("MainRoom", "GameRoom");
            return RedirectToAction("Index", "Home");
        }
    }
}
