using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Enums;
using Model.Models;
using Model.UIEntites;
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
        //get all studentGroup
        public IActionResult All()
        {
            if (User.IsInRole("Admin"))
                return View(new FollowRepository().GetAll<StudentGroup>().Value);
            return View(new FollowRepository().GetByColumName<StudentGroup>("CreaterId", getId()).Value);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Add(StudentGroup model)
        {
            if (model != null)
            {
                model.CreaterId = getId();
                var id = new FollowRepository().Insert<StudentGroup>(model).Value;
                if (id > 0)
                {
                    model.Id = id;
                    return View(model);
                }
            }

            return View();
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Updatepage(int id)
        {
            var model = new FollowRepository().GetByColumNameFist<StudentGroup>("Id", id).Value;
            ViewBag.message = "Edit";
            return View("Add", model);
        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Update(StudentGroup model)
        {
            if (model != null)
                new FollowRepository().Update<StudentGroup>(model, model.Id);
            return RedirectToAction("All", "StudentGroup");

        }

        // GetStudentsByTecherId
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AddStudent(int groupId, string groupName,int teacherId)
        {
            ViewBag.groupId = groupId;
            ViewBag.groupName = groupName;
            var l = new FollowRepository().GetStudentsByTecherId(teacherId);
            return View("AddStudent", l);
        }

        
        public IActionResult RemoveUserFromGroup(int id, int groupId, string groupName, int teacherId)
        {

            if (id > 0 || groupId > 0)
                new FollowRepository().Update(mm => mm.Add("GroupId", 0), id);            
            return AddStudent(groupId, groupName, teacherId);
        }

       
        public IActionResult AddUserToGroup(int groupId, int id, string groupName, int teacherId)
        {
            if (id > 0 || groupId > 0)
                new FollowRepository().Update(mm => mm.Add("GroupId", groupId), id);
            return AddStudent(groupId, groupName, teacherId);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AddCategory(int groupId, string groupName, int teacherId)
        {
            
            var rep =new FollowRepository();
            var allCategory = rep.GetByColumName<Category>("UserId",teacherId,"Id","Name").Value;
            List<SelectListItem> selectLists = new List<SelectListItem>();
            if (allCategory != null)
                selectLists= allCategory.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.allgroups = selectLists;

            var cg = rep.SelectTecherGroupeCategory(teacherId,groupId);
            var model = new UICategoryGroup();
            if(cg!=null)
            model.SelectedCategoryIds= cg.Select(x => x.CategoryId).ToList();
            model.GroupId = groupId;
            model.TeacherId = teacherId;
            model.GruopeName = groupName;
            return View("AddCategory", model);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult UpdateCategoryGroup(UICategoryGroup model,List<int> oldCategories)
        {
            if (model != null&&model?.GroupId!=null)
            {
                var rep = new FollowRepository();
                foreach (var item in model.SelectedCategoryIds)
                {
                    if (oldCategories.Contains(item)) continue;
                    rep.Insert<CategoryGroup>(new CategoryGroup { GroupId = model.GroupId, CategoryId = item });
                }
                foreach (var item in oldCategories)
                {
                    if (model.SelectedCategoryIds.Contains(item)) continue;
                    rep.DeleteCategoryGroup(item, model.GroupId) ;
                }
            }
            return RedirectToAction("All");
        }


        public IActionResult Results(int studentId,int teacherId)
        {          
            return View(new QuizeRepository().GetResults(studentId, teacherId));
        }
    }
}