﻿using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    
    public class CategoryController : Controller
    {
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Add()
        {
            var sc = new QuizeRepository().GetAll<Subject>("Id", "Name").Item1;
            var sl = new List<SelectListItem>();
            if (sc != null) sc.ForEach(mm => sl.Add(new SelectListItem { Value = mm.Id.ToString(), Text = mm.Name }));
            ViewBag.sl = sl;
            return View();
        }

        int getId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(
                    x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Value);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult All()
        {
            if (User.IsInRole("Admin"))
                return View(new QuizeRepository().GetAllCategory());
            return View(new QuizeRepository().GetAllCategory(getId()));
        }


        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Update(int id)
        {
            var rep = new QuizeRepository();
            var sc = rep.GetAll<Subject>("Id", "Name").Item1;
            var sl = new List<SelectListItem>();
            if (sc != null) sc.ForEach(mm => sl.Add(new SelectListItem { Value = mm.Id.ToString(), Text = mm.Name }));
            ViewBag.sl = sl;
            var m = rep.GetByColumNameFist<Category>("Id", id).Item1;
            ViewBag.message = "Edit";
            return View("Add", m);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var rep = new QuizeRepository();
            var m = rep.Delet<Category>(id);
            return RedirectToAction("All", "Category");
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Add(Category category)
        {
            var rep = new QuizeRepository();
            if (category != null)
            {
                category.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                var (id, b) = rep.Insert<Category>(category);
                if (b && id > 0)
                {
                    category.Id = id;
                    return View(category);
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Update(Category category)
        {
            var rep = new QuizeRepository();
            if (category != null)            
                 rep.Update<Category>(category, category.Id);
            return RedirectToAction("All", "Category");
        }
    }
}
