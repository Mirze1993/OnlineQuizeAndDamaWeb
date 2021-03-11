using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class GameRoomController : Controller
    {
        public IActionResult MainRoom()
        {
            return View();
        }

        public void RequestGame(int id)
        {
            var idd = id;
        }
    }
}
