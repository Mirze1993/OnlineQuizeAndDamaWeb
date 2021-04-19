using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.UIEntites;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private readonly IWebHostEnvironment enviloment;

        int getId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(
                    x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Value);
        }
        public ProfilController( IWebHostEnvironment _enviloment)
        {
            enviloment = _enviloment;
        }

        public IActionResult ViewProfil(int id)
        {
            if (id == 0) id = getId();
            var rep = new UserRepository();
            var user = new UserRepository().GetByColumNameFist("Id",id ).Item1;
            var claims = new UserRepository().GetByColumName<UserClaims>("UserId",id).Item1;
            UIProfil profil = new UIProfil();

            profil.Id = user.Id;
            profil.Email = user?.Email;
            profil.FullName = user?.Name;
            profil.UserName = user?.UserName;
            profil.Phone = user?.Phone;
            profil.Role = "";
            profil.ProfilPicture = user?.ProfilPicture;

            if (claims != null)
            {
                var bt= claims.Where(m => m.Type == "BirthDate").FirstOrDefault()?.Value;
                profil.BirthDate =Convert.ToDateTime(bt??DateTime.Now.ToString() );
                profil.Education = claims.Where(m => m.Type == "Education").FirstOrDefault()?.Value;
                profil.Skills = claims.Where(m => m.Type == "Skills").FirstOrDefault()?.Value;
                profil.Notes = claims.Where(m => m.Type == "Notes").FirstOrDefault()?.Value;
                profil.Location = claims.Where(m => m.Type == "Location").FirstOrDefault()?.Value;
                var rr = claims.Where(m => m.Type == "Role")?.ToList();
                rr?.Select(x => { profil.Role += x.Value + " "; return 0; })?.ToList();
            }
            return View("View", profil);
        }

        public IActionResult EditAppUser(UIProfil model)
        {
            new UserRepository().Update(m =>
            {
                m.Add("Name", model.FullName);               
                m.Add("Phone", model.Phone ?? "");
                m.Add("Email", model.Email??"");
            }, getId());
            return RedirectToAction("ViewProfil");
        }


        [HttpPost]
        public IActionResult EditClaims(UIProfil model)
        {
            var rep=new UserRepository();
            rep.UpdateOrCreateClaim(getId(), "Location", model.Location);
            rep.UpdateOrCreateClaim(getId(), "Education", model.Education);
            rep.UpdateOrCreateClaim(getId(), "Skills", model.Skills);
            rep.UpdateOrCreateClaim(getId(), "Notes", model.Notes);
            if(model.BirthDate.Year!=DateTime.Now.Year)
                rep.UpdateOrCreateClaim(getId(), "BirthDate", model.BirthDate.ToString("dd-MM-yyyy"));
            return RedirectToAction("ViewProfil");
        }

        [HttpPost]
        public bool ImageUpload(string oldPic, IFormFile file)
        {
            string pictureName = oldPic;

            if (string.IsNullOrEmpty(pictureName))
                pictureName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fn = Path.Combine(enviloment.WebRootPath, "img", "profil", "orginal", pictureName);
            using (var fs = new FileStream(fn, FileMode.Create))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            var fnc = Path.Combine(enviloment.WebRootPath, "img", "profil", "40x40", pictureName);
            using ( var img = Image.Load(file.OpenReadStream()))
            {
                var (w, h) = Resize(img, 60, 60);
                img.Mutate(m => m.Resize(w, h));
                img.Save(fnc); 
            }
            Response.Cookies.Delete("img");
            Response.Cookies.Append("img", pictureName);

            return new UserRepository().Update(m=>m.Add("ProfilPicture", pictureName), getId());
            
        }

        (int,int) Resize(Image img,int maxW,int maxh)
        {
            if (img.Width > maxW || img.Height > maxh)
            {
                double widthr = img.Width / maxW;
                double heightr = img.Height / maxh;

                double r = Math.Max(widthr,heightr);

                return ((int)(img.Width / r), (int)(img.Height / r));
            }
            return (maxW, maxh);
        }

    }
}

