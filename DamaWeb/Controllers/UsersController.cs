﻿using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.UIEntites;
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
            var users = repository.getRandomUsers();
            return View(users);
        }

        public IActionResult RequestGame(int id)
        {
            var u = repository.GetByColumNameFist("Id", id, "Name", "Email", "Id", "ProfilPicture").Value;
            return PartialView("RequestGame", u);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Search(string name)
        {
            var u = repository.GetWithCondition($"Name Like '%" + name + "%'", "Name", "Email", "Id", "ProfilPicture").Value;
            return View("UsersIndex", u);
        }
        
        public IActionResult EditUser(int userId)
        {
            var claims = repository.GetByColumName<UserClaims>("AppUserId", userId).Value;
            var u = repository.GetByColumNameFist("Id", userId).Value;
            var model = new UIUserClaims { Claims = claims, User = u };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteClaim(int id,int userId)
        {
            repository.Delet<UserClaims>(id);
            return RedirectToAction("EditUser",new { userId = userId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AddClaim(UserClaims claims)
        {
            repository.Insert<UserClaims>(claims);
            return RedirectToAction("EditUser",new { userId=claims.AppUserId});
        }
    }
}
