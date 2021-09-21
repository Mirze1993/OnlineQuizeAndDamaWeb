using DamaWeb.Repostory;
using MicroORM.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
                var (u, b) = repository.GetByColumNameFist("UserName", model.UserName);

                if (!b || u != default(AppUser))
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

                    var (role, rb) = repository.GetWithCondition<UserClaims>($"UserId={u.Id} and Type ='Role'");
                    if (rb) if (role != null)
                            role.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x.Value)));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var pic = u.ProfilPicture;
                    Response.Cookies.Delete("img");
                    if (!string.IsNullOrEmpty(pic))
                        Response.Cookies.Append("img", pic);

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

        [Authorize]
        [HttpPost]
        public JsonResult getNotifAndChatCount()
        {
            var userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var m = repository.GetNotifAndChatCount(userid);
            return new JsonResult(m);
        }
        [HttpPost]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }



        
    }
}
