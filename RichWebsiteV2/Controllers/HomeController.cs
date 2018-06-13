using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RichWebsiteV2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoL()
        {
            ViewData["Message"] = "League";

            return View();
        }

        public ActionResult GW2()
        {
            ViewData["Message"] = "Guild";

            return View();
        }

    }
}