using DamaWeb.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class GameRoomController : Controller
    {
        private readonly IHubContext<MainHub> hub;

        public GameRoomController(IHubContext<MainHub> _hub)
        {
            hub = _hub;
        }

        public IActionResult MainRoom()
        {
            return View();
        }

        public int RequestGame(int id)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var repository = new Repostory.GamesRepository();
            var b = repository.RequestGame(userid, id);
            if (b > 0)
            {
                var message = User.Identity.Name + " size oyun isteyi gonderdi";
                repository.Insert<Model.Models.Notification>(new Model.Models.Notification { UserId = id, Message = message, Type = Model.Models.NotificationType.GameRequest });
                hub.Clients.User(id.ToString()).SendAsync("notifications");

            }
            return b;
        }

        public JsonResult CheckGame()
        {
            var id = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var games = new Repostory.GamesRepository().GetUserGames(id);
            return new JsonResult(games);
        }

        [HttpPost]
        public bool AcceptRequest(int id)
        {
            var repository = new Repostory.GamesRepository();            
            var b = repository.AcceptGame(id) > 0;
            if (b)
            {
                var requserId = repository.GetByColumNameFist("Id", id).Item1.RequestUser;
                var message = "Qebul olunmus oyun isteyi";
                repository.Insert<Model.Models.Notification>(new Model.Models.Notification { UserId = requserId, Message = message, Type = Model.Models.NotificationType.GameAccept });
                hub.Clients.User(requserId.ToString()).SendAsync("notifications");
            }
            return b;
        }

        [HttpPost]
        public bool RejectRequest(int id)
        {
            var repository = new Repostory.GamesRepository();
            var requserId = repository.GetByColumNameFist("Id", id).Item1.RequestUser;
            var b = new Repostory.GamesRepository().Delet(id);
            if (b)
            {                
                var message = "Levg olunmus oyun isteyi";
                repository.Insert<Model.Models.Notification>(new Model.Models.Notification { UserId = requserId, Message = message, Type = Model.Models.NotificationType.GameReject });
                hub.Clients.User(requserId.ToString()).SendAsync("notifications");
            }
            return b;
        }



        public IActionResult Start(int id)
        {
            ViewBag.GameId = id;
            return View("Start");
        }
    }
}
