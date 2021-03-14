using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class GameRoomController : Controller
    {
        
        public IActionResult MainRoom()
        {
            return View();
        }

        public int RequestGame(int id)
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var repository = new Repostory.GamesRepository();
            var b = repository.RequestGame(userid, id);
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
            return new Repostory.GamesRepository().AcceptGame(id)>0;

        }

        [HttpPost]
        public bool RejectRequest(int id)
        {
            return new Repostory.GamesRepository().Delet(id);
        }

        
        
        public IActionResult Start(int id)
        {
            ViewBag.GameId = id;
            return View("Start");
        }
    }
}
