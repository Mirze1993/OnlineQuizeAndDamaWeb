using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class StudentGroupController : Controller
    {
        int getId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(
                    x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Value);
        }
        public IActionResult All()
        {
            if (User.IsInRole("Admin"))
                return View(new FollowRepository().GetAll<StudentGroup>().Item1);
            return View(new FollowRepository().GetByColumName<StudentGroup>("CreaterId", getId()).Item1);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StudentGroup model)
        {
            if (model != null)
            {
                model.CreaterId = getId();
                var id = new FollowRepository().Insert<StudentGroup>(model).Item1;
                if (id > 0)
                {
                    model.Id = id;
                    return View(model);
                }
            }

            return View();
        }


        public IActionResult Update(int id)
        {
            var model = new FollowRepository().GetByColumNameFist<StudentGroup>("Id", id).Item1;
            ViewBag.message = "Edit";
            return View("Add", model);
        }
        [HttpPost]
        public IActionResult Update(StudentGroup model)
        {
            if (model != null)
                new FollowRepository().Update<StudentGroup>(model, model.Id);
            return RedirectToAction("All", "StudentGroup");

        }
        public IActionResult AddStudent(int groupId,string groupName)
        {
            new FollowRepository().GetStudentsByTecherId(getId());
            return View();
        }
    }
}