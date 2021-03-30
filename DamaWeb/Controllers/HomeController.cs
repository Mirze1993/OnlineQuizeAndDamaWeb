using DamaWeb.Repostory;
using DamaWeb.Tools;
using MicroORM.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public class HomeController : Controller
    {

        UserRepository repository;

        public HomeController()
        {
            repository = new UserRepository();
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
            return View();
        }

        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UIAppUser model)
        {
            if (ModelState.IsValid)
            {
                var (u,b) = repository.GetByColumNameFist("UserName", model.UserName);

                if (!b||u!=default(AppUser))
                {
                    ModelState.AddModelError("", $"{model.UserName} is alreay  use");
                    return View();
                }
                var userId = repository.Insert(new AppUser()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Phone = model.Phone,
                    UserName = model.UserName,
                    Password = new HashCreate().CreateHashString(model.Password)
                });

                return Login(new UILogin { Password = model.Password, UserName = model.UserName }).Result;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UILogin login)
        {
            if (ModelState.IsValid)
            {
                var (b, u) = repository.ValidateUserCredentials(login.UserName, login.Password);
                if (b)
                {
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                    };


                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                        new Claim(ClaimTypes.Name,u.UserName),
                    };
                   // role.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x.RoleName)));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    var principial = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principial, properties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Istifadeci adi ve ya parol sehhvdir");
                return View();
            }
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        //[Authorize]
        //[HttpPost]
        //public void Online()
        //{
        //    var obj=User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        //    if (string.IsNullOrEmpty(obj)) return;
        //    ConnectionMapping.AddUser(User.Identity.Name,Convert.ToInt32(obj));
        //}
        //[Authorize]
        //[HttpPost]
        //public JsonResult GetOnlineUsers()
        //{
        //    return new JsonResult(ConnectionMapping.Users);
        //}
    }
}
