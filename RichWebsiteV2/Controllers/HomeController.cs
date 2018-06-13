using RichWebsite.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        [HttpPost]
        public ActionResult GW2(string apiKey)
        {
            string key = apiKey;
            AccountGW2Controller.setapiKey(apiKey);

            return View("Navigation");
        }

        public ActionResult Navigation()
        {
            return View();
        }

    }
}