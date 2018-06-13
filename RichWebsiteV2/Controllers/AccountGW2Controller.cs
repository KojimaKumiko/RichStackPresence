using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RichWebsite.Controllers
{
    public class AccountGW2Controller : Controller
    {
        string apiKeyGw2;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        [HttpPost]

        public ActionResult SaveApiKey(string GW2Key)
        {
            apiKeyGw2 = GW2Key;
            return View(GW2Key);
        }
    }
}