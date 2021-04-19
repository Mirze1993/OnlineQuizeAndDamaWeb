using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        public IActionResult SelectGroupe()
        {
            var l = new QuizeRepository().GetAll<Subject>("Id", "Name").Item1;
            var sl = new List<SelectListItem>();
            if (l != null)
                l.ForEach(mm => sl.Add(new SelectListItem { Value = mm.Id.ToString(), Text = mm.Name }));
            ViewBag.sl = sl;
            return View();
        }
        [HttpPost]
        public JsonResult SelectTeacher(int subjectId)
        {
            var l = new QuizeRepository().GetQuizeWriterUsers(subjectId);
            if(l?.Count>0)
            return new JsonResult(l);
            return new JsonResult("error") {StatusCode=(int)HttpStatusCode.BadRequest };
        }
        [HttpPost]
        public JsonResult GetCategores(int teacherID)
        {
            var l = new QuizeRepository().GetByColumName<Category>("UserId", teacherID,"Name","Description").Item1;
            if (l?.Count > 0)
                return new JsonResult(l);
            return new JsonResult("error") { StatusCode = (int)HttpStatusCode.BadRequest };
        }
        [HttpPost]
        public IActionResult FollowTeacher(int teacherId)
        {
            var teacher = new QuizeRepository().GetByColumNameFist<AppUser>("Id", teacherId, "Name","Id","Phone","Email","ProfilPicture").Item1;
            return PartialView("FollowTeacher",teacher);
        }

        [HttpPost]
        public int FollowRequest(int teacherId,int subjectId)
        {
            var rep = new FollowRepository();
            if (teacherId == 0|| teacherId==getId()) return 0;
            var t = rep.Follow(subjectId, getId(), teacherId);
            if(t>0)
            rep.Insert<Notification>(new Notification { Message = User.Identity.Name + " sizin derslere qatilmaq isteyir", Type = NotificationType.Follow, UserId=teacherId});
            return t;
        }

        public IActionResult WaitApproveFollow()
        {
            var rep = new FollowRepository().WaitApproveFollow(getId());           
            return View("WaitApproveFollow", rep);
        }

        public IActionResult DeleteFollow(int id)
        {
            var rep = new FollowRepository().Delet<StudentTeacher>(id);
            return RedirectToAction("All", "StudentGroup");
        }
        public IActionResult ApproveFollow(int id)
        {
            var rep = new FollowRepository().Update(mm=>mm.Add("Status",true),id);
            return RedirectToAction("WaitApproveFollow", "Follow");
        }

        int getId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(
                    x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Value);
        }

    }
}
