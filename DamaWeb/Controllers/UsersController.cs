using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserRepository repository;

        public UsersController(UserRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult UsersIndex()
        {
            var users= repository.getRandomUsers();
            return View(users);
        }
    }
}
