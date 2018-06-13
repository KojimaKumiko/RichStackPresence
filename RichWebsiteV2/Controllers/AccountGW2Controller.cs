using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RichWebsite.Controllers
{
    public class AccountGW2Controller : Controller
    {
        static string apiKeyGw2;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public static void setapiKey(string api)
        {
            apiKeyGw2 = api;
        }


    }
}