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
    [Authorize]
    public class LessonController : Controller
    {
        int getId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(
                    x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Value);
        }

        public IActionResult SelectLesson()
        {
            var model = new QuizeRepository().GetStudentTeacherByStudentId(getId());
            return View(model);
        }

        [HttpPost]
        public IActionResult GetCategoryInGroup(int groupId)
        {
            var model = new QuizeRepository().SelectCategoryInGroup(groupId);
            return View(model);
        }

        [HttpPost]
        public IActionResult StartQuize(int categoryId)
        {
            ViewBag.categoryId = categoryId;
            return View(new QuizeRepository().GetByColumName<Quize>("CategoryId", categoryId).Item1);
        }

        public bool Result(ResultQuize result)
        {
            result.ResultDate = DateTime.Now;
            result.StudentId = getId();
            return  new QuizeRepository().Insert<ResultQuize>(result).Item2;           
        }
    }
}
