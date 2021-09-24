using DamaWeb.Repostory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    [Authorize]
    public class QuizeController : Controller
    {
        private readonly IWebHostEnvironment enviloment;

        public QuizeController(IWebHostEnvironment _enviloment)
        {
            enviloment = _enviloment;
        }

        [HttpPost]
        public IActionResult AllByCategory(int categoryId)
        {
            ViewBag.categoryId = categoryId;
            return View("AllByCategory", new QuizeRepository().GetByColumName<Quize>("CategoryId", categoryId).t);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult GoAdd(int categoryId)
        {
            return View("Add",new Quize { CategoryId = categoryId });
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult View(int id)
        {
            ViewBag.edit = "View";
            return View("Add", new QuizeRepository().GetByColumNameFist("Id", id).t);
        }


        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult GoUpdate(int id)
        {
            ViewBag.edit = "Update";
            return View("Add", new QuizeRepository().GetByColumNameFist("Id", id).t);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> Update(Quize model, IFormFile Img, IFormFile Video,
            IFormFile QuestionImg, IFormFile AImg, IFormFile BImg, IFormFile CImg, IFormFile DImg, IFormFile EImg)
        {
            string pictureName = model.PictureSrc;
            string videoName = model.VideoSrc;
            string QusImgName = model.QuestionImgSrc;
            string AImgName = model.AImgSrc;
            string BImgName = model.BImgSrc;
            string CImgName = model.CImgSrc;
            string DImgName = model.DImgSrc;
            string EImgName = model.EImgSrc;

            if (Img != null)
            {
                if (pictureName == null)
                    pictureName = Guid.NewGuid() + Path.GetExtension(Img.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", pictureName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await Img.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (QuestionImg != null)
            {
                if (QusImgName == null)
                    QusImgName = Guid.NewGuid() + Path.GetExtension(QuestionImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", QusImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await QuestionImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (AImg != null)
            {
                if (AImgName == null)
                    AImgName = Guid.NewGuid() + Path.GetExtension(AImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", AImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await AImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (BImg != null)
            {
                if (BImgName == null)
                    BImgName = Guid.NewGuid() + Path.GetExtension(BImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", BImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await BImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (CImg != null)
            {
                if (CImgName == null)
                    CImgName = Guid.NewGuid() + Path.GetExtension(CImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", CImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await CImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (DImg != null)
            {
                if (DImgName == null)
                    DImgName = Guid.NewGuid() + Path.GetExtension(DImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", DImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await DImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (EImg != null)
            {
                if (EImgName == null)
                    EImgName = Guid.NewGuid() + Path.GetExtension(EImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", EImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await EImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }



            if (Video != null)
            {
                if (videoName == null)
                    videoName = Guid.NewGuid() + Path.GetExtension(Video.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Video", videoName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await Video.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            model.PictureSrc = pictureName; 
            model.VideoSrc = videoName;
            model.QuestionImgSrc = QusImgName;
            model.AImgSrc = AImgName;
            model.BImgSrc = BImgName;
            model.CImgSrc = CImgName;
            model.DImgSrc = DImgName;
            model.EImgSrc = EImgName;

            var b = new QuizeRepository().Update(model, model.Id);

            if (b)
                return View("Add", model);

            return View();
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> Add(Quize model, IFormFile Img, IFormFile Video,
            IFormFile QuestionImg, IFormFile AImg, IFormFile BImg, IFormFile CImg, IFormFile DImg, IFormFile EImg)
        {
            string pictureName = null;
            string videoName = null;
            string QusImgName = null;
            string AImgName = null;
            string BImgName = null;
            string CImgName = null;
            string DImgName = null;
            string EImgName = null;

            if (Img != null)
            {
                pictureName = Guid.NewGuid() + Path.GetExtension(Img.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", pictureName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await Img.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }


            if (QuestionImg != null)
            {
                QusImgName = Guid.NewGuid() + Path.GetExtension(QuestionImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", QusImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await QuestionImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (AImg != null)
            {
                AImgName = Guid.NewGuid() + Path.GetExtension(AImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", AImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await AImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (BImg != null)
            {
                BImgName = Guid.NewGuid() + Path.GetExtension(BImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", BImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await BImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (CImg != null)
            {
                CImgName = Guid.NewGuid() + Path.GetExtension(CImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", CImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await CImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (DImg != null)
            {
                DImgName = Guid.NewGuid() + Path.GetExtension(DImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", DImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await DImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            if (EImg != null)
            {
                EImgName = Guid.NewGuid() + Path.GetExtension(EImg.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", EImgName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await EImg.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }

            if (Video != null)
            {
                videoName = Guid.NewGuid() + Path.GetExtension(Video.FileName);
                var fn = Path.Combine(enviloment.WebRootPath, "Quize", "Video", videoName);

                using (var fs = new FileStream(fn, FileMode.Create))
                {
                    await Video.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }

            model.PictureSrc = pictureName;
            model.VideoSrc = videoName;
            model.QuestionImgSrc = QusImgName;
            model.AImgSrc = AImgName;
            model.BImgSrc = BImgName;
            model.CImgSrc = CImgName;
            model.DImgSrc = DImgName;
            model.EImgSrc = EImgName;

            var r = new QuizeRepository().Insert(model);

            if (r.Success && r.t > 0)
            {
                model.Id = r.t;
                return View(model);
            }

            return View();
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Delete(int id, int categoryId)
        {
            var rep = new QuizeRepository();
            var q = rep.GetByColumNameFist("Id", id, "VideoSrc", "PictureSrc").t;

            if (q.QuestionImgSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.QuestionImgSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }
            if (q.AImgSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.AImgSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }
            if (q.BImgSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.BImgSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }
            if (q.CImgSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.CImgSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }



            if (q.DImgSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.DImgSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }

            if (q.EImgSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.EImgSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }

            if (q.PictureSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Picture", q.PictureSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }



            if (q.VideoSrc != null)
            {
                var pt = Path.Combine(enviloment.WebRootPath, "Quize", "Video", q.VideoSrc);
                if (System.IO.File.Exists(pt))
                    System.IO.File.Delete(pt);
            }

            rep.Delet(id);
            return AllByCategory(categoryId);
        }
    }
}
