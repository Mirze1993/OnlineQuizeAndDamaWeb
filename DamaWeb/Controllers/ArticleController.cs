using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
