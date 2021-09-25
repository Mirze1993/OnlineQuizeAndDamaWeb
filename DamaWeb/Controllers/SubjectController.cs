using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubjectController : Controller
    {
        
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult All()
        {
            return View(new QuizeRepository().GetAll<Subject>().Value);
        }

        public IActionResult Update(int id)
        {
            var rep = new QuizeRepository();
            var m=rep.GetByColumNameFist<Subject>("Id", id).Value;
            ViewBag.message = "Edit";
            return View("Add",m);
        }

        public IActionResult Delete(int id)
        {
            var rep = new QuizeRepository();
            var m = rep.Delet<Subject>(id);
            return RedirectToAction("All", "Subject");
        }


        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Add(Subject subject)
        {
            var rep = new QuizeRepository();
            if (subject != null)
            {
                var r = rep.Insert<Subject>(subject);
                if (r.Success && r.Value > 0)
                {
                    subject.Id = r.Value;
                    return View(subject);
                }
            }
            return View();
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Update(Subject subject)
        {
            var rep = new QuizeRepository();
            if (subject != null)
            {
                var b = rep.Update<Subject>(subject,subject.Id);
                if (b)
                    return RedirectToAction("All", "Subject");
            }
            return View();
        }
    }
}
