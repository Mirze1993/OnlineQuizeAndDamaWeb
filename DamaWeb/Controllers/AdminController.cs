using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserRepository repository;
        public AdminController()
        {
            repository = new UserRepository();
        }
        public IActionResult UsersIndex()
        {
            var users = repository.getRandomUsers();
            return View(users);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Search(string name)
        {
            var u = repository.GetWithCondition($"Name Like '%" + name + "%'", "Name", "Email", "Id", "ProfilPicture").Value;
            return View("UsersIndex", u);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(int userId)
        {
            var claims = repository.GetByColumName<UserClaims>("AppUserId", userId).Value;
            var u = repository.GetByColumNameFist("Id", userId).Value;
            var model = new UIUserClaims { Claims = claims, User = u };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteClaim(int id, int userId)
        {
            repository.Delet<UserClaims>(id);
            return RedirectToAction("EditUser", new { userId = userId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AddClaim(UserClaims claims)
        {
            repository.Insert<UserClaims>(claims);
            return RedirectToAction("EditUser", new { userId = claims.AppUserId });
        }
    }
}
