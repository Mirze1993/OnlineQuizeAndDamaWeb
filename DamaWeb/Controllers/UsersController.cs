using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserRepository repository;
      

        public UsersController()
        {
            repository = new UserRepository();
        }

        public IActionResult UsersIndex()
        {
            var users= repository.getRandomUsers();
            return View(users);
        }

        public IActionResult RequestGame(int id)
        {
            var u = repository.GetByColumNameFist("Id", id, "Name", "Email", "Id", "ProfilPicture").Item1;
            return PartialView("RequestGame",u);
        }

        [HttpPost]
        public IActionResult Search(string name)
        {
            var u = repository.GetWithCondition($"Name Like '%"+ name+"%'", "Name", "Email", "Id","ProfilPicture").Item1;
            return View("UsersIndex", u);
        }
    }


}
